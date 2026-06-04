using CatrazAl.Business;
using CatrazAl.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatrazAl.Presentation
{
    public class MedicalRecordDisplay
    {
        private int closeOperationId = 6;
        private MedicalRecordBusiness medicalBusiness = new MedicalRecordBusiness();

        public MedicalRecordDisplay() { Input(); }
        private void ShowMenu() { Console.WriteLine("\n--- Medical Record ---\n1. List all\n2. Add\n3. Update\n4. Fetch\n5. Delete\n6. Exit"); }

        private void Input()
        {
            var op = -1;
            do
            {
                ShowMenu(); int.TryParse(Console.ReadLine(), out op);
                if (op == 1) ListAll(); else if (op == 2) Add(); else if (op == 3) Update(); else if (op == 4) Fetch(); else if (op == 5) Delete();
            } while (op != closeOperationId);
        }

        private void Delete() { Console.Write("Record ID to delete: "); medicalBusiness.Delete(int.Parse(Console.ReadLine())); }

        private void Fetch()
        {
            Console.Write("ID: "); MedicalRecord m = medicalBusiness.Get(int.Parse(Console.ReadLine()));
            if (m != null) Console.WriteLine($"{m.RecordId}: Prisoner {m.PrisonerId} | Diagnosis: {m.Diagnosis} | Dr. {m.DoctorLastName}");
        }

        private void Update()
        {
            Console.Write("ID to update: "); MedicalRecord m = medicalBusiness.Get(int.Parse(Console.ReadLine()));
            if (m != null)
            {
                Console.Write("Prisoner ID: "); m.PrisonerId = int.Parse(Console.ReadLine());
                Console.Write("Diagnosis: "); m.Diagnosis = Console.ReadLine();
                Console.Write("Treatment: "); m.Treatment = Console.ReadLine();
                Console.Write("Treatment Days: "); m.TreatmentDays = int.Parse(Console.ReadLine());
                Console.Write("Dr First Name: "); m.DoctorFirstName = Console.ReadLine();
                Console.Write("Dr Last Name: "); m.DoctorLastName = Console.ReadLine();
                Console.Write("Date (yyyy-mm-dd): "); m.RecordDate = DateTime.Parse(Console.ReadLine());
                medicalBusiness.Update(m);
            }
        }

        private void Add()
        {
            MedicalRecord m = new MedicalRecord();
            Console.Write("Prisoner ID: "); m.PrisonerId = int.Parse(Console.ReadLine());
            Console.Write("Diagnosis: "); m.Diagnosis = Console.ReadLine();
            Console.Write("Treatment: "); m.Treatment = Console.ReadLine();
            Console.Write("Treatment Days: "); m.TreatmentDays = int.Parse(Console.ReadLine());
            Console.Write("Dr First Name: "); m.DoctorFirstName = Console.ReadLine();
            Console.Write("Dr Last Name: "); m.DoctorLastName = Console.ReadLine();
            Console.Write("Date (yyyy-mm-dd): "); m.RecordDate = DateTime.Parse(Console.ReadLine());
            medicalBusiness.Add(m);
        }

        private void ListAll() { foreach (var m in medicalBusiness.GetAll()) Console.WriteLine($"{m.RecordId} - Prisoner {m.PrisonerId} ({m.Diagnosis})"); }
    }
}
