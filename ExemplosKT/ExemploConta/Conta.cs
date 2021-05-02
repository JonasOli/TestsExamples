namespace ExemplosKT.ExemploConta
{
    public class Conta
    {
        public int Id { get; set; }
        public double Saldo { get; set; }

        public void Sacar(double valor)
        {
            if (valor > Saldo)
            {
                throw new SaldoInsuficienteException();
            }

            Saldo -= valor;
        }
    }
}
