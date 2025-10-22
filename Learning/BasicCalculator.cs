using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning
{
    public class BasicCalculator
    {
        //Protected fields - accessible by child classes
        protected long firstNumber;
        protected long secondNumber;
        protected string operation;


        public BasicCalculator() {
            firstNumber = 0;
            secondNumber = 0;
            operation = "";
        
        }

        //Setter
        public void SetFirstNumber(long num) {
            firstNumber = num;
        
        }

        public void SetSecondNumber(long num)
        {
            secondNumber = num;

        }

        public void SetOperation(string op)
        {
            operation = op;
        }

        //VIRTUAL method - can be overridden by child classes
        public virtual long Calculate() {
            switch (operation)
            {
                case "ADD":
                case "+":
                    return Add(firstNumber, secondNumber);
                case "SUBTRACT":
                case "-":
                    return Subtract(firstNumber, secondNumber);
                case "MULTIPLY":
                case "*":
                    return Multiply(firstNumber, secondNumber);
                case "DIVIDE":
                case "/":
                    return Divide(firstNumber, secondNumber);
                default:
                    throw new InvalidOperationException($"Invalid Operation: {operation}");
            }

        
        }

        //protected methods - accessible by child classes
        protected long Add(long a, long b)
        {
            return a + b;
        }

        protected long Subtract(long a, long b)
        {
            return a - b;
        }

        protected long Multiply(long a, long b)
        {
            return a * b;
        }

        protected long Divide(long a, long b)
        {
            if(b == 0)
            {
                throw new DivideByZeroException();
            }
            return a / b;
        }
    }
}
