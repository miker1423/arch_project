"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Express = require("express");
var helloRouter = Express.Router();
helloRouter.get("/", function (req, res) {
    res.json({
        hello: "world"
    });
    res.end();
});
helloRouter.post("/", function (req, res) {
    var payload = req.body;
    console.log(payload.hello);
    res.json({
        hello: "world"
    });
    res.end();
});
exports.default = helloRouter;
