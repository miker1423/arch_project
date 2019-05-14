import component.MessageBroker
import config.config
import router.router
import io.ktor.application.Application
import io.ktor.server.engine.embeddedServer
import io.ktor.server.netty.Netty

fun main() {
    // val broker = MessageBroker()
    val server = embeddedServer(Netty, 8080, module = Application::main)
    server.start(wait = true)
}

fun Application.main() {
    config()
    router()
}

