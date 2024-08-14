using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.API.Controllers;
using PruebaTecnica.API.Utilidad;
using PruebaTecnica.BLL.Services;
using PruebaTecnica.BLL.Services.Contrato;
using PruebaTecnica.DAL.Repositories.Contrato;
using PruebaTecnica.Model;

namespace PruebaTecnica.Test
{
    public class MeteoControllerTest
    {
        MeteoController _controller;
        IDatoSensorService _service;
        private readonly IGenericRepository<DatoSensor> _datoSensorRepository;
        private readonly IGenericRepository<ParametroSensor> _parametroSensorRepository;
        private readonly IMapper _mapper;
        public MeteoControllerTest()
        {
            _service = new DatoSensorService(_parametroSensorRepository, _datoSensorRepository, _mapper);
            _controller = new MeteoController(_service);
        }

        [Fact]
        public void ConsultarTest()
        {
            var result = _controller.Consultar("day", "2024-05-10", "2024-06-10");

            Assert.IsType<Task<IActionResult>>(result);

            var contenido = result.Result as Task<IActionResult>;

            Assert.Null(contenido);
        }
    }
}