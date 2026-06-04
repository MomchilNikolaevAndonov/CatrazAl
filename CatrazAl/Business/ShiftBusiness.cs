using CatrazAl.Data;
using CatrazAl.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CatrazAl.Business
{
    public class ShiftBusiness
    {
        private PrisonDbContext prisonContext;

        public List<Shift> GetAll()
        {
            using (prisonContext = new PrisonDbContext())
            {
                return prisonContext.Shifts.ToList();
            }
        }

        public Shift Get(int id)
        {
            using (prisonContext = new PrisonDbContext())
            {
                return prisonContext.Shifts.Find(id);
            }
        }

        public void Add(Shift shift)
        {
            using (prisonContext = new PrisonDbContext())
            {
                prisonContext.Shifts.Add(shift);
                prisonContext.SaveChanges();
            }
        }

        public void Update(Shift shift)
        {
            using (prisonContext = new PrisonDbContext())
            {
                var item = prisonContext.Shifts.Find(shift.ShiftId);
                if (item != null)
                {
                    prisonContext.Entry(item).CurrentValues.SetValues(shift);
                    prisonContext.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (prisonContext = new PrisonDbContext())
            {
                var shift = prisonContext.Shifts.Find(id);
                if (shift != null)
                {
                    prisonContext.Shifts.Remove(shift);
                    prisonContext.SaveChanges();
                }
            }
        }
    }
}