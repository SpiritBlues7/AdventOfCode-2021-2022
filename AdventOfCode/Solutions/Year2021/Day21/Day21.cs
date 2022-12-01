using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2021
{

    class Day21 : ASolution
    {


        public Day21() : base(21, 2021, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            int player1 = 2;
            int player2 = 1;

            int diceMax = 100;
            int p1Score = 0;
            int p2Score = 0;
            int p1Pos = player1;
            int p2Pos = player2;

            int winner = -1;
            long turns = 0;
            bool p1Turn = true;
            int start = 1;
            while (winner == -1)
            {
                for (int i = start; i <= diceMax; i += 3)
                {

                    turns += 3;

                    int val1 = ((i + 1) % 100 == 0) ? 100 : (i + 1) % 100;
                    int val2 = ((i + 2) % 100 == 0) ? 100 : (i + 2) % 100;

                    if (p1Turn)
                    {

                        p1Pos = (p1Pos + i + val1 + val2) % 10;
                        if (p1Pos == 0)
                        {
                            p1Pos = 10;
                        }
                        p1Score += p1Pos;
                        p1Turn = false;
                    }
                    else
                    {

                        p2Pos = (p2Pos + i + val1 + val2) % 10;
                        if (p2Pos == 0)
                        {
                            p2Pos = 10;
                        }
                        p2Score += p2Pos;
                        p1Turn = true;
                    }



                    if (p1Score >= 1000)
                    {
                        winner = 1;
                        break;
                    }
                    if (p2Score >= 1000)
                    {
                        winner = 2;
                        break;
                    }

                    start = i + 3 - 100;
                }
                if (winner != -1)
                {
                    break;
                }

            }



            return (winner == 1) ? (turns * p2Score).ToString() : (turns * p1Score).ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            int player1 = 1;
            int player2 = 2;

            int p1Score = 0;
            int p2Score = 0;
            int p1Pos = player1;
            int p2Pos = player2;

            int winner = -1;
            long turns = 0;
            bool p1Turn = true;
            int start = 1;

            Dictionary<(int, int, int, int, int), long> scores = new Dictionary<(int, int, int, int, int), long>();

            //for (int i = 20; i >= 0; i--)
            //{
            //    for (int j = 20; j >= 0; j--)
            //    {
            //        for (int posI = 1; posI <= 10; posI--)
            //        {
            //            for (int posJ = 1; posJ <= 10; posJ--)
            //            {
            //                scores[(1, posI, posJ, i, j)] = 0;
            //                scores[(2, posI, posJ, i, j)] = 0;
            //            }
            //        }
            //    }
            //}
            long t = 0;
            for (int count = 20; count >= 0; count--)
            {
                Console.WriteLine(count);
                for (int i = 20; i >= count; i--)
                {
                    
                    for (int j = 20; j >= count; j--)
                    {
                        for (int posI = 1; posI <= 10; posI++)
                        {
                            for (int posJ = 1; posJ <= 10; posJ++)
                            {
                                if (scores.ContainsKey((1, posI, posJ, i, j)))
                                {
                                    continue;
                                }
                                else
                                {
                                    scores[(1, posI, posJ, i, j)] = 0;
                                    scores[(2, posI, posJ, i, j)] = 0;
                                }

                                for (int roll = 3; roll <= 9; roll++)
                                {
                                    int odds = 1;
                                    if (roll == 4 || roll == 8)
                                    {
                                        odds = 3;
                                    }
                                    if (roll == 6)
                                    {
                                        odds = 7;
                                    }
                                    if (roll == 5 || roll == 7)
                                    {
                                        odds = 6;
                                    }


                                    int newPos = (posI + roll) % 10;
                                    if (newPos == 0)
                                    {
                                        newPos = 10;
                                    }
                                    if (i + newPos >= 21)
                                    {
                                        scores[(1, posI, posJ, i, j)] += odds;
                                    }
                                    else
                                    {
                                        scores[(1, posI, posJ, i, j)] += odds * scores[(2, newPos, posJ, i + newPos, j)];
                                    }

                                    int newOppPos = (posJ + roll) % 10;
                                    if (newOppPos == 0)
                                    {
                                        newOppPos = 10;
                                    }
                                    if (j + newOppPos < 21)
                                    {
                                        scores[(2, posI, posJ, i, j)] += odds * scores[(1, posI, newOppPos, i, j + newOppPos)];
                                    }
                                }
                            }
                        }

                    }
                }
            }


            return scores[(1, player1, player2, 0, 0)].ToString();
        }
    }
}