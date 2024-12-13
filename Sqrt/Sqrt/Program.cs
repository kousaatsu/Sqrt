using System;
using System.Collections.Generic;

namespace Sqrt
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Sqrt calculation = new Sqrt(); // Создали новый объект для вычислений Sqrt

            Console.WriteLine("Введите число, из которого нужно вычислить корень"); // Просим пользователя ввести число
            int num = calculation.isCorrectNum(); // Пользователь вводит число и оно проверяется на корректность, сохраняется в переменную
            calculation.Calculate(num); // Производится разделение на пары и вычисление
            Console.ReadLine();
        }
    }
    public class Sqrt
    {
        private List<char> result; // Список для хранения цифр результата
        private List<string> fullNum; // Список для хранения пар

        public void Calculate(int number)
        {
            result = new List<char>(); // Список инициализируется

            if (number == 0 || number == 1) // Мгновенный результат для 0 и 1
            {
                Console.WriteLine($"Корень: {number}");
                return;
            }

            fullNum = new List<string>(); // Список инициализируется
            string stringNum = number.ToString(); // Число пользователя преобразуется в строку
            for (int i = stringNum.Length; i > 0; i -= 2) // Число разбивается на пары с конца, пары добавляются в список, если есть число без пары добавляется 0
            {
                int start = Math.Max(0, i - 2);
                fullNum.Add(stringNum.Substring(start, i - start));
            }
            fullNum.Reverse();

            RecursiveSqrt(fullNum[0], "", "");  // Запускается вычисление
            Console.Write($"Корень числа {number}: ");
            Console.WriteLine(string.Join("", result));
        }


        public void RecursiveSqrt(string fullStr, string remainder, string n, int precision = 0)
        {
            if (string.IsNullOrEmpty(fullStr)) return; // Если пары нет то метод завершается

            string workStr = remainder + fullStr; // Формируется число из остатка и текущей пары
            string left = !string.IsNullOrEmpty(n) ? (int.Parse(n) * 2).ToString() : ""; // Если в результате уже что-то есть то удваиваем
            int temp = 0;

            for (int i = 0; i <= 10; i++) // Перебор цифр чтобы найти максимально подходящий множитель
            {
                if (int.Parse(left + i) * i > int.Parse(workStr)) // Если выражение больше текущего числа, выбирается предыдущая цифра
                {
                    int digit = i - 1;
                    temp = int.Parse(left + digit) * digit;
                    remainder = (int.Parse(workStr) - temp).ToString();
                    result.Add(digit.ToString()[0]);
                    n += digit;
                    break;
                }
            }
            for (int i = 0; i < fullNum.Count; i++)
            {
                if (i == fullNum.Count - 1 && remainder == "0") // Если это последняя пара без остатка то всё просто завершается
                {
                    fullStr = "";
                    break;
                }
                if (i == fullNum.Count - 1 && fullNum[i] == fullStr && remainder != "0") // Если всё же есть остаток, просим ввести точность
                {
                    Console.WriteLine("Введите число разрядов, до которых требуется округлить число");
                    precision = isCorrectNum();
                    fullStr = "00";
                    precision -= 1;
                    fullNum[i] = "";
                    result.Add(',');
                    break;
                }

                if (fullNum[i] == fullStr && i < fullNum.Count - 1) // Если пара не последняя, то переходим к следующей и удаляем эту из списка
                {
                    fullStr = fullNum[i + 1];
                    fullNum.RemoveAt(i);
                    break;
                }
                if (i == fullNum.Count - 1 && precision == 0) // Если последняя, но точность не нужна, то просто заканчиваем
                {
                    fullStr = "";
                    break;
                }
                if (i == fullNum.Count - 1 && precision > 0) // Если последняя и нужна точность
                {
                    fullStr = "00";
                    precision -= 1;
                    break;
                }

            }
            RecursiveSqrt(fullStr, remainder, n, precision);
        }

        public int isCorrectNum()
        {
            while (true)
            {
                try
                {
                    int num = int.Parse(Console.ReadLine());
                    if (num >= 0) return num;
                    Console.WriteLine("Введите неотрицательное число");
                }
                catch
                {
                    Console.WriteLine("Введите число");
                }
            }
        }
    }
}