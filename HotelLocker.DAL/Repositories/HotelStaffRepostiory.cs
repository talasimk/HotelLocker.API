
using HotelLocker.DAL.EF;
using HotelLocker.DAL.Entities;
using HotelLocker.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelLocker.DAL.Repositories
{
    class HotelStaffRepository : IRepository<HotelStaff>
    {
        private readonly HotelContext db;

        public HotelStaffRepository(HotelContext context)
        {
            this.db = context;
        }

        public void Create(HotelStaff item) { }

        public void Delete(int id) { }

        public IEnumerable<HotelStaff> Find(Func<HotelStaff, bool> predicate)
        {
            return db.HotelStaffs.Where(predicate).ToList();
        }

        public HotelStaff Get(int id)
        {
            return db.HotelStaffs.Find(id);
        }

        public IEnumerable<HotelStaff> GetAll()
        {
            return db.HotelStaffs;
        }

        public void Update(HotelStaff item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
