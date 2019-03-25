using System;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace linq.model
{
    [Table("Hotel")]
    public class Hotel
    {
        [Key, Column(Order = 0)]
        public int Hotel_No { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }

        public Hotel() { Rooms = new HashSet<Room>(); }
    }
    [Table("Room")]
    public class Room
    {
        [Key, Column(Order = 0)]
        public int Room_No { get; set; }
        [Key, Column(Order = 1)]
        public int Hotel_No { get; set; }
        public string Types { get; set; }
        public double Price { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }

        public Room() { Bookings = new HashSet<Booking>(); }
    }

    [Table("Guest")]
    public class Guest
    {
        [Key, Column(Order = 0)]
        public int Guest_No { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }

        public Guest() { Bookings = new HashSet<Booking>(); }
    }

    [Table("Booking")]
    public class Booking
    {
        [Key, Column(Order = 0)]
        public int Booking_id { get; set; }
        public int Hotel_No { get; set; }
        public int Guest_No { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date_From { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date_To { get; set; }
        public int Room_No { get; set; }
        public virtual Room Room { get; set; }
        public virtual Guest Guest { get; set; }
    }
}
