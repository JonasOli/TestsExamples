using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExemplosKT.ExemploConta
{
    public class ContaBancariaService
    {
        private readonly IContaBancariaRepository _contaBancariaRepository;

        public ContaBancariaService(IContaBancariaRepository contaBancariaRepository)
        {
            _contaBancariaRepository = contaBancariaRepository;
        }

        public async Task EfetuarSaque(int idConta, double valorSaque)
        {
            var conta = await _contaBancariaRepository.ObterContaPorId(idConta);

            conta.Sacar(valorSaque);

            await _contaBancariaRepository.SalvarConta(conta);
        }

        public Task<IEnumerable<Conta>> ObterContas()
        {
            return _contaBancariaRepository.ObterContas();
        }
    }
}
