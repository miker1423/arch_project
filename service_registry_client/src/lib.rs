
mod service_registry_client {
    use uuid::Uuid;
    use serde::{Serialize, Deserialize};
    use reqwest::Body;

    #[derive(Serialize, Deserialize)]
    struct ServiceDefinition {
        service_type: String,
        ip_address: String,
        port: i32,
        api_version: i32
    }

    impl Into<Body> for ServiceDefinition {
        fn into(self) -> Body {
            let json = serde_json::to_string(&self);
            if let Ok(json) = json {
                Body::from(json)
            } else {
                Body::from("")
            }
        }
    }

    #[derive(Serialize, Deserialize)]
    struct SavedServiceRegistry {
        service_definition: ServiceDefinition,
        id: String
    }

    fn register_service(service: ServiceDefinition) -> bool {
        let client = reqwest::Client::new();
        let result = client.post("http://localhost:8085")
            .body(service)
            .send();
        match result {
            Ok(response) => response.status().is_success(),
            Err(_) => false
        }
    }

    fn remove_service(id: Uuid) -> bool {
        let client = reqwest::Client::new();
        let url = format!("http://localhost:8085/{}", id);
        let result = client.delete(url.as_str()).send();
        match result {
            Ok(response) => response.status().is_success(),
            Err(_) => false
        }
    }

    fn get_services() -> Option<Vec<SavedServiceRegistry>> {
        let response = reqwest::get("http://localhost:8085/");
        match response {
            Ok(mut response) =>
                if response.status().is_success() {
                    match response.json() {
                        Ok(t) => Some(t),
                        _ => None
                    }
                } else {
                    None
                }
            _ => None
        }
    }

    fn get_services_by_id(id: Uuid) -> Option<SavedServiceRegistry> {
        let url = format!("http://localhost:8085/{}", id);
        let response = reqwest::get(url.as_str());
        match response {
            Ok(mut response) =>
                if response.status().is_success() {
                    match response.json() {
                        Ok(t) => Some(t),
                        _ => None
                    }
                } else {
                    None
                }
            _ => None
        }
    }

    fn get_services_by_type(service_type: String) -> Option<Vec<SavedServiceRegistry>> {
        let url = format!("http://localhost:8085/{}", service_type);
        let response = reqwest::get(url.as_str());
        match response {
            Ok(mut response) =>
                if response.status().is_success() {
                    match response.json() {
                        Ok(t) => Some(t),
                        _ => None
                    }
                } else {
                    None
                }
            _ => None
        }
    }
}

#[cfg(test)]
mod tests {
}
