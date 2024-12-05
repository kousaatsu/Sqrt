using System;
using System.Collections.Generic;

namespace Sqrt
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var calculation = new Sqrt();

            Console.WriteLine("Введите число, из которого нужно вычислить корень");
            int num = calculation.isCorrectNum();
            calculation.Calculate(num);
            Console.ReadLine();
        }
    }
    public class Sqrt
    {
        private List<char> result;
        private List<string> fullNum;

        public void Calculate(int number)
        {
            result = new List<char>();

            if (number == 0 || number == 1)
            {
                Console.WriteLine($"Корень: {number}");
                return;
            }

            fullNum = new List<string>();
            string stringNum = number.ToString();
            for (int i = stringNum.Length; i > 0; i -= 2)
            {
                int start = Math.Max(0, i - 2);
                fullNum.Add(stringNum.Substring(start, i - start));
            }
            fullNum.Reverse();

            RecursiveSqrt(fullNum[0], "", "");
            Console.Write($"Корень числа {number}: ");
            Console.WriteLine(string.Join("", result));
        }


        public void RecursiveSqrt(string fullStr, string remainder, string n, int precision = 0)
        {
            if (string.IsNullOrEmpty(fullStr)) return;

            string workStr = remainder + fullStr;
            string left = !string.IsNullOrEmpty(n) ? (int.Parse(n) * 2).ToString() : "";
            int temp = 0;

            for (int i = 0; i <= 10; i++)
            {
                if (int.Parse(left + i) * i > int.Parse(workStr))
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
                if (i == fullNum.Count - 1 && remainder == "0")
                {
                    fullStr = "";
                    break;
                }
                if (i == fullNum.Count - 1 && fullNum[i] == fullStr && remainder != "0")
                {
                    Console.WriteLine("Введите число разрядов, до которых требуется округлить число");
                    precision = isCorrectNum();
                    fullStr = "00";
                    precision -= 1;
                    fullNum[i] = "";
                    result.Add(',');
                    break;
                }

                if (fullNum[i] == fullStr && i < fullNum.Count - 1)
                {
                    fullStr = fullNum[i + 1];
                    fullNum.RemoveAt(i);
                    break;
                }
                if (i == fullNum.Count - 1 && precision == 0)
                {

                    fullStr = "";
                    break;
                }
                if (i == fullNum.Count - 1 && precision > 0)
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