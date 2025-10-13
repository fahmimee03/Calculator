using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Flag to control the loop
            bool continueLoop = true;

            do
            {
                //INSTANTIATION: Create an object from the class
                BasicCalculator calc = new BasicCalculator();
                NumberConverter converter = new NumberConverter();

                try
                {
                    Console.WriteLine("Enter the first number \n");

                    int num1 = int.Parse(Console.ReadLine());
                    calc.SetFirstNumber(num1);



                    //Get operation from the user
                    Console.WriteLine("Enter the operation: ( +, -, *, /, AND, OR ,XOR): ");
                    string op = Console.ReadLine().ToUpper();
                    calc.SetOperation(op);

                    //Get second number from user
                    Console.WriteLine("Enter the second number \n");
                    int num2 = int.Parse(Console.ReadLine());
                    calc.SetSecondNumber(num2);

                    int result = calc.Calculate();

                    Console.WriteLine("\n╔════════════════════════════╗");
                    Console.WriteLine("║         RESULTS            ║");
                    Console.WriteLine("╠════════════════════════════╣");
                    Console.WriteLine($"║Decimal:       {result, -13}║");
                    Console.WriteLine($"║Binary:        {converter.ToBinary(result),-13}║");
                    Console.WriteLine($"║Hexadecimal:   {converter.ToHexadecimal(result),-13}║");
                    Console.WriteLine($"║Octal:         {converter.ToOctal(result),-13}║");
                    Console.WriteLine("╚════════════════════════════╝");
                }catch (FormatException)
                {
                    Console.WriteLine("\nError: Please enter a valid numbers only!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nUnexepected error: {ex.Message}");
                }
                Console.WriteLine("Press Enter to continue or any key to exit...");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key != ConsoleKey.Enter)
                {
                    //set flag to false
                    continueLoop = false;
                    Console.WriteLine("\nExiting...");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("==Programmer Calculator==\n");
                }
            } while (continueLoop); 
        }
    }
}
