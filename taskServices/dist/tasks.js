"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Express = require("express");
var models_1 = require("./models");
var rabbitMessages_1 = require("./rabbitMessages");
var _a = require('express-validator/check'), body = _a.body, validationResult = _a.validationResult;
var uuidv1 = require('uuid/v1');
var taskRouter = Express.Router();
var BAD_REQUEST = 400;
var NOT_FOUND = 404;
var PRECONDITION_FAILED = 412;
var jsonStructure = [body('taskID').isInt().optional(),
    body('title').exists(),
    body('description').exists(),
    body('dueDate').isInt().exists(),
    body('reminderHour').isInt({ min: 0, max: 23 }).exists(),
    body('complete').isBoolean().exists(),
    body('userID').isUUID().optional()];
exports.mapTask = new Map();
taskRouter.get("/:userID", function (req, res) {
    var userID = req.params.userID;
    var taskArray = createTaskArray(exports.mapTask, userID);
    if (taskArray.length <= 0) {
        return sendResponseWithStatusCode(res, NOT_FOUND);
    }
    res.json(taskArray);
    res.end();
});
taskRouter.post("/:userID", jsonStructure, function (req, res) {
    var errors = validationResult(req);
    if (!errors.isEmpty()) {
        return sendResponseWithStatusCodeAndJson(res, BAD_REQUEST, { errors: errors.array() });
    }
    var userID = req.params.userID;
    var taskID = createTaskID();
    var payload = req.body;
    var task = createTaskFromPayload(payload, userID, taskID);
    if (containsTask(exports.mapTask, task.taskID)) {
        return sendResponseWithStatusCode(res, PRECONDITION_FAILED);
    }
    exports.mapTask.set(task.taskID, task);
    res.json(task);
    res.end();
    var message = new models_1.Message(task.userID, task.taskID, task.complete, task.title, task.dueDate);
    rabbitMessages_1.sendRabbitMessage(JSON.stringify(message));
});
taskRouter.put("/:userID/:taskID", jsonStructure, function (req, res) {
    var errors = validationResult(req);
    if (!errors.isEmpty()) {
        return sendResponseWithStatusCodeAndJson(res, BAD_REQUEST, { errors: errors.array() });
    }
    var taskID = req.params.taskID;
    var userID = req.params.userID;
    var payload = req.body;
    var task = createTaskFromPayload(payload, userID, taskID);
    if (!containsTask(exports.mapTask, taskID) || !equals(exports.mapTask.get(taskID).userID, userID)) {
        return sendResponseWithStatusCode(res, PRECONDITION_FAILED);
    }
    exports.mapTask.set(task.taskID, task);
    res.json(task);
    res.end();
});
taskRouter.delete("/:userID/:taskID", function (req, res) {
    var userID = req.params.userID;
    var taskID = req.params.taskID;
    if (!containsTask(exports.mapTask, taskID) || !equals(exports.mapTask.get(taskID).userID, userID)) {
        return sendResponseWithStatusCode(res, PRECONDITION_FAILED);
    }
    exports.mapTask.delete(taskID);
    res.end();
});
taskRouter.post("/:userID/:taskID/complete", function (req, res) {
    var userID = req.params.userID;
    var taskID = req.params.taskID;
    if (!containsTask(exports.mapTask, taskID) || !equals(exports.mapTask.get(taskID).userID, userID)) {
        return sendResponseWithStatusCode(res, PRECONDITION_FAILED);
    }
    var task = exports.mapTask.get(taskID);
    task.changeComplete();
    res.json(task);
    res.end();
    var message = new models_1.Message(task.userID, task.taskID, task.complete, task.title, task.dueDate);
    rabbitMessages_1.sendRabbitMessage(JSON.stringify(message));
});
function containsTask(map, taskID) {
    return map.has(taskID);
}
exports.containsTask = containsTask;
function createTaskArray(map, userID) {
    var array = [];
    map.forEach(function (task, key) {
        if (task.userID == userID) {
            array.push(task);
        }
    });
    return array;
}
exports.createTaskArray = createTaskArray;
function createTaskFromPayload(payload, userID, taskID) {
    var task = new models_1.Task(taskID, payload.title, payload.description, payload.dueDate, payload.reminderHour, payload.complete, userID);
    return task;
}
exports.createTaskFromPayload = createTaskFromPayload;
function sendResponseWithStatusCode(res, statusCode) {
    res.status(statusCode);
    res.end();
}
exports.sendResponseWithStatusCode = sendResponseWithStatusCode;
function sendResponseWithStatusCodeAndJson(res, statusCode, json) {
    res.status(statusCode).json(json);
    res.end();
}
exports.sendResponseWithStatusCodeAndJson = sendResponseWithStatusCodeAndJson;
function equals(string1, string2) {
    return string1 == string2;
}
exports.equals = equals;
function createTaskID() {
    return uuidv1();
}
exports.createTaskID = createTaskID;
function saveMap() {
    var localStorage = JSON.stringify(Array.from(exports.mapTask.entries()));
    var fs = require('fs');
    fs.writeFileSync("map.txt", localStorage, function (err) {
        if (err) {
            console.log(err);
        }
    });
    return;
}
exports.saveMap = saveMap;
function obtainMap() {
    var fs = require('fs');
    exports.mapTask = new Map();
    console.log(fs.existsSync('map.txt'));
    if (fs.existsSync('map.txt')) {
        var rawdata = fs.readFileSync('map.txt');
        var dataJSON = JSON.parse(rawdata);
        for (var i = 0; i < dataJSON.length; i++) {
            var iTask = dataJSON[i][1];
            var task = new models_1.Task(iTask.taskID, iTask.title, iTask.description, iTask.dueDate, iTask.reminderHour, iTask.complete, iTask.userID);
            exports.mapTask.set(dataJSON[i][0], task);
        }
        // mapTask=new Map<string, Task>(JSON.parse(rawdata));
    }
}
exports.obtainMap = obtainMap;
exports.default = taskRouter;
