using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Model
{
    public class ParametroSensor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public long CodigoParametro {  get; set; }
        [Required]
        public string DescripcionLarga { get; set; } = null!;
        [Required]
        public string DescripcionMedia { get; set; } = null!;
        [Required]
        public string DescripcionCorta { get; set; } = null!;
        [Required]
        public string Abreviatura { get; set; } = null!;
        [Required]
        public string Observacion { get; set; } = null!;
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Required]
        public DateTime FechaModificacion { get; set; }
        [Required]
        public string Estado {  get; set; } = null!;
        [Required]
        public string Unidad { get; set; } = null!;

        public ICollection<DatoSensor> DatosSensores { get; set; }
    }
}
