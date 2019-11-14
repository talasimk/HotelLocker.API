using System;
using System.Collections.Generic;
using System.Linq;
using HotelLocker.DAL.EF;
using HotelLocker.DAL.Entities;
using HotelLocker.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelLocker.DAL.Repositories
{
    public class HotelRepository : IRepository<Hotel>
    {
        private readonly HotelContext db;

        public HotelRepository(HotelContext context)
        {
            this.db = context;
        }

        public void Create(Hotel item)
        {
            db.Hotels.Add(item);
        }

        public void Delete(int id)
        {
            Hotel hotel = db.Hotels.Find(id);
            if (hotel != null)
                db.Hotels.Remove(hotel);
        }

        public IEnumerable<Hotel> Find(Func<Hotel, bool> predicate)
        {
            return db.Hotels.Where(predicate).ToList();
        }

        public Hotel Get(int id)
        {
            return db.Hotels
                .Include(x => x.HotelStaffs)
                .Include(x => x.HotelAdmin)
                .Include(x => x.Rooms)
                .Include(x => x.UserBlackLists)
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Hotel> GetAll()
        {
            return db.Hotels
                .Include(x => x.HotelStaffs)
                .Include(x => x.HotelAdmin)
                .Include(x => x.Rooms)
                .Include(x => x.UserBlackLists);
        }

        public void Update(Hotel item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
