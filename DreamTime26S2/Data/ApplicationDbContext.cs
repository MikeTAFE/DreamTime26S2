using DreamTime26S2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DreamTime26S2.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Timeslot> Timeslots { get; set; }
        public DbSet<SlotType> SlotTypes { get; set; }
        public DbSet<Slot> Slots { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // In here, we can add custom configuration for entities, relationships, seed data, etc.

            // "Convention over configuration" = you should try to use data annotations and naming conventions over explicit configuration

            // This is an example of "configuration" rather than "convention" - manually defining foreign keys, cascade rules, etc
            // Book entity
            //builder.Entity<Book>()
            //    .HasOne(b => b.Genre)
            //    .WithMany(g => g.Books)
            //    .OnDelete(DeleteBehavior.Restrict);


            /*
             * SEED DATA (automatically seeded/inserted into the database - via DB migrations)
             */

            // Room types
            builder.Entity<RoomType>().HasData(
                new RoomType { Id = 1, Name = "Bibliophile", Capacity = 12 },
                new RoomType { Id = 2, Name = "Inkwell", Capacity = 10},
                new RoomType { Id = 3, Name = "Novel Nook", Capacity = 6 }
            );

            // Rooms
            builder.Entity<Room>().HasData(
                new Room { Code = "B01", RoomTypeId = 1 },
                new Room { Code = "B02", RoomTypeId = 1 },
                new Room { Code = "B03", RoomTypeId = 1 },
                new Room { Code = "I01", RoomTypeId = 2 },
                new Room { Code = "I02", RoomTypeId = 2 },
                new Room { Code = "I03", RoomTypeId = 2 },
                new Room { Code = "I04", RoomTypeId = 2 },
                new Room { Code = "N01", RoomTypeId = 3 },
                new Room { Code = "N02", RoomTypeId = 3 },
                new Room { Code = "N03", RoomTypeId = 3 },
                new Room { Code = "N04", RoomTypeId = 3 },
                new Room { Code = "N05", RoomTypeId = 3 },
                new Room { Code = "N06", RoomTypeId = 3 }
            );

            // Slot types
            builder.Entity<SlotType>().HasData(
                new SlotType { Id = 1, Name = "Morning" },
                new SlotType { Id = 2, Name = "Afternoon" },
                new SlotType { Id = 3, Name = "Evening" }
            );

            // Timeslots - list of available times in 30-minute intervals

            //// OPTION 1: Manually add each item
            //builder.Entity<Timeslot>().HasData(
            //    new Timeslot { Time = new TimeOnly(8, 0) },   // 8:00AM
            //    new Timeslot { Time = new TimeOnly(8, 30) },  // 8:30AM
            //    new Timeslot { Time = new TimeOnly(9, 0) }    // 9:00AM
            //);

            // OPTION 2: Loop through each timeslot

            // List to hold timeslots
            List<Timeslot> timeslots = new();

            // Loop to generate all timeslots (30-min intervals)
            TimeOnly startTime = new TimeOnly(8, 0);   // 8:00AM
            TimeOnly endTime = new TimeOnly(22, 0);    // 10:00PM
            for (int minutes = 0; startTime.AddMinutes(minutes) <= endTime; minutes += 30)
            {
                timeslots.Add(new Timeslot(startTime.AddMinutes(minutes)));
            }

            // Add timeslots to the EF collection
            builder.Entity<Timeslot>().HasData(timeslots);


            // Pass customisation through to the base DbContext class (via IdentityDbContext)
            base.OnModelCreating(builder);
        }
    }
}
