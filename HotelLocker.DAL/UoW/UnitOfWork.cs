using HotelLocker.DAL.EF;
using HotelLocker.DAL.Interfaces;
using System;
using HotelLocker.DAL.Repositories;
using HotelLocker.DAL.Entities;
using System.Threading.Tasks;

namespace HotelLocker.DAL.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HotelContext db;
        private GuestRepository guestRepository;
        private RoomRepository roomRepository;
        private HotelRepository hotelRepository;
        private HotelAdminRepository adminRepository;
        private HotelStaffRepository staffRepository;
        private ReservationRepository reservationRepository;
        private RoomAccessRepository accessRepository;
        private UserBlackListRepository blackListRepository;
        private UserRepository userRepository;

        public UnitOfWork(HotelContext context)
        {
            db = context;
        }
        public IRepository<Guest> Guests
        {
            get
            {
                if (guestRepository == null)
                    guestRepository = new GuestRepository(db);
                return guestRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public IRepository<Room> Rooms
        {
            get
            {
                if (roomRepository == null)
                    roomRepository = new RoomRepository(db);
                return roomRepository;
            }
        }

        public IRepository<Hotel> Hotels
        {
            get
            {
                if (hotelRepository == null)
                    hotelRepository = new HotelRepository(db);
                return hotelRepository;
            }
        }

        public IRepository<HotelAdmin> HotelAdmins
        {
            get
            {
                if (adminRepository == null)
                    adminRepository = new HotelAdminRepository(db);
                return adminRepository;
            }
        }

        public IRepository<HotelStaff> HotelStaffs
        {
            get
            {
                if (staffRepository == null)
                    staffRepository  = new HotelStaffRepository(db);
                return staffRepository;
            }
        }

        public IRepository<Reservation> Reservations
        {
            get
            {
                if (reservationRepository == null)
                    reservationRepository = new ReservationRepository(db);
                return reservationRepository;
            }
        }

        public IRepository<RoomAccess> RoomAccesses
        {
            get
            {
                if (accessRepository == null)
                    accessRepository = new RoomAccessRepository(db);
                return accessRepository;
            }
        }

        public IRepository<UserBlackList> UserBlackLists
        {
            get
            {
                if (blackListRepository == null)
                    blackListRepository = new UserBlackListRepository(db);
                return blackListRepository;
            }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

