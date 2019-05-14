import * as ip from "ip";

export interface ITask{
  taskID?: string,
  title: string,
  description: string,
  dueDate: number,
  reminderHour: number,
  complete: boolean,
  userID?: string,
}

export class Task implements ITask{
  taskID: string;
  title: string;
  description: string;
  dueDate: number;
  reminderHour: number;
  complete: boolean;
  userID: string;

  constructor(taskID: string,title: string,description: string,dueDate: number,
      reminderHour: number,complete: boolean,userID: string){
          this.taskID= taskID;
          this.title= title;
          this.description= description;
          this.dueDate= dueDate;
          this.reminderHour= reminderHour;
          this.complete= complete;
          this.userID= userID;
  }
  

  static isValidReminderHour(reminderHour: number): boolean{
      if (reminderHour<0 || reminderHour>23)
          return false;
      return true;
  }

  changeComplete(){
      this.complete=!this.complete;
  }

}

export class ServiceDefinition{
  ServiceType: string;
  IpAddress: string;
  Port: number;
  ApiVersion: string;

    constructor(ServiceType, Port, ApiVersion){
      this.ServiceType=ServiceType;
      this.Port=Port;
      this.ApiVersion=ApiVersion;
      this.IpAddress=ip.address();
    }
}

export class SavedServiceDefinition{
  ServiceDefinition: ServiceDefinition;
  Id: string;

  constructor(ServiceDefinition, Id){
    this.ServiceDefinition=ServiceDefinition;
    this.Id=Id;
  }
}

export class Message{
  Entity: number;
  Score: number;
  UserID: string;
  EntityID: string;
  HabitType: number;
  Completed: boolean;
  Title: string;
  DueDate: number;

  constructor(userID:string, entityID:string,completed:boolean, title:string, dueDate:number){
    this.Entity=1;
    this.Score=0;
    this.HabitType=0;

    this.UserID=userID;
    this.EntityID=entityID;
    this.Completed=completed;
    this.Title=title;
    this.DueDate=dueDate;

  }
}
