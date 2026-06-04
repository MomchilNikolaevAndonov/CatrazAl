using CatrazAl.Business;
using CatrazAl.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatrazAl.Presentation
{
    public class VisitDisplay
    {
        private int closeOperationId = 6;
        private VisitBusiness visitBusiness = new VisitBusiness();

        public VisitDisplay() { Input(); }
        private void ShowMenu() { Console.WriteLine("\n--- Visit ---\n1. List all\n2. Add\n3. Update\n4. Fetch\n5. Delete\n6. Exit"); }

        private void Input()
        {
            var op = -1;
            do
            {
                ShowMenu(); int.TryParse(Console.ReadLine(), out op);
                if (op == 1) ListAll(); else if (op == 2) Add(); else if (op == 3) Update(); else if (op == 4) Fetch(); else if (op == 5) Delete();
            } while (op != closeOperationId);
        }

        private void Delete() { Console.Write("Visit ID to delete: "); visitBusiness.Delete(int.Parse(Console.ReadLine())); }

        private void Fetch()
        {
            Console.Write("ID: "); Visit v = visitBusiness.Get(int.Parse(Console.ReadLine()));
            if (v != null) Console.WriteLine($"{v.VisitId}: Prisoner {v.PrisonerId} | Visitor: {v.VisitorFirstName} {v.VisitorLastName} ({v.VisitorRelation})");
        }

        private void Update()
        {
            Console.Write("ID to update: "); Visit v = visitBusiness.Get(int.Parse(Console.ReadLine()));
            if (v != null)
            {
                Console.Write("Prisoner ID: "); v.PrisonerId = int.Parse(Console.ReadLine());
                Console.Write("Visitor First Name: "); v.VisitorFirstName = Console.ReadLine();
                Console.Write("Visitor Last Name: "); v.VisitorLastName = Console.ReadLine();
                Console.Write("Relation: "); v.VisitorRelation = Console.ReadLine();
                Console.Write("Date (yyyy-mm-dd): "); v.VisitDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Duration (Mins): "); v.DurationMinuits = int.Parse(Console.ReadLine());
                visitBusiness.Update(v);
            }
        }

        private void Add()
        {
            Visit v = new Visit();
            Console.Write("Prisoner ID: "); v.PrisonerId = int.Parse(Console.ReadLine());
            Console.Write("Visitor First Name: "); v.VisitorFirstName = Console.ReadLine();
            Console.Write("Visitor Last Name: "); v.VisitorLastName = Console.ReadLine();
            Console.Write("Relation: "); v.VisitorRelation = Console.ReadLine();
            Console.Write("Date (yyyy-mm-dd): "); v.VisitDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Duration (Mins): "); v.DurationMinuits = int.Parse(Console.ReadLine());
            visitBusiness.Add(v);
        }

        private void ListAll() { foreach (var v in visitBusiness.GetAll()) Console.WriteLine($"{v.VisitId} - Prisoner {v.PrisonerId} visited by {v.VisitorFirstName}"); }
    }
}
