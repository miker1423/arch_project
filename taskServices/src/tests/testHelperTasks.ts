import * as taskHelper from "../tasks"
import * as models from "../models"
import { stringify } from "querystring";
let assert = require('assert');
let isuuid = require('isuuid');

describe('Helper methods', function(){

    describe('Generate task id (UUID)', function(){
        it('Should return true',function(){
            assert.ok(isuuid(taskHelper.createTaskID()));
        });
    });

    describe('String mangement', function(){
        describe('Equals', function(){
            let string1: string='Hello';
            let string2: string='Hello';
            let string3: string="Holi Crayoli";
            it('Should return true, as they are equal',function(){
                assert.ok(taskHelper.equals(string1,string2));
                assert.ok(taskHelper.equals(string2,string1));
            });
            it('Should return false, as they are different',function(){
                assert.equal(taskHelper.equals(string1,string3), false);
                assert.equal(taskHelper.equals(string3,string1), false);
            });
        });
        
    });

    let payload: models.ITask = {title: "Hola", description:"Jejejejeej", 
    dueDate:4, reminderHour:5464, complete:false};
    let task1=taskHelper.createTaskFromPayload(payload, "3456","45");
    let task2=taskHelper.createTaskFromPayload(payload, "3456","44");
    let task3=taskHelper.createTaskFromPayload(payload, "3455","46");

    describe('Create a task',function(){
        it('Should return a task', function(){
            let task4: models.Task=taskHelper.createTaskFromPayload(payload, "3455","46");
            assert.ok(task4 instanceof models.Task);
        });
        
    });

    describe('Functions using the map', function(){

        let map =new Map<string, models.Task>();

        map.set(task1.taskID,task1);
        map.set(task2.taskID,task2);
        map.set(task3.taskID,task3);

        describe('Contains Task', function(){
            
            it("Should return false, as the taskID doesn't exist", function(){
                let taskID="4829389";
                assert.equal(taskHelper.containsTask(map,taskID),false);
            });
            it("Should return true, as the task exists", function(){
                let taskID="45";
                assert.ok(taskHelper.containsTask(map,taskID));
            });
            
        });

        describe("Create array with similar user",function(){

            it("Should return an array of length 2, as 2 tasks have the same user", function(){
                let userID="3456";
                let array=taskHelper.createTaskArray(map, userID);
                assert.equal(array.length,2);
            });

            it("Should return an array of length 0, as none tasks have that user", function(){
                let userID="34";
                let array=taskHelper.createTaskArray(map, userID);
                assert.equal(array.length,0);
            });
                
        });
    });

});