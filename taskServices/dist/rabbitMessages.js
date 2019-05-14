"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var amqp = require('amqplib/callback_api');
function sendRabbitMessage(msg) {
    amqp.connect('amqp://services:services@192.168.137.1:5672', function (error0, connection) {
        if (error0) {
            throw error0;
        }
        connection.createChannel(function (error1, channel) {
            if (error1) {
                throw error1;
            }
            var exchange = 'message';
            //   var msg = process.argv.slice(2).join(' ') || 'Hello World!';
            channel.assertExchange(exchange, 'fanout', {
                durable: true
            });
            channel.publish(exchange, '', Buffer.from(msg, 'utf8'));
            console.log(" [x] Sent %s", msg);
        });
        setTimeout(function () {
            connection.close();
        }, 6000);
    });
}
exports.sendRabbitMessage = sendRabbitMessage;
