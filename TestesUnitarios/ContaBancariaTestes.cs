using ExemplosKT.ExemploConta;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestesUnitarios
{
    public class ContaBancariaTestes
    {
        private readonly Mock<IContaBancariaRepository> _contaBancariaRepositoryMock;
        private readonly ContaBancariaService _contaBancariaService;

        public ContaBancariaTestes()
        {
            _contaBancariaRepositoryMock = new Mock<IContaBancariaRepository>();

            _contaBancariaService = new ContaBancariaService(_contaBancariaRepositoryMock.Object);
        }

        [Fact]
        public async Task EfetuarSaque_DeveLancarUmaExcecaoCasoValorDoSaqueSejaMaiorQueSaldo()
        {
            // Arrange
            _contaBancariaRepositoryMock
                .Setup(c => c.ObterContaPorId(It.IsAny<int>()))
                .ReturnsAsync(new Conta { Id = 1, Saldo = 500 });

            var idConta = 1;
            var valorSaque = 1000;

            // Assert
            await Assert.ThrowsAsync<SaldoInsuficienteException>(() => _contaBancariaService.EfetuarSaque(idConta, valorSaque));
        }
    }
}
