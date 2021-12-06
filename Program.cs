using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Day2P2();
        }

        #region Day1
        
        public static void Day1P1()
        {
            var input = File.ReadAllLines("../../inputDay1.txt");
            var increases = 0;
            var prevLine = -1;
            foreach (var line in input)
            {
                var amount = int.Parse(line);
                if (prevLine != -1)
                {
                    if (amount > prevLine)
                    {
                        increases++;
                    }
                }
                prevLine = amount;
            }
            Console.WriteLine(increases);
        }

        static void Day1P2()
        {
            var input = File.ReadAllLines("../../inputDay1.txt");
            var increases = 0;
            var prevLines = new List<int>();
            foreach (var line in input)
            {
                var amount = int.Parse(line);
                if (prevLines.Count < 3)
                {
                    prevLines.Add(amount);
                }
                else
                {
                    var prevNumber = prevLines.Sum();
                    prevLines.RemoveAt(0);
                    prevLines.Add(amount);
                    var newNumber = prevLines.Sum();
                    if (newNumber > prevNumber)
                    {
                        increases++;
                    }
                }
            }
            Console.WriteLine(increases);
        }
        
        #endregion
        
        #region Day2

        static void Day2P1()
        {
            var input = File.ReadAllLines("../../inputDay2.txt");
            var horizontal = 0;
            var depth = 0;
            foreach (var line in input)
            {
                var commands = line.Split(' ');
                var command = commands[0];
                var amount = int.Parse(commands[1]);

                if (command == "forward")
                {
                    horizontal += amount;
                } else if (command == "up")
                {
                    depth -= amount;
                } else if (command == "down")
                {
                    depth += amount;
                }
            }
            Console.WriteLine(horizontal*depth);
        }
        
        static void Day2P2()
        {
            var input = File.ReadAllLines("../../inputDay2.txt");
            var horizontal = 0;
            var depth = 0;
            var aim = 0;
            foreach (var line in input)
            {
                var commands = line.Split(' ');
                var command = commands[0];
                var amount = int.Parse(commands[1]);

                if (command == "forward")
                {
                    horizontal += amount;
                    depth += aim * amount;
                } else if (command == "up")
                {
                    aim -= amount;
                } else if (command == "down")
                {
                    aim += amount;
                }
            }
            Console.WriteLine(horizontal*depth);
        }
        
        #endregion
    }
}
