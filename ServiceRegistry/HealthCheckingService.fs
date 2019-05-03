module HealthCheckingService 

open System.Threading
open System.Threading.Tasks
open Microsoft.Extensions.Hosting
open ServiceDb
open System.Timers
open System
open FSharp.Collections.ParallelSeq
open System.Net.Http

type TimedService(db: IServiceRegistryDb, client: HttpClient) =
    let timer = new Timer(TimeSpan.FromSeconds(5.).TotalMilliseconds)
    
    let elapsed _ =
        timer.Stop()
        let services = db.GetServices
        services
        |> List.toSeq
        |> PSeq.map (fun service -> Models.isAlive(client, service))
        |> PSeq.map(fun results -> results |> Async.RunSynchronously)
        |> PSeq.zip services
        |> PSeq.filter (fun (service, result) -> result)
        
        timer.Start()

    interface IHostedService with
        member this.StartAsync(token: CancellationToken) =
            timer.Elapsed.Add elapsed
            Task.CompletedTask
        member this.StopAsync(token: CancellationToken) = 
            Task.CompletedTask        