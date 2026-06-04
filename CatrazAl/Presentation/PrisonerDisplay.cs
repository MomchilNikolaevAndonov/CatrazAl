using CatrazAl.Business;
using CatrazAl.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatrazAl.Presentation
{
    public class PrisonerDisplay
    {
        private int closeOperationId = 6;
        private PrisonerBusiness prisonerBusiness = new PrisonerBusiness();

        public PrisonerDisplay() { Input(); }
        private void ShowMenu() { Console.WriteLine("\n--- Prisoner ---\n1. List all\n2. Add\n3. Update\n4. Fetch\n5. Delete\n6. Exit"); }

        private void Input()
        {
            var op = -1;
            do
            {
                ShowMenu(); int.TryParse(Console.ReadLine(), out op);
                if (op == 1) ListAll(); else if (op == 2) Add(); else if (op == 3) Update(); else if (op == 4) Fetch(); else if (op == 5) Delete();
            } while (op != closeOperationId);
        }

        private void Delete() { Console.Write("Prisoner ID to delete: "); prisonerBusiness.Delete(int.Parse(Console.ReadLine())); }

        private void Fetch()
        {
            Console.Write("ID: "); Prisoner p = prisonerBusiness.Get(int.Parse(Console.ReadLine()));
            if (p != null) Console.WriteLine($"{p.PrisonerId}: {p.FirstName} {p.LastName} | EGN: {p.Egn} | Crime ID: {p.CrimeId} | Cell: {p.CellId}");
        }

        private void Update()
        {
            Console.Write("ID to update: "); Prisoner p = prisonerBusiness.Get(int.Parse(Console.ReadLine()));
            if (p != null)
            {
                Console.Write("First Name: "); p.FirstName = Console.ReadLine();
                Console.Write("Last Name: "); p.LastName = Console.ReadLine();
                Console.Write("EGN: "); p.Egn = Console.ReadLine();
                Console.Write("DOB (yyyy-mm-dd): "); p.DateOfBirth = DateOnly.Parse(Console.ReadLine());
                Console.Write("Gender: "); p.Gender = Console.ReadLine();
                Console.Write("Crime ID: "); p.CrimeId = int.Parse(Console.ReadLine());
                Console.Write("Sentence Months: "); p.SentenceMonths = int.Parse(Console.ReadLine());
                Console.Write("Sentence Start (yyyy-mm-dd): "); p.SentenceStart = DateOnly.Parse(Console.ReadLine());
                Console.Write("Sentence End (yyyy-mm-dd): "); p.SentenceEnd = DateOnly.Parse(Console.ReadLine());
                Console.Write("Cell ID: "); p.CellId = int.Parse(Console.ReadLine());
                Console.Write("Block ID: "); p.PrisonBlockId = int.Parse(Console.ReadLine());
                Console.Write("Released (true/false): "); p.Released = bool.Parse(Console.ReadLine());
                prisonerBusiness.Update(p);
            }
        }

        private void Add()
        {
            Prisoner p = new Prisoner();
            Console.Write("First Name: "); p.FirstName = Console.ReadLine();
            Console.Write("Last Name: "); p.LastName = Console.ReadLine();
            Console.Write("EGN: "); p.Egn = Console.ReadLine();
            Console.Write("DOB (yyyy-mm-dd): "); p.DateOfBirth = DateOnly.Parse(Console.ReadLine());
            Console.Write("Gender: "); p.Gender = Console.ReadLine();
            Console.Write("Crime ID: "); p.CrimeId = int.Parse(Console.ReadLine());
            Console.Write("Sentence Months: "); p.SentenceMonths = int.Parse(Console.ReadLine());
            Console.Write("Sentence Start (yyyy-mm-dd): "); p.SentenceStart = DateOnly.Parse(Console.ReadLine());
            Console.Write("Sentence End (yyyy-mm-dd): "); p.SentenceEnd = DateOnly.Parse(Console.ReadLine());
            Console.Write("Cell ID: "); p.CellId = int.Parse(Console.ReadLine());
            Console.Write("Block ID: "); p.PrisonBlockId = int.Parse(Console.ReadLine());
            Console.Write("Released (true/false): "); p.Released = bool.Parse(Console.ReadLine());
            prisonerBusiness.Add(p);
        }

        private void ListAll() { foreach (var p in prisonerBusiness.GetAll()) Console.WriteLine($"{p.PrisonerId} - {p.FirstName} {p.LastName}"); }
    }
}
