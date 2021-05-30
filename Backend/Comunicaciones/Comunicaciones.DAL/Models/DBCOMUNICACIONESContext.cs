using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Comunicaciones.DAL.Models
{
    public partial class DBCOMUNICACIONESContext : DbContext
    {
        public DBCOMUNICACIONESContext()
        {
        }

        public DBCOMUNICACIONESContext(DbContextOptions<DBCOMUNICACIONESContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CorrespondenciaAuditorium> CorrespondenciaAuditoria { get; set; }
        public virtual DbSet<Correspondencium> Correspondencia { get; set; }
        public virtual DbSet<Funcionario> Funcionarios { get; set; }
        public virtual DbSet<Modulo> Modulos { get; set; }
        public virtual DbSet<ModuloPrincipal> ModuloPrincipals { get; set; }
        public virtual DbSet<Remitente> Remitentes { get; set; }
        public virtual DbSet<RemitenteAuditorium> RemitenteAuditoria { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<RolModulo> RolModulos { get; set; }
        public virtual DbSet<TipoCorrespondencium> TipoCorrespondencia { get; set; }
        public virtual DbSet<ViewConsultaCorrespondencia> ViewConsultaCorrespondencias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=DBCOMUNICACIONES;User Id=sa;Password=sa2019%;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<CorrespondenciaAuditorium>(entity =>
            {
                entity.HasKey(e => e.CauId)
                    .HasName("PK__correspo__66913F5E78F21154");

                entity.ToTable("correspondencia_auditoria");

                entity.Property(e => e.CauId)
                    .ValueGeneratedNever()
                    .HasColumnName("cau_id");

                entity.Property(e => e.CauAsunto)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("cau_asunto");

                entity.Property(e => e.CauDescripcion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("cau_descripcion");

                entity.Property(e => e.CauFechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("cau_fecha_registro");

                entity.Property(e => e.CauIdCorrespondencia).HasColumnName("cau_id_correspondencia");

                entity.HasOne(d => d.CauIdCorrespondenciaNavigation)
                    .WithMany(p => p.CorrespondenciaAuditoria)
                    .HasForeignKey(d => d.CauIdCorrespondencia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_correspondencia_auditoria__correspondencia");
            });

            modelBuilder.Entity<Correspondencium>(entity =>
            {
                entity.HasKey(e => e.CorId)
                    .HasName("PK__correspo__55FA04BA5BCEFE88");

                entity.ToTable("correspondencia");

                entity.Property(e => e.CorId)
                    .ValueGeneratedNever()
                    .HasColumnName("cor_id");

                entity.Property(e => e.CorAsunto)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("cor_asunto");

                entity.Property(e => e.CorConsecutivo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cor_consecutivo");

                entity.Property(e => e.CorDescripcion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("cor_descripcion");

                entity.Property(e => e.CorFechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("cor_fecha_creacion");

                entity.Property(e => e.CorIdDestinatario).HasColumnName("cor_id_destinatario");

                entity.Property(e => e.CorIdRemitente).HasColumnName("cor_id_remitente");

                entity.Property(e => e.CorIdTipoCorrespondencia).HasColumnName("cor_id_tipo_correspondencia");

                entity.HasOne(d => d.CorIdDestinatarioNavigation)
                    .WithMany(p => p.Correspondencia)
                    .HasForeignKey(d => d.CorIdDestinatario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_correspondencia__funcionario");

                entity.HasOne(d => d.CorIdRemitenteNavigation)
                    .WithMany(p => p.Correspondencia)
                    .HasForeignKey(d => d.CorIdRemitente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_correspondencia__remitente");

                entity.HasOne(d => d.CorIdTipoCorrespondenciaNavigation)
                    .WithMany(p => p.Correspondencia)
                    .HasForeignKey(d => d.CorIdTipoCorrespondencia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_correspondencia__tipo_correspondencia");
            });

            modelBuilder.Entity<Funcionario>(entity =>
            {
                entity.HasKey(e => e.FunId)
                    .HasName("PK__funciona__35A479284701C5CE");

                entity.ToTable("funcionario");

                entity.Property(e => e.FunId)
                    .ValueGeneratedNever()
                    .HasColumnName("fun_id");

                entity.Property(e => e.FunActivo).HasColumnName("fun_activo");

                entity.Property(e => e.FunApellidos)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("fun_apellidos");

                entity.Property(e => e.FunIdRol).HasColumnName("fun_id_rol");

                entity.Property(e => e.FunIdentificacion)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("fun_identificacion");

                entity.Property(e => e.FunNombres)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("fun_nombres");

                entity.HasOne(d => d.FunIdRolNavigation)
                    .WithMany(p => p.Funcionarios)
                    .HasForeignKey(d => d.FunIdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_funcionario__rol");
            });

            modelBuilder.Entity<Modulo>(entity =>
            {
                entity.HasKey(e => e.ModId)
                    .HasName("PK__modulo__65659BEE284F0728");

                entity.ToTable("modulo");

                entity.Property(e => e.ModId)
                    .ValueGeneratedNever()
                    .HasColumnName("mod_id");

                entity.Property(e => e.ModActivo).HasColumnName("mod_activo");

                entity.Property(e => e.ModIdModuloPrincipal).HasColumnName("mod_id_modulo_principal");

                entity.Property(e => e.ModNombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("mod_nombre");

                entity.HasOne(d => d.ModIdModuloPrincipalNavigation)
                    .WithMany(p => p.Modulos)
                    .HasForeignKey(d => d.ModIdModuloPrincipal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_modulo_principal__modulo");
            });

            modelBuilder.Entity<ModuloPrincipal>(entity =>
            {
                entity.HasKey(e => e.MprId)
                    .HasName("PK__modulo_p__4012566C538FA4EE");

                entity.ToTable("modulo_principal");

                entity.Property(e => e.MprId)
                    .ValueGeneratedNever()
                    .HasColumnName("mpr_id");

                entity.Property(e => e.MprActivo).HasColumnName("mpr_activo");

                entity.Property(e => e.MprNombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("mpr_nombre");
            });

            modelBuilder.Entity<Remitente>(entity =>
            {
                entity.HasKey(e => e.RemId)
                    .HasName("PK__remitent__331E1C5FBB5ED42D");

                entity.ToTable("remitente");

                entity.Property(e => e.RemId)
                    .ValueGeneratedNever()
                    .HasColumnName("rem_id");

                entity.Property(e => e.RemApellidos)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("rem_apellidos");

                entity.Property(e => e.RemFechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("rem_fecha_creacion");

                entity.Property(e => e.RemIdentificacion)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("rem_identificacion");

                entity.Property(e => e.RemNombres)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("rem_nombres");
            });

            modelBuilder.Entity<RemitenteAuditorium>(entity =>
            {
                entity.HasKey(e => e.RauId)
                    .HasName("PK__remitent__14238CE968E799EF");

                entity.ToTable("remitente_auditoria");

                entity.Property(e => e.RauId)
                    .ValueGeneratedNever()
                    .HasColumnName("rau_id");

                entity.Property(e => e.RauApellidos)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("rau_apellidos");

                entity.Property(e => e.RauFechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("rau_fecha_registro");

                entity.Property(e => e.RauIdRemitente).HasColumnName("rau_id_remitente");

                entity.Property(e => e.RauIdentificacion)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("rau_identificacion");

                entity.Property(e => e.RauNombres)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("rau_nombres");

                entity.HasOne(d => d.RauIdRemitenteNavigation)
                    .WithMany(p => p.RemitenteAuditoria)
                    .HasForeignKey(d => d.RauIdRemitente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_remitente_auditoria__remitente");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("rol");

                entity.Property(e => e.RolId)
                    .ValueGeneratedNever()
                    .HasColumnName("rol_id");

                entity.Property(e => e.RolActivo).HasColumnName("rol_activo");

                entity.Property(e => e.RolNombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("rol_nombre");
            });

            modelBuilder.Entity<RolModulo>(entity =>
            {
                entity.HasKey(e => new { e.RmoIdRol, e.RmoIdModulo })
                    .HasName("PK__rol_modu__ED4AFE744A71F226");

                entity.ToTable("rol_modulo");

                entity.Property(e => e.RmoIdRol).HasColumnName("rmo_id_rol");

                entity.Property(e => e.RmoIdModulo).HasColumnName("rmo_id_modulo");

                entity.HasOne(d => d.RmoIdModuloNavigation)
                    .WithMany(p => p.RolModulos)
                    .HasForeignKey(d => d.RmoIdModulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_rol_modulo__modulo");

                entity.HasOne(d => d.RmoIdRolNavigation)
                    .WithMany(p => p.RolModulos)
                    .HasForeignKey(d => d.RmoIdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_rol_modulo__rol");
            });

            modelBuilder.Entity<TipoCorrespondencium>(entity =>
            {
                entity.HasKey(e => e.TcoId)
                    .HasName("PK__tipo_cor__F8709CC12FC6A2D5");

                entity.ToTable("tipo_correspondencia");

                entity.Property(e => e.TcoId)
                    .ValueGeneratedNever()
                    .HasColumnName("tco_id");

                entity.Property(e => e.TcoActivo).HasColumnName("tco_activo");

                entity.Property(e => e.TcoConsecutivo).HasColumnName("tco_consecutivo");

                entity.Property(e => e.TcoNombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tco_nombre");

                entity.Property(e => e.TcoPrefijo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("tco_prefijo");
            });

            modelBuilder.Entity<ViewConsultaCorrespondencia>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VIEW_CONSULTA_CORRESPONDENCIAS");

                entity.Property(e => e.Asunto)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("asunto");

                entity.Property(e => e.Consecutivo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("consecutivo");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion");

                entity.Property(e => e.NombreDestinatario)
                    .IsRequired()
                    .HasMaxLength(201)
                    .IsUnicode(false)
                    .HasColumnName("nombre_destinatario");

                entity.Property(e => e.NombreRemitente)
                    .IsRequired()
                    .HasMaxLength(201)
                    .IsUnicode(false)
                    .HasColumnName("nombre_remitente");

                entity.Property(e => e.TipoCorrespondencia)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tipo_correspondencia");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
