using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using BookingHotel.Models;
using Newtonsoft.Json;

namespace BookingHotel.Models
{
    public enum State
    {  Заброньовано, Скасовано,  Здіснене, Не_здійснене};
        

    public class BlackList
    {
        [Key]
        public int BlackListId { get; set; }

        public int HotelsID { get; set; }
        public virtual Hotel Hotel { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        string State { get; set; }

        double Earning { get; set; }
    }

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    

    [JsonObject(IsReference = true)]
    public class Hotel
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public byte[] Image { get; set; }
        public virtual ICollection<Room> HotelsRooms { get; set; }

        public Hotel()
        {
            this.HotelsRooms = new List<Room>();
        }
    }

    [JsonObject(IsReference = true)]
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public virtual ICollection<Hotel> HotelsRooms { get; set; }

        public Room()
        {
            this.HotelsRooms = new List<Hotel>();
        }
    }

    public class HotelsRooms
    {
        public int Id { get; set; }

        public int HotelID { get; set; }
        public virtual Hotel Hotel { get; set; }

        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        public int Discount { get; set; }

        public int Count { get; set; }

        public int Earning { get; set; }
    }

    public class Purchase
    {
        [Key]
        public int PurchaseId { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int HotelsID { get; set; }
        public virtual Hotel Hotel { get; set; }

        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateBegin { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateEnd { get; set; }

        public int Price { get; set; }

        public int Earning { get; set; }

        public State PurchState { get; set; }

        [DataType(DataType.Date)]
        public DateTime TimeOfPurch { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<BlackList> BlackLists { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<HotelsRooms> HotelsRoomses { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}