namespace rescomac_back.repository.Model
{
    public partial class Modulo
    {
        public Modulo()
        {
            PermisoAccesos = new HashSet<PermisoAcceso>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Enlace { get; set; } = null!;

        public virtual ICollection<PermisoAcceso> PermisoAccesos { get; set; }
    }
}