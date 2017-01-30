using System;
using System.Collections.Generic;
using System.Text;

namespace StringCalculator
{
    public interface ICalculator
    {
        int Calculate(string input);
    }

    public class Calculator : ICalculator
    {
        public int Calculate(string input)
        {
            var returnNumber = 0;
            char[] delimiters = { ',', '\n' };
            var negitives = new List<int>();

            if (input.StartsWith("//")) HandleCustomDelimiters(ref input, ref delimiters);

            var split = input.Split(delimiters);

            foreach(var item in split)
            {
                int number;
                var IsNumber = int.TryParse(item, out number);
                if (!IsNumber) continue;

                if (number < 0) negitives.Add(number);

                returnNumber = returnNumber + number;
            }

            if (negitives.Count > 0) HandleNegativeNumbers(negitives);

            return returnNumber;
        }

        private void HandleCustomDelimiters(ref string input, ref char[] delimiters)
        {
            input = input.Replace("//", string.Empty);

            var delim = input.Substring(0, input.IndexOf("\n"));

            delimiters = new char[] { char.Parse(delim) };

            input = input.Substring(input.IndexOf("\n") + 1, (input.Length - delim.Length - 1));
        }

        private void HandleNegativeNumbers(List<int> negitives)
        {
            var badnumbers = new StringBuilder();
            negitives.ForEach(o => badnumbers.Append(o + ","));
            throw new Exception(string.Concat("negatives not allowed, passed: ", badnumbers.ToString().TrimEnd(',')));
        }
    }
}
