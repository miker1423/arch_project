
pub mod service_registry_client {
    use uuid::Uuid;
    use serde::{Serialize, Deserialize};
    use reqwest::{Body, Client};

    #[derive(Serialize, Deserialize, Debug)]
    #[serde(rename_all = "camelCase")]
    pub struct ServiceDefinition {
        pub service_type: String,
        pub ip_address: String,
        pub port: i32,
        pub api_version: String
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

    #[derive(Serialize, Deserialize, Debug)]
    #[serde(rename_all = "camelCase")]
    pub struct SavedServiceRegistry {
        service_definition: ServiceDefinition,
        pub id: String
    }

    pub struct ServiceRegistryClient {
        http_client: Client,
        base_url: String,
        port: i32
    }

    impl ServiceRegistryClient {
        pub fn new(base_url: String, port: i32) -> ServiceRegistryClient {
            let http_client = Client::new();
            ServiceRegistryClient {
                http_client,
                base_url,
                port
            }
        }

        pub fn register_service(&self, service: ServiceDefinition) -> Option<SavedServiceRegistry> {
            let url = format!("{}:{}/services", self.base_url, self.port);
            let result = self.http_client.post(url.as_str())
                .json(&service)
                .send();
            match result {
                Ok(mut response) =>
                    if response.status().is_success() {
                        let json = response.json();
                        dbg!(&json);
                        match json {
                            Ok(t) => Some(t),
                            _ => None
                        }
                    } else {
                        None
                    }
                Err(_) => None
            }
        }

        pub fn remove_service(&self, id: Uuid) -> bool {
            let url = format!("{}:{}/services/{}", self.base_url, self.port, id);
            let result = self.http_client.delete(url.as_str()).send();
            match result {
                Ok(response) => response.status().is_success(),
                Err(_) => false
            }
        }

        pub fn get_services(&self) -> Option<Vec<SavedServiceRegistry>> {
            let url = format!("{}:{}/services", self.base_url, self.port);
            let response = self.http_client.get(url.as_str()).send();
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

        pub fn get_services_by_id(&self, id: Uuid) -> Option<SavedServiceRegistry> {
            let url = format!("{}:{}/services/{}", self.base_url, self.port, id);
            let response = self.http_client.get(url.as_str()).send();
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

        pub fn get_services_by_type(&self, service_type: String) -> Option<Vec<SavedServiceRegistry>> {
            let url = format!("{}:{}/services/{}", self.base_url, self.port, service_type);
            let response = self.http_client.get(url.as_str()).send();
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
}

#[cfg(test)]
mod tests {
}
