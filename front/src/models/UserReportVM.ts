interface UserReportVM {
    id:	string,
    todayTasks: Map<string, string>
    delayedTask: Map<string, string>
    goodHabits: Map<string, string>
    badHabits: Map<string, string>
}