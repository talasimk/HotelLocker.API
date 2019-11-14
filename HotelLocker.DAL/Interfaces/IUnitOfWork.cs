using HotelLocker.DAL.Entities;
using HotelLocker.DAL.Repositories;
using System.Threading.Tasks;

namespace HotelLocker.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Room> Rooms { get; }
        IRepository<User> Users { get; }
        IRepository<Hotel> Hotels { get; }
        IRepository<Guest> Guests { get; }
        IRepository<HotelAdmin> HotelAdmins { get; }
        IRepository<HotelStaff> HotelStaffs { get; }
        IRepository<Reservation> Reservations { get; }
        IRepository<RoomAccess> RoomAccesses { get; }
        UserBlackListRepository UserBlackLists { get; }
        Task SaveAsync();
    }
}
