using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Day05
{
    class Polymer
    {
        static void Main(string[] args)
        {
            var input = ReadInputFile();
            var result = Part1(input);
            Console.WriteLine($"Part1: {result}");
        }

        public static int Part1(string input)
        {
            while (true)
            {
                var done = true;
                for (var i = 0; i < input.Length - 1;)
                {
                    // compare two parts
                    var one = (int)input[i];
                    var two = (int)input[i + 1];
                    var diff = Math.Abs(one - two);
                    if (diff == 32)
                    {
                        input = input.Remove(i, 2);
                        done = false;
                    }
                    else
                    {
                        i++;
                    }
                }

                if (done)
                    break;
            }
            

            return input.Length;
        }

        public static int Part2(string input)
        {
            var start = 65;
            var end = 90;

            return 0;
        }

        private static string ReadInputFile()
        {
            return File.ReadAllLines("Input.txt").ToList()[0];
        }
    }

    [TestFixture]
    public class PolymerTests
    {
        private const string TestInput = "dabAcCaCBAcCcaDA";
        
        [Test]
        public void Part1()
        {
            var result = Polymer.Part1(TestInput);
            Assert.That(result.Equals("dabCBAcaDA"));
        }

        [Test]
        public void Part2()
        {
            var result = Polymer.Part2(TestInput);
            Assert.That(result == 4);
        }
    }
}
