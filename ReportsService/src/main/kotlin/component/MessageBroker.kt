package component

import com.fasterxml.jackson.databind.ObjectMapper
import com.rabbitmq.client.*
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext
import models.EntityType
import models.Message
import models.UserHabit
import models.UserTask
import persistence.HabitRepository
import persistence.TaskRepository
import persistence.UserRepository
import java.time.Instant
import java.util.*

class MessageBroker {

    private val factory = ConnectionFactory()
    private val connection: Connection
    private val channel: Channel

    init {
        factory.host = ""
        factory.username = ""
        factory.password = ""

        connection = factory.newConnection()
        channel = connection.createChannel()
        GlobalScope.launch  { listen() }
    }

    private fun consumeHabits(msg: Message?) {
        msg?.let {
            val habit = UserHabit(msg.EntityId, msg.UserId, msg.Title, msg.Score, msg.HabitType)
            HabitRepository.addHabit(habit)
            UserRepository.addHabit(habit)
        }
    }

    private fun consumeTasks(msg: Message?) {
        msg?.let {
            val task = UserTask(
                msg.EntityId,
                msg.UserId,
                msg.Title,
                msg.Completed,
                Date.from(Instant.ofEpochSecond(msg.DueDate))
            )
            TaskRepository.addTask(task)
            UserRepository.addTask(task)
        }
    }

    private suspend fun listen() = withContext(Dispatchers.Default) {
        channel.exchangeDeclare("message", "fanout")
        channel.queueBind("q", "message", "")
        channel.basicConsume("q", true, DeliverCallback {_: String, delivery: Delivery ->
            val payload = String(delivery.body, Charsets.UTF_8)
            val msg = ObjectMapper().readValue(payload, Message::class.java)
            when(msg.Entity) {
                EntityType.Habit -> consumeHabits(msg)
                EntityType.Task -> consumeTasks(msg)
            }
        }, CancelCallback {})
    }




}