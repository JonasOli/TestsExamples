namespace TesteIntegracao
{
    public class User
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public User(string nome, string email, string senha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
        }
    }
}
