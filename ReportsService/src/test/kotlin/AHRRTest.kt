import models.UserHabit
import org.junit.jupiter.api.Test
import persistence.AdminHabitReportRepository

class AHRRTest {

    @Test
    fun testAddHabit(){
        val admin = AdminHabitReportRepository
        val habit = UserHabit("Test", 67, "cesar")

        admin.addHabit(habit)
        assert(admin.allHabits.isNotEmpty())
    }

    @Test
    fun testAddCorrectHabit(){
        val admin = AdminHabitReportRepository
        val habit = UserHabit("Test", 67, "cesar")

        admin.addHabit(habit)
        assert(admin.allHabits.last() == habit)
    }

    @Test
    fun testCheckBestHabitWithOne() {
        val admin = AdminHabitReportRepository
        val habit = UserHabit("Test", 67, "cesar")

        admin.addHabit(habit)
        assert(admin.habitsReport.bestHabit!! == habit)
    }

    @Test
    fun testCheckBestHabitWithMany() {
        val admin = AdminHabitReportRepository
        val habit1 = UserHabit("Test", 67, "cesar")
        val habit2 = UserHabit("Test", 70, "miguel")
        val habit3 = UserHabit("Test", 30, "chris")

        admin.addHabit(habit1)
        admin.addHabit(habit2)
        admin.addHabit(habit3)
        assert(admin.habitsReport.bestHabit!! == habit2)
    }

    @Test
    fun testCheckBestHabitNull() {
        val admin = AdminHabitReportRepository
        assert(admin.habitsReport.bestHabit == null)
    }

    @Test
    fun testCheckWorstHabitWithOne() {
        val admin = AdminHabitReportRepository
        val habit = UserHabit("Test", 67, "cesar")

        admin.addHabit(habit)
        assert(admin.habitsReport.worstHabit!! == habit)
    }

    @Test
    fun testCheckWorstHabitWithMany() {
        val admin = AdminHabitReportRepository
        val habit1 = UserHabit("Test", 67, "cesar")
        val habit2 = UserHabit("Test", 20, "miguel")
        val habit3 = UserHabit("Test", 30, "chris")

        admin.addHabit(habit1)
        admin.addHabit(habit2)
        admin.addHabit(habit3)
        assert(admin.habitsReport.worstHabit!! == habit2)
    }

    @Test
    fun testCheckWorstHabitNull() {
        val admin = AdminHabitReportRepository
        assert(admin.habitsReport.worstHabit == null)
    }


    @Test
    fun testZeroHabitsRangeRed() {
        val admin = AdminHabitReportRepository
        assert(admin.habitsReport.habitsPerRange.redRange == 0)
    }

    @Test
    fun testZeroHabitsRangeOrange() {
        val admin = AdminHabitReportRepository
        assert(admin.habitsReport.habitsPerRange.orangeRange == 0)
    }

    @Test
    fun testZeroHabitsRangeYellow() {
        val admin = AdminHabitReportRepository
        assert(admin.habitsReport.habitsPerRange.yellowRange == 0)
    }

    @Test
    fun testZeroHabitsRangeGreen() {
        val admin = AdminHabitReportRepository
        assert(admin.habitsReport.habitsPerRange.greenRange == 0)
    }

    @Test
    fun testZeroHabitsRangeBlue() {
        val admin = AdminHabitReportRepository
        assert(admin.habitsReport.habitsPerRange.blueRange == 0)
    }

    @Test
    fun testHabitsRangeWithOne() {
        val admin = AdminHabitReportRepository
        val habit = UserHabit("Test", 67, "cesar")

        admin.addHabit(habit)
        assert(admin.habitsReport.habitsPerRange.blueRange == 1)
    }

    @Test
    fun testHabitsRangeWithMany() {
        val admin = AdminHabitReportRepository
        val habit1 = UserHabit("Test 1", 67, "cesar")
        val habit2 = UserHabit("Test 2", 70, "miguel")
        val habit3 = UserHabit("Test 3", 30, "chris")
        val habit4 = UserHabit("Test 4", 42, "chris")
        val habit5 = UserHabit("Test 5", -3, "miguel")

        admin.addHabit(habit1)
        admin.addHabit(habit2)
        admin.addHabit(habit3)
        admin.addHabit(habit4)
        admin.addHabit(habit5)
        assert(admin.habitsReport.habitsPerRange.blueRange == 2)
        assert(admin.habitsReport.habitsPerRange.yellowRange == 1)
        assert(admin.habitsReport.habitsPerRange.greenRange == 1)
        assert(admin.habitsReport.habitsPerRange.redRange == 1)
        assert(admin.habitsReport.habitsPerRange.orangeRange == 0)
    }

}