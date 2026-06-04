using CatrazAl.Data;
using CatrazAl.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CatrazAl.Business
{
    public class VisitBusiness
    {
        private PrisonDbContext prisonContext;

        public List<Visit> GetAll()
        {
            using (prisonContext = new PrisonDbContext())
            {
                return prisonContext.Visits.ToList();
            }
        }

        public Visit Get(int id)
        {
            using (prisonContext = new PrisonDbContext())
            {
                return prisonContext.Visits.Find(id);
            }
        }

        public void Add(Visit visit)
        {
            using (prisonContext = new PrisonDbContext())
            {
                prisonContext.Visits.Add(visit);
                prisonContext.SaveChanges();
            }
        }

        public void Update(Visit visit)
        {
            using (prisonContext = new PrisonDbContext())
            {
                var item = prisonContext.Visits.Find(visit.VisitId);
                if (item != null)
                {
                    prisonContext.Entry(item).CurrentValues.SetValues(visit);
                    prisonContext.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (prisonContext = new PrisonDbContext())
            {
                var visit = prisonContext.Visits.Find(id);
                if (visit != null)
                {
                    prisonContext.Visits.Remove(visit);
                    prisonContext.SaveChanges();
                }
            }
        }
    }
}