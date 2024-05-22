
using RHJ.InventoryManagement.Domain;

namespace RHJ.InventoryManagement
{
    public class Program
    {
        static void Main(string[] args)
        {

            PrintingWelcome();

            Utilities.InitializeStock();

            Utilities.ShowMainMenu();

            Console.WriteLine("Application shutting down...");

            Console.ReadLine();

        }

        #region -Layout
        static void PrintingWelcome()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"Welcome to RHJ Solutions");
            Console.ResetColor();

            Console.WriteLine("Press Enter key to start logging in!");

            //accepting enter here
            Console.ReadLine();

            Console.Clear();

        }

        #endregion
    }
}
