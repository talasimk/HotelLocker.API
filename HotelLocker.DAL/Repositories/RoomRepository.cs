using HotelLocker.DAL.EF;
using HotelLocker.DAL.Entities;
using HotelLocker.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelLocker.DAL.Repositories
{
    public class RoomRepository : IRepository<Room>
    {
        private readonly HotelContext db;

        public RoomRepository(HotelContext context)
        {
            this.db = context;
        }

        public void Create(Room item)
        {
            db.Rooms.Add(item);
        }

        public void Delete(int id)
        {
            Room room = db.Rooms.Find(id);
            if (room != null)
                db.Rooms.Remove(room);
        }

        public IEnumerable<Room> Find(Func<Room, bool> predicate)
        {
            return db.Rooms.Where(predicate).ToList();
        }

        public Room Get(int id)
        {
            return db.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.Reservations)
                .Where(r => r.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Room> GetAll()
        {
            return db.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.Reservations);
        }

        public void Update(Room item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
