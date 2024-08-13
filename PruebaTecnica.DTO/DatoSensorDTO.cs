using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruenaTecnica.DTO
{
    public class DatoSensorDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public long CodigoParametro { get; set; }
        [Required]
        public int ParametroSensoresId { get; set; }
        [Required]
        public string NombreParametro { get; set; } = null!;
        [Required]
        public DateTime FechaDato { get; set; }
        [Required]
        public decimal ValorNumero { get; set; }

        public virtual ParametroSensorDTO ParametroSensor { get; set; }
    }
}
