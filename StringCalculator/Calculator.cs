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
            string[] delimiters = { ",", "\n" };
            var negitives = new List<int>();

            if (input.StartsWith("//")) HandleCustomDelimiters(ref input, ref delimiters);

            var split = input.Split(delimiters, int.MaxValue, StringSplitOptions.RemoveEmptyEntries);

            foreach(var item in split)
            {
                int number;
                var IsNumber = int.TryParse(item, out number);
                if (!IsNumber || number > 1000) continue;

                if (number < 0) negitives.Add(number);

                returnNumber = returnNumber + number;
            }

            if (negitives.Count > 0) HandleNegativeNumbers(negitives);

            return returnNumber;
        }

        private void HandleCustomDelimiters(ref string input, ref string[] delimiters)
        {
            input = input.Replace("//[", string.Empty);

            var delim = input.Substring(0, input.IndexOf("]\n"));

            if (input.Contains("]["))
            {
                var split = delim.Split(new string[] { "][" }, int.MaxValue, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in split) AddToStringArray(ref delimiters, item);
            }
            else
            {
                AddToStringArray(ref delimiters, delim);
            }

            input = input.Substring(input.IndexOf("]\n") + 2, (input.Length - delim.Length - 2));
        }

        private void HandleNegativeNumbers(List<int> negitives)
        {
            var badnumbers = new StringBuilder();
            negitives.ForEach(o => badnumbers.Append(o + ","));
            throw new Exception(string.Concat("negatives not allowed, passed: ", badnumbers.ToString().TrimEnd(',')));
        }

        private void AddToStringArray(ref string[] delimiters, string item)
        {
            Array.Resize(ref delimiters, delimiters.Length + 1);
            delimiters[delimiters.Length - 1] = item;
        }
    }
}
