using Microsoft.EntityFrameworkCore;
using Restaurant_API_Klas.Enums;
using Restaurant_API_Klas.Models;

namespace Restaurant_API_Klas.Data
{
    public class RestaurantContext:DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext>options ) : base(options)
        {
                
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configure customer
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);
                entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
                entity.Property(e => e.Email)
                .IsRequired();
                entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15);

            });
            // configure Table
            modelBuilder.Entity<Table>(entity =>
            {
                entity.HasKey(e => e.TableId);
                entity.Property(e => e.Capacity)
                .IsRequired();
                entity.Property(e => e.Location)
                .HasMaxLength(50);
            });
            // configure Reservation
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.ReservationId);
                entity.Property(e => e.DateTime)
                    .IsRequired();
                entity.Property(e => e.SpecialRequests)
                    .HasMaxLength(100);

                //configure relationships
                entity.HasOne(e => e.Customer)
                    .WithMany(e => e.Reservations)
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Table)
                    .WithMany(e => e.Reservations)
                    .HasForeignKey(e => e.TableId)
                    .OnDelete(DeleteBehavior.Cascade);

            });

            // Seed data for Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, Name = "Kenan", Email = "kenan.kurda@example.com", PhoneNumber = "1234567890" },
                new Customer { CustomerId = 2, Name = "Rik", Email = "rik.riks@example.com", PhoneNumber = "0987654321" },
                new Customer { CustomerId = 3, Name = "Olesia", Email = "olesia.olesias@example.com", PhoneNumber = "1112223333" },
                new Customer { CustomerId = 4, Name = "Salvatore", Email = "salvatore.salvatore@example.com", PhoneNumber = "4445556666" }
            );

            // Seed data for Tables
            modelBuilder.Entity<Table>().HasData(
                new Table { TableId = 1, Capacity = 4, Location = SpecialRequests.WindowSeat },
                new Table { TableId = 2, Capacity = 2, Location = SpecialRequests.Anniversary },
                new Table { TableId = 3, Capacity = 6, Location = SpecialRequests.Birthday },
                new Table { TableId = 4, Capacity = 8, Location = SpecialRequests.None }
            );

            // Seed data for Reservations
            modelBuilder.Entity<Reservation>().HasData(
                new Reservation { ReservationId = 1, CustomerId = 1, TableId = 1, DateTime = new DateTime(2024, 6, 14, 19, 0, 0), SpecialRequests = SpecialRequests.WindowSeat },
                new Reservation { ReservationId = 2, CustomerId = 2, TableId = 2, DateTime = new DateTime(2024, 6, 14, 20, 0, 0), SpecialRequests = SpecialRequests.Anniversary },
                new Reservation { ReservationId = 3, CustomerId = 3, TableId = 3, DateTime = new DateTime(2024, 6, 15, 18, 0, 0), SpecialRequests = SpecialRequests.HighChair },
                new Reservation { ReservationId = 4, CustomerId = 4, TableId = 4, DateTime = new DateTime(2024, 6, 15, 19, 0, 0), SpecialRequests = SpecialRequests.HighChair },
                new Reservation { ReservationId = 5, CustomerId = 1, TableId = 2, DateTime = new DateTime(2024, 6, 16, 17, 0, 0), SpecialRequests = SpecialRequests.QuietTable }
            );
        }
    }
}
