using Newtonsoft.Json;
using PruebaTecnica.View.Models;

namespace PruebaTecnica.View.Services
{
    public class ServiceAPI : IServiceAPI
    {
        private readonly string _urlBase;

        public ServiceAPI()
        {
             var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _urlBase = builder.GetSection("ApiSettings:urlBase").Value;
        }

        public async Task<Response> ObtenerParametrosSensor(string modo, DateTime fechaInicio, DateTime fechaFinal)
        {
            var cliente = new HttpClient();
            Response resultado = null;
            cliente.BaseAddress = new Uri(_urlBase);



            var response = await cliente.GetAsync($"api/meteo/{modo}/{fechaInicio.Year}-{fechaInicio.Month}-{fechaInicio.Day}/{fechaFinal.Year}-{fechaFinal.Month}-{fechaFinal.Day}");

            if(response.IsSuccessStatusCode) 
            {
                var json_response = await response.Content.ReadAsStringAsync();

                resultado = JsonConvert.DeserializeObject<Response>(json_response);
            }

            return resultado;

            
        }
    }
}
