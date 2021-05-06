using ExemplosKT.ExemploConta;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestesUnitarios
{
    public class ContaBancariaTestes
    {
        private readonly Mock<IContaBancariaRepository> contaBancariaRepositoryMock;
        private readonly ContaBancariaService contaBancariaService;

        public ContaBancariaTestes()
        {
            contaBancariaRepositoryMock = new Mock<IContaBancariaRepository>();
            contaBancariaService = new ContaBancariaService(contaBancariaRepositoryMock.Object);
        }

        [Fact]
        public async Task EfetuarSaque_DeveLancarUmaExcecaoCasoOSaldoSejaInsuficiente()
        {
            // Arrange
            contaBancariaRepositoryMock
                .Setup(c => c.ObterContaPorId(It.IsAny<int>()))
                .ReturnsAsync(new Conta { Id = 1, Saldo = 200 });

            var idConta = 1;
            var valorSaque = 500;

            // Act & Assert
            await Assert.ThrowsAsync<SaldoInsuficienteException>(() => contaBancariaService.EfetuarSaque(idConta, valorSaque));
        }

        // Ma pratica no uso de mocks. Sempre retorna um caso feliz
        [Fact]
        public async Task ObterContas_DeveRetornarTodasAsContasCadastradas()
        {
            var conta1 = new Conta { Id = 1, Saldo = 100 };
            var conta2 = new Conta { Id = 2, Saldo = 1000 };
            var conta3 = new Conta { Id = 3, Saldo = 10000 };

            contaBancariaRepositoryMock
                .Setup(c => c.ObterContas())
                .ReturnsAsync(new List<Conta> { conta1, conta2, conta3 });

            var contas = await contaBancariaService.ObterContas();

            Assert.Contains(conta1, contas);
            Assert.Contains(conta2, contas);
            Assert.Contains(conta3, contas);
        }
    }
}
