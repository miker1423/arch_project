mod user;
mod state;

use crate::user::User;
use crate::state::AppState;
use actix_web::{server, App, HttpRequest, Responder};
use std::sync::Arc;
use timer::Timer;
use time::Duration;

fn greet(req: &HttpRequest<Arc<AppState>>) -> impl Responder {
    let state: &AppState = req.state();
    state.add_user(User::new());

    let to = req.match_info().get("name").unwrap_or("World");
    format!("Hello {}!", to)
}

fn main() {
    let address = "127.0.0.1:8000";

    println!("{}", address);
    let state = Arc::new(AppState::new());
    let timer = Timer::new();

    let _guard = {
        let cloned_state = state.clone();
        timer.schedule_repeating(Duration::seconds(5), move || {
            let is_modified = cloned_state.is_modified();
            if !is_modified { return; }

            for user in cloned_state.users.read().unwrap().iter() {
                let _serialized: String = String::from(user);
            }
        })
    };


    server::new(move || {
        App::with_state(state.clone())
            .resource("/", |r| r.f(greet))
            .resource("/{name}", |r| r.f(greet))
    })
    .bind(address)
    .expect("Can not bind to port 8000")
    .run();
}
