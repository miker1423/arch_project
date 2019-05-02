module DictionaryWrapper

open System
open Models
open ServiceDb

type DictionaryWrapper() =
    let store = dict[]

    interface IServiceRegistryDb with 
        member this.Add service = 
            let id = Guid.NewGuid()
            let savedService = { Id = id; ServiceDefinition = service }
            store.Item(id) <- savedService
            savedService

        member this.Remove id = 
            if store.ContainsKey(id) then 
                let service = store.Item(id)
                if store.Remove(id) then Some service
                else None                
            else None      

        member this.FindServiceByType typeSearch =
            store.Values
            |> Seq.where(fun service -> service.ServiceDefinition.ServiceType.Equals(typeSearch))
            |> Seq.map(fun result -> result.ServiceDefinition)
            |> Seq.toList

        member this.GetServices =
            store.Values 
            |> Seq.map(fun service -> service.ServiceDefinition) 
            |> Seq.toList

        member this.FindServiceById id = 
            if store.ContainsKey(id) then 
                store.Item(id).ServiceDefinition 
                |> Some
            else None                   
             