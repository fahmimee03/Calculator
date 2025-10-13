using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning
{
    internal class BasicCalculator
    {
        //Protected fields - accessible by child classes
        protected int firstNumber;
        protected int secondNumber;
        protected string operation;


        public BasicCalculator() {
            firstNumber = 0;
            secondNumber = 0;
            operation = "";
        
        }

        //Setter
        public void SetFirstNumber(int num) {
            firstNumber = num;
        
        }

        public void SetSecondNumber(int num)
        {
            secondNumber = num;

        }

        public void SetOperation(string op)
        {
            operation = op;
        }

        //VIRTUAL method - can be overridden by child classes
        public virtual int Calculate() {
            switch (operation)
            {
                case "+":
                    return Add(firstNumber, secondNumber);
                case "-":
                    return Subtract(firstNumber, secondNumber);
                case "*":
                    return Multiply(firstNumber, secondNumber);
                case "/":
                    return Divide(firstNumber, secondNumber);
                default:
                    Console.WriteLine("invalid Operation!");
                    return 0;
            }

        
        }

        //protected methods - accessible by child classes
        protected int Add(int a, int b)
        {
            return a + b;
        }

        protected int Subtract(int a, int b)
        {
            return a - b;
        }

        protected int Multiply(int a, int b)
        {
            return a * b;
        }

        protected int Divide(int a, int b)
        {
            if(b == 0)
            {
                Console.WriteLine("Cannot divide by zero!");
                return 0;
            }
            return a / b;
        }
    }
}
