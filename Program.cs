using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

// 1. TEST AVL TREE
int[] numbers = Enumerable.Range(1, 10000).ToArray();
Shuffle(numbers);

AVLTree tree = new AVLTree();

Stopwatch watch = Stopwatch.StartNew();

foreach (int number in numbers)
    tree.Insert(number);

watch.Stop();
Console.WriteLine("AVL Tree Insertion: " + watch.ElapsedMilliseconds + " ms");

watch.Restart();

foreach (int number in numbers)
    tree.Search(number);

watch.Stop();
Console.WriteLine("AVL Tree Searching: " + watch.ElapsedMilliseconds + " ms");

// 2. TEST TASK SCHEDULING
PriorityTaskScheduler scheduler = new PriorityTaskScheduler();

watch.Restart();

for (int i = 0; i < 10000; i++)
    scheduler.Enqueue(new TaskItem("Task " + i, 10000 - i));

watch.Stop();
Console.WriteLine("Task Scheduling Insertion: " + watch.ElapsedMilliseconds + " ms");

watch.Restart();

while (scheduler.Count > 0)
    scheduler.Dequeue();

watch.Stop();
Console.WriteLine("Task Scheduling Removal: " + watch.ElapsedMilliseconds + " ms");

// 3. TEST SORTING ALGORITHMS
Random random = new Random(42);

int[] original = Enumerable.Range(1, 5000)
    .Select(_ => random.Next(1, 100000))
    .ToArray();

int[] bubbleArray = (int[])original.Clone();
int[] mergeArray = (int[])original.Clone();

watch.Restart();
BubbleSort(bubbleArray);
watch.Stop();

Console.WriteLine("Bubble Sort: " + watch.ElapsedMilliseconds + " ms");

watch.Restart();
OptimizedSorting.MergeSort(mergeArray);
watch.Stop();

Console.WriteLine("Merge Sort: " + watch.ElapsedMilliseconds + " ms");

// 4. TEST TASK EXECUTION
TaskExecution executor = new TaskExecution();

TextWriter originalOutput = Console.Out;

try
{
    Console.SetOut(TextWriter.Null);

    watch.Restart();

    for (int i = 1; i <= 1000; i++)
    {
        executor.ExecuteTask("Task " + i, () => { });
    }

    watch.Stop();
}
finally
{
    Console.SetOut(originalOutput);
}

Console.WriteLine("Task Execution (1000 tasks): " +
                  watch.ElapsedMilliseconds + " ms");

Console.WriteLine("\nAll performance tests completed successfully.");

// BUBBLE SORT FOR COMPARISON
static void BubbleSort(int[] array)
{
    for (int i = 0; i < array.Length - 1; i++)
    {
        for (int j = 0; j < array.Length - i - 1; j++)
        {
            if (array[j] > array[j + 1])
            {
                int temp = array[j];
                array[j] = array[j + 1];
                array[j + 1] = temp;
            }
        }
    }
}

// SHUFFLE TEST DATA
static void Shuffle(int[] array)
{
    Random random = new Random(42);

    for (int i = array.Length - 1; i > 0; i--)
    {
        int j = random.Next(i + 1);

        (array[i], array[j]) = (array[j], array[i]);
    }
}
