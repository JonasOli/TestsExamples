using ExemplosKT.ExemploConta;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestesUnitarios
{
    public class ContaBancariaTestes
    {
        private readonly Mock<IContaBancariaRepository> _contaBancariaRepositoryMock = new Mock<IContaBancariaRepository>();
        private readonly ContaBancariaService _contaBancariaService;

        public ContaBancariaTestes()
        {
            _contaBancariaService = new ContaBancariaService(_contaBancariaRepositoryMock.Object);
        }

        [Fact]
        public async Task EfetuarSaque_DeveLancarUmasExcecaoCasoValorSejaMaiorQueSaldo()
        {
            _contaBancariaRepositoryMock
                .Setup(c => c.ObterValorSaldoPorIdConta(It.IsAny<int>()))
                .ReturnsAsync(new Conta { Id = 1, Saldo = 100 });

            var idConta = 1;
            var valorSaque = 200;

            var excecao = await Assert.ThrowsAnyAsync<SaldoInsuficienteException>(async () => await _contaBancariaService.EfetuarSaque(idConta, valorSaque));
        }

        [Fact]
        public async Task ObterContas_DeveRetornarTodasAsContasCadastradas()
        {
            var conta1 = new Conta { Id = 1, Saldo = 100 };
            var conta2 = new Conta { Id = 2, Saldo = 1000 };
            var conta3 = new Conta { Id = 3, Saldo = 10000 };

            _contaBancariaRepositoryMock
                .Setup(c => c.ObterContas())
                .ReturnsAsync(new List<Conta> { conta1, conta2, conta3 });

            var contas = await _contaBancariaService.ObterContas();

            Assert.Contains(conta1, contas);
            Assert.Contains(conta2, contas);
            Assert.Contains(conta3, contas);
        }
    }
}
