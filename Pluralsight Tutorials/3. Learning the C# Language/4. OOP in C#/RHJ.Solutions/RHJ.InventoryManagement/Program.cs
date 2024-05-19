namespace RHJ.InventoryManagement
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine(PersonalCouter(6));
            Console.ReadLine();
        }

        static int PersonalCouter(int num)
        {
            Console.WriteLine(num);
            return num == 10 ? num : PersonalCouter(num + 1);
        }
    }
}
