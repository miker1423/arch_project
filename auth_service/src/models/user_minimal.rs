use serde::{Serialize, Deserialize};
use crate::models::user::User;

#[derive(Serialize, Deserialize, Clone)]
pub struct UserMinimal {
    pub username: String,
    pub email: String,
    pub id: String
}

impl UserMinimal {
    pub fn change_email(&self, user: &mut User) {
        user.email = self.email.clone();
    }
}

impl From<User> for UserMinimal {
    fn from(user: User) -> Self {
        Self { username: user.username, email: user.email, id: user.id }
    }
}