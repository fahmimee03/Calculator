using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Learning
{
    internal class ProgrammerCalculator : BasicCalculator
    {
        public ProgrammerCalculator() : base() { }

        public override int Calculate() {
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

        private int BitwiseAnd(int a, int b)
        {
            return a & b;
        }

        private int BitwiseOr(int a, int b)
        {
            return a | b;
        }

        private int BitwiseXor(int a, int b)
        {
            return a ^ b;
        }

    }
}
