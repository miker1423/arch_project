use std::collections::HashMap;
use std::sync::RwLock;
use std::fs::File;
use std::io::Read;
use crate::models::{user::User, user_minimal::UserMinimal};

pub struct AppState {
    modified: RwLock<bool>,
    pub users: RwLock<HashMap<String, User>>,
    pub file_path: String
}

impl AppState {
    pub fn load_from_file(path: &str) -> AppState {
        let file = File::open(path);
        if let Err(_) = file {
            println!("DB not found, creating new one");
            if let Err(_) = File::create(path) {
                println!("Unable to create DB");
                panic!("FUCK!");
            }
            return AppState::new_with_path(path);
        }

        let mut text = String::new();
        if let Err(_) = file.unwrap().read_to_string(&mut text) {
            println!("Failed to read file");
            return AppState::new_with_path(path);
        }

        let full_text: Vec<&str> = text.split("\n").collect();
        let mut map = HashMap::<String, User>::new();
        for line in full_text {
            let user = User::from(line);
            map.insert(user.username.clone(), user);
        }

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
}

impl Db for AppState {
    fn update_user(&self, user: &mut UserMinimal) -> Option<UserMinimal> {
        if let Ok(mut table) = self.users.write() {
            let mut result = table.get_mut(&user.username);
            if let None = result {
                return None;
            }

            let mut saved_user = result.
            user.change_email(&mut saved_user);
            result.replace(&mut saved_user.clone());
            return Some(UserMinimal::from(saved_user.clone()));
        }

        return None;
    }

    fn add_user(&self, user: User) {
        if let Ok(mut table) = self.users.write() {
            let _ = table.entry(user.username.clone()).or_insert(user);
            if let Ok(mut is_modified) = self.modified.write() {
                *is_modified = true;
            }
        }
    }

    fn remove_user(&self, user_id: String) -> Option<User> {
        return match self.users.write() {
            Ok(mut table) => table.remove(&user_id),
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