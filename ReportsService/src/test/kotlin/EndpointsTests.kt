import io.ktor.application.Application
import io.ktor.client.HttpClient
import io.ktor.client.features.json.JacksonSerializer
import io.ktor.client.features.json.JsonFeature
import io.ktor.client.request.get
import io.ktor.server.engine.embeddedServer
import io.ktor.server.netty.Netty
import io.ktor.server.netty.NettyApplicationEngine
import kotlinx.coroutines.runBlocking
import models.UserReport
import models.UserTask
import models.UsersHabitReport
import models.UsersTaskReport
import org.junit.jupiter.api.BeforeAll
import org.junit.jupiter.api.Test
import persistence.UserRepository
import java.util.*

class EndpointsTests {

    companion object {
        lateinit var client: HttpClient
        lateinit var server: NettyApplicationEngine

        @BeforeAll @JvmStatic
        fun init() {
            server = embeddedServer(Netty, 8080, module = Application::main)
            client = HttpClient {
                install(JsonFeature) {
                    serializer = JacksonSerializer()
                }
            }
            server.start(false)
        }
    }

    @Test
    fun testGetHabits() {
        runBlocking {
            val habits = client.get<UsersHabitReport>("http://localhost:8080/reports/admin/habits")
            println(habits)
        }
    }

    @Test
    fun testGetTasks() {
        runBlocking {
            val tasks = client.get<UsersTaskReport>("http://localhost:8080/reports/admin/tasks")
            println(tasks)
        }
    }

    @Test
    fun testGetUserReport() {
        val date = GregorianCalendar(2019, Calendar.FEBRUARY, 1).time
        val task = UserTask("1", "1", "Test 1", false, date)
        UserRepository.addTask(task)

        runBlocking {
            val tasks = client.get<UserReport>("http://localhost:8080/reports/users/1")
            println(tasks)
        }
    }
}