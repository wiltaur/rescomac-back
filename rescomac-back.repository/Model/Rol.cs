namespace rescomac_back.repository.Model
{
    public partial class Rol
    {
        public Rol()
        {
            PermisoAccesos = new HashSet<PermisoAcceso>();
            Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }

        public virtual ICollection<PermisoAcceso> PermisoAccesos { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}