using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PetDL.Entities
{
    public partial class petdbContext : DbContext
    {
        public petdbContext()
        {
        }

        public petdbContext(DbContextOptions<petdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cat> Cats { get; set; }
        public virtual DbSet<Meal> Meals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:pet-manage-db.database.windows.net,1433;Initial Catalog=petdb;Persist Security Info=False;User ID=petadmin;Password=p@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cat>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Meal>(entity =>
            {
                entity.Property(e => e.FoodType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.Meals)
                    .HasForeignKey(d => d.CatId)
                    .HasConstraintName("FK__Meals__CatId__02FC7413");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
