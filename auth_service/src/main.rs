mod user;
mod state;
mod db_worker;

use crate::user::User;
use crate::state::AppState;
use actix_web::{server, App, HttpRequest, Responder, Json, HttpResponse, HttpMessage, Error};
use std::sync::Arc;
use futures::future::Future;
use actix_web::FromRequest;
use actix_web::http::Method;
use actix_web::AsyncResponder;
use uuid::Uuid;

fn greet(req: &HttpRequest<Arc<AppState>>) -> Box<Future<Item=HttpResponse, Error=Error>> {
    req.json().from_err()
        .and_then(|value: User| {
            let user = value.add_id();
            Ok(HttpResponse::Ok().json(user))
        })
        .responder()
}

fn find_user(req: &HttpRequest<Arc<AppState>>) -> impl Responder {
    let email = req.match_info().get("email").unwrap_or("");
    let state: &AppState = req.state();
    if let Some(username) = state.find_username(email) {
        format!("username: {}", username)
    } else {
        format!("not found")
    }
}

fn register_user(req: &HttpRequest<Arc<AppState>>) -> impl Responder {
    let username = String::from(req.match_info().get("username").unwrap_or(""));
    let email = String::from(req.match_info().get("email").unwrap_or(""));
    let user = User::build(username, email, Uuid::new_v4().to_string());
    let state: &AppState = req.state();

    state.add_user(user);
    format!("Success")
}

fn main() {
    let address = "127.0.0.1:8000";

    println!("{}", address);
    let state = Arc::new(AppState::load_from_file("./users.db"));

    let _guard = db_worker::start_new(state.clone());

    server::new(move || {
        App::with_state(state.clone())
            .resource("/", |r| r.method(Method::POST).f(greet))
            .resource("/{name}", |r| r.f(greet))
            .resource("/add/{username}/{email}", |r| r.f(register_user))
            .resource("/find/{email}", |r| r.f(find_user))

    })
    .bind(address)
    .expect("Can not bind to port 8000")
    .run();
}
