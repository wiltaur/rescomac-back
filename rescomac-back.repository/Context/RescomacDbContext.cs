using Microsoft.EntityFrameworkCore;
using rescomac_back.repository.Model;

namespace rescomac_back.repository.Context
{
    public partial class RescomacDbContext : DbContext
    {
        public RescomacDbContext()
        {
        }

        public RescomacDbContext(DbContextOptions<RescomacDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Modulo> Modulos { get; set; } = null!;
        public virtual DbSet<Parametro> Parametros { get; set; } = null!;
        public virtual DbSet<PermisoAcceso> PermisoAccesos { get; set; } = null!;
        public virtual DbSet<Propiedad> Propiedads { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<Vehiculo> Vehiculos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Modulo>(entity =>
            {
                entity.ToTable("Modulo");

                entity.Property(e => e.Enlace).HasMaxLength(100);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Parametro>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("Parametro");

                entity.Property(e => e.Codigo).HasMaxLength(50);

                entity.Property(e => e.Valor).HasMaxLength(50);
            });

            modelBuilder.Entity<PermisoAcceso>(entity =>
            {
                entity.ToTable("PermisoAcceso");

                entity.HasOne(d => d.IdModuloNavigation)
                    .WithMany(p => p.PermisoAccesos)
                    .HasForeignKey(d => d.IdModulo)
                    .HasConstraintName("FK_PermisoAcceso_Modulo");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.PermisoAccesos)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK_PermisoAcceso_Rol");
            });

            modelBuilder.Entity<Propiedad>(entity =>
            {
                entity.ToTable("Propiedad");

                entity.Property(e => e.Apellido).HasMaxLength(50);

                entity.Property(e => e.Apto).HasMaxLength(10);

                entity.Property(e => e.Correo).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Torre).HasMaxLength(10);
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Rol");

                entity.Property(e => e.Descripcion).HasMaxLength(100);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.Apellido).HasMaxLength(50);

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .HasColumnName("correo");

                entity.Property(e => e.Contresena).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK_Usuario_Rol");
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.HasKey(e => e.Placa);

                entity.ToTable("Vehiculo");

                entity.Property(e => e.Placa).HasMaxLength(10);

                entity.Property(e => e.Color).HasMaxLength(50);

                entity.Property(e => e.FechaIngreso).HasColumnType("date");

                entity.Property(e => e.FechaSalida).HasColumnType("date");

                entity.Property(e => e.Marca).HasMaxLength(50);

                entity.HasOne(d => d.IdPropiedadNavigation)
                    .WithMany(p => p.Vehiculos)
                    .HasForeignKey(d => d.IdPropiedad)
                    .HasConstraintName("FK_Vehiculo_Propiedad");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
