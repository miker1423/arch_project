mod user;
mod state;
mod db_worker;
mod handlers;

use crate::state::AppState;
use actix_web::{server, App, };
use std::sync::Arc;
use actix_web::http::Method;

fn main() {
    let address = "127.0.0.1:8000";

    println!("{}", address);
    let state = Arc::new(AppState::load_from_file("./users.db"));

    let _guard = db_worker::start_new(state.clone());

    server::new(move || {
        App::with_state(state.clone())
            .resource("/", |r| r.method(Method::POST).with(handlers::greet))
            .resource("/add/{username}/{email}", |r| r.f(handlers::register_user))
            .resource("/find/{email}", |r| r.f(handlers::find_user))

    })
    .bind(address)
    .expect("Can not bind to port 8000")
    .run();
}
