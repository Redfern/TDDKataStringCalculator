using System;

namespace StringCalculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("String calculator");
            Console.WriteLine("********************************************************");
            Console.WriteLine("");

            var input = "1,2,3";
            var output = new Calculator().Calculate(input);
            Console.WriteLine(string.Concat("For ", input, "we got.. ", output));

            // keep the console open
            Console.WriteLine("");
            Console.WriteLine("********************************************************");
            Console.WriteLine("---> Press enter to close console");
            // keep the console open
            Console.Read();
        }
    }
}
