using System;

class Program
{
    static void Swap(ref int a, ref int b)
    {
        (a, b) = (b, a);
    }

    static void Main()
    {
        int x = 5;
        int y = 10;

        Console.WriteLine($"До:    x = {x}, y = {y}");

        // При вызове ключевое слово ref нужно писать ещё раз.
        // Так C# заставляет явно показать, что переменные могут измениться.
        // Переменные ДОЛЖНЫ быть инициализированы до вызова (у нас они уже имеют значения).
        Swap(ref x, ref y);

        Console.WriteLine($"После: x = {x}, y = {y}");
    }
}
