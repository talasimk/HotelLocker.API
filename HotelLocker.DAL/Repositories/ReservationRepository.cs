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
    class ReservationRepository : IRepository<Reservation>
    {
        private readonly HotelContext db;

        public ReservationRepository(HotelContext context)
        {
            this.db = context;
        }

        public void Create(Reservation item)
        {
            db.Reservations.Add(item);
        }

        public void Delete(int id)
        {
            Reservation res = db.Reservations.Find(id);
            if (res != null)
                db.Reservations.Remove(res);
        }

        public IEnumerable<Reservation> Find(Func<Reservation, bool> predicate)
        {
            return db.Reservations.Where(predicate).ToList();
        }

        public Reservation Get(int id)
        {
            return db.Reservations
                .Include(r => r.Guest)
                .Include(r => r.Room)
                .Include(r => r.Room.Hotel)
                .Where(r => r.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Reservation> GetAll()
        {
            return db.Reservations
                .Include(r => r.Guest)
                .Include(r => r.Room)
                .Include(r => r.Room.Hotel);
        }

        public void Update(Reservation item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
