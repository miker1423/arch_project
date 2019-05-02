module Models

open System

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