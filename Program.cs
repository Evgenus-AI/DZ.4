using System;

class Program
{
    // static, потому что вызываем из Main (он тоже static) и объект класса нам не нужен.
    // Входные параметры: два int. Возвращаемое значение: int.
    static int Max(int a, int b)
    {
        // Тернарный оператор: условие ? если_true : если_false
        // Это короткая запись if/else, которая возвращает значение.
        return a > b ? a : b;
    }

    static void Main()
    {
        // Тест: набор пар чисел, включая отрицательные, равные и нули.
        // (int, int) — это кортеж: два значения в одной «упаковке».
        (int, int)[] testCases =
        {
            (3, 7),
            (10, 2),
            (-5, -9),
            (0, 0),
            (4, 4),
            (-1, 1)
        };

        foreach (var (x, y) in testCases)   // сразу «распаковываем» кортеж в x и y
        {
            Console.WriteLine($"Max({x}, {y}) = {Max(x, y)}");
        }
    }
}