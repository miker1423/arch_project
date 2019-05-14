import models.UserHabit
import models.UserTask
import org.junit.jupiter.api.Assertions
import org.junit.jupiter.api.Test
import persistence.HabitRepository
import persistence.TaskRepository
import persistence.UserRepository
import java.time.Instant
import java.util.*

class UserTest {

    @Test
    fun testAddTask(){
        val date = GregorianCalendar(2019, Calendar.JUNE, 1).time
        val task = UserTask("1", "1", "Test 1", false, date)

        UserRepository.addTask(task)
        assert(UserRepository.allReports[task.userId]!!.tasks.isNotEmpty())
    }

    @Test
    fun testAddHabit(){
        val habit = UserHabit("1","1", "Test", 67, 1)

        UserRepository.addHabit(habit)
        assert(UserRepository.allReports[habit.userId]!!.habits.isNotEmpty())
    }

    @Test
    fun testAddTodayTask(){
        val task = UserTask("1", "1", "Test 1", false, Date.from(Instant.now()))

        UserRepository.addTask(task)
        val tupleExp: MutableMap<String, String> = mutableMapOf(Pair(task.id, task.title))
        val tuple = UserRepository.userReport[task.userId]!!.todayTasks
        Assertions.assertEquals(tupleExp, tuple)
    }

    @Test
    fun testAddDelayedTask(){
        val date = GregorianCalendar(2019, Calendar.FEBRUARY, 1).time
        val task = UserTask("1", "1", "Test 1", false, date)

        UserRepository.addTask(task)
        val tuple: MutableMap<String, String> = mutableMapOf(Pair(task.id, task.title))
        Assertions.assertEquals(tuple, UserRepository.userReport[task.userId]!!.delayedTask)
    }

    @Test
    fun testAddGoodHabit(){
        val habit = UserHabit("1","1", "Test", 67, 0)

        UserRepository.addHabit(habit)
        val tuple: MutableMap<String, String> = mutableMapOf(Pair(habit.id, habit.title))
        Assertions.assertEquals(tuple, UserRepository.userReport[habit.userId]!!.goodHabits)
    }

    @Test
    fun testAddBadHabit(){
        val habit = UserHabit("1","1", "Test", 67, 1)

        UserRepository.addHabit(habit)
        val tuple: MutableMap<String, String> = mutableMapOf(Pair(habit.id, habit.title))
        Assertions.assertEquals(tuple, UserRepository.userReport[habit.userId]!!.badHabits)
    }
}