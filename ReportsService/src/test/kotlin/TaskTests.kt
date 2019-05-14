import models.UserTask
import org.junit.jupiter.api.Assertions
import org.junit.jupiter.api.Test
import persistence.TaskRepository
import java.time.Instant
import java.util.*

class TaskTests {

    @Test
    fun testAddTask(){
        val date = GregorianCalendar(2019, Calendar.JUNE, 1).time
        val task = UserTask("1", "1", "Test 1", false, date)

        TaskRepository.addTask(task)
        assert(TaskRepository.allTasks.isNotEmpty())
    }

    @Test
    fun testAddCorrectTask(){
        val date = GregorianCalendar(2019, Calendar.JUNE, 1).time
        val task = UserTask("1", "1", "Test 1", false, date)

        TaskRepository.addTask(task)
        assert(TaskRepository.allTasks.contains(task.id))
    }

    @Test
    fun testCheckTodayTask(){
        val task = UserTask("1", "1", "Test 1", false, Date.from(Instant.now()))

        TaskRepository.addTask(task)
        Assertions.assertEquals(1, TaskRepository.tasksReport.availableTodayTask)
    }

    @Test
    fun testCheckTodayTasksButCompleted(){
        val task = UserTask("1", "1", "Test 1", true, Date.from(Instant.now()))

        TaskRepository.addTask(task)
        Assertions.assertEquals(0, TaskRepository.tasksReport.availableTodayTask)
    }

    @Test
    fun testCheckDelayedTask(){
        val date = GregorianCalendar(2019, Calendar.FEBRUARY, 1).time
        val task = UserTask("1", "1", "Test 1", false, date)

        TaskRepository.addTask(task)
        Assertions.assertEquals(1, TaskRepository.tasksReport.delayedTask)
    }

    @Test
    fun testCheckAvailableTask(){
        val date = GregorianCalendar(2019, Calendar.JUNE, 1).time
        val task = UserTask("1", "1", "Test 1", false, date)

        TaskRepository.addTask(task)
        Assertions.assertEquals(1, TaskRepository.tasksReport.availableTask)
    }

    @Test
    fun testCheckCompletedTasksBeforeDueDate(){
        val date = GregorianCalendar(2019, Calendar.JUNE, 1).time
        val completionDate = GregorianCalendar(2019, Calendar.MAY, 1).time
        val task = UserTask("1", "1", "Test 1", true, date, completionDate)

        TaskRepository.addTask(task)
        Assertions.assertEquals(1, TaskRepository.tasksReport.completedBeforeDue)
    }

    @Test
    fun testCheckCompletedTasksAfterDueDate(){
        val date = GregorianCalendar(2019, Calendar.MAY, 1).time
        val completionDate = GregorianCalendar(2019, Calendar.JUNE, 1).time
        val task = UserTask("1", "1", "Test 1", true, date, completionDate)

        TaskRepository.addTask(task)
        Assertions.assertEquals(1, TaskRepository.tasksReport.completedAfterDue)
    }
}