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
        private int firstNumber;
        private int secondNumber;
        private string operation;

        //Constructor - runs when you create a new calculator
        public calculator()
        {
            firstNumber = 0;
            secondNumber = 0;
            operation = "";

        }

        //Public method - allows controlled access to set data
        public void SetFirstNumber(int number) {
            firstNumber = number;
        
        }

        public void SetSecondNumber(int number) {

            secondNumber = number;
        }

        public void SetOperation(String op) {

            operation = op;
            
        }

        //ABSTRACTION: simple method to hide complex logic
        public int Calculate()
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
        private int Add(int a, int b)
        {
            return a + b;
        }

        private int Subtract(int a, int b)
        {
            return a - b;
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



        public int GetResult() {

            return firstNumber;
        
        }

    }
}
