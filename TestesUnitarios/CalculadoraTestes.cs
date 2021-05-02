using ExemplosKT;
using System;
using Xunit;

namespace TestesUnitarios
{
    public class CalculadoraTestes
    {
        private readonly Calculadora _calculadora;

        public CalculadoraTestes()
        {
            _calculadora = new Calculadora();
        }

        [Theory]
        [InlineData(new double[] { 1, 2 }, 3)]
        [InlineData(new double[] { double.PositiveInfinity, double.PositiveInfinity }, double.PositiveInfinity)]
        [InlineData(new double[] { double.NegativeInfinity, double.NegativeInfinity }, double.NegativeInfinity)]
        [InlineData(new double[] { }, 0)]
        [InlineData(null, 0)]
        public void Soma_DeveSomarNumerosCorretamente(double[] numerosParaSomar, double resultadoEsperado)
        {
            // Act
            var resultado = _calculadora.Soma(numerosParaSomar);

            // Assert
            Assert.Equal(resultadoEsperado, resultado);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(5, 120)]
        [InlineData(20, 2_432_902_008_176_640_000)]
        public void Fatorial_DeveRetornarCorretamenteOFatorialDeUmNumero(int numero, long resuldadoEsperado)
        {
            // Act
            long resultado = _calculadora.Fatorial(numero);

            // Assert
            Assert.Equal(resuldadoEsperado, resultado);
        }

        [Fact]
        public void Fatorial_DeveLancarUmaExcessaoCasoNumeroSejaMenorQueZero()
        {
            var numeroNegativo = -10;

            var excecao = Assert.ThrowsAny<FatorialNegativoException>(() => _calculadora.Fatorial(numeroNegativo));
        }
    }
}
