using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning
{
    public class NumberConverter
    {
        public string ToBinary(long number)
        {
            if (number == 0) return "0";

            string binary = "";
            long temp = number;

            //Handle negative numbers
            if (temp < 0)
            {
                //for simplicity, we'll show absolute value
                temp = Math.Abs(temp);
                binary = "-";
            }
            while (temp > 0)
            {
                long remainder = temp % 2;
                binary = remainder + binary;
                temp = temp / 2;
            }

            return binary;
        }

        public string ToHexadecimal(long number) {
            if (number == 0) return "0";

            string hex = "";
            long temp = Math.Abs(number);
            string hexChars = "0123456789ABCDEF";

            while (temp > 0)
            {
                long remainder = temp % 16;
                hex = hexChars[(int)remainder] + hex;
                temp = temp / 16;
            }

            if (number < 0)
                hex = "-" + hex;

            return hex;

        }

        public string ToOctal(long number)
        {
            if (number ==0) return "0";

            string octal = "";
            long temp = Math.Abs(number);

            while (temp > 0)
            {
                long remainder = temp % 8;
                octal = remainder + octal;
                temp = temp / 8;
            }

            if (number < 0)
                octal = "-" + octal;

            return octal;
        }

    }
}
