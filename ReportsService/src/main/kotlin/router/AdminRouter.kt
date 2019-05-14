package router

import io.ktor.application.call
import io.ktor.response.respond
import io.ktor.routing.*
import persistence.HabitRepository
import persistence.TaskRepository

fun Route.admin(){

    get("/habits") {
        val report = HabitRepository.habitsReport
        call.respond(report)
    }

    get("/tasks") {
        val report = TaskRepository.tasksReport
        call.respond(report)
    }
}