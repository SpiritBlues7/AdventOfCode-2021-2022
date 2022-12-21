using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2022
{

    class Day21 : ASolution
    {
        Dictionary<string, (string, string)> addingMonkeys = new Dictionary<string, (string, string)>();
        Dictionary<string, (string, string)> addingMonkeysTwo = new Dictionary<string, (string, string)>();
        Dictionary<string, (string, string)> minusMonkeys = new Dictionary<string, (string, string)>();
        Dictionary<string, (string, string)> minusMonkeysTwo = new Dictionary<string, (string, string)>();
        Dictionary<string, (string, string)> divideMonkeys = new Dictionary<string, (string, string)>();
        Dictionary<string, (string, string)> divideMonkeysTwo = new Dictionary<string, (string, string)>();
        Dictionary<string, (string, string)> multiplyMonkeys = new Dictionary<string, (string, string)>();
        Dictionary<string, (string, string)> multiplyMonkeysTwo = new Dictionary<string, (string, string)>();
        Dictionary<string, long> yellingMonkeys = new Dictionary<string, long>();


        public Day21() : base(21, 2022, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();

            addingMonkeys = new Dictionary<string, (string, string)>();
            minusMonkeys = new Dictionary<string, (string, string)>();
            divideMonkeys = new Dictionary<string, (string, string)>();
            multiplyMonkeys = new Dictionary<string, (string, string)>();
            yellingMonkeys = new Dictionary<string, long>();

            for (long i = 0; i < lines.Count(); i++)
            {
                string[] comp = lines[i].Split(":");
                string monkey = comp[0];

                if (comp[1].Length < 8 || monkey.Equals("humn"))
                {
                    yellingMonkeys[monkey] = long.Parse(comp[1].Replace(" ", ""));
                }
                else
                {
                    var compTwo = comp[1].Trim().Split(" ");
                    if (compTwo[1].Equals("+"))
                    {
                        addingMonkeys[monkey] = (compTwo[0], compTwo[2]);
                        
                    }                    
                    if (compTwo[1].Equals("-"))
                    {
                        minusMonkeys[monkey] = (compTwo[0], compTwo[2]);
                    }                   
                    if (compTwo[1].Equals("/"))
                    {
                        divideMonkeys[monkey] = (compTwo[0], compTwo[2]);
                    }                   
                    if (compTwo[1].Equals("*"))
                    {
                        multiplyMonkeys[monkey] = (compTwo[0], compTwo[2]);
                    }
                }
            }

            long ans = determine("root");

            return ans.ToString();
        }

        public long determine(string monkey)
        {
            if (yellingMonkeys.ContainsKey(monkey))
            {
                return yellingMonkeys[monkey];
            }
            if (addingMonkeys.ContainsKey(monkey))
            {
                return determine(addingMonkeys[monkey].Item1) + determine(addingMonkeys[monkey].Item2);
            }
            if (minusMonkeys.ContainsKey(monkey))
            {
                return determine(minusMonkeys[monkey].Item1) - determine(minusMonkeys[monkey].Item2);
            }
            if (divideMonkeys.ContainsKey(monkey))
            {
                return determine(divideMonkeys[monkey].Item1) / determine(divideMonkeys[monkey].Item2);
            }
            if (multiplyMonkeys.ContainsKey(monkey))
            {
                return determine(multiplyMonkeys[monkey].Item1) * determine(multiplyMonkeys[monkey].Item2);
            }
            return 0;
        }

        protected override string SolvePartTwo(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();

            addingMonkeys = new Dictionary<string, (string, string)>();
            addingMonkeysTwo = new Dictionary<string, (string, string)>();
            minusMonkeys = new Dictionary<string, (string, string)>();
            minusMonkeysTwo = new Dictionary<string, (string, string)>();
            divideMonkeys = new Dictionary<string, (string, string)>();
            divideMonkeysTwo = new Dictionary<string, (string, string)>();
            multiplyMonkeys = new Dictionary<string, (string, string)>();
            multiplyMonkeysTwo = new Dictionary<string, (string, string)>();
            yellingMonkeys = new Dictionary<string, long>();

            for (long i = 0; i < lines.Count(); i++)
            {
                string[] comp = lines[i].Split(":");
                string monkey = comp[0];


                if (comp[1].Length < 8)
                {
                    yellingMonkeys[monkey] = long.Parse(comp[1].Replace(" ", ""));
                }
                else
                {
                    var compTwo = comp[1].Trim().Split(" ");
                    if (compTwo[1].Equals("+"))
                    {
                        if (!addingMonkeys.ContainsKey(monkey))
                        {
                            addingMonkeys[monkey] = (compTwo[0], compTwo[2]);
                        }
                        else
                        {
                            addingMonkeysTwo[monkey] = (compTwo[0], compTwo[2]);
                        }

                        if (!minusMonkeys.ContainsKey(compTwo[0]))
                        {
                            minusMonkeys[compTwo[0]] = (monkey, compTwo[2]);
                        }
                        else
                        {
                            minusMonkeysTwo[compTwo[0]] = (monkey, compTwo[2]);
                        }
                        if (!minusMonkeys.ContainsKey(compTwo[2]))
                        {
                            minusMonkeys[compTwo[2]] = (monkey, compTwo[0]);
                        }
                        else
                        {
                            minusMonkeysTwo[compTwo[2]] = (monkey, compTwo[0]);
                        }

                    }
                    if (compTwo[1].Equals("-"))
                    {
                        if (!minusMonkeys.ContainsKey(monkey))
                        {
                            minusMonkeys[monkey] = (compTwo[0], compTwo[2]);
                        }
                        else
                        {
                            minusMonkeysTwo[monkey] = (compTwo[0], compTwo[2]);
                        }

                        if (!addingMonkeys.ContainsKey(compTwo[0]))
                        {
                            addingMonkeys[compTwo[0]] = (monkey, compTwo[2]);
                        }
                        else
                        {
                            addingMonkeysTwo[compTwo[0]] = (monkey, compTwo[2]);
                        }
                        if (!minusMonkeys.ContainsKey(compTwo[2]))
                        {
                            minusMonkeys[compTwo[2]] = (compTwo[0], monkey);
                        }
                        else
                        {
                            minusMonkeysTwo[compTwo[2]] = (compTwo[0], monkey);
                        }

                    }
                    if (compTwo[1].Equals("/"))
                    {
                        if (!divideMonkeys.ContainsKey(monkey))
                        {
                            divideMonkeys[monkey] = (compTwo[0], compTwo[2]);
                        }
                        else
                        {
                            divideMonkeysTwo[monkey] = (compTwo[0], compTwo[2]);
                        }

                        if (!multiplyMonkeys.ContainsKey(compTwo[0]))
                        {
                            multiplyMonkeys[compTwo[0]] = (monkey, compTwo[2]);
                        }
                        else
                        {
                            multiplyMonkeysTwo[compTwo[0]] = (monkey, compTwo[2]);
                        }
                        if (!divideMonkeys.ContainsKey(compTwo[2]))
                        {
                            divideMonkeys[compTwo[2]] = (compTwo[0], monkey);
                        }
                        else
                        {
                            divideMonkeysTwo[compTwo[2]] = (compTwo[0], monkey);
                        }

                    }
                    if (compTwo[1].Equals("*"))
                    {
                        if (!multiplyMonkeys.ContainsKey(monkey))
                        {
                            multiplyMonkeys[monkey] = (compTwo[0], compTwo[2]);
                        }
                        else
                        {
                            multiplyMonkeysTwo[monkey] = (compTwo[0], compTwo[2]);
                        }
                        if (!divideMonkeys.ContainsKey(compTwo[0]))
                        {
                            divideMonkeys[compTwo[0]] = (monkey, compTwo[2]);
                        }
                        else
                        {
                            divideMonkeysTwo[compTwo[0]] = (monkey, compTwo[2]);
                        }
                        if (!divideMonkeys.ContainsKey(compTwo[2]))
                        {
                            divideMonkeys[compTwo[2]] = (monkey, compTwo[0]);
                        }
                        else
                        {
                            divideMonkeysTwo[compTwo[2]] = (monkey, compTwo[0]);
                        }

                    }
                }
            }

            HashSet<string> unsolved = new HashSet<string>();
            yellingMonkeys.Remove("humn");
            long ans1 = determine2("humn", unsolved);


            return ans1.ToString();
        }

        public long determine2(string monkey, HashSet<string> unsolved)
        {
            if (monkey == "wgbd")
            {
                unsolved.Add("wgbd");
                long temp = determine2("rqsg", unsolved);
                unsolved.Remove("wgbd");
                return temp;
            }
            if (monkey == "pppw")
            {
                unsolved.Add("pppw");
                long temp = determine2("sjmn", unsolved);
                unsolved.Remove("pppw");
                return temp;
            }
            if (yellingMonkeys.ContainsKey(monkey))
            {
                if (unsolved.Contains(monkey))
                {
                    unsolved.Remove(monkey);
                }
                return yellingMonkeys[monkey];
            }
            unsolved.Add(monkey);
            if (addingMonkeys.ContainsKey(monkey))
            {
                if (!unsolved.Contains(addingMonkeys[monkey].Item1) && !unsolved.Contains(addingMonkeys[monkey].Item2))
                {              
                    return determine2(addingMonkeys[monkey].Item1, unsolved) + determine2(addingMonkeys[monkey].Item2, unsolved);
                }
            }
            if (minusMonkeys.ContainsKey(monkey))
            {
                if (!unsolved.Contains(minusMonkeys[monkey].Item1) && !unsolved.Contains(minusMonkeys[monkey].Item2))
                {
                    return determine2(minusMonkeys[monkey].Item1, unsolved) - determine2(minusMonkeys[monkey].Item2, unsolved);
                }
            }
            if (divideMonkeys.ContainsKey(monkey))
            {
                if (!unsolved.Contains(divideMonkeys[monkey].Item1) && !unsolved.Contains(divideMonkeys[monkey].Item2))
                {
                    return determine2(divideMonkeys[monkey].Item1, unsolved) / determine2(divideMonkeys[monkey].Item2, unsolved);
                }
            }
            if (multiplyMonkeys.ContainsKey(monkey))
            {
                if (!unsolved.Contains(multiplyMonkeys[monkey].Item1) && !unsolved.Contains(multiplyMonkeys[monkey].Item2))
                {
                    return determine2(multiplyMonkeys[monkey].Item1, unsolved) * determine2(multiplyMonkeys[monkey].Item2, unsolved);
                }
            }
            if (addingMonkeysTwo.ContainsKey(monkey))
            {
                if (!unsolved.Contains(addingMonkeysTwo[monkey].Item1) && !unsolved.Contains(addingMonkeysTwo[monkey].Item2))
                {
                    return determine2(addingMonkeysTwo[monkey].Item1, unsolved) + determine2(addingMonkeysTwo[monkey].Item2, unsolved);
                }
            }
            if (minusMonkeysTwo.ContainsKey(monkey))
            {
                if (!unsolved.Contains(minusMonkeysTwo[monkey].Item1) && !unsolved.Contains(minusMonkeysTwo[monkey].Item2))
                {
                    return determine2(minusMonkeysTwo[monkey].Item1, unsolved) - determine2(minusMonkeysTwo[monkey].Item2, unsolved);
                }
            }
            if (divideMonkeysTwo.ContainsKey(monkey))
            {
                if (!unsolved.Contains(divideMonkeysTwo[monkey].Item1) && !unsolved.Contains(divideMonkeysTwo[monkey].Item2))
                {
                    return determine2(divideMonkeysTwo[monkey].Item1, unsolved) / determine2(divideMonkeysTwo[monkey].Item2, unsolved);
                }
            }
            if (multiplyMonkeysTwo.ContainsKey(monkey))
            {
                if (!unsolved.Contains(multiplyMonkeysTwo[monkey].Item1) && !unsolved.Contains(multiplyMonkeysTwo[monkey].Item2))
                {
                    return determine2(multiplyMonkeysTwo[monkey].Item1, unsolved) * determine2(multiplyMonkeysTwo[monkey].Item2, unsolved);
                }
            }
            return 0;
        }
    }
}