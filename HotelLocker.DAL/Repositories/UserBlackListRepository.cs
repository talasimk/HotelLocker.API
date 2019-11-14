using HotelLocker.DAL.EF;
using HotelLocker.DAL.Entities;
using HotelLocker.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelLocker.DAL.Repositories
{
    public class UserBlackListRepository 
    {
        private readonly HotelContext db;

        public UserBlackListRepository(HotelContext context)
        {
            this.db = context;
        }

        public void Create(UserBlackList item)
        {
            db.UserBlackLists.Add(item);
        }

        public void Delete(UserBlackList blackList)
        {
            if (blackList != null)
                db.UserBlackLists.Remove(blackList);
        }

        public IEnumerable<UserBlackList> Find(Func<UserBlackList, bool> predicate)
        {
            return db.UserBlackLists.Where(predicate).ToList();
        }

        public UserBlackList Get(int id)
        {
            return db.UserBlackLists
                .Include(x => x.Hotel)
                .Include(x => x.Guest)
                .Where(x => x.HotelId == id)
                .FirstOrDefault();
        }

        public IEnumerable<UserBlackList> GetAll()
        {
            return db.UserBlackLists
                .Include(x => x.Hotel)
                .Include(x => x.Guest);
        }

        public void Update(UserBlackList item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
