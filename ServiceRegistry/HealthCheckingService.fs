module HealthCheckingService 

open System.Threading
open System.Threading.Tasks
open Microsoft.Extensions.Hosting
open ServiceDb
open System.Timers
open System
open FSharp.Collections.ParallelSeq
open System.Net.Http
open Microsoft.Extensions.Logging

type TimedService(db: IServiceRegistryDb, client: IHttpClientFactory , logger: ILogger<TimedService>) =
    let timer = new Timer(TimeSpan.FromSeconds(5.).TotalMilliseconds)
    
    let elapsed _ =
        timer.Stop()
        let services = db.GetServices
        let client = client.CreateClient()
        let removedServices = 
            services 
            |> List.toSeq
            |> PSeq.map (fun service -> Models.isAlive(client, service.ServiceDefinition))
            |> PSeq.map(fun results -> results |> Async.RunSynchronously)
            |> PSeq.zip services
            |> PSeq.filter (fun (_, result) -> not result)
            |> PSeq.map (fun (service, _) -> service.Id)
            |> PSeq.map (fun id -> db.Remove(id))
            |> PSeq.filter (fun service -> service.IsSome)
            |> PSeq.toList

        logger.LogInformation(sprintf "Removed %i services" removedServices.Length) 
        timer.Start()

    interface IHostedService with
        member this.StartAsync(token: CancellationToken) =
            timer.Elapsed.Add elapsed
            timer.Start()
            Task.CompletedTask
        member this.StopAsync(token: CancellationToken) = 
            timer.Stop()
            Task.CompletedTask        