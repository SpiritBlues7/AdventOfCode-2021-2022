using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2021
{

    class Day04 : ASolution
    {
        // long[] Lines;
        string[] Lines;
        List<int> nums = new List<int>();
        List<(List<(int, bool)>, bool)> boards = new List<(List<(int, bool)>, bool)>();

        public Day04() : base(04, 2021, "")
        {
            //Debug = true;

            Lines = Input.SplitByNewline();
            nums = Lines[0].ToIntArray(",").ToList();

            int boardCount = 0;
            int count = 1;

            for (int i = 1; i < Lines.Length; i++)
            {

                if (count == 1)
                {
                    boards.Add((new List<(int, bool)>(), false));
                }

                int[] currentLine = Lines[i].ToIntArray(" ");
                for (int j = 0; j < currentLine.Length; j++)
                {
                    boards[boardCount].Item1.Add((currentLine[j], false));
                }


                if (count == 5)
                {
                    count = 1;
                    boardCount++;
                } else
                {
                    count++;
                }
                
                     
            }

        }

        protected override string SolvePartOne()
        {
            for (int i = 0; i < nums.Count; i++)
            {
                int currentNum = nums[i];

                for (int j = 0; j < boards.Count; j++)
                {
                    for (int k = 0; k < boards[j].Item1.Count; k++)
                    {
                        if (boards[j].Item1[k].Item1 == currentNum)
                        {
                            boards[j].Item1[k] = (currentNum, true);
                        }
                    }
                }

                for (int j = 0; j < boards.Count; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        int rowTotal = 0;
                        for (int l = 0; l < 5; l++)
                        {
                            if (boards[j].Item1[k * 5 + l].Item2)
                            {
                                rowTotal++;
                            }
                        }

                        int colTotal = 0;
                        for (int l = 0; l < 5; l++)
                        {
                            if (boards[j].Item1[l * 5 + k].Item2)
                            {
                                colTotal++;
                            }
                        }

                        if (rowTotal == 5)
                        {
                            int sumOfNonSelected = 0;
                            for (int l = 0; l < boards[j].Item1.Count; l++)
                            {
                                if (!boards[j].Item1[l].Item2)
                                {
                                    sumOfNonSelected += boards[j].Item1[l].Item1;
                                }
                            }

                            Console.WriteLine("Sum: {0}, Num: {1}, Combined: {2}", sumOfNonSelected.ToString(), currentNum.ToString(), (sumOfNonSelected * currentNum).ToString());
                            return (sumOfNonSelected * currentNum).ToString();
                        }

                    }
                }
            }



            return "";
        }

        protected override string SolvePartTwo()
        {
            int boardsWon = 0;
            for (int i = 0; i < nums.Count; i++)
            {
                int currentNum = nums[i];

                for (int j = 0; j < boards.Count; j++)
                {
                    for (int k = 0; k < boards[j].Item1.Count; k++)
                    {
                        if (boards[j].Item1[k].Item1 == currentNum)
                        {
                            boards[j].Item1[k] = (currentNum, true);
                        }
                    }
                }

                for (int j = 0; j < boards.Count; j++)
                {
                    if (boards[j].Item2)
                    {
                        continue;
                    }
                    for (int k = 0; k < 5; k++)
                    {
                        if (boards[j].Item2)
                        {
                            continue;
                        }
                        int rowTotal = 0;
                        for (int l = 0; l < 5; l++)
                        {
                            if (boards[j].Item1[k * 5 + l].Item2)
                            {
                                rowTotal++;
                            }
                        }

                        int colTotal = 0;
                        for (int l = 0; l < 5; l++)
                        {
                            if (boards[j].Item1[l * 5 + k].Item2)
                            {
                                colTotal++;
                            }
                        }

                        if (rowTotal == 5 || colTotal == 5)
                        {
                            
                            if (boardsWon == boards.Count - 1)
                            {
                                int sumOfNonSelected = 0;
                                for (int l = 0; l < boards[j].Item1.Count; l++)
                                {
                                    if (!boards[j].Item1[l].Item2)
                                    {
                                        sumOfNonSelected += boards[j].Item1[l].Item1;
                                    }
                                }

                                Console.WriteLine("Sum: {0}, Num: {1}, Combined: {2}", sumOfNonSelected.ToString(), currentNum.ToString(), (sumOfNonSelected * currentNum).ToString());
                                return (sumOfNonSelected * currentNum).ToString();
                            } else
                            {
                                boardsWon++;
                                boards[j] = (new List<(int, bool)>(), true);
                            }


                        }

                    }
                }
            }



            return "";
        }
    }
}
