using CatrazAl.Data;
using CatrazAl.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using CatrazAl.Data.Models;

namespace CatrazAl.Business
{
    public class PrisonerBusiness
    {
        private PrisonDbContext prisonContext;

        public List<Prisoner> GetAll()
        {
            using (prisonContext = new PrisonDbContext())
            {
                return prisonContext.Prisoners.ToList();
            }
        }

        public Prisoner Get(int id)
        {
            using (prisonContext = new PrisonDbContext())
            {
                return prisonContext.Prisoners.Find(id);
            }
        }

        public void Add(Prisoner prisoner)
        {
            using (prisonContext = new PrisonDbContext())
            {
                prisonContext.Prisoners.Add(prisoner);
                prisonContext.SaveChanges();
            }
        }

        public void Update(Prisoner prisoner)
        {
            using (prisonContext = new PrisonDbContext())
            {
                var item = prisonContext.Prisoners.Find(prisoner.PrisonerId);
                if (item != null)
                {
                    prisonContext.Entry(item).CurrentValues.SetValues(prisoner);
                    prisonContext.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (prisonContext = new PrisonDbContext())
            {
                var prisoner = prisonContext.Prisoners.Find(id);
                if (prisoner != null)
                {
                    prisonContext.Prisoners.Remove(prisoner);
                    prisonContext.SaveChanges();
                }
            }
        }
    }
}