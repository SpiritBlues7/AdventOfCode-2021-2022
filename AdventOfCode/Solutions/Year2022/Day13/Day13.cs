using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Security;

namespace AdventOfCode.Solutions.Year2022
{

    class Day13 : ASolution
    {

        public Day13() : base(13, 2022, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] pairs = input.Split("\n\n");
            long sum = 0;

            for (int i = 0; i < pairs.Count(); i++)
            {
                string[] lines = pairs[i].SplitByNewline();
                List<dynamic> line1 = ConvertLineToList(lines[0]);
                List<dynamic> line2 = ConvertLineToList(lines[1]);

                if (CompareTwoLists(line1, line2) == -1)
                {
                    sum += i + 1;
                }
            }

            return sum.ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] pairs = input.Split("\n\n");

            List<List<dynamic>> everything = new List<List<dynamic>>();
            string token1 = "[[2]]";
            string token2 = "[[6]]";
            List<dynamic> token1List = ConvertLineToList(token1);
            List<dynamic> token2List = ConvertLineToList(token2);
            everything.Add(token1List);
            everything.Add(token2List);

            for (int i = 0; i < pairs.Count(); i++)
            {
                string[] lines = pairs[i].SplitByNewline();
                List<dynamic> line1 = ConvertLineToList(lines[0]);
                List<dynamic> line2 = ConvertLineToList(lines[1]);
                everything.Add(line1);
                everything.Add(line2);
            }

            everything.Sort((x, y) => CompareTwoLists(x, y));
            int token1Index = 0;
            int token2Index = 0;
            for (int i = 0; i < everything.Count(); i++)
            {
                if (everything[i] == token1List)
                {
                    token1Index = i + 1;
                }
                if (everything[i] == token2List)
                {
                    token2Index = i + 1;
                }
            }

            return (token1Index * token2Index).ToString();
        }

        public int CompareTwoLists(List<dynamic> line1, List<dynamic> line2)
        {
            for (int i = 0; i < line1.Count(); i++)
            {
                if (i >= line2.Count())
                {
                    return 1;
                }
                if (line1[i] is long && line2[i] is long)
                {
                    int comparison = line1[i].CompareTo(line2[i]);
                    if (comparison != 0)
                    {
                        return comparison;
                    }

                }
                else if (line1[i] is List<dynamic> && line2[i] is List<dynamic>)
                {
                    int comparison =  CompareTwoLists(line1[i], line2[i]);
                    if (comparison != 0)
                    {
                        return comparison;
                    }
                }
                else if (line1[i] is long && line2[i] is List<dynamic>)
                {
                    int comparison = CompareTwoLists(new List<dynamic>() { line1[i] }, line2[i]);
                    if (comparison != 0)
                    {
                        return comparison;
                    }
                }

                else if (line1[i] is List<dynamic> && line2[i] is long)
                {
                    int comparison = CompareTwoLists(line1[i], new List<dynamic>() { line2[i] });
                    if (comparison != 0)
                    {
                        return comparison;
                    }
                }
            }

            if (line2.Count > line1.Count)
            {
                return -1;
            }
            return 0;
        }

        public List<dynamic> ConvertLineToList(string line)
        {
            List<dynamic> compList = new List<dynamic>();
            // Empty list
            if (line.Count() == 0)
            {
                return compList;
            }
            // Num by itself
            if (line.Count() == 1)
            {
                compList.Add(line.ToLong());
                return compList;
            }
            

            int depth = 0;
            string comp = "";
            for (int i = 1; i < line.Count() - 1; i++)
            {
                
                switch (line[i])
                {
                    case '[':
                        depth++;
                        comp += line[i];
                        break;
                    case ']':
                        depth--;
                        comp += line[i];
                        break;
                    case ',':
                        if (depth == 0)
                        {
                            if (!comp.Contains("]"))
                            {
                                compList.Add(comp.ToLong());
                            }
                            else
                            {
                                compList.Add(ConvertLineToList(comp));
                            }
                            comp = "";
                        } else
                        {
                            comp += line[i];
                        }
                        break;
                    default:
                        comp += line[i];
                        break;
                }

                // end of line, so perform one more time
                if (i == line.Count() - 2)
                {
                    if (!comp.Contains("]"))
                    {
                        compList.Add(comp.ToLong());
                    }
                    else
                    {
                        compList.Add(ConvertLineToList(comp));
                    }
                    comp = "";
                }
            }

            return compList;
        }
    }
}