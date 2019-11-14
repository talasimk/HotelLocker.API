using HotelLocker.Common.Enums;
using HotelLocker.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelLocker.DAL.EF
{
    public class HotelContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelAdmin> HotelAdmins { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<HotelStaff> HotelStaffs { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomAccess> RoomAccesses { get; set; }
        public DbSet<UserBlackList> UserBlackLists { get; set; }


        public HotelContext(DbContextOptions<HotelContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<UserBlackList>()
                .HasKey(r => new { r.GuestId, r.HotelId});

            modelBuilder
                .Entity<RoomAccess>()
                .Property(p => p.DateTime)
                .HasColumnType("datetime2");

            modelBuilder.Entity<Room>()
                .Property(p => p.Category)
                .HasColumnType("int")
                .HasDefaultValueSql(((int)RoomCategory.Standart).ToString());

            modelBuilder.Entity<HotelStaff>()
                .Property(p => p.Category)
                .HasColumnType("int")
                .HasDefaultValueSql(((int)StaffCategory.Maid).ToString());

            modelBuilder
                .Entity<Room>()
                .Property(p => p.IsAvailable)
                .HasDefaultValueSql("1");

            modelBuilder
                .Entity<RoomAccess>()
                .Property(p => p.IsOpen)
                .HasDefaultValueSql("1");

            modelBuilder
                .Entity<User>()
                .HasIndex(p => p.Email)
               .IsUnique();

            modelBuilder.Entity<HotelStaff>()
                .HasOne(p => p.Hotel)
                .WithMany(p => p.HotelStaffs)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserBlackList>()
                .HasOne(p => p.Hotel)
                .WithMany(p => p.UserBlackLists)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(p => p.Room)
                .WithMany(p => p.Reservations)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RoomAccess>()
                .HasOne(p => p.User)
                .WithMany(p => p.RoomAccesses)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

    }
}
