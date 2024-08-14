using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PruebaTecnica.View.Models
{
    public class DatosBusqueda : PageModel
    {
        public DateTime FechaInicio {  get; set; }
        public DateTime FechaFinal {  get; set; }
        public string Modo {  get; set; } = null!;

        public Response Response { get; set; }
    }
}
