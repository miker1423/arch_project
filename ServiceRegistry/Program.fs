module Server

open Saturn
open ServiceDb
open DictionaryWrapper
open Microsoft.Extensions.DependencyInjection
open HealthCheckingService
open Microsoft.AspNetCore.Cors.Infrastructure

let cors(builder: CorsPolicyBuilder) =  
        builder.AllowAnyHeader() |> ignore
        builder.AllowAnyMethod() |> ignore
        builder.AllowAnyOrigin() |> ignore


let addServices(services:IServiceCollection) =
    services
        .AddCors(fun options -> options.AddDefaultPolicy(cors))
        .AddHttpClient()
        .AddSingleton<IServiceRegistryDb, DictionaryWrapper>()
        .AddHostedService<TimedService>()

let endpointPipe = pipeline {
    plug head
    plug requestId
}

let app = application {
    pipe_through endpointPipe
    use_router Router.appRouter
    error_handler (fun ex _ -> pipeline { json ex })
    url "http://0.0.0.0:8085/"
    service_config addServices
    memory_cache
    use_cors "Default" cors 
}

[<EntryPoint>]
let main _ =
    run app
    0 
