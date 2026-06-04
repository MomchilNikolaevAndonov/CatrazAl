using CatrazAl.Business;
using CatrazAl.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatrazAl.Presentation
{
    public class ShiftDisplay
    {
        private int closeOperationId = 6;
        private ShiftBusiness shiftBusiness = new ShiftBusiness();

        public ShiftDisplay() { Input(); }
        private void ShowMenu() { Console.WriteLine("\n--- Shift ---\n1. List all\n2. Add\n3. Update\n4. Fetch\n5. Delete\n6. Exit"); }

        private void Input()
        {
            var op = -1;
            do
            {
                ShowMenu();
                int.TryParse(Console.ReadLine(), out op);
                if (op == 1) ListAll(); else if (op == 2) Add(); else if (op == 3) Update(); else if (op == 4) Fetch(); else if (op == 5) Delete();
            } while (op != closeOperationId);
        }

        private void Delete() { Console.Write("Shift ID to delete: "); shiftBusiness.Delete(int.Parse(Console.ReadLine())); }

        private void Fetch()
        {
            Console.Write("Shift ID: "); Shift s = shiftBusiness.Get(int.Parse(Console.ReadLine()));
            if (s != null) Console.WriteLine($"{s.ShiftId}: {s.ShiftName} | Block: {s.PrisonBlockId} | {s.StartTime} to {s.EndTime}");
        }

        private void Update()
        {
            Console.Write("Shift ID to update: "); Shift s = shiftBusiness.Get(int.Parse(Console.ReadLine()));
            if (s != null)
            {
                Console.Write("Shift Name: "); s.ShiftName = Console.ReadLine();
                Console.Write("Start Time (yyyy-mm-dd hh:mm): "); if (DateTime.TryParse(Console.ReadLine(), out DateTime st)) s.StartTime = st;
                Console.Write("End Time (yyyy-mm-dd hh:mm): "); if (DateTime.TryParse(Console.ReadLine(), out DateTime et)) s.EndTime = et;
                Console.Write("Prison Block ID: "); s.PrisonBlockId = int.Parse(Console.ReadLine());
                shiftBusiness.Update(s);
            }
        }

        private void Add()
        {
            Shift s = new Shift();
            Console.Write("Shift Name: "); s.ShiftName = Console.ReadLine();
            Console.Write("Start Time (yyyy-mm-dd hh:mm): "); if (DateTime.TryParse(Console.ReadLine(), out DateTime st)) s.StartTime = st;
            Console.Write("End Time (yyyy-mm-dd hh:mm): "); if (DateTime.TryParse(Console.ReadLine(), out DateTime et)) s.EndTime = et;
            Console.Write("Prison Block ID: "); s.PrisonBlockId = int.Parse(Console.ReadLine());
            shiftBusiness.Add(s);
        }

        private void ListAll()
        {
            foreach (var s in shiftBusiness.GetAll()) Console.WriteLine($"{s.ShiftId} - {s.ShiftName} | Block {s.PrisonBlockId}");
        }
    }
}
