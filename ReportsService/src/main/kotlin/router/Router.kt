package router

import io.ktor.application.Application
import io.ktor.routing.Route
import io.ktor.routing.route
import io.ktor.routing.routing

fun Application.router() = routing {
    trace { println(it.buildText()) }
    route("/reports/admin", Route::admin)
    route("/reports/users", Route::users)
}