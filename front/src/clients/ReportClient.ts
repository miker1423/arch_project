export default class ReportClient {

    async GetReport(userId: string) {
        var res = await fetch(`api/users/${userId}`, {
            method: "GET"
        });

        if (res.ok) {
            var json = await res.json();
            return json as UserReportVM;
        }

        console.log(`ERROR: ${res}`);
        return null;
    }

}