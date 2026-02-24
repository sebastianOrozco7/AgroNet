using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace AgroNet.DTOs.UsuarioDto
{
    public class UsuarioCreateDto
    {
        [Required(ErrorMessage = "El Rol es Obligatorio")]
        public int IdRol {  get; set; }

        [Required(ErrorMessage = "El Nombre es Obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Correo es Obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El Telefono es Obligatorio")]
        [Phone(ErrorMessage = "El formato del teléfono no es válido")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "La Contraseña es Obligatorio")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        public string Password { get; set; }
    }
}
