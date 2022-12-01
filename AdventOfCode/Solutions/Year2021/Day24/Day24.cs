using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2021
{

    class Day24 : ASolution
    {


        public Day24() : base(24, 2021, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            
    //inp a -Read an input value and write it to variable a.
    //add a b - Add the value of a to the value of b, then store the result in variable a.
    //mul a b - Multiply the value of a by the value of b, then store the result in variable a.
    //div a b - Divide the value of a by the value of b, truncate the result to an integer, then store the result in variable a. (Here, "truncate" means to round the value toward zero.)
    //mod a b - Divide the value of a by the value of b, then store the remainder in variable a. (This is also called the modulo operation.)
    //eql a b - If the value of a and b are equal, then store the value 1 in variable a. Otherwise, store the value 0 in variable a.

            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();


            
            long best = 14914975971114;


            long cur = 14000075001114;

            long i = 0;
            for (long a = 0; a <= 900000000000; a += 100000000000)
            {
                for (long b = 0; b <= 90000000000; b += 10000000000)
                {
                    for (long k = 0; k <= 9000000000; k += 1000000000)
                    {
                        for (long l = 0; l <= 900000000; l += 100000000)
                        {
                            for (long m = 0; m <= 900000; m += 100000)
                            {
                                for (long o = 0; o <= 90000; o += 10000)
                                {
                                    i = cur + a + b + k + l + m + o;

                                    int cIndex = 0;
                                    long[] vars = new long[26];
                                    string currentNum = i.ToString();
                                    int currentNumLength = currentNum.Length;
                                    for (int j = 0; j < (14 - currentNumLength); j++)
                                    {
                                        currentNum = "0" + currentNum;
                                    }
                                    //currentNum = "14914975971114";

                                    foreach (string line in lines)
                                    {

                                        string[] parts = line.Split(" ");
                                        string command = parts[0];
                                        char vari = parts[1][0];
                                        string param2 = "";
                                        int param2Num = -1;
                                        bool numSuccess = false;
                                        if (parts.Length > 2)
                                        {
                                            param2 = parts[2];
                                            if (int.TryParse(param2, out param2Num))
                                            {
                                                numSuccess = true;
                                            }

                                        }

                                        if (command == "inp")
                                        {
                                            vars[vari - 'a'] = currentNum[cIndex] - '0';
                                            cIndex++;
                                            continue;
                                        }

                                        if (command == "add")
                                        {
                                            if (numSuccess)
                                            {
                                                vars[vari - 'a'] = vars[vari - 'a'] + param2Num;
                                            }
                                            else
                                            {
                                                vars[vari - 'a'] = vars[vari - 'a'] + vars[param2[0] - 'a'];
                                            }

                                            continue;
                                        }

                                        if (command == "mul")
                                        {
                                            if (numSuccess)
                                            {
                                                vars[vari - 'a'] = vars[vari - 'a'] * param2Num;
                                            }
                                            else
                                            {
                                                vars[vari - 'a'] = vars[vari - 'a'] * vars[param2[0] - 'a'];
                                            }
                                            continue;
                                        }

                                        if (command == "div")
                                        {
                                            if (numSuccess)
                                            {
                                                vars[vari - 'a'] = vars[vari - 'a'] / param2Num;
                                            }
                                            else
                                            {
                                                vars[vari - 'a'] = vars[vari - 'a'] / vars[param2[0] - 'a'];
                                            }
                                            continue;
                                        }

                                        if (command == "mod")
                                        {
                                            if (numSuccess)
                                            {
                                                vars[vari - 'a'] = vars[vari - 'a'] % param2Num;
                                            }
                                            else
                                            {
                                                vars[vari - 'a'] = vars[vari - 'a'] % vars[param2[0] - 'a'];
                                            }
                                            continue;
                                        }

                                        if (command == "eql")
                                        {
                                            if (numSuccess)
                                            {
                                                vars[vari - 'a'] = (vars[vari - 'a'] == param2Num) ? 1 : 0;
                                            }
                                            else
                                            {
                                                vars[vari - 'a'] = (vars[vari - 'a'] == vars[param2[0] - 'a']) ? 1 : 0;
                                            }

                                            continue;
                                        }


                                        //if (vars[25] < 5)
                                        //{
                                        //    Console.WriteLine(vars[25]);
                                        //    Console.WriteLine(i);
                                        //}


                                    }


                                    if (vars[25] == 0)
                                    {
                                        if (!currentNum.Contains("0"))
                                        {
                                            Console.WriteLine(vars[25]);
                                            Console.WriteLine(i);
                                            if (i < best)
                                            {
                                                Console.WriteLine("$$$$$$$$$$$$");
                                            }
                                            if (i <= best)
                                            {
                                                best = i;
                                                Console.WriteLine(i);
                                            }
                                        }
                                    }

                                }


                            }
                        }
                    }
                }
            }

        

            



            return "";
        }

        protected override string SolvePartTwo(string input)
        {
            return null;
        }
    }
}