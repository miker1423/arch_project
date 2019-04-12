use crate::models::user::User;
use jwt::Token;
use sha2::Sha256;
use sha2::Digest;

pub struct UserView {
    pub username: String,
    pub email: String,
    pub password: String
}

impl Into<User> for UserView {
    fn into(self) -> User {
        let mut hasher = Sha256::new();
        hasher.input(self.password);
        let password_hash = hasher.result();
        let password_hash = format!("{:x?}", password_hash);
        User::build_no_id(self.username, self.email, password_hash)
    }
}