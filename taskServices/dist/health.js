"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Express = require("express");
var healthRouter = Express.Router();
healthRouter.get("/health", function (req, res) {
    // res.json({
    //     hello: "world"
    // });
    res.status(200);
    res.end();
});
exports.default = healthRouter;
