import {ServiceDefinition, SavedServiceDefinition}  from "./models"

import fetch from "node-fetch";

export async function postNotifyExistance(): Promise<string> {
    let service = new ServiceDefinition("task",3000,"v1");
    let serviceJSON= JSON.stringify(service);
    

    const response = await fetch("http://192.168.137.1:8085/services", {
        method: "POST",
        headers: {
            "Content-Type": "appliction/json"
        },
        body: serviceJSON
    });
    let body = await response.json()
    

    return body.id;
      
}

export async function deleteService(serviceID: string)  {
    let url="http://192.168.137.1:8085/services/"+serviceID;
    const response = await fetch( url, {
        method: "Delete"}
    );
}