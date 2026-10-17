using System;

class Program
{
    static long Factorial(int n)
    {
        if (n < 0)
            throw new ArgumentOutOfRangeException(nameof(n), "Факториал определён только для n >= 0");
        if (n <= 1)
            return 1;

        // Шаг рекурсии. checked(...) — проверка переполнения для этого выражения,
        // чтобы на больших n получить OverflowException, а не тихий неверный результат.
        return checked(n * Factorial(n - 1));
    }

    static void Main()
    {
        int[] tests = { 0, 1, 5, 10, 20 };

        foreach (int n in tests)
        {
            Console.WriteLine($"{n}! = {Factorial(n)}");
        }

        // Проверка переполнения: 21! не помещается в long
        try
        {
            Console.WriteLine($"21! = {Factorial(21)}");
        }
        catch (OverflowException)
        {
            Console.WriteLine("21! — переполнение long");
        }
    }
}
