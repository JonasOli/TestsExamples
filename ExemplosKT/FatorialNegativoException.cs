using System;

namespace ExemplosKT
{
    public class FatorialNegativoException : Exception
    {
        public static string message = "Numero deve ser maior que zero";

        public FatorialNegativoException() : base(message)
        { }
    }
}
