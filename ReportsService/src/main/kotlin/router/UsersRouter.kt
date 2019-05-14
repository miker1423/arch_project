package router

import io.ktor.application.call
import io.ktor.http.HttpStatusCode
import io.ktor.response.respond
import io.ktor.routing.*
import persistence.UserRepository

fun Route.users(){

    get("/{id}") {
        val id = call.parameters["id"]?: ""
        val report = UserRepository.userReport[id]

        if (report == null) call.respond(HttpStatusCode.NotFound, "UserID $id Not Found")
        else call.respond(report)
    }
}