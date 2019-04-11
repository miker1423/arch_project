
use crate::state::AppState;
use crate::user::User;
use actix_web::{Responder, HttpRequest, Json};
use std::sync::Arc;
use uuid::Uuid;

pub fn register_user(req: &HttpRequest<Arc<AppState>>) -> impl Responder {
    let username = String::from(req.match_info().get("username").unwrap_or(""));
    let email = String::from(req.match_info().get("email").unwrap_or(""));
    let user = User::build(username, email, Uuid::new_v4().to_string());
    let state: &AppState = req.state();

    state.add_user(user);
    format!("Success")
}

pub fn greet((req, user) : (HttpRequest<Arc<AppState>>, Json<User>)) -> impl Responder {
    println!("{:?}", user);
    format!("YEAH")
}

pub fn find_user(req: &HttpRequest<Arc<AppState>>) -> impl Responder {
    let email = req.match_info().get("email").unwrap_or("");
    let state: &AppState = req.state();
    if let Some(username) = state.find_username(email) {
        format!("username: {}", username)
    } else {
        format!("not found")
    }
}