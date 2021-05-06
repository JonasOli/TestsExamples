using System;

namespace ExemplosKT.ExemploConta
{
    public class SaldoInsuficienteException : Exception
    {
        public static string message = "Valor do saque é maior que o saldo";

        public SaldoInsuficienteException()
            : base()
        { }
    }
}
