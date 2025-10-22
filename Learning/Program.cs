using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning
{
    public class Program
    {
       /* static void Main(string[] args)
        {


            //INSTANTIATION: Create an object from the class
            BasicCalculator calc;
            NumberConverter converter = new NumberConverter();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════╗");
                Console.WriteLine("║     CALCULATOR MENU SYSTEM         ║");
                Console.WriteLine("╠════════════════════════════════════╣");
                Console.WriteLine("║ 1. Basic Calculator                ║");
                Console.WriteLine("║ 2. Programmer's Calculator         ║");
                Console.WriteLine("║ 3. Scientific Calculator           ║");
                Console.WriteLine("║ 4. Exit                            ║");
                Console.WriteLine("╚════════════════════════════════════╝");
                Console.Write("\nChoose calculator type: ");

                string choice = Console.ReadLine();
                if (choice == "4")
                {
                    Console.WriteLine("\nThank you for using the calculator.");
                    break;
                }

                switch (choice)
                {
                    case "1":
                        calc = new BasicCalculator();
                        Console.WriteLine("\nBasic Calculator selected");
                        Console.WriteLine("Available Operation : + , - , * , /");
                        break;
                    case "2":
                        calc = new ProgrammerCalculator();
                        Console.WriteLine("\nProgrammer Calculator selected");
                        Console.WriteLine("Available operations: + , - , * , / , AND , OR , XOR");
                        break;
                    case "3":
                        calc = new ScientificCalculator();
                        Console.WriteLine("\nScientific Calculator selected");
                        Console.WriteLine("Available operations: + , - , * , / , POW , MOD");
                        break;
                    default:
                        Console.WriteLine("invalid input! please try again.");
                        Console.ReadKey();
                        continue;
                }

                UseCalculator(calc, converter);

                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();

            }

        }

        static void UseCalculator(BasicCalculator calc, NumberConverter converter)
        {
            try
            {
                    Console.WriteLine("Enter the first number \n");
                    int num1 = int.Parse(Console.ReadLine());
                    calc.SetFirstNumber(num1);



                    //Get operation from the user
                    Console.WriteLine("Enter the operation: ");
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
                    Console.WriteLine($"║Decimal:       {result,-13}║");
                    Console.WriteLine($"║Binary:        {converter.ToBinary(result),-13}║");
                    Console.WriteLine($"║Hexadecimal:   {converter.ToHexadecimal(result),-13}║");
                    Console.WriteLine($"║Octal:         {converter.ToOctal(result),-13}║");
                    Console.WriteLine("╚════════════════════════════╝");
            }
            catch (FormatException)
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
                Console.WriteLine("\nExiting...");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("==Programmer Calculator==\n");
            }
        }*/

    }
}
