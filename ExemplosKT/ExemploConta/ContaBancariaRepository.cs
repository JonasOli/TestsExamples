using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExemplosKT.ExemploConta
{
    public class ContaBancariaRepository : IContaBancariaRepository
    {
        private readonly string _filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "/ExemploConta/contas.json";

        public async Task<Conta> ObterContaPorId(int idConta)
        {
            var contas = await ObterContas();

            return contas.FirstOrDefault(conta => conta.Id == idConta);
        }

        public async Task SalvarConta(Conta conta)
        {
            var contas = (await ObterContas()).ToList();

            contas = contas.Select(c =>
            {
                if (c.Id == conta.Id)
                {
                    c = conta;
                }

                return c;
            }).ToList();

            await File.WriteAllTextAsync(_filePath, JsonConvert.SerializeObject(contas));
        }

        public async Task<IEnumerable<Conta>> ObterContas()
        {
            //using var fileReader = new StreamReader(_filePath);

            //var jsonString = await fileReader.ReadToEndAsync();

            //return JsonConvert.DeserializeObject<IEnumerable<Conta>>(jsonString);

            throw new System.Exception();
        }
    }
}
