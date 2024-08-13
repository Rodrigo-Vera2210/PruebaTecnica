using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.BLL.Services.Contrato;
using PruebaTecnica.DAL.Repositories.Contrato;
using PruebaTecnica.Model;
using PruenaTecnica.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.BLL.Services
{
    public class DatoSensorService : IDatoSensorService
    {
        //Creacion de variables de los repositorios y Mapper
        private readonly IGenericRepository<DatoSensor> _datoSensorRepository;
        private readonly IGenericRepository<ParametroSensor> _parametroSensorRepository;
        private readonly IMapper _mapper;

        public DatoSensorService(IGenericRepository<ParametroSensor> parametroSensorRepository, IGenericRepository<DatoSensor> datoSensorRepository, IMapper mapper)
        {
            _datoSensorRepository = datoSensorRepository;
            _parametroSensorRepository = parametroSensorRepository;
            _mapper = mapper;
        }

        //Metodo para buscar los datos max, min y promedio por cada dia o mes dependiendo del modo de busqueda
        public async Task<List<ParametroSensorDTO>> Busqueda(string modo, DateTime fechaDesde, DateTime fechaHasta)
        {
            
            IQueryable<DatoSensor> query = await _datoSensorRepository.Consultar(p => p.FechaDato >= fechaDesde.AddHours(-5).ToUniversalTime() && p.FechaDato <= fechaHasta.AddHours(-5).ToUniversalTime()); // Busqueda general desde la fecha de inicio hasta la fecha final
            
            var listaParametros = new List<ParametroSensorDTO>(); //lista donde se acumulan los valores de max, min y promedio por cada parametro
            try
            {
                /*
                 Condicion para el metodo de busqueda por dia
                 Primero busca los parametros medidos en el dia y contruye un modelo donde se almacenara los datos max, min y promedio y los guarda en listaParametros
                 Despues entra en bucle hasta que el dia de busqueda sea mayor que el de la fecha final buscada, y se incrementa en cada interacion 1 dia
                 Dentro del bucle busca los valores max, min y promedio por dia y los acumula dentro del parametro realizando una comprobacion por medio del Id de los parametros
                 */
                if (modo == "day") {
                    
                    var parametrosId = query.GroupBy(c => c.ParametroSensoresId).Select(c => c.Key).ToList();

                    foreach (var item in parametrosId) listaParametros.Add(_mapper.Map<ParametroSensorDTO>(await _parametroSensorRepository.Obtener(p => p.Id == item)));

                    var fecha = fechaDesde; 
                    while (fecha < fechaHasta) {
                        var queryDia = query.Where(p => p.FechaDato >= fecha.AddHours(-5).ToUniversalTime() && p.FechaDato <= fecha.AddDays(1).AddHours(-5).ToUniversalTime());
                        var lista = queryDia.GroupBy(c => c.ParametroSensoresId)
                            .Select(c => new {
                                codigo = c.Key,
                                maximo = c.Select(d => d.ValorNumero).Max(),
                                minimo = c.Select(d => d.ValorNumero).Min(),
                                promedio = decimal.Round(c.Select(d => d.ValorNumero).Average(), 2)
                            }).ToList();

                        foreach (var item in lista)
                        {
                            foreach (var item1 in listaParametros)
                            {
                                if (item.codigo == item1.Id)
                                {
                                    item1.Values.Max_Data.Add(item.maximo);
                                    item1.Values.Min_Data.Add(item.minimo);
                                    item1.Values.Avg_Data.Add(item.promedio);
                                }
                            }
                        }
                        fecha = fecha.AddDays(1);
                    }
                }
                /*
                 Condicion para el metodo de busqueda por mes
                 Primero busca los parametros medidos en el mes y contruye un modelo donde se almacenara los datos max, min y promedio y los guarda en listaParametros
                 Despues entra en bucle hasta que el dia de busqueda sea mayor que el de la fecha final buscada, y se incrementa en cada interacion 1 mes
                 Dentro del bucle busca los valores max, min y promedio por dia y los acumula dentro del parametro realizando una comprobacion por medio del Id de los parametros
                 */
                else if (modo == "month")
                {
                    var parametrosId = query.GroupBy(c => c.ParametroSensoresId).Select(c => c.Key).ToList();

                    foreach (var item in parametrosId) listaParametros.Add(_mapper.Map<ParametroSensorDTO>(await _parametroSensorRepository.Obtener(p => p.Id == item)));

                    var fecha = new DateTime(fechaDesde.Year,fechaDesde.Month,1);
                    while (fecha < fechaHasta)
                    {
                        var queryMonth = query.Where(p => p.FechaDato >= fecha.AddHours(-5).ToUniversalTime() && p.FechaDato.Date <= fecha.AddMonths(1).AddDays(-1).ToUniversalTime());
                        var lista = queryMonth.GroupBy(c => c.ParametroSensoresId)
                            .Select(c => new {
                                codigo = c.Key,
                                maximo = c.Select(d => d.ValorNumero).Max(),
                                minimo = c.Select(d => d.ValorNumero).Min(),
                                promedio = decimal.Round(c.Select(d => d.ValorNumero).Average(), 2)
                            }).ToList();

                        foreach (var item in lista)
                        {
                            foreach (var item1 in listaParametros)
                            {
                                if (item.codigo == item1.Id)
                                {
                                    item1.Values.Max_Data.Add(item.maximo);
                                    item1.Values.Min_Data.Add(item.minimo);
                                    item1.Values.Avg_Data.Add(item.promedio);
                                }
                            }
                        }
                        fecha = fecha.AddMonths(1);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return listaParametros;
        }

        // Metodo para la transformar las fechas en string para una mejor visualizacion
        public async Task<List<string>> ObtenerFecha(string modo, DateTime fechaDesde, DateTime fechaHasta)
        {
            List<string> fechas = new List<string>(); // array donde se acumulan las fechas
            /*
            En un bluque hasta que la fecha buscada sea mayor que la fecha final, transforma la fecha en el formato string asignado
            */
            if (modo == "day"){
                var fecha = fechaDesde;
                while (fecha < fechaHasta)
                {
                    var Mes = fecha.Month < 10 ? "0" + fecha.Month.ToString() : fecha.Month.ToString();
                    var Dia = fecha.Day < 10 ? "0" + fecha.Day.ToString() : fecha.Day.ToString();
                    fechas.Add($"{fecha.Year}-{Mes}-{Dia}");
                    fecha = fecha.AddDays(1);
                }
            }
            /*
            En un bluque hasta que la fecha buscada sea mayor que la fecha final, busca el rango de fechas del mes en la interacion
            */
            else if (modo == "month")
            {
                var fecha = fechaDesde;
                while (fecha < fechaHasta)
                {
                    var Mes = fecha.Month < 10 ? "0" + fecha.Month.ToString() : fecha.Month.ToString();
                    if(fecha.Month == fechaDesde.Month)
                    {
                        fecha = new DateTime(fecha.Year, fecha.Month, 1);
                        fechas.Add($"{fecha.Year}-{Mes}-{fechaDesde.Day} -- {fecha.Year}-{Mes}-{fecha.AddMonths(1).AddDays(-1).Day}");
                    }
                    else if(fecha.Month == fechaHasta.Month){
                        var Dia = fechaHasta.Day < 10 ? "0" + fechaHasta.Day.ToString() : fechaHasta.Day.ToString();
                        fechas.Add($"{fecha.Year}-{Mes}-01 -- {fecha.Year}-{Mes}-{Dia}");
                    }
                    else
                    {
                        fechas.Add($"{fecha.Year}-{Mes}-01 -- {fecha.Year}-{Mes}-{fecha.AddMonths(1).AddDays(-1).Day}");
                    }
                    fecha = fecha.AddMonths(1);
                }
            }
            return fechas;
        }
    }
}
