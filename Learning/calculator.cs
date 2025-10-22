using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning
{
    internal class calculator
    {
        //ENCAPSULATION : PRIVATE FIELD (DATA) - HIDDEN FROM THE OUTSIDE... like package
        private long firstNumber;
        private long secondNumber;
        private string operation;

        //Constructor - runs when you create a new calculator
        public calculator()
        {
            firstNumber = 0;
            secondNumber = 0;
            operation = "";

        }

        //Public method - allows controlled access to set data
        public void SetFirstNumber(long number) {
            firstNumber = number;
        
        }

        public void SetSecondNumber(long number) {

            secondNumber = number;
        }

        public void SetOperation(String op) {

            operation = op;
            
        }

        //ABSTRACTION: simple method to hide complex logic
        public long Calculate()
        {
            //Decide which operation to perform
            switch (operation) {
                case "+":
                    return Add(firstNumber, secondNumber);
                case "-":
                    return Subtract(firstNumber, secondNumber);
                case "AND":
                    return BitwiseAnd(firstNumber, secondNumber);
                case "OR":
                    return BitwiseOr(firstNumber, secondNumber);
                case "XOR":
                    return BitwiseXor(firstNumber, secondNumber);
                default:
                    Console.WriteLine("Invalid Operation");
                    return 0;
            
            }
        }

        //ABSTRACTION: Private helper methods (hide complexity)
        private long Add(long a, long b)
        {
            return a + b;
        }

        private long Subtract(long a, long b)
        {
            return a - b;
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



        public long GetResult() {

            return firstNumber;
        
        }

    }
}
