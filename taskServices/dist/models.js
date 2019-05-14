"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ip = require("ip");
var Task = /** @class */ (function () {
    function Task(taskID, title, description, dueDate, reminderHour, complete, userID) {
        this.taskID = taskID;
        this.title = title;
        this.description = description;
        this.dueDate = dueDate;
        this.reminderHour = reminderHour;
        this.complete = complete;
        this.userID = userID;
    }
    Task.isValidReminderHour = function (reminderHour) {
        if (reminderHour < 0 || reminderHour > 23)
            return false;
        return true;
    };
    Task.prototype.changeComplete = function () {
        this.complete = !this.complete;
    };
    return Task;
}());
exports.Task = Task;
var ServiceDefinition = /** @class */ (function () {
    function ServiceDefinition(ServiceType, Port, ApiVersion) {
        this.ServiceType = ServiceType;
        this.Port = Port;
        this.ApiVersion = ApiVersion;
        this.IpAddress = ip.address();
    }
    return ServiceDefinition;
}());
exports.ServiceDefinition = ServiceDefinition;
var SavedServiceDefinition = /** @class */ (function () {
    function SavedServiceDefinition(ServiceDefinition, Id) {
        this.ServiceDefinition = ServiceDefinition;
        this.Id = Id;
    }
    return SavedServiceDefinition;
}());
exports.SavedServiceDefinition = SavedServiceDefinition;
var Message = /** @class */ (function () {
    function Message(userID, entityID, completed, title, dueDate) {
        this.Entity = 1;
        this.Score = 0;
        this.HabitType = 0;
        this.UserID = userID;
        this.EntityID = entityID;
        this.Completed = completed;
        this.Title = title;
        this.DueDate = dueDate;
    }
    return Message;
}());
exports.Message = Message;
