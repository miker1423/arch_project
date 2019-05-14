"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var mongoose = require("mongoose");
mongoose.connect("mongodb://localhost/test", { useNewUrlParser: true });
var db = mongoose.connection;
db.on("error", console.error.bind(console, "connection error:"));
db.once("open", function () {
    console.log("connected");
});
var kitty = new mongoose.Schema({
    name: String
});
var Kitten = mongoose.model("Kitten", kitty);
exports.default = Kitten;
