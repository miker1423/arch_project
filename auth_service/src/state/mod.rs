use std::collections::HashSet;

use crate::user::User;
use std::sync::RwLock;

pub struct AppState {
    modified: RwLock<bool>,
    pub users: RwLock<HashSet<User>>,
    file_path: String
}

impl AppState {
    pub fn new() -> AppState {
        AppState {
            modified: RwLock::new(false),
            file_path: String::new(),
            users: RwLock::new(HashSet::new())
        }
    }

    pub fn load_from_file(path: &str) -> AppState {
        AppState {
            modified: RwLock::new(false),
            file_path: String::from(path),
            users: RwLock::new(HashSet::new())
        }
    }

    pub fn add_user(&self, user: User) {
        if let Ok(mut table) = self.users.write() {
            if let Ok(mut is_modified) = self.modified.write() {
                *is_modified = table.insert(user);
            }
        }
    }

    pub fn is_modified(&self) -> bool { *self.modified.read().unwrap() }
}
