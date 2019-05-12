use crate::models::user::User;
use sha2::{Sha256, Digest};
use serde::{Deserialize, Serialize};
use base64::encode;

#[derive(Serialize, Deserialize)]
pub struct UserView {
    pub username: String,
    pub email: String,
    pub id: String,
    pub password: String
}

impl Into<User> for UserView {
    fn into(self) -> User {
        let mut hasher = Sha256::new();
        hasher.input(self.password);
        let password_hash = hasher.result();
        let password_hash = encode(&password_hash);
        User::build(self.username, self.email, self.id, password_hash)
    }
}