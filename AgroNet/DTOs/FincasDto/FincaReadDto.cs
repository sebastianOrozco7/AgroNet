

namespace AgroNet.DTOs.FincasDto
{
    public class FincaReadDto
    {
        public int FincaId { get; set; }
        public string UsuarioNombre { get; set; }
        public string Nombre { get; set; }
        public string Municipio { get; set; }
        public string Direccion { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }

    }
}
