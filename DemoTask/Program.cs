
//Demo01
//class Program
//{
//    static void PrintNumber(string message)
//    {
//        for (int i = 0; i < 5; i++)
//        {
//            Console.WriteLine($"{message} : {i}");
//            Thread.Sleep(1000);
//        }
//    }
//    static void Main()
//    {
//        Thread.CurrentThread.Name = "main";
//        Task task01 = new Task(() => PrintNumber("Task 01"));
//        task01.Start();
//        Task task02 = Task.Run(delegate { PrintNumber("Task 02"); });
//        Task task03 = new Task(new Action(() => { PrintNumber("Task 03"); }));
//        task03.Start();
//        Console.WriteLine($"Thread '{Thread.CurrentThread.Name}");
//        Console.ReadKey();

//    }
//}

//Demo02
class Program
{
    
    static void Main()
    {
        Task[] tasks = new Task[5];
        String taskData = "Hello";
        for (int i = 0; i < 5; i++) {
            tasks[i] = Task.Run(() =>
            {
                Console.WriteLine($"Task = {Task.CurrentId}, obj = {taskData}, " +
                    $"ThreadId = {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(1000);
            });
        }
        try
        {
            Task.WaitAll(tasks);
        } catch (AggregateException ex) {
            Console.WriteLine("One more ex: ");
            foreach (var e in ex.Flatten().InnerExceptions) Console.WriteLine("{0}", e.Message);
        }
        Console.WriteLine("Status of completed tasks");
        foreach(var t in tasks)
        {
            Console.WriteLine("Task #{0} : {1}", t.Id, t.Status);
        }

    }
}
