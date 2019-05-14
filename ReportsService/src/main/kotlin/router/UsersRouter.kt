package router

import io.ktor.application.call
import io.ktor.response.respond
import io.ktor.routing.*
import persistence.UserRepository

fun Route.users(){

    get("/habits") {
        // val report = UserRepository.
        // call.respond(report)
    }
}