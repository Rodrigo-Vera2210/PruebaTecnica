using PruebaTecnica.View.Models;

namespace PruebaTecnica.View.Models
{
    public class Response
    {
        public List<string>? device_dates { get; set; }
        public List<ParametroSensor>? device_data { get; set; }
    }
}
