using System;

class Program
{
    // НОД двух чисел по алгоритму Евклида:
    // пока b != 0, заменяем пару (a, b) на (b, a % b). Когда b станет 0, ответ лежит в a.
    // Пример: (48, 18) -> (18, 12) -> (12, 6) -> (6, 0) => НОД = 6
    static int Gcd(int a, int b)
    {
        // Числа должны быть натуральными (> 0). Проверяем, чтобы не получить странные результаты.
        if (a <= 0 || b <= 0)
            throw new ArgumentOutOfRangeException(a <= 0 ? nameof(a) : nameof(b), "Числа должны быть натуральными (> 0)");

        while (b != 0)
        {
            (a, b) = (b, a % b); // a и b — копии (передача по значению), поэтому спокойно меняем их
        }

        return a;
    }

    // ПЕРЕГРУЗКА: то же имя Gcd, но другое число параметров (три вместо двух).
    // Компилятор сам выбирает нужный вариант по количеству и типам аргументов.
    // Идея: НОД(a, b, c) = НОД(НОД(a, b), c)
    static int Gcd(int a, int b, int c)
    {
        return Gcd(Gcd(a, b), c); // валидация уже есть внутри двухпараметрического метода
    }

    static void Main()
    {
        // Два числа
        Console.WriteLine($"Gcd(48, 18) = {Gcd(48, 18)}");   // 6
        Console.WriteLine($"Gcd(17, 5) = {Gcd(17, 5)}");     // 1 (взаимно простые)
        Console.WriteLine($"Gcd(100, 25) = {Gcd(100, 25)}"); // 25
        Console.WriteLine($"Gcd(7, 7) = {Gcd(7, 7)}");       // 7

        // Три числа
        Console.WriteLine($"Gcd(48, 18, 30) = {Gcd(48, 18, 30)}"); // 6
        Console.WriteLine($"Gcd(12, 18, 27) = {Gcd(12, 18, 27)}"); // 3
        Console.WriteLine($"Gcd(8, 9, 10) = {Gcd(8, 9, 10)}");     // 1

        // Проверка некорректного ввода
        try
        {
            Gcd(0, 5);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}