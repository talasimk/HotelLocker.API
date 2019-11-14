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
    class HotelAdminRepository : IRepository<HotelAdmin>
    {
        private readonly HotelContext db;

        public HotelAdminRepository(HotelContext context)
        {
            this.db = context;
        }

        public void Create(HotelAdmin item) { }

        public void Delete(int id) { }

        public IEnumerable<HotelAdmin> Find(Func<HotelAdmin, bool> predicate)
        {
            return db.HotelAdmins.Where(predicate).ToList();
        }

        public HotelAdmin Get(int id)
        {
            return db.HotelAdmins.Find(id);
        }

        public IEnumerable<HotelAdmin> GetAll()
        {
            return db.HotelAdmins;
        }

        public void Update(HotelAdmin item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
