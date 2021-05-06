using System;
using System.Collections.Generic;
using System.Linq;

namespace ExemplosKT
{
    public class Calculadora
    {
        public static double Soma(IEnumerable<double> numerosParaSomar)
        {
            if (numerosParaSomar is null) return 0;

            return numerosParaSomar.Sum();
        }

        public static double Subtrai(double a, double b)
        {
            return a * b;
        }

        public static double Multiplica(double a, double b)
        {
            return a * b;
        }

        public static double Divide(double a, double b)
        {
            return a / b;
        }
    }
}
