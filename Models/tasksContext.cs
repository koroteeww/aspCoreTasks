using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace aspCoreEmpty.Models
{
    public partial class tasksContext : DbContext
    {
        public tasksContext()
        {
        }

        public tasksContext(DbContextOptions<tasksContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Statuses> Statuses { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<Types> Types { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=mysqlpassword;database=tasks");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.Idcategories);

                entity.ToTable("categories", "tasks");

                entity.HasIndex(e => e.Idcategories)
                    .HasName("idcategories_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Idcategories)
                    .HasColumnName("idcategories")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(95)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Statuses>(entity =>
            {
                entity.HasKey(e => e.Idstatuses);

                entity.ToTable("statuses", "tasks");

                entity.HasIndex(e => e.Idstatuses)
                    .HasName("idstatuses_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Idstatuses)
                    .HasColumnName("idstatuses")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.ToTable("tasks", "tasks");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Chtosdelat)
                    .HasColumnName("chtosdelat")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Datasozdaniya).HasColumnName("datasozdaniya");

                entity.Property(e => e.Datazakritiya).HasColumnName("datazakritiya");

                entity.Property(e => e.Foto)
                    .HasColumnName("foto")
                    .HasColumnType("blob");

                entity.Property(e => e.Idstatus)
                    .HasColumnName("idstatus")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idtype)
                    .HasColumnName("idtype")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Ispolnitel)
                    .HasColumnName("ispolnitel")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nomer)
                    .HasColumnName("nomer")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Pomeschenie)
                    .HasColumnName("pomeschenie")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Sozdatel)
                    .HasColumnName("sozdatel")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Sozdatelemail)
                    .HasColumnName("sozdatelemail")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Types>(entity =>
            {
                entity.HasKey(e => e.Idtypes);

                entity.ToTable("types", "tasks");

                entity.HasIndex(e => e.Idtypes)
                    .HasName("idtypes_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Idtypes)
                    .HasColumnName("idtypes")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Idcategory)
                    .HasColumnName("idcategory")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(95)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Idusers);

                entity.ToTable("users", "tasks");

                entity.HasIndex(e => e.Idusers)
                    .HasName("idusers_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Idusers)
                    .HasColumnName("idusers")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Hash)
                    .HasColumnName("hash")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Isadminordispatcher)
                    .HasColumnName("isadminordispatcher")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
