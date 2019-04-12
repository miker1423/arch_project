
use crate::state::AppState;
use actix_web::{Responder, HttpRequest, Json, HttpResponse, Path, ResponseError};
use std::sync::Arc;
use uuid::Uuid;
use crate::models::user::User;
use crate::models::user_view::UserView;

pub fn create_user((req, user) : (HttpRequest<Arc<AppState>>, Json<UserView>)) -> impl Responder {
    let new_user: User = user.into_inner().into();
    let new_user = new_user.add_id();
    let state: &AppState = req.state();
    state.add_user(new_user.clone());
    HttpResponse::Ok().json(new_user)
}

pub fn update_user((req, user): (HttpRequest<Arc<AppState>>, Json<User>)) -> impl Responder {
    HttpResponse::Ok()
}

pub fn find_user((req, user): (HttpRequest<Arc<AppState>>, Path<String>)) -> impl Responder {
    let username = user.into_inner();
    let state: &AppState = req.state();
    if let Some(user) = state.find_user(user.as_str()) {
        HttpResponse::Ok().json(user)
    } else {
        HttpResponse::BadRequest().finish()
    }

}

pub fn delete_user((req, username): (HttpRequest<Arc<AppState>>, Path<String>)) -> impl Responder {
    HttpResponse::Ok()
}

pub fn login((req, user): (HttpRequest<Arc<AppState>>, Json<User>)) -> impl Responder {
    HttpResponse::Ok()
}

pub fn is_healthy(req: HttpRequest<Arc<AppState>>) -> impl Responder {
    HttpResponse::Ok()
}