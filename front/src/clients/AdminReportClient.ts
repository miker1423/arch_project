export default class AdminReportClient {

    async GetAdminTasksReport() {
        var res = await fetch(`api/admin/tasks`, {
            method: "GET"
        });

        if (res.ok) {
            var json = await res.json();
            return json as UsersTaskReport;
        }

        console.log(`ERROR: ${res}`);
        return null;
    }

    async GetAdminHabitsReport() {
        var res = await fetch(`api/admin/habits`, {
            method: "GET"
        });

        if (res.ok) {
            var json = await res.json();
            return json as UsersHabitReport;
        }

        console.log(`ERROR: ${res}`);
        return null;
    }
}