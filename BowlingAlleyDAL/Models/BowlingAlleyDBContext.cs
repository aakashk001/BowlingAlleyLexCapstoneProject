using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BowlingAlleyDAL.Models
{
    public partial class BowlingAlleyDBContext : DbContext
    {
        public BowlingAlleyDBContext()
        {
        }

        public BowlingAlleyDBContext(DbContextOptions<BowlingAlleyDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BookingSlots> BookingSlots { get; set; }
        public virtual DbSet<Reservations> Reservations { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<ReservationDetails> ReservationDetails { get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source =(localdb)\\MSSQLLocalDB;Initial Catalog=BowlingAlleyDB;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingSlots>(entity =>
            {
                entity.HasKey(e => e.SlotId);
            });

            modelBuilder.Entity<Reservations>(entity =>
            {
                entity.HasKey(e => e.ReservationId);

                entity.Property(e => e.ReservedOn)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.ReservedByNavigation)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.ReservedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservations_Roles");

                entity.HasOne(d => d.Slot)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.SlotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservations_BookingSlots");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK_Role");

                entity.Property(e => e.EmpName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
