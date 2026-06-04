using CatrazAl.Business;
using CatrazAl.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatrazAl.Presentation
{
    public class CellDisplay
    {
        private int closeOperationId = 6;
        private CellBusiness cellBusiness = new CellBusiness();

        public CellDisplay() { Input(); }

        private void ShowMenu()
        {
            Console.WriteLine("\n" + new string('-', 40));
            Console.WriteLine("                 Cell                   ");
            Console.WriteLine(new string('-', 40));
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
            Console.WriteLine("Enter Cell ID to delete: ");
            cellBusiness.Delete(int.Parse(Console.ReadLine()));
        }

        private void Fetch()
        {
            Console.WriteLine("Enter Cell ID: ");
            Cell cell = cellBusiness.Get(int.Parse(Console.ReadLine()));
            if (cell != null) Console.WriteLine($"ID: {cell.CellId} | BlockId: {cell.PrisonBlockId} | Cap: {cell.Capacity} | Kind: {cell.Kind}");
        }

        private void Update()
        {
            Console.WriteLine("Enter Cell ID to update: ");
            Cell cell = cellBusiness.Get(int.Parse(Console.ReadLine()));
            if (cell != null)
            {
                Console.WriteLine("Enter PrisonBlockId: ");
                cell.PrisonBlockId = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Capacity: ");
                cell.Capacity = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Kind: ");
                cell.Kind = Console.ReadLine();
                cellBusiness.Update(cell);
            }
        }

        private void Add()
        {
            Cell cell = new Cell();
            Console.WriteLine("Enter PrisonBlockId: ");
            cell.PrisonBlockId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Capacity: ");
            if (int.TryParse(Console.ReadLine(), out int cap)) cell.Capacity = cap;
            Console.WriteLine("Enter Kind: ");
            cell.Kind = Console.ReadLine();
            cellBusiness.Add(cell);
        }

        private void ListAll()
        {
            foreach (var c in cellBusiness.GetAll())
                Console.WriteLine($"{c.CellId} - Block {c.PrisonBlockId} - {c.Kind} (Cap: {c.Capacity})");
        }
    }
}
