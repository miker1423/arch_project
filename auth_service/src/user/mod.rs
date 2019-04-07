
#[derive(Eq, PartialEq, Hash, Debug)]
pub struct User {
    pub username: String,
    pub email: String
}

impl User {
    pub fn new() -> User {
        User { username: String::new(), email: String::new() }
    }
}

impl From<&str> for User {
    fn from(source: &str) -> User {
        let splitted: Vec<&str> = source.split(',').collect();
        if splitted.len() != 2 {
            return User { username: String::new(), email: String::new() };
        }
        return User {
            username: String::from(splitted[0]),
            email: String::from(splitted[1])
        }
    }
}

impl From<&User> for String {
    fn from(user: &User) -> String {
        format!("{},{}\n", user.username, user.email)
    }
}