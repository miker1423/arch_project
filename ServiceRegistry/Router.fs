module Router

open System
open ServiceDb
open Saturn
open Giraffe
open FSharp.Control.Tasks.V2.ContextInsensitive
open Microsoft.AspNetCore.Http
open Models
open System.Net

let findAllServices =
    fun (next: HttpFunc) (ctx: HttpContext) -> 
        let service = ctx.GetService<IServiceRegistryDb>()
        service.GetServices |> Controller.json ctx 

let addService =
    fun (next:HttpFunc) (ctx: HttpContext) -> 
        let service = ctx.GetService<IServiceRegistryDb>()
        task {
            let! model = ctx.BindJsonAsync<ServiceDefinition>()
            let mutable address = IPAddress.None
            if IPAddress.TryParse(model.IpAddress, &address) then 
                let normalized = {
                    model with ServiceType = model.ServiceType.ToLowerInvariant(); IpAddress = model.IpAddress.ToLowerInvariant()
                }
                let result = service.Add(normalized)
                return! Controller.json ctx result
            else
                ctx.SetStatusCode 401
                return Some ctx
        }

let findService(input: string) = 
    fun (next: HttpFunc) (ctx: HttpContext) -> 
        let db = ctx.GetService<IServiceRegistryDb>()
        let mutable id = Guid.Empty
        if Guid.TryParse(input, &id) then 
            let service = db.FindServiceById id
            if service.IsSome then 
                Controller.json ctx service.Value
            else 
                ctx.SetStatusCode 404
                task { return Some ctx }        
        else 
            let services = db.FindServiceByType input
            Controller.json ctx services

let deleteService(input: string) =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        let db = ctx.GetService<IServiceRegistryDb>()
        let mutable id = Guid.Empty
        if Guid.TryParse(input, &id) then 
            let deleteResult = db.Remove(id)
            if deleteResult.IsSome then
                Controller.json ctx deleteResult.Value
            else 
                ctx.SetStatusCode 404
                task { return Some ctx }            
        else
            ctx.SetStatusCode 404
            task { return Some ctx }

let servicesRouter = router {
    get "/" findAllServices
    post "/" addService
    getf "/%s" findService
    deletef "%s" deleteService
}

let appRouter = router {
    forward "/services" servicesRouter
}