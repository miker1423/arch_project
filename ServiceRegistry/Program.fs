module Server

open Saturn
open ServiceDb
open DictionaryWrapper
open Microsoft.Extensions.DependencyInjection

let endpointPipe = pipeline {
    plug head
    plug requestId
}

let app = application {
    pipe_through endpointPipe
    use_router Router.appRouter
    error_handler (fun ex _ -> pipeline { json ex })
    url "http://0.0.0.0:8085/"
    service_config (fun config -> config.AddSingleton<IServiceRegistryDb, DictionaryWrapper>() )
    memory_cache
}

[<EntryPoint>]
let main _ =
    run app
    0 
