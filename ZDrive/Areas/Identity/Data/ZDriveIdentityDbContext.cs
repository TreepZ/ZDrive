using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
#nullable disable
namespace ZDrive.Models
{
    public partial class ZDriveIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public ZDriveIdentityDbContext(DbContextOptions<ZDriveIdentityDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<ReservedSeat> ReservedSeats { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<Stop> Stops { get; set; }
        public virtual DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ZDrive;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.Licenseplate)
                    .HasName("PK__Car__4EE211DF6095B5D0");

                entity.Property(e => e.Licenseplate).IsUnicode(false);

                entity.Property(e => e.SizeOfCar).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Car__UserID__2B3F6F97");
            });

            modelBuilder.Entity<ReservedSeat>(entity =>
            {
                entity.HasKey(e => new { e.RouteId, e.UserId })
                    .HasName("PK__Reserved__51EF16677448DF17");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.ReservedSeats)
                    .HasForeignKey(d => d.RouteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ReservedS__Route__5CD6CB2B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ReservedSeats)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ReservedS__UserI__5DCAEF64");
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.Property(e => e.CarId).IsUnicode(false);

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Routes)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK__Routes__CarID__35BCFE0A");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Routes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Routes__UserID__5EBF139D");
            });

            modelBuilder.Entity<Stop>(entity =>
            {
                entity.Property(e => e.StopAddress).IsUnicode(false);

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.Stops)
                    .HasForeignKey(d => d.RouteId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Stops__RouteID__6E01572D");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).IsUnicode(false);

                entity.Property(e => e.UserEmail).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);

                entity.Property(e => e.UserType).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
