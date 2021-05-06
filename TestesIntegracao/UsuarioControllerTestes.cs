using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TesteIntegracao;
using Xunit;

namespace TestesIntegracao
{
    public class UsuarioControllerTestes : TesteIntegracaoBase, IAsyncLifetime
    {
        private const string rota = "/api/user";
        private const string rotaArquivo = "criarUsuario.sql";

        public UsuarioControllerTestes(ApiWebApplicationFactory webApplicationFactory)
            : base(webApplicationFactory)
        { }

        public async Task InitializeAsync()
        {
            await ExecResetBanco();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        [Fact]
        public async Task GetUsers_DeveListarUsuarioCadastrados()
        {
            // Arrange
            await ExecutarSql(LerArquivo(rotaArquivo));

            // Act
            var result = await _httpClient.GetAsync($"{rota}/list");
            var usuarios = await result.Content.ReadFromJsonAsync<IEnumerable<User>>();

            // Assert
            Assert.Equal("Usuario Teste", usuarios.FirstOrDefault().Nome);
            Assert.Equal("teste@teste.com", usuarios.FirstOrDefault().Email);
            Assert.Equal("1234", usuarios.FirstOrDefault().Senha);
            Assert.Equal("Usuario Teste 2", usuarios.LastOrDefault().Nome);
            Assert.Equal("teste2@teste2.com", usuarios.LastOrDefault().Email);
            Assert.Equal("12345", usuarios.LastOrDefault().Senha);
        }

        [Fact]
        public async Task CreateUser_DeveCriarUmUsuario()
        {
            // Arrange
            var userDto = new StringContent(
                JsonConvert.SerializeObject(new UserDto { Nome = "Usuario teste", Email = "teste@teste.com", Senha = "1234" }),
                Encoding.UTF8,
                "application/json"
            );

            // Act
            var result = await _httpClient.PostAsync(rota, userDto);

            var usuario = await ExecObter<User>(@"
                SELECT
                    Nome as Nome,
                    Email as Email
                    Senha as Senha
                FROM usuarios
                where email = 'teste@teste.com'");

            // Assert
            Assert.True(result.IsSuccessStatusCode);
            Assert.Equal("Usuario teste", usuario.Nome);
            Assert.Equal("teste@teste.com", usuario.Email);
            Assert.Equal("1234", usuario.Senha);
        }
    }
}
