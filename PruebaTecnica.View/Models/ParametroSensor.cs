using PruebaTecnica.View.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.View.Models
{
    public class ParametroSensor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public long CodigoParametro { get; set; }
        [Required]
        public string DescripcionLarga { get; set; } = null!;
        [Required]
        public string Abreviatura { get; set; } = null!;
        public Valores Values {get; set;} = new Valores();
    }
}
