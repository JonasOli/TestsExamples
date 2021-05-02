using System;
using System.Collections.Generic;
using System.Linq;

namespace ExemplosKT
{
    public class Calculadora
    {
        public double Soma(IEnumerable<double> numerosParaSomar)
        {
            if (numerosParaSomar is null || !numerosParaSomar.Any()) return 0;
            
            return numerosParaSomar.Aggregate(0.0, (acc, numero) => acc += numero);
        }

        public long Fatorial(int numero)
        {
            if (numero < 0)
            {
                throw new FatorialNegativoException();
            }

            if (numero is 0 || numero is 1)
            {
                return 1;
            }

            return numero * Fatorial(numero - 1);
        }
    }
}
