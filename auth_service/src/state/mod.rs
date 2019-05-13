use crate::models::{user::User, user_minimal::UserMinimal};
use std::collections::HashMap;
use std::sync::RwLock;
use std::fs::File;
use std::io::Read;
use std::convert::TryFrom;

pub struct AppState {
    modified: RwLock<bool>,
    pub users: RwLock<HashMap<String, User>>,
    pub file_path: String
}

impl AppState {
    pub fn load_from_file(path: &str) -> AppState {
        let file = File::open(path);
        if let Err(_) = file {
            if let Err(_) = File::create(path) {
            }
            return AppState::new_with_path(path);
        }

        let mut text = String::new();
        if let Err(_) = file.unwrap().read_to_string(&mut text) {
            return AppState::new_with_path(path);
        }

        let full_text: Vec<&str> = text.split("\n").collect();
        let mut map = HashMap::<String, User>::new();
        for line in full_text {
            if let Ok(user) = User::try_from(line) {
                map.insert(user.username.clone(), user);
            }
        }

        println!("Loaded");
        AppState {
            modified: RwLock::new(false),
            file_path: String::from(path),
            users: RwLock::new(map)
        }
    }

    fn new_with_path(path: &str) -> AppState {
        AppState {
            modified: RwLock::new(false),
            file_path: String::from(path),
            users: RwLock::new(HashMap::new())
        }
    }

    pub fn is_modified(&self) -> bool { *self.modified.read().unwrap() }

    pub fn finish_write(&self) {
        if let Ok(mut modified) = self.modified.write() {
            *modified = false;
        }
    }

    pub fn mark_modified(&self) {
        if let Ok(mut is_modified) = self.modified.write() {
            *is_modified = true;
        }
    }
}

impl Db for AppState {
    fn update_user(&self, user: &mut UserMinimal) -> Option<UserMinimal> {
        if let Ok(mut table) = self.users.write() {
            let mut happend = false;
            table.entry(user.username.clone()).and_modify(|f| {
                user.change_email(f);
                happend = true;
            });

            return if happend {
                self.mark_modified();
                Some(user.clone())
            } else {
                None
            }
        }

        return None;
    }

    fn add_user(&self, user: User) {
        if let Ok(mut table) = self.users.write() {
            let _ = table.entry(user.username.clone()).or_insert(user);
            self.mark_modified();
        }
    }

    fn remove_user(&self, user_id: String) -> Option<User> {
        return match self.users.write() {
            Ok(mut table) => table.remove(&user_id).and_then(|u| {
                self.mark_modified();
                Some(u)
            }),
            _ => None
        };
    }

    fn find_username(&self, email: &str) -> Option<String> {
        return match self.users.read().unwrap().get(email) {
            Some(user) => Some(user.username.clone()),
            None => None
        };
    }

    fn find_user(&self, username: &str) -> Option<User> {
        if let Some(table) = self.users.read().ok() {
            return match table.get(username) {
                Some(user) => Some(user.clone()),
                None => None
            };
        }
        None
    }
}


pub trait Db {
    fn update_user(&self, user: &mut UserMinimal) -> Option<UserMinimal>;
    fn add_user(&self, user: User);
    fn remove_user(&self, user_id: String) -> Option<User>;
    fn find_username(&self, email: &str) -> Option<String>;
    fn find_user(&self, username: &str) -> Option<User>;
}