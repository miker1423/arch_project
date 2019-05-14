pub mod state;
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
use uuid::Uuid;

fn main() {
    let address = "0.0.0.0:8000";

    let service = ServiceDefinition {
        service_type: "auth_service".into(),
        ip_address: "0.0.0.0".into(),
        port: 8000,
        api_version: "1".into()
    };

    let registry_client = ServiceRegistryClient::new("http://localhost".into(), 8085);
    let result =  registry_client.register_service(service);
    dbg!(&result);
    let id = match result {
        Some(saved_service) => saved_service.id,
        None => "".into()
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

    let message =
        match Uuid::parse_str(id.as_str()) {
            Ok(id) => {
                let removed = registry_client.remove_service(id);
                if removed {
                    "Removed service from registry"
                } else {
                    "Failed to remove service"
                }
            },
            _ => "Failed to parse id"
        };
    println!("{}", message);
}




#[cfg(test)]
mod test {
    use crate::state::*;
    use crate::models::user::*;
    #[test]
    fn test_add(){
        let app_state = get_state_with_user();
        let users_length = app_state.users.read().unwrap().len();
        assert_eq!(users_length, 1);
    }

    #[test]
    fn test_remove_user() {
        let app_state = get_state_with_user();

        let _ = app_state.remove_user("some_username".into());
        let users_length = app_state.users.read().unwrap().len();
        assert_eq!(users_length, 0);
    }

    #[test]
    fn test_find_user() {
        let app_state = get_state_with_user();

        if let Some(_) = app_state.find_user("some_username") {
            assert!(true);
        } else {
            assert!(false);
        }
    }

    #[test]
    fn test_find_username() { 
        let app_state = get_state_with_user();
        let result = app_state.find_username("test_user");
        if let Some(email) = result {
            assert_eq!(email, "test_user");
        }
    }

    fn get_state_with_user() -> AppState {       
        let app_state = AppState::load_from_file("");
        let user = User {
            email: "test_user".into(),
            id: "some_id".into(),
            password_hash: "some_hash".into(),
            username: "some_username".into()
        };
        app_state.add_user(user);
        app_state
    }
}