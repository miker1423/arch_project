"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var models = require("../models");
var assert = require('assert');
describe('Tasks Class', function () {
    describe("ReminderHourValidation", function () {
        it('should return false', function () {
            !assert.equal(models.Task.isValidReminderHour(-2), false);
            assert.equal(models.Task.isValidReminderHour(25), false);
        });
        it('should return true', function () {
            assert.ok(models.Task.isValidReminderHour(0));
            assert.ok(models.Task.isValidReminderHour(23));
            assert.ok(models.Task.isValidReminderHour(6));
        });
    });
    describe("Task object (non static) methods", function () {
        var task = new models.Task("55", "Hola", "Mi proyecto", 5, 23, false, "dlajfldsf");
        describe("Change complete method", function () {
            it("should change the status from false to true", function () {
                task.changeComplete();
                assert.ok(task.complete);
            });
            it("should change the status from true to false", function () {
                task.changeComplete();
                assert.equal(task.complete, false);
            });
        });
    });
});
