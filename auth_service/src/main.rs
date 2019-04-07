mod user;
mod state;

use crate::user::User;
use crate::state::AppState;
use actix_web::{server, App, HttpRequest, Responder, HttpResponse, HttpMessage};
use actix_web::AsyncResponder;
use std::sync::Arc;
use timer::Timer;
use time::Duration;
use std::fs::File;
use std::io::Write;
use std::fs::OpenOptions;
use bytes::Bytes;
use futures::future::Future;

fn greet(req: &HttpRequest<Arc<AppState>>) -> impl Responder {
    req.json()
        .from_err()
        .and_then(|a: User| {
            println!("{:?}", a);
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
    let user = User { username, email };
    let state: &AppState = req.state();

    state.add_user(user);
    format!("Success")
}

fn main() {
    let address = "127.0.0.1:8000";

    println!("{}", address);
    let state = Arc::new(AppState::load_from_file("./users.db"));
    let timer = Timer::new();

    let _guard = {
        let cloned_state = state.clone();
        timer.schedule_repeating(Duration::seconds(5), move || {
            let is_modified = cloned_state.is_modified();
            if !is_modified { return; }

            let result = OpenOptions::new().write(true).open(cloned_state.file_path.clone());
            if let Ok(mut file) = result {
                println!("Writting file");
                for user in cloned_state.users.read().unwrap().values() {
                    let serialized: String = String::from(user);
                    let result = file.write(serialized.as_bytes());
                    if let Err(e) = result {
                        println!("{}", e);
                    }
                }
                let result = file.flush();
                if let Err(e) = result {
                    println!("{}", e);
                }
            }
            cloned_state.finish_write();
        })
    };

    server::new(move || {
        App::with_state(state.clone())
            .resource("/", |r| r.f(greet))
            .resource("/{name}", |r| r.f(greet))
            .resource("/add/{username}/{email}", |r| r.f(register_user))
            .resource("/find/{email}", |r| r.f(find_user))

    })
    .bind(address)
    .expect("Can not bind to port 8000")
    .run();
}
