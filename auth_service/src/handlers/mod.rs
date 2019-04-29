use actix_web::{Responder, HttpRequest, Json, HttpResponse, Path };
use std::sync::Arc;
use crate::models::{
    user::User,
    user_view::UserView,
    user_minimal::UserMinimal,
    token::{TokenProducer, TokenSender }
};
use crate::state::{AppState, Db};

pub fn create_user((req, user): (HttpRequest<Arc<AppState>>, Json<UserView>)) -> impl Responder {
    let user: User = user.into_inner().into();
    let user = user.add_id();
    let state: &AppState = req.state();
    state.add_user(user.clone());
    HttpResponse::Ok().json(UserMinimal::from(user))
}

pub fn update_user((req, user): (HttpRequest<Arc<AppState>>, Json<UserMinimal>)) -> impl Responder {
    let state: &AppState = req.state();
    let result = state.update_user(&mut user.clone());
    return match result {
        Some(user) => HttpResponse::Ok().json(user),
        _ => HttpResponse::BadRequest().finish()
    };
}

pub fn find_user((req, user): (HttpRequest<Arc<AppState>>, Path<String>)) -> impl Responder {
    let state: &AppState = req.state();
    return match state.find_user(user.as_str()) {
        Some(user) => HttpResponse::Ok().json(UserMinimal::from(user)),
        None => HttpResponse::BadRequest().finish()
    };
}

pub fn delete_user((req, username): (HttpRequest<Arc<AppState>>, Path<String>)) -> impl Responder {
    let state: &AppState = req.state();
    return match state.remove_user(username.into_inner()) {
        Some(user) => HttpResponse::Ok().json(UserMinimal::from(user)),
        None => HttpResponse::BadRequest().finish()
    };
}

pub fn login((req, user): (HttpRequest<Arc<AppState>>, Json<UserView>)) -> impl Responder {
    let user: User = user.into_inner().into();
    let state: &AppState = req.state();
    if let Some(u) = state.find_user(user.username.as_str()) {
        if u.password_hash.eq(&user.password_hash) {
            let token = TokenProducer::retrieve_token(&user);
            let token = TokenSender { token: token.unwrap() };
            return HttpResponse::Ok().json(token);
        } else {
            return HttpResponse::BadRequest().finish();
        }
    } else {
        return HttpResponse::BadRequest().finish();
    }
}

pub fn is_healthy(_: HttpRequest<Arc<AppState>>) -> impl Responder {
    HttpResponse::Ok()
}