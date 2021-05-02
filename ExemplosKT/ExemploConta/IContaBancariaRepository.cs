using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExemplosKT.ExemploConta
{
    public interface IContaBancariaRepository
    {
        Task<Conta> ObterValorSaldoPorIdConta(int idConta);
        Task SalvarConta(Conta conta);
        Task<IEnumerable<Conta>> ObterContas();
    }
}
