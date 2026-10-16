using System;

class Program
{
    // Рекурсия = метод вызывает сам себя. Нужны два обязательных элемента:
    //   1) базовый случай (когда останавливаемся),
    //   2) шаг, который приближает нас к базовому случаю.
    // n! = n * (n-1)!, а 0! = 1 и 1! = 1.
    static long Factorial(int n)
    {
        if (n < 0)
            throw new ArgumentOutOfRangeException(nameof(n), "Факториал определён только для n >= 0");

        // Базовый случай: без него рекурсия никогда бы не закончилась
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
