namespace rescomac_back.repository.Model
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public byte[] Contresena { get; set; } = null!;
        public int IdRol { get; set; }

        public virtual Rol IdRolNavigation { get; set; } = null!;
    }
}