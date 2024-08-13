using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.API.Utilidad;
using PruebaTecnica.BLL.Services.Contrato;
using PruenaTecnica.DTO;
using System.Globalization;
using System.Resources;

namespace PruebaTecnica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeteoController : ControllerBase
    {
        private readonly IDatoSensorService _sensorService;

        public MeteoController(IDatoSensorService sensorService)
        {
            _sensorService = sensorService;
        }

        [HttpGet]
        [Route("{modo}/{fechaDesde}/{fechaHasta}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Consultar([FromRoute] string modo,[FromRoute] string fechaDesde,[FromRoute] string fechaHasta)
        {
            var rsp = new Response<ParametroSensorDTO>();
            try
            {
                //Transforma el formato ingresado al reconocible con el sistema
                var fechaI = DateTime.ParseExact(fechaDesde + " 0:00", "yyyy-M-d H:m", null);
                var fechaF = DateTime.ParseExact(fechaHasta + " 23:59", "yyyy-M-d H:m", null);

                if (fechaF > DateTime.Now || fechaI > fechaF) return BadRequest(rsp);
                
                rsp.device_data = await _sensorService.Busqueda(modo, fechaI, fechaF); // Busca los valores de los dispositivos

                if (rsp.device_data.Count == 0) return NotFound("No existen datos en ese rango de fechas"); 
                
                rsp.device_dates = await _sensorService.ObtenerFecha(modo, fechaI, fechaF); // Busca los valores de las fechas
                return Ok(rsp);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
