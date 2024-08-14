using PruebaTecnica.View.Models;

namespace PruebaTecnica.View.Services
{
    public interface IServiceAPI
    {
        Task<Response> ObtenerParametrosSensor(string modo, DateTime fechaInicio, DateTime fechaFinal); 
    }
}
