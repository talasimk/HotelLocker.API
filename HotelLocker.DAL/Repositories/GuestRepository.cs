using HotelLocker.DAL.EF;
using HotelLocker.DAL.Entities;
using HotelLocker.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelLocker.DAL.Repositories
{
    public class GuestRepository : IRepository<Guest>
    {
        private readonly HotelContext db;

        public GuestRepository(HotelContext context)
        {
            this.db = context;
        }

        public void Create(Guest item) { }

        public void Delete(int id)  { }

        public IEnumerable<Guest> Find(Func<Guest, bool> predicate)
        {
            return db.Guests.Where(predicate).ToList();
        }

        public Guest Get(int id)
        {
            return db.Guests.Find(id);
        }

        public IEnumerable<Guest> GetAll()
        {
            return db.Guests;
        }

        public void Update(Guest item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
