using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SupernumeraryEmps.Models
{
    public partial class WebPortalContext : DbContext
    {
        public WebPortalContext()
        {
        }

        public WebPortalContext(DbContextOptions<WebPortalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SupernumeraryEmpStatus> SupernumeraryEmpStatus { get; set; }
        public virtual DbSet<SupernumeraryEmps> SupernumeraryEmps { get; set; }
        public virtual DbSet<SupernumeraryEmpsHistory> SupernumeraryEmpsHistory { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=Supawit127;database=WebPortal", x => x.ServerVersion("8.0.19-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DescrRu)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<SupernumeraryEmpStatus>(entity =>
            {
                entity.ToTable("SupernumeraryEmp_Status");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DescrRu)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<SupernumeraryEmps>(entity =>
            {
                entity.HasIndex(e => e.Status)
                    .HasName("status_idx");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Iin)
                    .IsRequired()
                    .HasColumnName("IIN")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Room)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.SupernumeraryEmps)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("status");
            });

            modelBuilder.Entity<SupernumeraryEmpsHistory>(entity =>
            {
                entity.ToTable("SupernumeraryEmps_History");

                entity.HasIndex(e => e.SupernumeraryEmpId)
                    .HasName("application_idx");

                entity.HasIndex(e => e.SupernumeraryEmpsStatusId)
                    .HasName("status_idx");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.SupernumeraryEmpId).HasColumnName("SupernumeraryEmpID");

                entity.Property(e => e.SupernumeraryEmpsStatusId).HasColumnName("SupernumeraryEmps_StatusID");

                entity.HasOne(d => d.SupernumeraryEmp)
                    .WithMany(p => p.SupernumeraryEmpsHistory)
                    .HasForeignKey(d => d.SupernumeraryEmpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("applicationid");

                entity.HasOne(d => d.SupernumeraryEmpsStatus)
                    .WithMany(p => p.SupernumeraryEmpsHistory)
                    .HasForeignKey(d => d.SupernumeraryEmpsStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("statusid");
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.HasIndex(e => e.RoleId)
                    .HasName("role_idx");

                entity.HasIndex(e => e.Uid)
                    .HasName("ruusers_idx");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Uid).HasColumnName("UID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("role");

                entity.HasOne(d => d.U)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.Uid)
                    .HasConstraintName("user");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PRIMARY");

                entity.Property(e => e.Uid).HasColumnName("UID");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
