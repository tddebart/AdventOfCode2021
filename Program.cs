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
            Day3P2();
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
                }
                else if (command == "up")
                {
                    depth -= amount;
                }
                else if (command == "down")
                {
                    depth += amount;
                }
            }

            Console.WriteLine(horizontal * depth);
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
                }
                else if (command == "up")
                {
                    aim -= amount;
                }
                else if (command == "down")
                {
                    aim += amount;
                }
            }

            Console.WriteLine(horizontal * depth);
        }

        #endregion

        #region Day3

        static void Day3P1()
        {
            var input = File.ReadAllLines("../../inputDay3.txt");

            var gammaRate = new int[input[0].Length];
            var epsilonRate = new int[input[0].Length];


            for (int i = 0; i < input[0].Length; i++)
            {
                var ones = 0;
                var zeros = 0;
                foreach (var line in input)
                {
                    if (line[i] == '0')
                    {
                        zeros++;
                    }
                    else
                    {
                        ones++;
                    }
                }

                if (ones > zeros)
                {
                    gammaRate[i] = 1;
                    epsilonRate[i] = 0;
                }
                else
                {
                    gammaRate[i] = 0;
                    epsilonRate[i] = 1;
                }
            }

            var gamRate = Convert.ToInt32(gammaRate.Aggregate("", (current, number) => current + number),2);
            var epiRate = Convert.ToInt32(epsilonRate.Aggregate("", (current, number) => current + number),2);
            Console.WriteLine(gamRate +"\n"+ epiRate,2);
            Console.WriteLine(gamRate * epiRate);
        }
        
        static void Day3P2()
        {
            var input = File.ReadAllLines("../../inputDay3.txt");

            var oxyInput = input.Clone() as string[];
            var co2Input = input.Clone() as string[];

            int oxyNumber = 0;
            var co2Number = 0;
            
            if (oxyInput == null || co2Input == null)
            {
                return;
            }
            
            for (int i = 0; i < oxyInput[0].Length; i++)
            {
                var ones = 0;
                var zeros = 0;
                foreach (var line in oxyInput.Select(t => t[i]))
                {
                    if (line == '0')
                    {
                        zeros++;
                    }
                    else
                    {
                        ones++;
                    }
                }
                

                if (ones < zeros)
                {
                    oxyInput = oxyInput.Where(t => t[i] == '0').ToArray();
                }
                else
                {
                    oxyInput = oxyInput.Where(t => t[i] == '1').ToArray();
                }
                
                if (oxyInput.Length == 1)
                {
                    oxyNumber = Convert.ToInt32(oxyInput[0], 2);
                    Console.WriteLine(oxyNumber);
                    break;
                }
            }
            
            for (int i = 0; i < co2Input[0].Length; i++)
            {
                var ones = 0;
                var zeros = 0;
                foreach (var line in co2Input.Select(t => t[i]))
                {
                    if (line == '0')
                    {
                        zeros++;
                    }
                    else
                    {
                        ones++;
                    }
                }
                

                if (ones < zeros)
                {
                    co2Input = co2Input.Where(t => t[i] == '1').ToArray();
                }
                else
                {
                    co2Input = co2Input.Where(t => t[i] == '0').ToArray();
                }
                
                if (co2Input.Length == 1)
                {
                    co2Number = Convert.ToInt32(co2Input[0], 2);
                    Console.WriteLine(co2Number);
                    break;
                }
            }
            
            Console.WriteLine(oxyNumber * co2Number);
        }

        #endregion
    }
}
