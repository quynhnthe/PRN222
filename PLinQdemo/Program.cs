
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net.Mail;

class Program
{
    // IsPrime returns true if number is Prime,
    private static bool IsPrime(int number)
    {
        bool result = true;
        if (number < 2)
        {
            return false;
        }
        for (var divisor = 2; divisor <= Math.Sqrt(number) && result == true; divisor++)
        {
            if (number % divisor ==0) {
            result = false;

        }


        }
        return result;

    }//end IsPrime

    private static IList<int> GetPrimeList(IList<int> numbers) => numbers.Where(IsPrime).ToList();
    private static IList<int> GetPrimeListWithParallel(IList<int> numbers)
    {
        var primeNumbers = new ConcurrentBag<int>();
        Parallel.ForEach(numbers, number =>
        {
            if (IsPrime(number))
            {
                primeNumbers.Add(number);
            }
        });
        return primeNumbers.ToList();
    }//end GetprimeListWithpara11e1
    static void Main() {
        // 2 million
        var limit = 100000;
        var numbers = Enumerable.Range(0, limit).ToList();
        var watch = Stopwatch.StartNew();
        var primeNumbersFromForeach = GetPrimeList(numbers);
        watch.Stop(); // dừng lại các luồng khác để thực hiện xong luồng trên
        var watchForParallel = Stopwatch.StartNew();
        var primeNumbersFromParallelForeach = GetPrimeListWithParallel(numbers);
        watchForParallel.Stop();
        Console.WriteLine($"Classical foreach loop | Total prime numbers: " +
            $"{primeNumbersFromForeach.Count} | Time Taken: " +
            $"{watch.ElapsedMilliseconds} ms.");
        Console.WriteLine($"Parallel.ForEach loop | Total prime numbers: " +
            $"{primeNumbersFromParallelForeach.Count} | Time Taken: " +
            $"{watchForParallel.ElapsedMilliseconds} ms.");
        Console.WriteLine("Press ..");
        Console.ReadLine();
    }
}