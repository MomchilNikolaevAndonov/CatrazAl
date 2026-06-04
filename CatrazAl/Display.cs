using CatrazAl.Presentation;
using Microsoft.Data.SqlClient;
using System;

namespace CatrazAl
{
    public class Display
    {
        static SqlConnection dbCon = new SqlConnection("Server=DESKTOP-7G89GA4\\SQLEXPRESS; Database=prison_db; Integrated Security=true; TrustServerCertificate=True;");

        public Display()
        {
            if (dbCon.State != System.Data.ConnectionState.Open)
            {
                dbCon.Open();
            }
            Input();
        }

        private void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine(LogoTitle());
            Console.WriteLine("=== MAIN MENU ===");
            Console.WriteLine("1. Manage Prisoners");
            Console.WriteLine("2. Manage Cells");
            Console.WriteLine("3. Manage Prison Blocks");
            Console.WriteLine("4. Manage Guards");
            Console.WriteLine("5. Manage Shifts");
            Console.WriteLine("6. Manage Crimes");
            Console.WriteLine("7. Manage Punishments");
            Console.WriteLine("8. Manage Medical Records");
            Console.WriteLine("9. Manage Visits");
            Console.WriteLine("10. Exit Application");
            Console.Write("\nSelect an option (1-10): ");
        }
        private string LogoTitle()
        {
            return @"
 ____              __                           ______   ___      
/\  _`\           /\ \__                       /\  _  \ /\_ \     
\ \ \/\_\     __  \ \ ,_\  _ __    __     ____ \ \ \L\ \\//\ \    
 \ \ \/_/_  /'__`\ \ \ \/ /\`'__\/'__`\  /\_ ,`\\ \  __ \ \ \ \   
  \ \ \L\ \/\ \L\.\_\ \ \_\ \ \//\ \L\.\_\/_/  /_\ \ \/\ \ \_\ \_ 
   \ \____/\ \__/.\_\\ \__\\ \_\\ \__/.\_\ /\____\\ \_\ \_\/\____\
    \/___/  \/__/\/_/ \/__/ \/_/ \/__/\/_/ \/____/ \/_/\/_/\/____/                                                   
                                                                  
            ";
        }

        private void Input()
        {
            var operation = -1;
            do
            {
                ShowMenu();
                if (int.TryParse(Console.ReadLine(), out operation))
                {
                    switch (operation)
                    {
                        case 1:
                            new PrisonerDisplay();
                            break;
                        case 2:
                            new CellDisplay();
                            break;
                        case 3:
                            new PrisonBlockDisplay();
                            break;
                        case 4:
                            new GuardDisplay();
                            break;
                        case 5:
                            new ShiftDisplay();
                            break;
                        case 6:
                            new CrimeDisplay();
                            break;
                        case 7:
                            new PunishmentDisplay();
                            break;
                        case 8:
                            new MedicalRecordDisplay();
                            break;
                        case 9:
                            new VisitDisplay();
                            break;
                        case 10:
                            Console.WriteLine("Exiting application. Goodbye!");
                            break;
                        default:
                            Console.WriteLine("Invalid selection. Press any key to try again.");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number. Press any key to try again.");
                    Console.ReadKey();
                }
            } while (operation != 10);
        }
    }
}