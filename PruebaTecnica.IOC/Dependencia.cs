using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PruebaTecnica.BLL.Services;
using PruebaTecnica.BLL.Services.Contrato;
using PruebaTecnica.DAL.Repositories;
using PruebaTecnica.DAL.Repositories.Contrato;
using PruebaTecnica.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencia(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IDatoSensorService, DatoSensorService>();
        }
    }
}
