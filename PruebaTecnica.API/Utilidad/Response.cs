using PruenaTecnica.DTO;

namespace PruebaTecnica.API.Utilidad
{
    public class Response
    {
        public List<string>? device_dates { get; set; }
        public List<ParametroSensorDTO>? device_data { get; set; }
    }
}
