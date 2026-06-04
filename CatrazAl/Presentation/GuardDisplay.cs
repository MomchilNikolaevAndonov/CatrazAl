using CatrazAl.Business;
using CatrazAl.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatrazAl.Presentation
{
    public class GuardDisplay
    {
        private int closeOperationId = 6;
        private GuardBusiness guardBusiness = new GuardBusiness();

        public GuardDisplay() { Input(); }

        private void ShowMenu()
        {
            Console.WriteLine("\n--- Guard ---");
            Console.WriteLine("1. List all\n2. Add\n3. Update\n4. Fetch\n5. Delete\n6. Exit");
        }

        private void Input()
        {
            var op = -1;
            do
            {
                ShowMenu();
                int.TryParse(Console.ReadLine(), out op);
                if (op == 1) ListAll();
                else if (op == 2) Add();
                else if (op == 3) Update();
                else if (op == 4) Fetch(); else if (op == 5) Delete();
            } while (op != closeOperationId);
        }

        private void Delete()
        {
            Console.Write("Enter Guard ID: ");
            guardBusiness.Delete(int.Parse(Console.ReadLine()));
        }

        private void Fetch()
        {
            Console.Write("Enter Guard ID: ");
            Guard g = guardBusiness.Get(int.Parse(Console.ReadLine()));
            if (g != null) Console.WriteLine($"{g.GuardId}: {g.FirstName} {g.LastName} | Rank: {g.GuardRank} | Shift: {g.ShiftId}");
        }

        private void Update()
        {
            Console.Write("Enter Guard ID to update: ");
            Guard g = guardBusiness.Get(int.Parse(Console.ReadLine()));
            if (g != null)
            {
                Console.Write("First Name: "); g.FirstName = Console.ReadLine();
                Console.Write("Last Name: "); g.LastName = Console.ReadLine();
                Console.Write("Rank: "); g.GuardRank = Console.ReadLine();
                Console.Write("Phone: "); g.Phone = Console.ReadLine();
                Console.Write("Shift ID: "); g.ShiftId = int.Parse(Console.ReadLine());
                guardBusiness.Update(g);
            }
        }

        private void Add()
        {
            Guard g = new Guard();
            Console.Write("First Name: "); g.FirstName = Console.ReadLine();
            Console.Write("Last Name: "); g.LastName = Console.ReadLine();
            Console.Write("Rank: "); g.GuardRank = Console.ReadLine();
            Console.Write("Phone: "); g.Phone = Console.ReadLine();
            Console.Write("Shift ID: "); g.ShiftId = int.Parse(Console.ReadLine());
            guardBusiness.Add(g);
        }

        private void ListAll()
        {
            foreach (var g in guardBusiness.GetAll()) Console.WriteLine($"{g.GuardId} - {g.FirstName} {g.LastName} ({g.GuardRank})");
        }
    }
}
