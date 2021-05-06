using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace TesteIntegracao
{
    public class UserDto
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly string _connectionString;
        private readonly SqlConnection _sqlConnection;

        public UserController()
        {
            _connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION");
            _sqlConnection = new SqlConnection(_connectionString);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _sqlConnection.QueryAsync<User>(
                @"SELECT
                    Nome as Nome,
                    Email as Email,
                    Senha as Senha
                FROM usuarios"
            );

            return Ok(users);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@email", email);

            var user = await GetUserByEmailFunc(email, cancellationToken);

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto user, CancellationToken cancellationToken)
        {
            try
            {
                var userResult = await GetUserByEmailFunc(user.Email, cancellationToken);

                if (userResult != null)
                {
                    throw new Exception("Usuario já existe");
                }

                var parameters = new DynamicParameters();
                parameters.Add("@email", user.Email);
                parameters.Add("@name", user.Nome);
                parameters.Add("@password", user.Senha);

                await _sqlConnection.ExecuteAsync("INSERT INTO usuarios (Nome, Email, Senha) VALUES (@name, @email, @password);", parameters);

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromQuery] string email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@email", email);

            await _sqlConnection.ExecuteAsync("DELETE FROM usuarios WHERE email = @email", parameters);

            return Ok();
        }

        private Task<User> GetUserByEmailFunc(string email, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@email", email);

            return _sqlConnection.QueryFirstOrDefaultAsync<User>(
                new CommandDefinition(
                    @"SELECT 
                        Nome as Nome,
                        Email as Email,
                        Senha as Senha
                    FROM usuarios 
                    WHERE email = @email",
                    parameters,
                    cancellationToken: cancellationToken
                )
            );
        }
    }
}
