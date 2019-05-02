module Router

open System
open ServiceDb
open Saturn
open Giraffe
open FSharp.Control.Tasks.V2.ContextInsensitive
open Microsoft.AspNetCore.Http
open Models
open System.Net
open Saturn.ControllerHelpers
open Saturn.ControllerHelpers

type Message = { Text:string }

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

let servicesRouter = router {
    get "/" findAllServices
    post "/" addService
}

let appRouter = router {
    forward "/services" servicesRouter
}