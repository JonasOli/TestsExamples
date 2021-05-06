using Dapper;
using Respawn;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace TestesIntegracao
{
    [CollectionDefinition("TestesIntegracao", DisableParallelization = true)]
    [Collection("TestesIntegracao")]
    public class TesteIntegracaoBase : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly string _connectionString;
        protected readonly ApiWebApplicationFactory _apiWebApplicationFactory;
        protected readonly HttpClient _httpClient;
        private readonly Checkpoint _checkpoint = new Checkpoint
        {
            // Tabelas para serem limpas
            SchemasToExclude = new[]
            {
                "usuarios"
            }
        };

        public TesteIntegracaoBase(ApiWebApplicationFactory webApplicationFactory)
        {
            _apiWebApplicationFactory = webApplicationFactory;
            _httpClient = _apiWebApplicationFactory.CreateClient();
            _connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION");
        }

        // Funcao para limpar certas tabelas no banco
        protected async Task ExecResetBanco()
        {
            await _checkpoint.Reset(_connectionString);
        }

        // Funcao para executar uma query que nao tenha uma retorno
        protected async Task ExecutarSql(string sql)
        {
            using SqlConnection conexao = new SqlConnection(_connectionString);

            await conexao.ExecuteAsync(sql);
        }

        /// <summary>
        /// Funcao que recebe uma query se busca e retorna o primeiro objeto encontrado
        /// </summary>
        /// <param name="sql">Query para buscar no banco</param>
        protected async Task<T> ExecObter<T>(string sql)
        {
            using var conexao = new SqlConnection(_connectionString);

            return await conexao.QueryFirstOrDefaultAsync<T>(sql);
        }

        // Funcao para ler um arquivo sql e retornar a query em texto
        protected string LerArquivo(string scriptPath)
        {
            string diretorio = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            return File.ReadAllText(diretorio + $@"/{scriptPath}", System.Text.Encoding.UTF8);
        }

    }
}
