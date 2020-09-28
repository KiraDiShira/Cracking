using System;
using System.Text;

namespace ArraysAndStrings
{
    public class OneDotSix
    {
        private string Compress(string input)
        {
            if (input.Length < 2)
            {
                return input;
            }

            var builder = new StringBuilder();
            int charCounter = 0;
            for (int i = 0; i < input.Length; i++)
            {
                char character = input[i];
                charCounter++;

                if (i + 1 >= input.Length || character != input[i + 1])
                {
                    builder.Append(character.ToString() + charCounter);
                    charCounter = 0;
                }
            }

            string output = builder.ToString();
            if (output.Length > input.Length)
            {
                return input;
            }

            return builder.ToString();
        }

        public void Run()
        {
            string input = "aabcccccaaa";
            string output = Compress(input);
            Console.WriteLine(output);
            Console.ReadLine();
        }
    }
}
