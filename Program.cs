using System;

class Program
{
    // n считается с 1: Fibonacci(1) = 1, Fibonacci(2) = 1, Fibonacci(3) = 2 ...
    static long Fibonacci(int n)
    {
        if (n < 1)
            throw new ArgumentOutOfRangeException(nameof(n), "Номер числа Фибоначчи должен быть >= 1");

        // Базовый случай: первые два числа ряда равны 1. Здесь рекурсия останавливается.
        if (n <= 2)
            return 1;

        // Шаг рекурсии прямо по формуле из условия: Fk = Fk-1 + Fk-2
        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }

    static void Main()
    {
        // Первые 10 чисел ряда
        for (int i = 1; i <= 10; i++)
        {
            Console.WriteLine($"F({i}) = {Fibonacci(i)}");
        }

        // Для наглядности: большое число (займёт заметную долю секунды из-за множества повторных вызовов)
        Console.WriteLine($"F(30) = {Fibonacci(30)}");
    }
}
