namespace rescomac_back.repository.Model
{
    public partial class Propiedad
    {
        public Propiedad()
        {
            Vehiculos = new HashSet<Vehiculo>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public int Identificacion { get; set; }
        public int Celular { get; set; }
        public string Correo { get; set; } = null!;
        public string Torre { get; set; } = null!;
        public string Apto { get; set; } = null!;

        public virtual ICollection<Vehiculo> Vehiculos { get; set; }
    }
}