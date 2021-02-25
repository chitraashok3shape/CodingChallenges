using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(CheckIfStringIsCorrect1("[{]}"));//false
            Console.WriteLine(CheckIfStringIsCorrect1("[{}]"));//true
            Console.WriteLine(CheckIfStringIsCorrect1("([{)]}"));//false
            Console.WriteLine(CheckIfStringIsCorrect1("()"));//true
            Console.WriteLine(CheckIfStringIsCorrect1("({[first]})"));//true
            Console.WriteLine(CheckIfStringIsCorrect1("([{ order]})"));//false

            Console.ReadKey();

        }

        private bool CheckIfStringIsCorrect(string stringToCheck)
        {
            var counOpeningst = 0;
            var counEndings = 0;

            for (var i = 0; i >= stringToCheck.Length - 1; i++)
            {
                if (counEndings > counOpeningst)
                {
                    return false;
                }

                if (stringToCheck[i] == '(')
                {
                    counOpeningst++;

                }

                if (stringToCheck[i] == ')')
                {
                    counEndings++;

                }
            }

            return counOpeningst == counEndings;
        }

        static Dictionary<char, int> _inputDictionary = new Dictionary<char, int>();
        private static bool CheckIfStringIsCorrect1(string stringToCheck)
        {
            Stack<char> stackIn = new Stack<char>();

            foreach (char c in stringToCheck)
            {
                if (c == '(' || c == '{' || c == '[')
                    stackIn.Push(c);
                else if (c == ')' || c == '}' || c == ']')
                {
                    return GetOppMatch(c).Equals(stackIn.Pop());
                }
            }

            return false;
        }

        private static char GetOppMatch(char c)
        {
            if (c == '{')
                return '}';
            if (c == '(')
                return ')';
            if (c == '[')
                return ']';
            if (c == '}')
                return '{';
            if (c == ')')
                return '(';
            if (c == ']')
                return '[';

            return 'n';
        }



        private static Juice PourOJ()
        {
            Console.WriteLine("Pouring orange juice");
            return new Juice();
        }

        private static void ApplyJam(Toast toast) =>
            Console.WriteLine("Putting jam on the toast");

        private static void ApplyButter(Toast toast) =>
            Console.WriteLine("Putting butter on the toast");

        private static async Task<Toast> ToastBreadAsync(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            await Task.Delay(3000);
            Console.WriteLine("Remove toast from toaster");

            return new Toast();
        }
        private static Toast ToastBread(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            Task.Delay(3000).Wait();
            Console.WriteLine("Remove toast from toaster");

            return new Toast();
        }

        private static async Task<Bacon> FryBaconAsync(int slices)
        {
            Console.WriteLine($"putting {slices} slices of bacon in the pan");
            Console.WriteLine("cooking first side of bacon...");
            await Task.Delay(3000);
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("flipping a slice of bacon");
            }
            Console.WriteLine("cooking the second side of bacon...");
            await Task.Delay(3000);
            Console.WriteLine("Put bacon on plate");

            return new Bacon();
        }
        private static Bacon FryBacon(int slices)
        {
            Console.WriteLine($"putting {slices} slices of bacon in the pan");
            Console.WriteLine("cooking first side of bacon...");
            Task.Delay(3000).Wait();
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("flipping a slice of bacon");
            }
            Console.WriteLine("cooking the second side of bacon...");
            Task.Delay(3000).Wait();
            Console.WriteLine("Put bacon on plate");

            return new Bacon();
        }

        private static async Task<Egg> FryEggsAsync(int howMany)
        {
            Console.WriteLine("Warming the egg pan...");
            await Task.Delay(3000);
            Console.WriteLine($"cracking {howMany} eggs");
            Console.WriteLine("cooking the eggs ...");
            await Task.Delay(3000);
            Console.WriteLine("Put eggs on plate");

            return new Egg();
        }
        private static async Task<Egg> FryEggs(int howMany)
        {
            Console.WriteLine("Warming the egg pan...");
            await Task.Delay(3000);
            Console.WriteLine($"cracking {howMany} eggs");
            Console.WriteLine("cooking the eggs ...");
            await Task.Delay(3000);
            Console.WriteLine("Put eggs on plate");

            return new Egg();
        }

        private static Coffee PourCoffee()
        {
            Console.WriteLine("Pouring coffee");
            return new Coffee();
        }
    }

    internal class Toast
    {
    }

    internal class Juice
    {
    }

    internal class Bacon
    {
    }

    internal class Coffee
    {
    }

    internal class Egg
    {
    }


}
