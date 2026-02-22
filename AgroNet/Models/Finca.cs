namespace AgroNet.Models
{
    public class Finca
    {
        public int FincaId { get; set; }
        public int IdUsuario { get; set; }
        public string NombreFinca { get; set; }
        public string Municipio { get; set; }
        public string Direccion {  get; set; }
        public decimal Latitud {  get; set; }
        public decimal Longitud { get; set; }

        //conexiones
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Cosecha> Cosechas { get; set; }
    }
}
