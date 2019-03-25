using System.Data.Entity;
using linq.model;

namespace linq.entitymodel
{

    public class EntityModel : DbContext
    {
        public EntityModel() : base("name=EntityModel") { }

        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Guest>()
                .Property(e => e.Name);

            modelBuilder.Entity<Guest>()
                .Property(e => e.Address);

            modelBuilder.Entity<Guest>()
                .HasMany(e => e.Bookings)
                .WithRequired(e => e.Guest)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hotel>()
                .Property(e => e.Name);

            modelBuilder.Entity<Hotel>()
                .Property(e => e.Address);

            modelBuilder.Entity<Hotel>()
                .HasMany(e => e.Rooms)
                .WithRequired(e => e.Hotel);

            modelBuilder.Entity<Room>()
                .Property(e => e.Types);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.Bookings)
                .WithRequired(e => e.Room)
                .HasForeignKey(e => new { e.Room_No, e.Hotel_No })
                .WillCascadeOnDelete(false);
        }
    }
}
