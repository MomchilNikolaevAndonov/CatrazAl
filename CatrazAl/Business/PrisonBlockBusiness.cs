using CatrazAl.Data;
using CatrazAl.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CatrazAl.Business
{
    public class PrisonBlockBusiness
    {
        private PrisonDbContext prisonContext;

        public List<PrisonBlock> GetAll()
        {
            using (prisonContext = new PrisonDbContext())
            {
                return prisonContext.PrisonBlocks.ToList();
            }
        }

        public PrisonBlock Get(int id)
        {
            using (prisonContext = new PrisonDbContext())
            {
                return prisonContext.PrisonBlocks.Find(id);
            }
        }

        public void Add(PrisonBlock prisonBlock)
        {
            using (prisonContext = new PrisonDbContext())
            {
                prisonContext.PrisonBlocks.Add(prisonBlock);
                prisonContext.SaveChanges();
            }
        }

        public void Update(PrisonBlock prisonBlock)
        {
            using (prisonContext = new PrisonDbContext())
            {
                var item = prisonContext.PrisonBlocks.Find(prisonBlock.PrisonBlockId);
                if (item != null)
                {
                    prisonContext.Entry(item).CurrentValues.SetValues(prisonBlock);
                    prisonContext.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (prisonContext = new PrisonDbContext())
            {
                var block = prisonContext.PrisonBlocks.Find(id);
                if (block != null)
                {
                    try
                    {
                        prisonContext.PrisonBlocks.Remove(block);
                        prisonContext.SaveChanges();
                    }
                    catch (Microsoft.EntityFrameworkCore.DbUpdateException)
                    {
                        Console.WriteLine("\n[ERROR] Cannot delete this Prison Block!");
                        Console.WriteLine("There are still Cells, Prisoners, or Shifts assigned to it.");
                        Console.WriteLine("You must delete or reassign them before deleting this block.");
                    }
                }
                else
                {
                    Console.WriteLine("Prison block not found.");
                }
            }
        }
    }
}