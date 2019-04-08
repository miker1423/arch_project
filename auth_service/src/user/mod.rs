use serde::{Deserialize, Serialize};
use uuid::Uuid;

#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct User {
    pub username: String,
    pub email: String,
    pub id: String
}

impl User {
    pub fn new() -> User {
        User { username: String::new(), email: String::new(), id: Uuid::new_v4().to_string() }
    }

    pub fn build(username: String, email: String, id: String) -> User {
        User { username, email, id }
    }

    pub fn add_id(self) -> Self {
        User { id: Uuid::new_v4().to_string(), ..self }
    }
}

impl From<&str> for User {
    fn from(source: &str) -> User {
        let splitted: Vec<&str> = source.split(',').collect();
        if splitted.len() != 2 {
            return User::new();
        }

        return User::build(
            String::from(splitted[0]),
            String::from(splitted[2]),
            String::from(splitted[1])
        )
    }
}

impl From<&User> for String {
    fn from(user: &User) -> String {
        format!("{},{},{}\n", user.username, user.id, user.email)
    }
}