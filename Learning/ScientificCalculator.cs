using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning
{
    public class ScientificCalculator : BasicCalculator
    {
        public ScientificCalculator() : base() { }

        public override long Calculate() {
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

        private long Power(long baseNum, long exponent)
        {
            return (int)Math.Pow(baseNum, exponent);
        }

        private long Modulo(long a, long b)
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
