"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
Object.defineProperty(exports, "__esModule", { value: true });
var body_parser_1 = require("body-parser");
var Express = require("express");
var health_1 = require("./health");
var tasks_1 = require("./tasks");
var taskHelper = require("./tasks");
var clientSR_1 = require("./clientSR");
var cors = require('cors');
function exitHandler(serviceID) {
    return __awaiter(this, void 0, void 0, function () {
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    console.log(serviceID);
                    taskHelper.saveMap();
                    return [4 /*yield*/, clientSR_1.deleteService(serviceID)];
                case 1:
                    _a.sent();
                    process.exit();
                    return [2 /*return*/];
            }
        });
    });
}
function main() {
    return __awaiter(this, void 0, void 0, function () {
        var app, serviceID;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    app = Express();
                    app.use(cors());
                    taskHelper.obtainMap();
                    app.use(body_parser_1.json());
                    app.use(health_1.default);
                    app.use("/tasks", tasks_1.default);
                    app.listen(3000, function () {
                        console.log("Running!");
                    });
                    return [4 /*yield*/, clientSR_1.postNotifyExistance()];
                case 1:
                    serviceID = _a.sent();
                    console.log(serviceID);
                    process.stdin.resume(); //so the program will not close instantly
                    //do something when app is closing
                    process.on('exit', exitHandler.bind(null, serviceID));
                    //catches ctrl+c event
                    process.on('SIGINT', exitHandler.bind(null, serviceID));
                    // catches "kill pid" (for example: nodemon restart)
                    process.on('SIGUSR1', exitHandler.bind(null, serviceID));
                    process.on('SIGUSR2', exitHandler.bind(null, serviceID));
                    //catches uncaught exceptions
                    process.on('uncaughtException', exitHandler.bind(null, serviceID));
                    return [2 /*return*/];
            }
        });
    });
}
main();
