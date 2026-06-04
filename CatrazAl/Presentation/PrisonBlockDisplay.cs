using CatrazAl.Business;
using CatrazAl.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatrazAl.Presentation
{
    public class PrisonBlockDisplay
    {
        private int closeOperationId = 6;
        private PrisonBlockBusiness blockBusiness = new PrisonBlockBusiness();

        public PrisonBlockDisplay() { Input(); }

        private void ShowMenu()
        {
            Console.WriteLine("\n" + new string('-', 40));
            Console.WriteLine("              Prison Block              ");
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("1. List all entries\n2. Add new entry\n3. Update entry\n4. Fetch entry by ID\n5. Delete entry by ID\n6. Exit");
        }

        private void Input()
        {
            var operation = -1;
            do
            {
                ShowMenu();
                int.TryParse(Console.ReadLine(), out operation);
                switch (operation)
                {
                    case 1: ListAll(); break;
                    case 2: Add(); break;
                    case 3: Update(); break;
                    case 4: Fetch(); break;
                    case 5: Delete(); break;
                }
            } while (operation != closeOperationId);
        }

        private void Delete()
        {
            Console.WriteLine("Enter Block ID to delete: ");
            blockBusiness.Delete(int.Parse(Console.ReadLine()));
            Console.WriteLine("Done.");
        }

        private void Fetch()
        {
            Console.WriteLine("Enter Block ID to fetch: ");
            PrisonBlock block = blockBusiness.Get(int.Parse(Console.ReadLine()));
            if (block != null) Console.WriteLine($"ID: {block.PrisonBlockId} | Name: {block.PrisonBlock1}");
        }

        private void Update()
        {
            Console.WriteLine("Enter Block ID to update: ");
            PrisonBlock block = blockBusiness.Get(int.Parse(Console.ReadLine()));
            if (block != null)
            {
                Console.WriteLine("Enter new Block Name: ");
                block.PrisonBlock1 = Console.ReadLine();
                blockBusiness.Update(block);
            }
        }

        private void Add()
        {
            PrisonBlock block = new PrisonBlock();
            Console.WriteLine("Enter Block Name: ");
            block.PrisonBlock1 = Console.ReadLine();
            blockBusiness.Add(block);
        }

        private void ListAll()
        {
            foreach (var item in blockBusiness.GetAll())
                Console.WriteLine($"{item.PrisonBlockId} - {item.PrisonBlock1}");
        }
    }
}
