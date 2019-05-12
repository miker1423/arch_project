mod state;
mod db_worker;
mod handlers;
mod models;

use crate::state::AppState;
use actix_web::{server, App};
use std::sync::Arc;
use actix_web::http::Method;
use std::thread::spawn;
use service_registry_client::service_registry_client::{
    ServiceRegistryClient,
    ServiceDefinition
};

fn main() {
    let address = "127.0.0.1:8000";

    let service = ServiceDefinition {
        service_type: "auth_service".into(),
        ip_address: "127.0.0.1".into(),
        port: 8000,
        api_version: 1
    };

    let registry_client = ServiceRegistryClient::new("http://localhost".into(), 8085);
    let result =  registry_client.register_service(service);
    let id = match result {
        Some(saved_service) => saved_service.id,
        None => ""
    };

    println!("{}", address);
    let state = Arc::new(AppState::load_from_file("./users.db"));

    let cloned_state = state.clone();
    spawn(move || db_worker::start_new(cloned_state));

    server::new(move || {
        App::with_state(state.clone())
            .resource("/health", |r| r.method(Method::GET).with(handlers::is_healthy))
            .scope("/users", |users| {
                users.resource("", |r| {
                  r.method(Method::PUT).with(handlers::update_user);
                  r.method(Method::POST).with(handlers::create_user);
                }).resource("/{username}", |r| {
                  r.method(Method::GET).with(handlers::find_user);
                  r.method(Method::DELETE).with(handlers::delete_user);
                })
            })
            .resource("/login", |r| {
                r.method(Method::POST).with(handlers::login);
            })
    })
    .bind(address)
    .expect("Can not bind to port 8000")
    .run();

    let removed = registry_client.remove_service(id.into());
    let message = if removed {
        "Removed service from registry"
    } else {
        "Failed to remove service"
    };
    println!(message);
}
