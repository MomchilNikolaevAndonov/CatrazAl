using CatrazAl.Data;
using CatrazAl.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CatrazAl.Business
{
    public class CellBusiness
    {
        private PrisonDbContext prisonContext;

        public List<Cell> GetAll()
        {
            using (prisonContext = new PrisonDbContext())
            {
                return prisonContext.Cells.ToList();
            }
        }

        public Cell Get(int id)
        {
            using (prisonContext = new PrisonDbContext())
            {
                return prisonContext.Cells.Find(id);
            }
        }

        public void Add(Cell cell)
        {
            using (prisonContext = new PrisonDbContext())
            {
                prisonContext.Cells.Add(cell);
                prisonContext.SaveChanges();
            }
        }

        public void Update(Cell cell)
        {
            using (prisonContext = new PrisonDbContext())
            {
                var item = prisonContext.Cells.Find(cell.CellId);
                if (item != null)
                {
                    prisonContext.Entry(item).CurrentValues.SetValues(cell);
                    prisonContext.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (prisonContext = new PrisonDbContext())
            {
                var cell = prisonContext.Cells.Find(id);
                if (cell != null)
                {
                    prisonContext.Cells.Remove(cell);
                    prisonContext.SaveChanges();
                }
            }
        }
    }
}