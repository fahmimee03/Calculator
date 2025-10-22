using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Learning
{
    public class ProgrammerCalculator : BasicCalculator
    {
        public ProgrammerCalculator() : base() { }

        public override long Calculate() {
            switch (operation) {
                case "AND":
                    return BitwiseAnd(firstNumber, secondNumber);
                case "OR":
                    return BitwiseOr(firstNumber, secondNumber);
                case "XOR":
                    return BitwiseXor(firstNumber, secondNumber);
                default:
                    return base.Calculate();
            }

        }

        private long BitwiseAnd(long a, long b)
        {
            return a & b;
        }

        private long BitwiseOr(long a, long b)
        {
            return a | b;
        }

        private long BitwiseXor(long a, long b)
        {
            return a ^ b;
        }

    }
}
