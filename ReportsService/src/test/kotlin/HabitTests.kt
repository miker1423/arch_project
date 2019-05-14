import models.UserHabit
import org.junit.jupiter.api.Assertions
import org.junit.jupiter.api.Test
import persistence.HabitRepository

class HabitTests {

    @Test
    fun testAddHabit(){
        val habit = UserHabit("1","Test", 67, "cesar")

        HabitRepository.addHabit(habit)
        assert(HabitRepository.allHabits.isNotEmpty())
    }

    @Test
    fun testAddCorrectHabit(){
        val habit = UserHabit("1","Test", 67, "cesar")

        HabitRepository.addHabit(habit)
        assert(HabitRepository.allHabits.contains(habit.id))
    }

    @Test
    fun testCheckBestHabitWithOne() {
        val habit = UserHabit("1","Test", 67, "cesar")

        HabitRepository.addHabit(habit)
        assert(HabitRepository.habitsReport.bestHabit!! == habit)
    }

    @Test
    fun testCheckBestHabitWithMany() {
        val habit1 = UserHabit("1","Test", 67, "cesar")
        val habit2 = UserHabit("2","Test", 70, "miguel")
        val habit3 = UserHabit("3","Test", 30, "chris")

        HabitRepository.addHabit(habit1)
        HabitRepository.addHabit(habit2)
        HabitRepository.addHabit(habit3)
        assert(HabitRepository.habitsReport.bestHabit!! == habit2)
    }

    @Test
    fun testCheckBestHabitNull() {
        assert(HabitRepository.habitsReport.bestHabit == null)
    }

    @Test
    fun testCheckWorstHabitWithOne() {
        val habit = UserHabit("1","Test", 67, "cesar")

        HabitRepository.addHabit(habit)
        assert(HabitRepository.habitsReport.worstHabit!! == habit)
    }

    @Test
    fun testCheckWorstHabitWithMany() {
        val habit1 = UserHabit("1","Test", 67, "cesar")
        val habit2 = UserHabit("2","Test", 20, "miguel")
        val habit3 = UserHabit("3","Test", 30, "chris")

        HabitRepository.addHabit(habit1)
        HabitRepository.addHabit(habit2)
        HabitRepository.addHabit(habit3)
        assert(HabitRepository.habitsReport.worstHabit!! == habit2)
    }

    @Test
    fun testCheckWorstHabitNull() {
        assert(HabitRepository.habitsReport.worstHabit == null)
    }


    @Test
    fun testZeroHabitsRangeRed() {
        assert(HabitRepository.habitsReport.habitsPerRange.redRange == 0)
    }

    @Test
    fun testZeroHabitsRangeOrange() {
        assert(HabitRepository.habitsReport.habitsPerRange.orangeRange == 0)
    }

    @Test
    fun testZeroHabitsRangeYellow() {
        assert(HabitRepository.habitsReport.habitsPerRange.yellowRange == 0)
    }

    @Test
    fun testZeroHabitsRangeGreen() {
        assert(HabitRepository.habitsReport.habitsPerRange.greenRange == 0)
    }

    @Test
    fun testZeroHabitsRangeBlue() {
        assert(HabitRepository.habitsReport.habitsPerRange.blueRange == 0)
    }

    @Test
    fun testHabitsRangeWithOne() {
        val habit = UserHabit("1","Test", 67, "cesar")

        HabitRepository.addHabit(habit)
        assert(HabitRepository.habitsReport.habitsPerRange.blueRange == 1)
    }

    @Test
    fun testHabitsRangeWithMany() {
        val habit1 = UserHabit("1", "Test 1", 67, "cesar")
        val habit2 = UserHabit("2", "Test 2", 70, "miguel")
        val habit3 = UserHabit("3", "Test 3", 30, "chris")
        val habit4 = UserHabit("4", "Test 4", 42, "chris")
        val habit5 = UserHabit("5", "Test 5", -3, "miguel")

        HabitRepository.addHabit(habit1)
        HabitRepository.addHabit(habit2)
        HabitRepository.addHabit(habit3)
        HabitRepository.addHabit(habit4)
        HabitRepository.addHabit(habit5)

        with(HabitRepository.habitsReport.habitsPerRange) {
            Assertions.assertEquals(2, blueRange)
            Assertions.assertEquals(1, greenRange)
            Assertions.assertEquals(1, yellowRange)
            Assertions.assertEquals(0, orangeRange)
            Assertions.assertEquals(1, redRange)
        }
    }

}