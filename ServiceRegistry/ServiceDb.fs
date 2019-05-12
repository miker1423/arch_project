module ServiceDb

open Models
open System

type IServiceRegistryDb = 
    abstract member Add: ServiceDefinition -> SavedServiceDefinition
    abstract member Remove: Guid -> Option<SavedServiceDefinition>
    abstract member GetServices: list<SavedServiceDefinition>
    abstract member FindServiceByType: string -> list<SavedServiceDefinition>
    abstract member FindServiceById: Guid -> Option<SavedServiceDefinition>
