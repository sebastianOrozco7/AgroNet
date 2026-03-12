
using System.ComponentModel.DataAnnotations;

namespace AgroNet.DTOs.FincasDto
{
    public class FincaCreateDto
    {
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Municipio es obligatorio")]
        public string Municipio { get; set; }

        [Required(ErrorMessage = "La Direccion es obligatoria")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "La latitud es obligatoria")]
        [Range(-90.0, 90.0, ErrorMessage = "La latitud debe estar entre -90 y 90 grados.")]
        public decimal Latitud { get; set; }

        [Required(ErrorMessage = "La longitud es obligatoria")]
        [Range(-180.0, 180.0, ErrorMessage = "La longitud debe estar entre -180 y 180 grados.")]
        public decimal Longitud { get; set; }
    }
}
