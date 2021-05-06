using ExemplosKT.ExemploConta;
using System;
using System.Threading.Tasks;

namespace ExemplosKT
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var contaService = new ContaBancariaService(new ContaBancariaRepository());

                await contaService.EfetuarSaque(1, 1000);
                
                Console.WriteLine("Saque efetuado com sucesso");
            }
            catch (SaldoInsuficienteException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
