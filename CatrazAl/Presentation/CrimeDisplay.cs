using CatrazAl.Business;
using CatrazAl.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatrazAl.Presentation
{
    public class CrimeDisplay
    {
        private int closeOperationId = 6;
        private CrimeBusiness crimeBusiness = new CrimeBusiness();

        public CrimeDisplay() { Input(); }

        private void ShowMenu()
        {
            Console.WriteLine("\n" + new string('-', 40));
            Console.WriteLine(new string(' ', 17) + "Crime" + new string(' ', 18));
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
            Console.WriteLine("Enter Crime ID to delete: ");
            crimeBusiness.Delete(int.Parse(Console.ReadLine()));
            Console.WriteLine("Done.");
        }

        private void Fetch()
        {
            Console.WriteLine("Enter Crime ID to fetch: ");
            Crime crime = crimeBusiness.Get(int.Parse(Console.ReadLine()));
            if (crime != null) Console.WriteLine($"ID: {crime.CrimeId} | Crime: {crime.Crime1}");
            else Console.WriteLine("Not found!");
        }

        private void Update()
        {
            Console.WriteLine("Enter Crime ID to update: ");
            Crime crime = crimeBusiness.Get(int.Parse(Console.ReadLine()));
            if (crime != null)
            {
                Console.WriteLine("Enter new Crime Name/Desc: ");
                crime.Crime1 = Console.ReadLine();
                crimeBusiness.Update(crime);
            }
        }

        private void Add()
        {
            Crime crime = new Crime();
            Console.WriteLine("Enter Crime Name/Desc: ");
            crime.Crime1 = Console.ReadLine();
            crimeBusiness.Add(crime);
        }

        private void ListAll()
        {
            foreach (var item in crimeBusiness.GetAll())
                Console.WriteLine($"{item.CrimeId} - {item.Crime1}");
        }
    }
}
