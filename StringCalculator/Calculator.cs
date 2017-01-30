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
            int returnNumber = 0;
            var split = input.Split(',');

            foreach(var item in split)
            {
                int number;
                var IsNumber = int.TryParse(item, out number);
                if (IsNumber) returnNumber = returnNumber + number;
            }

            return returnNumber;
        }
    }
}
