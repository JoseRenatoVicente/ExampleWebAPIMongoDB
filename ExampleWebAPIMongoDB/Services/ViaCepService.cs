using ExampleWebAPIMongoDB.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ExampleWebAPIMongoDB.Services
{
    public class ViaCepService
    {
        public async Task<Address> ConsultarCEP(string cep)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://viacep.com.br/");
                HttpResponseMessage response = await client.GetAsync($"ws/{cep}/json/");

                if (response.IsSuccessStatusCode)
                {
                    ViaCep endereco = await response.Content.ReadFromJsonAsync<ViaCep>();

                    return new Address(endereco.bairro, endereco.localidade, endereco.cep, endereco.logradouro, endereco.uf);
                }
                return null;
            }
        }
    }
}
