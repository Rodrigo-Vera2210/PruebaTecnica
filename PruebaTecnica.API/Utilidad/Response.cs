namespace PruebaTecnica.API.Utilidad
{
    public class Response<T>
    {
        public List<string> device_dates { get; set; }
        public List<T> device_data { get; set; }
    }
}
