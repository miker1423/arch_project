interface TaskVM {
    taskId?: string;
    title: string;
    description: string;
    dueDate: number;
    reminderHour: number;
    reminderDays: number;
    complete: boolean;
    userId?: string;
}