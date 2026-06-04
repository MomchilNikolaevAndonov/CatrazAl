using CatrazAl.Business;
using CatrazAl.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatrazAl.Presentation
{
    public class PunishmentDisplay
    {
        private int closeOperationId = 6;
        private PunishmentBusiness punishmentBusiness = new PunishmentBusiness();

        public PunishmentDisplay() { Input(); }
        private void ShowMenu() { Console.WriteLine("\n--- Punishment ---\n1. List all\n2. Add\n3. Update\n4. Fetch\n5. Delete\n6. Exit"); }

        private void Input()
        {
            var op = -1;
            do
            {
                ShowMenu(); int.TryParse(Console.ReadLine(), out op);
                if (op == 1) ListAll(); else if (op == 2) Add(); else if (op == 3) Update(); else if (op == 4) Fetch(); else if (op == 5) Delete();
            } while (op != closeOperationId);
        }

        private void Delete() { Console.Write("Punishment ID to delete: "); punishmentBusiness.Delete(int.Parse(Console.ReadLine())); }

        private void Fetch()
        {
            Console.Write("ID: "); Punishment p = punishmentBusiness.Get(int.Parse(Console.ReadLine()));
            if (p != null) Console.WriteLine($"{p.PunishmentId}: Prisoner {p.PrisonerId} | {p.PunishmentType} for {p.PunishmentDays} days | Reason: {p.Reason}");
        }

        private void Update()
        {
            Console.Write("ID to update: "); Punishment p = punishmentBusiness.Get(int.Parse(Console.ReadLine()));
            if (p != null)
            {
                Console.Write("Prisoner ID: "); p.PrisonerId = int.Parse(Console.ReadLine());
                Console.Write("Reason: "); p.Reason = Console.ReadLine();
                Console.Write("Days: "); p.PunishmentDays = int.Parse(Console.ReadLine());
                Console.Write("Start Date (yyyy-mm-dd): "); p.StartDate = DateTime.Parse(Console.ReadLine());
                Console.Write("End Date (yyyy-mm-dd): "); p.EndDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Type: "); p.PunishmentType = Console.ReadLine();
                punishmentBusiness.Update(p);
            }
        }

        private void Add()
        {
            Punishment p = new Punishment();
            Console.Write("Prisoner ID: "); p.PrisonerId = int.Parse(Console.ReadLine());
            Console.Write("Reason: "); p.Reason = Console.ReadLine();
            Console.Write("Days: "); p.PunishmentDays = int.Parse(Console.ReadLine());
            Console.Write("Start Date (yyyy-mm-dd): "); p.StartDate = DateTime.Parse(Console.ReadLine());
            Console.Write("End Date (yyyy-mm-dd): "); p.EndDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Type: "); p.PunishmentType = Console.ReadLine();
            punishmentBusiness.Add(p);
        }

        private void ListAll()
        {
            foreach (var p in punishmentBusiness.GetAll()) Console.WriteLine($"{p.PunishmentId} - Prisoner {p.PrisonerId} ({p.PunishmentType})");
        }
    }
}
