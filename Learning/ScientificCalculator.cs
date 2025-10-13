using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning
{
    internal class ScientificCalculator : BasicCalculator
    {
        public ScientificCalculator() : base() { }

        public override int Calculate() {
            switch (operation)
            {
                case "POW":
                    return Power(firstNumber, secondNumber);
                case "MOD":
                    return Modulo(firstNumber, secondNumber);
                default:
                    return base.Calculate();
            }
        }

        private int Power(int baseNum, int exponent)
        {
            return (int)Math.Pow(baseNum, exponent);
        }

        private int Modulo(int a, int b)
        {
            if (b == 0)
            {
                Console.WriteLine("Cannot modulo by zero!");
                return 0;
            }
            return a % b;
        }

    }

}
