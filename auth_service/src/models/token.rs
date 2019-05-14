use crate::models::user::User;
use jwt::{Token, Header, Registered};
use crypto::sha2::Sha256;
use serde::Serialize;

pub struct TokenProducer;

#[derive(Serialize)]
pub struct TokenSender {
    pub token: String
}

impl TokenProducer {
    pub fn retrieve_token(user: &User) -> Option<String> {
        let headers: Header = Default::default();
        let claims = Registered {
            iss: Some("arch_project".into()),
            sub: Some(user.username.clone()),
            ..Default::default()
        };

        let token = Token::new(headers, claims);
        token.signed(b"secret_key", Sha256::new()).ok()
    }

    pub fn retrieve_admin_token(user: &User) -> Option<String> {
        let headers: Header = Default::default();
        let claims = Registered {
            iss: Some("arch_project".into()),
            sub: Some(user.username.clone()),
            jti: Some("true".into()),
            ..Default::default()
        };
        let token = Token::new(headers, claims);
        token.signed(b"secret_key", Sha256::new()).ok()
    }
}