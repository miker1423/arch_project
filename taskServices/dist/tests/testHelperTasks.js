"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var taskHelper = require("../tasks");
var models = require("../models");
var assert = require('assert');
var isuuid = require('isuuid');
describe('Helper methods', function () {
    describe('Generate task id (UUID)', function () {
        it('Should return true', function () {
            assert.ok(isuuid(taskHelper.createTaskID()));
        });
    });
    describe('String mangement', function () {
        describe('Equals', function () {
            var string1 = 'Hello';
            var string2 = 'Hello';
            var string3 = "Holi Crayoli";
            it('Should return true, as they are equal', function () {
                assert.ok(taskHelper.equals(string1, string2));
                assert.ok(taskHelper.equals(string2, string1));
            });
            it('Should return false, as they are different', function () {
                assert.equal(taskHelper.equals(string1, string3), false);
                assert.equal(taskHelper.equals(string3, string1), false);
            });
        });
    });
    var payload = { title: "Hola", description: "Jejejejeej",
        dueDate: 4, reminderHour: 5464, complete: false };
    var task1 = taskHelper.createTaskFromPayload(payload, "3456", "45");
    var task2 = taskHelper.createTaskFromPayload(payload, "3456", "44");
    var task3 = taskHelper.createTaskFromPayload(payload, "3455", "46");
    describe('Create a task', function () {
        it('Should return a task', function () {
            var task4 = taskHelper.createTaskFromPayload(payload, "3455", "46");
            assert.ok(task4 instanceof models.Task);
        });
    });
    describe('Functions using the map', function () {
        var map = new Map();
        map.set(task1.taskID, task1);
        map.set(task2.taskID, task2);
        map.set(task3.taskID, task3);
        describe('Contains Task', function () {
            it("Should return false, as the taskID doesn't exist", function () {
                var taskID = "4829389";
                assert.equal(taskHelper.containsTask(map, taskID), false);
            });
            it("Should return true, as the task exists", function () {
                var taskID = "45";
                assert.ok(taskHelper.containsTask(map, taskID));
            });
        });
        describe("Create array with similar user", function () {
            it("Should return an array of length 2, as 2 tasks have the same user", function () {
                var userID = "3456";
                var array = taskHelper.createTaskArray(map, userID);
                assert.equal(array.length, 2);
            });
            it("Should return an array of length 0, as none tasks have that user", function () {
                var userID = "34";
                var array = taskHelper.createTaskArray(map, userID);
                assert.equal(array.length, 0);
            });
        });
    });
});
