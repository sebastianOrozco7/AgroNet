using AgroNet.Models;
using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace AgroNet.DTOs.PedidoDto
{
    public class PedidoCreateDto
    {
        [Required(ErrorMessage = "El Id de la cosecha es Obligatorio")]
        public int IdCosecha { get; set; }

        [Required(ErrorMessage = "La cantidad Es Obligatoria")]
        [Range(typeof(decimal), "0.01", "1000000", ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public decimal CantidadSolicitada { get; set; }
    }
}
