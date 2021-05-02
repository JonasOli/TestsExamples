using System;

namespace ExemplosKT.ExemploConta
{
    public class SaldoInsuficienteException : Exception
    {
        public SaldoInsuficienteException()
            : base("Valor do saque é maior que o saldo")
        { }
    }
}
