using System;
using System.Threading; // нужен ТОЛЬКО для Thread.Sleep в задаче 3 (пауза на 3 секунды)

// Уровень ворчливости деда: перечисление (enum), нумерация с нуля: Low = 0, Medium = 1 
enum Grumpiness { Low, Medium, High, Extreme }

// Задача 4: структура Дед
struct Ded
{
    public string Name;           
    public Grumpiness Level;      
    public string[] Phrases;      
    public int Bruises;           // синяки от бабки; у int значение по умолчанию 0, отдельно присваивать не надо

    // Метод внутри структуры. Принимает деда и список «плохих» слов (params).
    // ref перед Ded — ВАЖНО: структура это значимый тип, без ref метод получил бы КОПИЮ деда,
    // и синяки поставились бы копии, а не настоящему деду.
    // params должен стоять последним в списке параметров.
    public static int CountBruises(ref Ded ded, params string[] badWords)
    {
        int count = 0;

        foreach (string phrase in ded.Phrases)
        {
            string lowPhrase = phrase.ToLower(); // приводим к нижнему регистру, чтобы «Гады» и «гады» считались одинаково

            foreach (string word in badWords)
            {
                if (lowPhrase.Contains(word.ToLower()))
                {
                    count++; // фингал за каждое найденное слово
                }
            }
        }

        ded.Bruises += count; // записываем синяки самому деду
        return count;
    }
}

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== МЕНЮ ===");
            Console.WriteLine("Введите номер задачи (1-4) или exit / закрыть для выхода:");

            // ?? "" — «если ReadLine вернул null, возьми пустую строку»; убирает предупреждение компилятора
            string choice = (Console.ReadLine() ?? "").Trim().ToLower();

            switch (choice)
            {
                case "1":
                    Task1();
                    break;
                case "2":
                    Task2();
                    break;
                case "3":
                    if (Task3())
                    {
                        return; // выход из Main = программа завершается, консоль закрывается
                    }
                    break;
                case "4":
                    Task4();
                    break;
                case "exit":
                case "закрыть":
                    return;
                default:
                    Console.WriteLine("Нет такого пункта меню.");
                    break;
            }
        }
    }

    // ---------- Задача 1 ----------
    static void Task1()
    {
        Console.WriteLine();
        Console.WriteLine("=== Задача 1 ===");
        Console.WriteLine("Массив из 20 случайных чисел. Вы вводите два числа из массива, они меняются местами.");
        Console.WriteLine("От вас: ввести два числа, которые есть в массиве.");

        Random ran = new Random();
        int[] arr = new int[20];
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = ran.Next(1, 100); 
        }

        Console.WriteLine("Исходный массив:");
        PrintArray(arr);

        Console.Write("Введите первое число: ");
        int first = int.Parse(Console.ReadLine() ?? "");
        Console.Write("Введите второе число: ");
        int second = int.Parse(Console.ReadLine() ?? "");

        // IndexOf ищет значение и возвращает индекс первого вхождения, если не нашёл — -1
        // (если в массиве есть повторы, меняем местами первые найденные)
        int index1 = Array.IndexOf(arr, first);
        int index2 = Array.IndexOf(arr, second);

        if (index1 == -1 || index2 == -1)
        {
            Console.WriteLine("Одного из этих чисел нет в массиве, менять нечего.");
            return;
        }

        // Классический обмен через временную переменную
        int temp = arr[index1];
        arr[index1] = arr[index2];
        arr[index2] = temp;

        Console.WriteLine("Массив после обмена:");
        PrintArray(arr);
    }

    static void PrintArray(int[] array)
    {
        foreach (int n in array)
        {
            Console.Write("{0} ", n);
        }
        Console.WriteLine();
    }

    // ---------- Задача 2 ----------
    // Возвращает сумму (return), произведение отдаёт через ref, среднее — через out.
    // Порядок: обычные параметры, а params — последним.
    static long ProcessArray(ref long product, out double average, params int[] numbers)
    {
        long sum = 0;

        foreach (int n in numbers)
        {
            sum += n;
            product *= n; // product пришёл «снаружи» уже инициализированным (единицей) — это требование ref
        }

        // out-параметр ОБЯЗАТЕЛЬНО присвоить внутри метода на любом пути выполнения
        if (numbers.Length == 0)
        {
            average = 0; // защита: делить на ноль элементов нельзя
        }
        else
        {
            average = (double)sum / numbers.Length; // (double) нужен, иначе деление целых отбросит дробную часть
        }

        return sum;
    }

    static void Task2()
    {
        Console.WriteLine();
        Console.WriteLine("=== Задача 2 ===");
        Console.WriteLine("Метод с params: возвращает сумму, через ref отдаёт произведение, через out — среднее арифметическое.");
        Console.WriteLine("От вас: ничего, программа сама покажет два вызова.");

        // Вызов 1: список аргументов через запятую
        long product = 1; // для ref переменную надо инициализировать до вызова; 1, потому что произведение
        double average;   // для out инициализировать не нужно
        long sum = ProcessArray(ref product, out average, 2, 4, 6, 8, 10);
        Console.WriteLine("Вызов со списком 2, 4, 6, 8, 10:");
        Console.WriteLine("Сумма: {0}, произведение: {1}, среднее: {2}", sum, product, average);

        // Вызов 2: готовый массив
        int[] arr = new int[] { 3, 5, 7 };
        product = 1; // обязательно сбрасываем, иначе умножение продолжится со старого значения
        sum = ProcessArray(ref product, out average, arr);
        Console.WriteLine("Вызов с массивом 3, 5, 7:");
        Console.WriteLine("Сумма: {0}, произведение: {1}, среднее: {2}", sum, product, average);
    }

    // ---------- Задача 3 ----------
    // «Шрифт» цифр 3x5: массив из 10 цифр, у каждой массив из 5 строк (ступенчатый массив)
    static string[][] digitsFont = new string[][]
    {
        new string[] { "###", "# #", "# #", "# #", "###" }, // 0
        new string[] { " # ", "## ", " # ", " # ", "###" }, // 1
        new string[] { "###", "  #", "###", "#  ", "###" }, // 2
        new string[] { "###", "  #", "###", "  #", "###" }, // 3
        new string[] { "# #", "# #", "###", "  #", "  #" }, // 4
        new string[] { "###", "#  ", "###", "  #", "###" }, // 5
        new string[] { "###", "#  ", "###", "# #", "###" }, // 6
        new string[] { "###", "  #", "  #", "  #", "  #" }, // 7
        new string[] { "###", "# #", "###", "# #", "###" }, // 8
        new string[] { "###", "# #", "###", "  #", "###" }  // 9
    };

    // Возвращает true, если пользователь решил выйти из программы
    static bool Task3()
    {
        Console.WriteLine();
        Console.WriteLine("=== Задача 3 ===");
        Console.WriteLine("Рисуем цифру символами #. Для числа вне 0-9 консоль краснеет на 3 секунды.");
        Console.WriteLine("От вас: вводить числа. Не число — программа упадёт с исключением. exit / закрыть — выход.");

        while (true)
        {
            Console.Write("Введите число: ");
            string input = (Console.ReadLine() ?? "").Trim().ToLower();

            if (input == "exit" || input == "закрыть")
            {
                return true;
            }
            
            int number = int.Parse(input);

            if (number < 0 || number > 9)
            {
                Console.BackgroundColor = ConsoleColor.Red; // красный фон
                Console.Clear();                            // Clear перекрашивает всю консоль в цвет фона
                Console.WriteLine("Ошибка: нужна цифра от 0 до 9!");
                Thread.Sleep(3000);                         // пауза 3000 миллисекунд = 3 секунды
                Console.ResetColor();                       // возвращаем обычные цвета
                Console.Clear();
            }
            else
            {
                foreach (string line in digitsFont[number])
                {
                    Console.WriteLine(line);
                }
            }
        }
    }

    // ---------- Задача 4 ----------
    static void Task4()
    {
        Console.WriteLine();
        Console.WriteLine("=== Задача 4 ===");
        Console.WriteLine("5 дедов с разным числом фраз. Бабка ставит фингал за каждое матерное слово из списка.");
        Console.WriteLine("От вас: ничего, программа сама всё посчитает.");

        // Массив структур. Поля заполняем через инициализатор объекта, как в примере с Player из лекции.
        // Bruises не указываем — он равен 0 по умолчанию.
        Ded[] deds = new Ded[]
        {
            new Ded { Name = "Семёныч", Level = Grumpiness.Low,     Phrases = new string[] { "Ироды!" } },
            new Ded { Name = "Петрович", Level = Grumpiness.Medium,  Phrases = new string[] { "Гады!", "Тьфу на вас!" } },
            new Ded { Name = "Михалыч", Level = Grumpiness.High,    Phrases = new string[] { "Проститутки!", "Бестолочи!", "Куда катится мир!" } },
            new Ded { Name = "Фёдорыч", Level = Grumpiness.Extreme, Phrases = new string[] { "Гады!", "Проститутки!", "Сорванцы!", "Чтоб вас!" } },
            new Ded { Name = "Игнатьич", Level = Grumpiness.Medium,  Phrases = new string[] { "Гады!", "Гады вы все!", "Проститутки!", "Вот в наше время...", "Тьфу!" } }
        };

        // Список матерных слов (передаём через params — просто через запятую)
        for (int i = 0; i < deds.Length; i++)
        {
            // ref deds[i] — передаём САМ элемент массива, а не его копию
            int bruises = Ded.CountBruises(ref deds[i], "гады", "проститутки");

            Console.WriteLine("{0} (ворчливость: {1}), фраз: {2}, получил фингалов сейчас: {3}, всего синяков: {4}",
                deds[i].Name, deds[i].Level, deds[i].Phrases.Length, bruises, deds[i].Bruises);
        }
    }
}