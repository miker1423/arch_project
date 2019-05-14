
import * as Express from "express";

const healthRouter = Express.Router();

healthRouter.get("/health", (req, res) => {
    // res.json({
    //     hello: "world"
    // });
    res.status(200);
    res.end();
});

export default healthRouter;