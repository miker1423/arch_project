use serde::{Deserialize, Serialize};
use uuid::Uuid;
use std::convert::TryFrom;
use std::error::Error;
use std::fmt::Display;

#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct User {
    pub username: String,
    pub email: String,
    pub id: String,
    pub password_hash: String
}

impl User {
    pub fn new() -> User {
        User {
            username: String::new(),
            email: String::new(),
            id: Uuid::new_v4().to_string(),
            password_hash: String::new()
        }
    }

    pub fn build(username: String, email: String, id: String, password_hash: String) -> User {
        User { username, email, id, password_hash }
    }

    pub fn build_no_id(username: String, email: String, password_hash: String) -> User {
        User { username, email, password_hash, id: String::new() }
    }

    pub fn add_id(self) -> Self {
        User { id: Uuid::new_v4().to_string(), ..self }
    }
}

#[derive(Debug)]
pub struct UserError;

impl Error for UserError {
}

impl Display for UserError {
    fn fmt(&self, f: &mut std::fmt::Formatter) -> std::fmt::Result {
        write!(f, "crap")
    }
}

impl TryFrom<&str> for User {
    type Error = UserError;
    fn try_from(source: &str) -> Result<User, UserError> {
        let splitted: Vec<&str> = source.split(',').collect();
        if splitted.len() != 4 {
            return Err(UserError {});
        }

        Ok(User::build(
            String::from(splitted[0]),
            String::from(splitted[2]),
            String::from(splitted[1]),
            String::from(splitted[3])
        ))
    }
}

impl From<&User> for String {
    fn from(user: &User) -> String {
        format!("{},{},{},{}\n", user.username, user.id, user.email, user.password_hash)
    }
}