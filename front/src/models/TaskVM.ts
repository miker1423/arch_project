interface TaskVM {
    id: string;
    title: string;
    desc: string;
    dueDate: number;
    reminderHour: number;
    reminderDays: number;
    complete: boolean;
    userId: string;
}