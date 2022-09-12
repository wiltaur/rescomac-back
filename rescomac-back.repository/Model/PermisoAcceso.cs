namespace rescomac_back.repository.Model
{
    public partial class PermisoAcceso
    {
        public int Id { get; set; }
        public bool Estado { get; set; }
        public int IdRol { get; set; }
        public int IdModulo { get; set; }

        public virtual Modulo IdModuloNavigation { get; set; } = null!;
        public virtual Rol IdRolNavigation { get; set; } = null!;
    }
}