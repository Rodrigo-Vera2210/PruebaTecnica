using PruenaTecnica.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.BLL.Services.Contrato
{
    public interface IDatoSensorService
    {
        Task<List<ParametroSensorDTO>> Busqueda(string modo, DateTime fechaDesde, DateTime fechaHasta);
        List<string> ObtenerFecha(string modo, DateTime fechaDesde, DateTime fechaHasta);
    }
}
