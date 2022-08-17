using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestXUnitInDotNet
{
    public static class Mathematics
    {
        public static bool IsEvenNumber(int num)
        {
            return num % 2 == 0 ? true : false;
        }
        public static bool IsOddNumber(int num)
        {
            return num % 2 == 1 ? true : false;
        }
        public static int SquareOfNumber(int num)
        {
            int sum = 0;
            for (int count = 1; count <= num; count++)
            {
                sum = count * count;
            }
            return sum;
        }
        public static double Addition(double num1, double num2)
        {
            return (num1 + num2);
        }
        public static double Subtraction(double num1, double num2)
        {
            return (num1 - num2);
        }
        public static double Multiplication(double num1, double num2)
        {
            return (num1 * num2);
        }
        public static double Division(double num1, double num2)
        {
            return (num1 / num2);
        }
    }
}
