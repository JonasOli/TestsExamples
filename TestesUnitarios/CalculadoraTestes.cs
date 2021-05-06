using ExemplosKT;
using System.Collections.Generic;
using Xunit;

namespace TestesUnitarios
{
    public class CalculadoraTestes
    {
        [Theory]
        [InlineData(new double[] { 1, 2, 3 }, 6)]
        [InlineData(new double[] { double.PositiveInfinity, double.PositiveInfinity }, double.PositiveInfinity)]
        [InlineData(new double[] { double.NegativeInfinity, double.NegativeInfinity }, double.NegativeInfinity)]
        [InlineData(new double[] { }, 0)]
        [InlineData(null, 0)]
        public void Soma_DeveCalcularCorratamenteASomaDeNumeros(IEnumerable<double> numerosParaSomar, double resultadoEsperado)
        {
            // Act
            var resultado = Calculadora.Soma(numerosParaSomar);

            // Assert
            Assert.Equal(resultadoEsperado, resultado);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(5, 120)]
        [InlineData(20, 2_432_902_008_176_640_000)]
        public void Fatorial_DeveCalcularCorretamenteOFatorialDeUmNumero(int numeroParaCalcular, long resultadoEsperado)
        {
            // Act
            long resultado = Calculadora.Fatorial(numeroParaCalcular);

            // Assert
            Assert.Equal(resultadoEsperado, resultado);
        }

        [Fact]
        public void Fatorial_DeveLancarUmaExecacaoCasoNumeroSejaMenorQueZero()
        {
            // Arrange
            int numeroNegativo = -10;

            // Act & Assert
            Assert.Throws<FatorialNegativoException>(() => Calculadora.Fatorial(numeroNegativo));
        }
    }
}
