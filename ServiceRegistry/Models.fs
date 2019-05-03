module Models

open System
open System.Net.Http

type ServiceDefinition = {
    ServiceType: string
    IpAddress: string
    Port: int
    ApiVersion: string
}

type SavedServiceDefinition = {
    ServiceDefinition: ServiceDefinition
    Id: Guid
}

let isAlive(client: HttpClient, service: ServiceDefinition) =
    async {
        let url = sprintf "http://%s:%i/health" service.IpAddress service.Port
        let! response = client.GetAsync(url) |> Async.AwaitTask
        return response.IsSuccessStatusCode
    }