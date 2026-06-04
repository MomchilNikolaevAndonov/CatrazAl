using CatrazAl.Data;
using CatrazAl.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CatrazAl.Business
{
    public class GuardBusiness
    {
        private PrisonDbContext prisonContext;

        public List<Guard> GetAll()
        {
            using (prisonContext = new PrisonDbContext())
            {
                return prisonContext.Guards.ToList();
            }
        }

        public Guard Get(int id)
        {
            using (prisonContext = new PrisonDbContext())
            {
                return prisonContext.Guards.Find(id);
            }
        }

        public void Add(Guard guard)
        {
            using (prisonContext = new PrisonDbContext())
            {
                prisonContext.Guards.Add(guard);
                prisonContext.SaveChanges();
            }
        }

        public void Update(Guard guard)
        {
            using (prisonContext = new PrisonDbContext())
            {
                var item = prisonContext.Guards.Find(guard.GuardId);
                if (item != null)
                {
                    prisonContext.Entry(item).CurrentValues.SetValues(guard);
                    prisonContext.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (prisonContext = new PrisonDbContext())
            {
                var guard = prisonContext.Guards.Find(id);
                if (guard != null)
                {
                    prisonContext.Guards.Remove(guard);
                    prisonContext.SaveChanges();
                }
            }
        }
    }
}