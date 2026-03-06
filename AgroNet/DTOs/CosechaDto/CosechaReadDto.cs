namespace AgroNet.DTOs.CosechaDto
{
    public class CosechaReadDto
    {
        public int CosechaId { get; set; }
        public string FincaNombre { get; set; }
        public string ProductoNombre { get; set; }
        public decimal CantidadDisponible { get; set; }
        public decimal PrecioPorUnidad { get; set; }
        public DateTime FechaEstimada { get; set; }
        public string Estado { get; set; }

    }
}
