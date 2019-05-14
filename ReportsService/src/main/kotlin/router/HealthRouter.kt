package router

import io.ktor.application.call
import io.ktor.http.HttpStatusCode
import io.ktor.response.respond
import io.ktor.routing.*
import persistence.HabitRepository
import persistence.TaskRepository

fun Route.check(){

    get("/") {
        call.respond(HttpStatusCode.NoContent, "")
    }
}