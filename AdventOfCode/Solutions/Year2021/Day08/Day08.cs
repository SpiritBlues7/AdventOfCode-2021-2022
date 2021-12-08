using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2021
{

    class Day08 : ASolution
    {
        public Day08() : base(08, 2021, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            var lines = input.SplitByNewline();
            int total = 0;
            foreach (var line in lines)
            {
                var inputDigits = line.Split(" | ")[0].Split(" ");
                var outputDigits = line.Split(" | ")[1].Split(" ");

                var lengths = new List<int>() { 2, 3, 4, 7 };

                
                foreach (string digit in outputDigits) {
                    for (int i = 0; i < lengths.Count; i++)
                    {
                        if (lengths[i] == digit.Length)
                        {
                            total++;
                        }
                    }
                }
            }

            return total.ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            var lines = input.SplitByNewline();


            var sums = new List<int>();
            foreach (var line in lines)
            {
                var numsToSegments = new Dictionary<int, HashSet<char>>();

                for (int i = 0; i < 10; i++)
                {
                    numsToSegments[i] = new HashSet<char>();
                }

                var inputDigits = line.Split(" | ")[0].Split(" ");
                var outputDigits = line.Split(" | ")[1].Split(" ");

                var lengths = new Dictionary<int, int>();
                lengths.Add(0, 6);
                lengths.Add(1, 2);
                lengths.Add(2, 5);
                lengths.Add(3, 5);
                lengths.Add(4, 4);
                lengths.Add(5, 5);
                lengths.Add(6, 6);
                lengths.Add(7, 3);
                lengths.Add(8, 7);
                lengths.Add(9, 6);

                var uniqueLengths = new List<int> { 1, 4, 7, 8};
                

                foreach (string digit in inputDigits)
                {
                    foreach (var num in uniqueLengths)
                    {
                        if (lengths[num] == digit.Length)
                        {
                            foreach (char c in digit)
                            {
                                numsToSegments[num].Add(c);
                            }
                        }
                    }
                }


                foreach (string digit in inputDigits)
                {
                    HashSet<char> digitSet = new HashSet<char>();
                    foreach (var c in digit)
                    {
                        digitSet.Add(c);
                    }

                    if (numsToSegments.Values.Contains(digitSet))
                    {
                        continue;
                    }

                    else if (digitSet.Count == lengths[0] && 
                        digitSet.IsSupersetOf(numsToSegments[7]) &&
                        !digitSet.IsSupersetOf(numsToSegments[4]))
                    {
                        numsToSegments[0] = digitSet;
                    }

                    else  if (digitSet.Count == lengths[3] && 
                        digitSet.IsSupersetOf(numsToSegments[7]) &&
                        !digitSet.IsSupersetOf(numsToSegments[4]))
                    {
                        numsToSegments[3] = digitSet;
                    }

                    else if (digitSet.Count == lengths[6] &&
                       !digitSet.IsSupersetOf(numsToSegments[7]) &&
                       !digitSet.IsSupersetOf(numsToSegments[4]))
                    {
                        numsToSegments[6] = digitSet;
                    }

                    else if (digitSet.Count == lengths[9] &&
                       digitSet.IsSupersetOf(numsToSegments[7]) &&
                       digitSet.IsSupersetOf(numsToSegments[4]))
                    {
                        numsToSegments[9] = digitSet;
                    }

                    
                    else if (digitSet.Count == lengths[5])
                    {
                        var intersetTest = digitSet.Intersect(numsToSegments[4]);
                        if (intersetTest.Count() == 3)
                        {
                            numsToSegments[5] = digitSet;
                        } else
                        {
                            numsToSegments[2] = digitSet;
                        }
                    }

                }

                string sumString = "";
                foreach (string digit in outputDigits)
                {
                    HashSet<char> digitSet = new HashSet<char>();
                    foreach (var c in digit)
                    {
                        digitSet.Add(c);
                    }

                    foreach (var entry in numsToSegments)
                    {
                        if (entry.Value.SetEquals(digitSet))
                        {
                            sumString += entry.Key;
                        }
                    }
                    
                }

                sums.Add(Convert.ToInt32(sumString));


            }

            return sums.Sum().ToString();
        }
    }
}
