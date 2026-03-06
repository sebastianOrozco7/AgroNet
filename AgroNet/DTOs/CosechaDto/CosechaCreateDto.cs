using AgroNet.Models;
using System.ComponentModel.DataAnnotations;

namespace AgroNet.DTOs.CosechaDto
{
    public class CosechaCreateDto
    {
        [Required(ErrorMessage = "La Finca Es Obligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "El identificador de la Finca no es válido.")]
        public int IdFinca { get; set; }

        [Required(ErrorMessage = "El producto Es Obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El identificador del Producto no es válido.")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "La cantidad disponible Es Obligatoria")]
        [Range(typeof(decimal), "0.01", "1000000", ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public decimal CantidadDisponible { get; set; }

        [Required(ErrorMessage = "El precio por unidad Es Obligatorio")]
        [Range(typeof(decimal), "50", "100000000", ErrorMessage = "El precio debe ser mayor a 50 pesos.")]
        public decimal PrecioPorUnidad { get; set; }

        [Required(ErrorMessage = "La Fecha estimada Es Obligatoria")]
        public DateTime FechaEstimada { get; set; }

        [Required(ErrorMessage = "El estado Es Obligatorio")]
        [EnumDataType(typeof(EstadoCosecha), ErrorMessage = "Estado no válido.")]
        public EstadoCosecha Estado { get; set; }
    }
}
