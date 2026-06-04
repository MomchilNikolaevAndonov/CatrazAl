using CatrazAl.Data;
using CatrazAl.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CatrazAl.Business
{
    public class CrimeBusiness
    {
        private PrisonDbContext prisonContext;

        public List<Crime> GetAll()
        {
            using (prisonContext = new PrisonDbContext())
            {
                return prisonContext.Crimes.ToList();
            }
        }

        public Crime Get(int id)
        {
            using (prisonContext = new PrisonDbContext())
            {
                return prisonContext.Crimes.Find(id);
            }
        }

        public void Add(Crime crime)
        {
            using (prisonContext = new PrisonDbContext())
            {
                prisonContext.Crimes.Add(crime);
                prisonContext.SaveChanges();
            }
        }

        public void Update(Crime crime)
        {
            using (prisonContext = new PrisonDbContext())
            {
                var item = prisonContext.Crimes.Find(crime.CrimeId);
                if (item != null)
                {
                    prisonContext.Entry(item).CurrentValues.SetValues(crime);
                    prisonContext.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (prisonContext = new PrisonDbContext())
            {
                var crime = prisonContext.Crimes.Find(id);
                if (crime != null)
                {
                    prisonContext.Crimes.Remove(crime);
                    prisonContext.SaveChanges();
                }
            }
        }
    }
}