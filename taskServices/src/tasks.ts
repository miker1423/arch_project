import * as Express from "express";
import { MessageChannel } from "worker_threads";
import {Task, ITask, Message}  from "./models"
import {sendRabbitMessage} from "./rabbitMessages"
const { body, validationResult } = require('express-validator/check');
const uuidv1 = require('uuid/v1');

const taskRouter = Express.Router();

const BAD_REQUEST=400;
const NOT_FOUND=404;
const PRECONDITION_FAILED=412;

let jsonStructure=[body('taskID').isInt().optional(), //eventually change to uuid userID and taskID
                    body('title').exists(),
                    body('description').exists(), 
                    body('dueDate').isInt().exists(), 
                    body('reminderHour').isInt({min:0, max:23}).exists(),
                    body('complete').isBoolean().exists(),
                    body('userID').isUUID().optional()];

export let mapTask = new Map<string, Task>();

taskRouter.get("/:userID",(req, res) => { //ready

    let userID: string=req.params.userID;
    let taskArray=createTaskArray(mapTask,userID);

    if (taskArray.length<=0){
        return sendResponseWithStatusCode(res,NOT_FOUND);
    }

    res.json(taskArray);
    res.end();
});

taskRouter.post("/:userID",jsonStructure,(req, res) => { //ready

    const errors = validationResult(req);

    if (!errors.isEmpty()) {
        return sendResponseWithStatusCodeAndJson(res,BAD_REQUEST,{ errors: errors.array()})
    }

    let userID: string=req.params.userID;
    let taskID: string=createTaskID();

    const payload = req.body as ITask;
    let task: Task = createTaskFromPayload(payload, userID, taskID);

    if (containsTask(mapTask, task.taskID)){
        return sendResponseWithStatusCode(res,PRECONDITION_FAILED);
    }
    
    mapTask.set(task.taskID,task);

    res.json(task);
    res.end();

    let message: Message= new Message(task.userID,task.taskID,task.complete,task.title,task.dueDate);
    sendRabbitMessage(JSON.stringify(message));

});

taskRouter.put("/:userID/:taskID",jsonStructure,(req, res) => {

    const errors = validationResult(req);
    if (!errors.isEmpty()) {
        return sendResponseWithStatusCodeAndJson(res,BAD_REQUEST,{ errors: errors.array() })
    }

    let taskID: string=req.params.taskID;
    let userID: string=req.params.userID;

    const payload = req.body as ITask;
    let task: Task =createTaskFromPayload(payload,userID,taskID);
    
    if (!containsTask(mapTask, taskID) || !equals(mapTask.get(taskID).userID, userID)){
        return sendResponseWithStatusCode(res,PRECONDITION_FAILED);    
    }

    mapTask.set(task.taskID,task);

    res.json(task);
    res.end();


});

taskRouter.delete("/:userID/:taskID",(req, res) => { //ready

    let userID:string=req.params.userID;
    let taskID:string=req.params.taskID;

    if (!containsTask(mapTask, taskID) || !equals(mapTask.get(taskID).userID, userID)){
        return sendResponseWithStatusCode(res,PRECONDITION_FAILED);    
    }

    mapTask.delete(taskID);
    res.end();

});

taskRouter.post("/:userID/:taskID/complete",(req, res) => { //ready

    let userID: string=req.params.userID;
    let taskID: string=req.params.taskID;

    if (!containsTask(mapTask, taskID) || !equals(mapTask.get(taskID).userID,userID)){
        return sendResponseWithStatusCode(res,PRECONDITION_FAILED);    
    }

    let task: Task= mapTask.get(taskID);
    task.changeComplete();

    res.json(task);
    res.end();

    let message: Message= new Message(task.userID,task.taskID,task.complete,task.title,task.dueDate);
    sendRabbitMessage(JSON.stringify(message));
    
});

export function containsTask(map: Map<string,Task>, taskID: string): boolean{
    return map.has(taskID);
}

export function createTaskArray(map: Map<string,Task>,userID: string): Task[]{
    let array=[];
    map.forEach((task: Task, key: string) => {
        if (task.userID==userID){
            array.push(task)
        }
    });

    return array;
}

export function createTaskFromPayload(payload: ITask, userID: string, taskID: string): Task{
    let task: Task = new Task(taskID,payload.title,payload.description,
        payload.dueDate,payload.reminderHour,payload.complete,userID);

    return task;
}


export function sendResponseWithStatusCode(res,statusCode: number){

    res.status(statusCode);
    res.end();

}

export function sendResponseWithStatusCodeAndJson(res,statusCode: number, json: any){

    res.status(statusCode).json(json);
    res.end();

}

export function equals(string1: string, string2: string):boolean{
    return string1==string2;
}

export function createTaskID(): string{
    return uuidv1();
}

export function saveMap() {
    let localStorage = JSON.stringify(Array.from(mapTask.entries()));
    

    let fs = require('fs');
    fs.writeFileSync("map.txt", localStorage, function(err) {
        if (err) {
            console.log(err);
        }
    });

    return;
    
}

export function obtainMap() {
    const fs = require('fs');

    mapTask=new Map<string, Task>();
    console.log(fs.existsSync('map.txt'));
    if (fs.existsSync('map.txt')){
        let rawdata = fs.readFileSync('map.txt'); 
        let dataJSON = JSON.parse(rawdata);

        for(let i=0; i<dataJSON.length;i++){
            let iTask: ITask= dataJSON[i][1];
            let task: Task= new Task(iTask.taskID,iTask.title,iTask.description,iTask.dueDate,iTask.reminderHour,
                iTask.complete,iTask.userID);
            
            mapTask.set(dataJSON[i][0],task);
        }
        
        
        // mapTask=new Map<string, Task>(JSON.parse(rawdata));
    }

   
}

export default taskRouter;

