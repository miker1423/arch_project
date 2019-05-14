import { json } from "body-parser";
import * as Express from "express";
import healthRouter from "./health";
import taskRouter from "./tasks";
import * as taskHelper from "./tasks"
import { monitorEventLoopDelay } from "perf_hooks";
import { postNotifyExistance, deleteService } from "./clientSR";
const cors = require('cors')

async function exitHandler(serviceID: string) {
    console.log(serviceID);
    
    taskHelper.saveMap();
    await deleteService(serviceID);
    process.exit();
}

async function main() {

    const app = Express();
    app.use(cors())

    taskHelper.obtainMap();

    app.use(json());

    app.use(healthRouter);
    app.use("/tasks", taskRouter);

    app.listen(3000, () => {
        console.log("Running!");
    });

    const serviceID = await postNotifyExistance();
    console.log(serviceID);


    process.stdin.resume();//so the program will not close instantly

    //do something when app is closing
    process.on('exit', exitHandler.bind(null, serviceID));

    //catches ctrl+c event
    process.on('SIGINT', exitHandler.bind(null, serviceID));

    // catches "kill pid" (for example: nodemon restart)
    process.on('SIGUSR1', exitHandler.bind(null, serviceID));
    process.on('SIGUSR2', exitHandler.bind(null, serviceID));

    //catches uncaught exceptions
    process.on('uncaughtException', exitHandler.bind(null, serviceID));
}

main();





