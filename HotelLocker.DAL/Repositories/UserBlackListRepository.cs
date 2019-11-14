using HotelLocker.DAL.EF;
using HotelLocker.DAL.Entities;
using HotelLocker.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelLocker.DAL.Repositories
{
    class UserBlackListRepository : IRepository<UserBlackList>
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

        public void Delete(int id)
        {
            UserBlackList blackList = db.UserBlackLists.Find(id);
            if (blackList != null)
                db.UserBlackLists.Remove(blackList);
        }

        public IEnumerable<UserBlackList> Find(Func<UserBlackList, bool> predicate)
        {
            return db.UserBlackLists.Where(predicate).ToList();
        }

        public UserBlackList Get(int id)
        {
            return db.UserBlackLists.Find(id);
        }

        public IEnumerable<UserBlackList> GetAll()
        {
            return db.UserBlackLists;
        }

        public void Update(UserBlackList item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
