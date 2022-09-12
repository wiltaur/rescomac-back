namespace rescomac_back.repository.Model
{
    public partial class Vehiculo
    {
        public string Placa { get; set; } = null!;
        public string? Marca { get; set; }
        public int? Modelo { get; set; }
        public string? Color { get; set; }
        public bool EstadoParqueo { get; set; }
        public bool EstadoPago { get; set; }
        public int Tipo { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaSalida { get; set; }
        public int IdPropiedad { get; set; }

        public virtual Propiedad IdPropiedadNavigation { get; set; } = null!;
    }
}