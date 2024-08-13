using PruebaTecnica.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruenaTecnica.DTO
{
    public class ParametroSensorDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public long CodigoParametro { get; set; }
        [Required]
        public string DescripcionLarga { get; set; } = null!;
        [Required]
        public string Abreviatura { get; set; } = null!;
        public ValoresDTO Values {get; set;} = new ValoresDTO();
    }
}
