using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2022
{

    class Day11 : ASolution
    {


        public Day11() : base(11, 2022, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] monkeyInput = input.Split("\n\n");
            Dictionary<long, Queue<long>> monkeyItems = new Dictionary<long, Queue<long>>();
            Dictionary<long, long> monkeyInspectCount = new Dictionary<long, long>();

            for (int i = 0; i < monkeyInput.Count(); i++)
            {
                monkeyInspectCount[i] = 0;
                string[] monkeyInputLines = monkeyInput[i].Split("\n");
                string[] startingItemInput = monkeyInputLines[1].Trim().Split();
                monkeyItems[i] = new Queue<long>();
                for (int j = 2; j < startingItemInput.Count(); j++)
                {
                    monkeyItems[i].Enqueue(startingItemInput[j].Replace(",", "").ToLong());
                }
            }

            for (int c = 0; c < 20; c++)
            {
                for (int i = 0; i < monkeyInput.Count(); i++)
                {
                    string[] monkeyInputLines = monkeyInput[i].Split("\n");
                    string[] operationInput = monkeyInputLines[2].Trim().Split();
                    while (monkeyItems[i].Count() != 0)
                    {
                        monkeyInspectCount[i]++;
                        long itm = monkeyItems[i].Dequeue();
                        switch (operationInput[4])
                        {
                            case ("+"):
                                if (operationInput[5].Equals("old"))
                                {
                                    itm += itm;
                                }
                                else
                                {
                                    itm += operationInput[5].ToLong();
                                }

                                break;
                            case ("*"):
                                if (operationInput[5].Equals("old"))
                                {
                                    itm *= itm;
                                }
                                else
                                {
                                    itm *= operationInput[5].ToLong();
                                }
                                break;
                        }
                        itm = itm / 3;


                        string[] testClauseInput = monkeyInputLines[3].Trim().Split();
                        long divider = testClauseInput[3].ToLong();

                        string[] testTrueInput = monkeyInputLines[4].Trim().Split();
                        long trueMonkey = testTrueInput[5].ToLong();

                        string[] testFalseInput = monkeyInputLines[5].Trim().Split();
                        long falseMonkey = testFalseInput[5].ToLong();
                        if (itm % divider == 0)
                        {
                            monkeyItems[trueMonkey].Enqueue(itm);
                        }
                        else
                        {
                            monkeyItems[falseMonkey].Enqueue(itm);
                        }
                    }
                }
            }


            List<long> monkeyInspections = monkeyInspectCount.Select(x => x.Value).ToList();
            monkeyInspections.Sort();
            long firstMonkeyMax = monkeyInspections[monkeyInspections.Count() - 1];
            long secondMonkeyMax = monkeyInspections[monkeyInspections.Count() - 2];
            return (firstMonkeyMax * secondMonkeyMax).ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] monkeyInput = input.Split("\n\n");
            Dictionary<long, Queue<long>> monkeyItems = new Dictionary<long, Queue<long>>();
            Dictionary<long, long> monkeyInspectCount = new Dictionary<long, long>();
            HashSet<long> dividers = new HashSet<long>();
           
            long modValue = 1;
            for (int i = 0; i < monkeyInput.Count(); i++)
            {
                monkeyInspectCount[i] = 0;
                string[] monkeyInputLines = monkeyInput[i].Split("\n");
                string[] startingItemInput = monkeyInputLines[1].Trim().Split();
                monkeyItems[i] = new Queue<long>();
                for (int j = 2; j < startingItemInput.Count(); j++)
                {
                    monkeyItems[i].Enqueue(startingItemInput[j].Replace(",", "").ToLong());
                }

                string[] testClauseInput = monkeyInputLines[3].Trim().Split();
                long divider = testClauseInput[3].ToLong();
                dividers.Add(divider);


            }
            foreach (var divs in dividers)
            {
                modValue *= divs;
            }
            for (int c = 0; c <10000; c++)
            {
                for (int i = 0; i < monkeyInput.Count(); i++)
                {
                    string[] monkeyInputLines = monkeyInput[i].Split("\n");
                    string[] operationInput = monkeyInputLines[2].Trim().Split();
                    while (monkeyItems[i].Count() != 0)
                    {
                        monkeyInspectCount[i]++;
                        long itm = monkeyItems[i].Dequeue();

                        switch (operationInput[4])
                        {
                            case ("+"):
                                if (operationInput[5].Equals("old"))
                                {
                                    itm += itm;
                                } else
                                {
                                    itm += operationInput[5].ToLong();
                                }

                                break;
                            case ("*"):
                                if (operationInput[5].Equals("old"))
                                {
                                    itm *= itm;
                                } else
                                {
                                    itm *= operationInput[5].ToLong();
                                }
                                break;
                        }
                        itm = itm % modValue;

                        string[] testClauseInput = monkeyInputLines[3].Trim().Split();
                        long divider = testClauseInput[3].ToLong();

                        string[] testTrueInput = monkeyInputLines[4].Trim().Split();
                        long trueMonkey = testTrueInput[5].ToLong();

                        string[] testFalseInput = monkeyInputLines[5].Trim().Split();
                        long falseMonkey = testFalseInput[5].ToLong();

                        if (itm % divider == 0)
                        {
                            monkeyItems[trueMonkey].Enqueue(itm);
                        }
                        else
                        {
                            monkeyItems[falseMonkey].Enqueue(itm);
                        }
                    }
                }
            }

            List<long> monkeyInspections = monkeyInspectCount.Select(x => x.Value).ToList();
            monkeyInspections.Sort();
            long firstMonkeyMax = monkeyInspections[monkeyInspections.Count() - 1];
            long secondMonkeyMax = monkeyInspections[monkeyInspections.Count() - 2];
            return (firstMonkeyMax * secondMonkeyMax).ToString();
        }
    }
}