using CatrazAl.Data;
using CatrazAl.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CatrazAl.Business
{
    public class PunishmentBusiness
    {
        private PrisonDbContext prisonContext;

        public List<Punishment> GetAll()
        {
            using (prisonContext = new PrisonDbContext())
            {
                return prisonContext.Punishments.ToList();
            }
        }

        public Punishment Get(int id)
        {
            using (prisonContext = new PrisonDbContext())
            {
                return prisonContext.Punishments.Find(id);
            }
        }

        public void Add(Punishment punishment)
        {
            using (prisonContext = new PrisonDbContext())
            {
                prisonContext.Punishments.Add(punishment);
                prisonContext.SaveChanges();
            }
        }

        public void Update(Punishment punishment)
        {
            using (prisonContext = new PrisonDbContext())
            {
                var item = prisonContext.Punishments.Find(punishment.PunishmentId);
                if (item != null)
                {
                    prisonContext.Entry(item).CurrentValues.SetValues(punishment);
                    prisonContext.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (prisonContext = new PrisonDbContext())
            {
                var punishment = prisonContext.Punishments.Find(id);
                if (punishment != null)
                {
                    prisonContext.Punishments.Remove(punishment);
                    prisonContext.SaveChanges();
                }
            }
        }
    }
}