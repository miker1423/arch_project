module ServiceDb

open Models
open System

type IServiceRegistryDb = 
    abstract member Add: ServiceDefinition -> SavedServiceDefinition
    abstract member Remove: Guid -> Option<SavedServiceDefinition>
    abstract member GetServices: list<ServiceDefinition>
    abstract member FindServiceByType: string -> list<ServiceDefinition>
    abstract member FindServiceById: Guid -> Option<ServiceDefinition>
