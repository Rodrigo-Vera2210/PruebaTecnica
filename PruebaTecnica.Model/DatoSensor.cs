using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Model
{
    public class DatoSensor
    {
        [Key]
        public int Id { get; set; }
        public long CodigoParametro { get; set; }
        public int ParametroSensoresId {  get; set; }
        public string NombreParametro { get; set; } = null!;
        public DateTime FechaDato { get; set; }
        public decimal ValorNumero { get; set; }

        public virtual ParametroSensor ParametroSensor { get; set; }
    }
}
