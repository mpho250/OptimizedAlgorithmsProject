using System;
using System.Collections.Generic;

public class TaskExecution
{
    private readonly List<string> executionLogs = new List<string>();

    public void ExecuteTask(string taskName, Action taskAction)
    {
        if (string.IsNullOrWhiteSpace(taskName))
            throw new ArgumentException("Task name is required.");

        if (taskAction == null)
            throw new ArgumentNullException(nameof(taskAction));

        try
        {
            Console.WriteLine($"Starting task: {taskName}");
            executionLogs.Add($"Started: {taskName}");

            taskAction();

            Console.WriteLine($"Task completed: {taskName}");
            executionLogs.Add($"Completed: {taskName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Task failed: {taskName}");
            Console.WriteLine($"Error: {ex.Message}");

            executionLogs.Add($"Failed: {taskName} - {ex.Message}");
        }
        finally
        {
            Console.WriteLine($"Execution finished: {taskName}");
        }
    }

    public void DisplayLogs()
    {
        Console.WriteLine("\nTASK EXECUTION LOGS");

        foreach (string log in executionLogs)
        {
            Console.WriteLine(log);
        }
    }
}
