using APIUsuarios;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TestesIntegracao
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Conexao do banco de dados
            Environment.SetEnvironmentVariable("DATABASE_CONNECTION", "Server=localhost,1433;Database=testesIntegracao;User Id=SA;Password=Senh@1234;");

            // Fazer rotas permitirem usuario anonimo
            builder.ConfigureServices(services =>
            {
                services.AddMvcCore(options => options.Filters.Add(new AllowAnonymousFilter()));
            });
        }
    }
}
