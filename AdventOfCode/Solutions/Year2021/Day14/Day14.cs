using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2021
{

    class Day14 : ASolution
    {


        public Day14() : base(14, 2021, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            var fixedInput = input.Replace("\r\n", "\n");
            string[] components = fixedInput.Split("\n\n");
            var current = components[0];
            var insertions = components[1].SplitByNewline();

            var insertionComplete = new Dictionary<string, char>();
            foreach (var insertion in insertions)
            {
                var insertComp = insertion.Split(" -> ");
                var insertRule = insertComp[0];
                var insertChar = insertComp[1];

                insertionComplete[insertRule] = insertChar[0];
            }


            for (int count = 0; count < 10; count++)
            {
                string newString = "";
                for (int i = 0; i < current.Length - 1; i++)
                {
                    newString += current[i];
                    var twoChars = current[i].ToString() + current[i + 1].ToString();
                    if (insertionComplete.ContainsKey(twoChars))
                    {
                        newString += insertionComplete[twoChars];
                    }


                }
                newString += current.Last();
                current = newString;
                //Console.WriteLine(newString);
            }

            var charArr = current.ToCharArray();
            var queryMin = charArr.GroupBy(item => item).OrderByDescending(g => g.Count()).Select(g => g.Key).Last();
            var queryMax = charArr.GroupBy(item => item).OrderByDescending(g => g.Count()).Select(g => g.Key).First();

            var queryMinNum = charArr.Count(g => g == queryMin);
            var queryMaxNum = charArr.Count(g => g == queryMax);
            Console.WriteLine("Min Char: " + queryMin.ToString());
            Console.WriteLine("Max Char: " + queryMax.ToString());
            return (queryMaxNum - queryMinNum).ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            var fixedInput = input.Replace("\r\n", "\n");
            string[] components = fixedInput.Split("\n\n");
            var current = components[0];
            var insertions = components[1].SplitByNewline();

            var insertionComplete = new Dictionary<string, char>();
            var insertAmounts = new Dictionary<string, long>();
            foreach (var insertion in insertions)
            {
                var insertComp = insertion.Split(" -> ");
                var insertRule = insertComp[0];
                var insertChar = insertComp[1];

                insertionComplete[insertRule] = insertChar[0];
                insertAmounts[insertRule] = 0;
            }

            for (int i = 0; i < current.Length - 1; i++)
            {
                var twoChars = current[i].ToString() + current[i + 1].ToString();
                if (insertAmounts.ContainsKey(twoChars))
                {
                    insertAmounts[twoChars]++;
                }
            }


            for (int count = 0; count < 40; count++)
            {
                var insertAmountsNew = new Dictionary<string, long>();

                foreach (KeyValuePair<string, long> entry in insertAmounts)
                {
                    var insertChar = insertionComplete[entry.Key];
                    var insertPairLeft = entry.Key[0].ToString() + insertChar.ToString();
                    var insertPairRight = insertChar.ToString() + entry.Key[1].ToString();
                    if (!insertAmountsNew.ContainsKey(insertPairLeft))
                    {
                        insertAmountsNew[insertPairLeft] = entry.Value;
                    } 
                    else
                    {
                        insertAmountsNew[insertPairLeft] += entry.Value;
                    }
                    if (!insertAmountsNew.ContainsKey(insertPairRight))
                    {
                        insertAmountsNew[insertPairRight] = entry.Value;
                    } else
                    {
                        insertAmountsNew[insertPairRight] += entry.Value;
                    }
                    
                    
                }

                insertAmounts = new Dictionary<string, long>(insertAmountsNew);
            }

            var letterCounts = new Dictionary<char, long>();
            
            foreach (KeyValuePair<string, long> entry in insertAmounts)
            {
                if (!letterCounts.ContainsKey(entry.Key[0]))
                {
                    letterCounts[entry.Key[0]] = entry.Value;
                } else
                {
                    letterCounts[entry.Key[0]] += entry.Value;
                }    

            }

            letterCounts[current.Last()] += 1;

            var maxLetterCount = letterCounts.Max(x => x.Value);
            var minLetterCount = letterCounts.Min(x => x.Value);
            return (maxLetterCount - minLetterCount).ToString();
        }
    }
}