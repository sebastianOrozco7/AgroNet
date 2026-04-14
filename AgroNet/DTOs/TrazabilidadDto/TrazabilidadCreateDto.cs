using AgroNet.Models;
using System.ComponentModel.DataAnnotations;

namespace AgroNet.DTOs.TrazabilidadDto
{
    public class TrazabilidadCreateDto
    {

        [Required(ErrorMessage = "El Id del Pedido es Obligatorio")]
        public int IdPedido { get; set; }

        [Required(ErrorMessage = "El estado nuevo es Obligatorio")]
        public EstadoPedido EstadoNuevo { get; set; }

        [Required(ErrorMessage = "La observacion es Obligatoria")]
        public string Observacion { get; set; }
    }
}
