using HotelLocker.DAL.EF;
using HotelLocker.DAL.Entities;
using HotelLocker.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelLocker.DAL.Repositories
{
    public class RoomAccessRepository : IRepository<RoomAccess>
    {
        private readonly HotelContext db;

        public RoomAccessRepository(HotelContext context)
        {
            this.db = context;
        }

        public void Create(RoomAccess item)
        {
            db.RoomAccesses.Add(item);
        }

        public void Delete(int id)
        {
            RoomAccess room = db.RoomAccesses.Find(id);
            if (room != null)
                db.RoomAccesses.Remove(room);
        }

        public IEnumerable<RoomAccess> Find(Func<RoomAccess, bool> predicate)
        {
            return db.RoomAccesses.Where(predicate).ToList();
        }

        public RoomAccess Get(int id)
        {
            return db.RoomAccesses
                .Include(x => x.Room)
                .ThenInclude(x => x.Hotel)
                .Include(x => x.User)
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<RoomAccess> GetAll()
        {
            return db.RoomAccesses
                .Include(x => x.Room)
                .ThenInclude(x => x.Hotel)
                .Include(x => x.User);
        }

        public void Update(RoomAccess item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
