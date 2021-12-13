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
            Day8P2();
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
        
        #region Day4
        
        static void Day4P1()
        {
            var input = File.ReadAllLines("../../inputDay4.txt");
            int[] drawNumbersInOrder;
            drawNumbersInOrder = input[0].Split(',').Select(t => Convert.ToInt32(t)).ToArray();

            // Create boards
            List<bingoBoard> boards = new List<bingoBoard>();
            for (int i = 2; i < input.Length; i+=6)
            {
                var numbers = new int[25];
                for (int x = 0; x < 5; x++)
                {
                    var tempNumb = input[i + x].Split(' ').Where(n => int.TryParse(n, out _)).ToArray();
                    for (int j = 0; j < 5; j++)
                    {
                        numbers[x * 5 + j] = Convert.ToInt32(tempNumb[j]);
                    }
                }
                boards.Add(new bingoBoard(numbers));
            }

            foreach (var number in drawNumbersInOrder)
            {
                foreach (var board in boards)
                {
                    board.MarkNumber(number);
                    if (board.IsBingo())
                    {
                        Console.WriteLine("Board with first number" + board.numbers[0].number + " is bingo!");
                        
                        // calculate the score
                        int score = 0;
                        foreach (var bNumb in board.numbers.Where(n => !n.marked))
                        {
                            score+= bNumb.number;
                        }

                        score *= number;
                        Console.WriteLine("Score: " + score);
                        return;
                    }
                }
            }
            
        }

        static void Day4P2()
        {
            var input = File.ReadAllLines("../../inputDay4.txt");
            var drawNumbersInOrder = input[0].Split(',').Select(t => Convert.ToInt32(t)).ToArray();

            // Create boards
            List<bingoBoard> boards = new List<bingoBoard>();
            for (int i = 2; i < input.Length; i+=6)
            {
                var numbers = new int[25];
                for (int x = 0; x < 5; x++)
                {
                    var tempNumb = input[i + x].Split(' ').Where(n => int.TryParse(n, out _)).ToArray();
                    for (int j = 0; j < 5; j++)
                    {
                        numbers[x * 5 + j] = Convert.ToInt32(tempNumb[j]);
                    }
                }
                boards.Add(new bingoBoard(numbers));
                boards[boards.Count-1].boardIndex = i-2 / 6;
            }

            foreach (var number in drawNumbersInOrder)
            {
                foreach (var board in boards)
                {
                    board.MarkNumber(number);
                    
                    if (board.IsBingo())
                    {
                        board.hasWon = true;
                        if (boards.Where(b => !b.hasWon).ToArray().Length == 0)
                        {
                            Console.WriteLine("Board with first number " + board.numbers[0].number + " is last bingo!");
                            
                            // calculate the score
                            int score = 0;
                            foreach (var bNumb in board.numbers.Where(n => !n.marked))
                            {
                                score += bNumb.number;
                            }

                            Console.WriteLine(score + " * " + number);
                            score *= number;
                            Console.WriteLine("Score: " + score);
                            return;
                        }
                    }
                }
            }
        }
        
        class bingoBoard
        {
            public readonly BingoNumber[] numbers = new BingoNumber[25];
            public bool hasWon;
            public int boardIndex;
            
            public bingoBoard(int[] numbers)
            {
                for (int i = 0; i < numbers.Length; i++)
                {
                    this.numbers[i] = new BingoNumber(numbers[i]);
                }
            }
            
            public void MarkNumber(int number)
            {
                numbers.Where(t => t.number == number).ToList().ForEach(t => t.marked = true);
            }
            
            public bool IsBingo()
            {
                //Check horizontal
                for (int i = 0; i < 5; i++)
                {
                    if (numbers[i * 5].marked && numbers[i * 5 + 1].marked && numbers[i * 5 + 2].marked && numbers[i * 5 + 3].marked && numbers[i * 5 + 4].marked)
                    {
                        return true;
                    }
                }
                
                //Check vertical
                for (int i = 0; i < 5; i++)
                {
                    if (numbers[i].marked && numbers[i + 5].marked && numbers[i + 10].marked && numbers[i + 15].marked && numbers[i + 20].marked)
                    {
                        return true;
                    }
                }

                return false;
            }
            
        }

        class BingoNumber
        {
            public int number;
            public bool marked;
            
            public BingoNumber(int number)
            {
                this.number = number;
            }
            
        }
        
        #endregion
        
        #region Day5

        static void Day5P1()
        {
            var input = File.ReadAllLines("../../inputDay5.txt");
            var coords = new List<Coordinate>();
            foreach (var line in input)
            {
                var tempCoords = line.Split('>').Select(t =>
                    t.Replace(" ", "").Replace("-", "").Split(',')).ToArray();
                
                coords.Add(new Coordinate(Convert.ToInt32(tempCoords[0][0]), Convert.ToInt32(tempCoords[0][1])));
                coords.Add(new Coordinate(Convert.ToInt32(tempCoords[1][0]), Convert.ToInt32(tempCoords[1][1])));
            }
                
            var lineCoords = new List<Coordinate>();
            
            for (int i = 0; i < coords.Count; i+=2)
            {
                var coord1 = coords[i];
                var coord2 = coords[i+1];
                lineCoords.AddRange(GetLineCoordinates(coord1, coord2));
            }

            var grid = new Grid(coords.Max(m => m.x), coords.Max(m => m.y));
            
            foreach(var coord in lineCoords)
            {
                grid.grid[coord.x, coord.y] += 1;
            }
            
            Console.WriteLine(grid.GetOverlaps());
        }
        
        static void Day5P2()
        {
            var input = File.ReadAllLines("../../inputDay5.txt");
            var coords = new List<Coordinate>();
            foreach (var line in input)
            {
                var tempCoords = line.Split('>').Select(t =>
                    t.Replace(" ", "").Replace("-", "").Split(',')).ToArray();
                
                coords.Add(new Coordinate(Convert.ToInt32(tempCoords[0][0]), Convert.ToInt32(tempCoords[0][1])));
                coords.Add(new Coordinate(Convert.ToInt32(tempCoords[1][0]), Convert.ToInt32(tempCoords[1][1])));
            }
                
            var lineCoords = new List<Coordinate>();
            
            for (int i = 0; i < coords.Count; i+=2)
            {
                var coord1 = coords[i];
                var coord2 = coords[i+1];
                lineCoords.AddRange(GetLineCoordinates(coord1, coord2));
            }

            var grid = new Grid(coords.Max(m => m.x), coords.Max(m => m.y));
            
            foreach(var coord in lineCoords)
            {
                grid.grid[coord.x, coord.y] += 1;
            }
            
            Console.WriteLine(grid.GetOverlaps());
        }

        static Coordinate[] GetLineCoordinates(Coordinate coord1,Coordinate coord2)
        {
            var coords = new List<Coordinate>();
            if (coord1.x == coord2.x)
            {
                if (coord2.y > coord1.y)
                {
                    for (int i = coord1.y; i <= coord2.y; i++)
                    {
                        coords.Add(new Coordinate(coord1.x, i));
                    }
                }
                else
                {
                    for (int i = coord2.y; i <= coord1.y; i++)
                    {
                        coords.Add(new Coordinate(coord1.x, i));
                    }
                }
            } else if (coord1.y == coord2.y)
            {
                if (coord2.x > coord1.x)
                {
                    for (int i = coord1.x; i <= coord2.x; i++)
                    {
                        coords.Add(new Coordinate(i, coord1.y));
                    }
                }
                else
                {
                    for (int i = coord2.x; i <= coord1.x; i++)
                    {
                        coords.Add(new Coordinate(i, coord1.y));
                    }
                }
            }
            else
            {
                var xDiff = coord2.x - coord1.x;
                var yDiff = coord2.y - coord1.y;
                var xInc = xDiff / Math.Abs(xDiff);
                var yInc = yDiff / Math.Abs(yDiff);

                for (int i = 0; i < Math.Abs(coord1.x-coord2.x)+1; i++)
                {
                    coords.Add(new Coordinate(coord1.x+xInc*i, coord1.y+yInc*i));
                }


            }
            return coords.ToArray();
        }

        class Coordinate
        {
            public int x;
            public int y;
            
            public Coordinate(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        class Grid
        {
            public int[,] grid;
            
            public Grid(int width, int height)
            {
                grid = new int[width+1,height+1];
                for (int i = 0; i < width+1; i++)
                {
                    for (int j = 0; j < height+1; j++)
                    {
                        grid[i,j] = 0;
                    }
                }
            }
            
            public int GetOverlaps()
            {
                var overlaps = 0;
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        if (grid[i,j] > 1)
                        {
                            overlaps++;
                        }
                    }
                }
                return overlaps;
            }

        }

        #endregion
        
        #region Day6
        
        static void Day6P1()
        {
            var input = File.ReadAllLines("../../inputDay6.txt");
            var fishes = new List<long>();
            foreach (var line in input)
            {
                var split = line.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
                foreach (var fLine in split)
                {
                    fishes.Add(long.Parse(fLine));
                }
            }

            for (int i = 0; i < 80; i++)
            {
                var newFishes = new List<long>();
                for (var j = 0; j < fishes.Count; j++)
                {
                    if (fishes[j] == 0)
                    {
                        fishes[j] = 6;
                        newFishes.Add(8);
                    }
                    else
                    {
                        fishes[j]--;
                    }
                }

                fishes.AddRange(newFishes);
            }

            Console.WriteLine(fishes.Count);

        }

        #endregion
        
        #region Day7

        private static void Day7P1()
        {
            var input = File.ReadAllLines("../../inputDay7.txt");
            var crabs = new List<int>();
            
            foreach (var line in input)
            {
                crabs.AddRange(line.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
            }
            
            var min = crabs.Min();
            var max = crabs.Max();

            var fuelUsed = new int[max-min];
            for (int i = min; i < max; i++)
            {
                foreach (var crab in crabs)
                {
                    fuelUsed[i]+= Math.Abs(crab - i);
                }
            }

            Console.WriteLine(Array.IndexOf(fuelUsed, fuelUsed.Min()) + min);
            Console.WriteLine(fuelUsed.Min());
        }
        
        private static void Day7P2()
        {
            var input = File.ReadAllLines("../../inputDay7.txt");
            var crabs = new List<int>();
            
            foreach (var line in input)
            {
                crabs.AddRange(line.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
            }
            
            var min = crabs.Min();
            var max = crabs.Max();

            var fuelUsed = new int[max-min];
            for (int i = min; i < max; i++)
            {
                foreach (var crab in crabs)
                {
                    var fuel = 0;
                    for (int j = 0; j < Math.Abs(crab - i)+1; j++)
                    {
                        fuel += j;
                    }

                    fuelUsed[i] += fuel;
                }
            }

            Console.WriteLine(Array.IndexOf(fuelUsed, fuelUsed.Min()) + min);
            Console.WriteLine(fuelUsed.Min());
        }
        
        #endregion
        
        #region Day8

        private static void Day8P1()
        {
            var input = File.ReadAllLines("../../inputDay8.txt");
            var amount = 0;
            foreach (var line in input)
            {
                var secondParts = line.Split('|')[1].Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);
                
                foreach(var part in secondParts)
                {
                    if (new int[] { 2, 3, 4, 7 }.Any(n => part.Length == n))
                    {
                        amount++;
                    }
                    
                }
            }
        }
        
        private static void Day8P2()
        {
            var input = File.ReadAllLines("../../inputDay8.txt");
            var amount = 0;
            foreach (var line in input)
            {
                var secondParts = line.Split('|')[1].Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);
                var number = "";
                foreach(var part in secondParts)
                {
                    if (part == "ab")
                    {
                        number += "1";
                    } else if (part == "gcdfa")
                    {
                        number += "2";
                    } else if (part == "fbcad")
                    {
                        number += "3";
                    } else if (part == "eafb")
                    {
                        number += "4";
                    } else if (part == "cdfbe")
                    {
                        number += "5";
                    } else if (part == "cdfgeb")
                    {
                        number += "6";
                    } else if (part == "dab")
                    {
                        number += "7";
                    } else if (part == "acedgfb")
                    {
                        number += "8";
                    } else if (part == "cefabd")
                    {
                        number += "9";
                    }

                }

                amount += int.Parse(number);
            }
            Console.WriteLine(amount);
        }
        
        #endregion
        
        
    }
}
