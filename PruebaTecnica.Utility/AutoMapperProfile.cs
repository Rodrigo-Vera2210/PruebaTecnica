using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PruebaTecnica.Model;
using PruenaTecnica.DTO;

namespace PruebaTecnica.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            #region DatosSensor
            CreateMap<DatoSensor, DatoSensorDTO>().ReverseMap();
            #endregion

            #region ParametroSensor
            CreateMap<ParametroSensor, ParametroSensorDTO>().ReverseMap();
            #endregion
        }
    }
}
