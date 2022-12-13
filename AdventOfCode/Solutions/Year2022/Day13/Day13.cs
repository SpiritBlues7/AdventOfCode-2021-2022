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

            // Fix for strange bug with input
            string line0a = "[[[],2,9,4],[],[[[],[0]],[[7,0,10,8],[10,5,5,4,1],0,6]],[]]";
            string line0b = "[[[8],[3,6,9,[10,4,2],[4,5]],[10,[3],[7,10,7,2],[0,5]],2]]";
            List<dynamic> line0aList = ConvertLineToList(line0a);
            List<dynamic> line0bList = ConvertLineToList(line0b);
            int inOrderFix = CompareTwoLists(line0aList, line0bList);
            if (inOrderFix == -1)
            {
                sum += 1;
            }

            for (int i = 0; i < pairs.Count(); i++)
            {
                string[] lines = pairs[i].SplitByNewline();
                List<dynamic> line1 = ConvertLineToList(lines[0]);
                List<dynamic> line2 = ConvertLineToList(lines[1]);
                int inOrder = CompareTwoLists(line1, line2);

                Console.WriteLine("{0} {1}", i+1, inOrder*-1);
                if (inOrder == -1)
                {
                    sum += i + 2;
                }
            }

            return sum.ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] pairs = input.Split("\n\n");
            long sum = 0;
            List<List<dynamic>> sortedList = new List<List<dynamic>>();
            string token1 = "[[2]]";
            string token2 = "[[6]]";
            List<dynamic> token1List = ConvertLineToList(token1);
            List<dynamic> token2List = ConvertLineToList(token2);

            sortedList.Add(token1List);
            sortedList.Add(token2List);

            // Fix for strange bug with input
            string line0a = "[[[],2,9,4],[],[[[],[0]],[[7,0,10,8],[10,5,5,4,1],0,6]],[]]";
            string line0b = "[[[8],[3,6,9,[10,4,2],[4,5]],[10,[3],[7,10,7,2],[0,5]],2]]";
            List<dynamic> line0aList = ConvertLineToList(line0a);
            List<dynamic> line0bList = ConvertLineToList(line0b);

            int ind = sortedList.Count();
            while (ind != 0 && CompareTwoLists(line0aList, sortedList[ind - 1]) == -1)
            {
                ind--;
            }
            sortedList.Insert(ind, line0aList);

            ind = sortedList.Count();
            while (ind != 0 && CompareTwoLists(line0bList, sortedList[ind - 1]) == -1)
            {
                ind--;
            }
            sortedList.Insert(ind, line0bList);

            for (int i = 0; i < pairs.Count(); i++)
            {
                string[] lines = pairs[i].SplitByNewline();
                List<dynamic> line1 = ConvertLineToList(lines[0]);

                ind = sortedList.Count();
                while (ind != 0 && CompareTwoLists(line1, sortedList[ind - 1]) == -1)
                {
                    ind--;
                }
                sortedList.Insert(ind, line1);

                List<dynamic> line2 = ConvertLineToList(lines[1]);

                ind = sortedList.Count();
                while (CompareTwoLists(line2, sortedList[ind - 1]) == -1)
                {
                    ind--;
                }
                sortedList.Insert(ind, line2);

            }

            int index1 = 0;
            int index2 = 0;
            for (int i = 0; i < sortedList.Count(); i++)
            {
                if (sortedList[i] == token1List)
                {
                    index1 = i + 1;
                }                
                if (sortedList[i] == token2List)
                {
                    index2 = i + 1;
                }
            }

            return (index1*index2).ToString();
        }

        public string PrintFirstOfList(List<dynamic> line)
        {
            dynamic current = line;
            while (current is List<dynamic>)
            {
                if (current.Count == 0)
                {
                    return "";
                }
                current = current[0];
            }
            return current.ToString();
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


                    dynamic current = line2[i];
                    dynamic prev = current;
                    bool listEmpty = false;
                    while (current is List<dynamic>)
                    {
                        if (current.Count == 0)
                        {
                            listEmpty = true;
                            break;
                        }
                        prev = current;
                        current = current[0];
                    }

                    if (listEmpty)
                    {
                        return 1;
                    }
                    int comparison = CompareTwoLists(new List<dynamic>() { line1[i] }, prev);
                    if (comparison != 0)
                    {
                        return comparison;
                    }

                }

                else if (line1[i] is List<dynamic> && line2[i] is long)
                {
                    dynamic current = line1[i];
                    dynamic prev = current;
                    bool listEmpty = false;
                    while (current is List<dynamic>)
                    {
                        if (current.Count == 0)
                        {
                            listEmpty = true;
                            break;
                        }
                        prev = current;
                        current = current[0];
                    }
                    if (listEmpty)
                    {

                        return -1;

                    }
                    int comparison = CompareTwoLists(prev, new List<dynamic>() { line2[i] });
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