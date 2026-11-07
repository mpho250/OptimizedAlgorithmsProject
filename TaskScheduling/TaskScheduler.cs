using System;
using System.Collections.Generic;

public class TaskItem
{
    public string Name { get; set; }
    public int Priority { get; set; }

    public TaskItem(string name, int priority)
    {
        Name = name;
        Priority = priority;
    }
}

public class PriorityTaskScheduler
{
    private readonly List<TaskItem> heap = new List<TaskItem>();

    public int Count => heap.Count;

    // Add a task to the priority queue
    public void Enqueue(TaskItem task)
    {
        heap.Add(task);
        int index = heap.Count - 1;

        while (index > 0)
        {
            int parent = (index - 1) / 2;

            if (heap[parent].Priority <= heap[index].Priority)
                break;

            Swap(parent, index);
            index = parent;
        }
    }

    // Execute the highest-priority task first
    public TaskItem Dequeue()
    {
        if (heap.Count == 0)
            throw new InvalidOperationException("No tasks available.");

        TaskItem highestPriorityTask = heap[0];

        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);

        int index = 0;

        while (true)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;
            int smallest = index;

            if (left < heap.Count &&
                heap[left].Priority < heap[smallest].Priority)
                smallest = left;

            if (right < heap.Count &&
                heap[right].Priority < heap[smallest].Priority)
                smallest = right;

            if (smallest == index)
                break;

            Swap(index, smallest);
            index = smallest;
        }

        return highestPriorityTask;
    }

    private void Swap(int first, int second)
    {
        TaskItem temp = heap[first];
        heap[first] = heap[second];
        heap[second] = temp;
    }
}
