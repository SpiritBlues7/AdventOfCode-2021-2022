using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;

namespace AdventOfCode.Solutions.Year2022
{

    class Day05 : ASolution
    {

        public Day05() : base(05, 2022, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] inputParts = input.Split("\n\n");
            string[] cratesStart = inputParts[0].SplitByNewline();
            string[] lines = inputParts[1].SplitByNewline();

            List<Stack<string>> cratesStacks = new List<Stack<string>>();
            int crateAmount = cratesStart[cratesStart.Count() - 1].Split("  ").Count();
            

            for (int j = 0; j < crateAmount; j++)
            {
                Stack<string> st = new Stack<string>();
                cratesStacks.Add(st);
            }


            for (int i = cratesStart.Count() - 2; i >= 0; i--)
            {

                cratesStart[i] = cratesStart[i].Replace("[", "");
                cratesStart[i] = cratesStart[i].Replace("]", "");
                cratesStart[i] = cratesStart[i].Replace("    ", " ");
                string[] crateLine = cratesStart[i].Split(" ");
                for (int j = 0; j < crateAmount; j++)
                {
                    if (j + 1 > crateLine.Count())
                    {
                        continue;
                    }
                    if (crateLine[j] != "" && crateLine[j] != "0")
                    {
                        cratesStacks[j].Push(crateLine[j]);
                    }
                }
            }

            foreach (string line in lines)
            {
                int moveAmount = int.Parse(line.Replace("move ", "").Split(" from")[0]);
                int fromCrate = int.Parse(line.Split(" from ")[1].Split(" to ")[0]);
                int toCrate = int.Parse(line.Split(" to ")[1]);

                for (int i = 0; i < moveAmount; i++)
                {
                    if (cratesStacks[fromCrate - 1].Count() <= 0)
                    {
                        continue;
                    }
                    cratesStacks[toCrate - 1].Push(cratesStacks[fromCrate - 1].Pop());
                }
            }

            string ans = "";
            for (int j = 0; j < crateAmount; j++)
            {
                ans += cratesStacks[j].Pop();
            }

            return ans;
        }

        protected override string SolvePartTwo(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] inputParts = input.Split("\n\n");
            string[] cratesStart = inputParts[0].SplitByNewline();
            string[] lines = inputParts[1].SplitByNewline();

            List<Stack<string>> cratesStacks = new List<Stack<string>>();
            int crateAmount = cratesStart[cratesStart.Count() - 1].Split("  ").Count();


            for (int j = 0; j < crateAmount; j++)
            {
                Stack<string> st = new Stack<string>();
                cratesStacks.Add(st);
            }


            for (int i = cratesStart.Count() - 2; i >= 0; i--)
            {

                cratesStart[i] = cratesStart[i].Replace("[", "");
                cratesStart[i] = cratesStart[i].Replace("]", "");
                cratesStart[i] = cratesStart[i].Replace("    ", " ");
                string[] crateLine = cratesStart[i].Split(" ");
                for (int j = 0; j < crateAmount; j++)
                {
                    if (j + 1 > crateLine.Count())
                    {
                        continue;
                    }
                    if (crateLine[j] != "" && crateLine[j] != "0")
                    {
                        cratesStacks[j].Push(crateLine[j]);
                    }

                }
            }

            foreach (string line in lines)
            {
                int moveAmount = int.Parse(line.Replace("move ", "").Split(" from")[0]);
                int fromCrate = int.Parse(line.Split(" from ")[1].Split(" to ")[0]);
                int toCrate = int.Parse(line.Split(" to ")[1]);

                Stack<string> st = new Stack<string>();
                for (int i = 0; i < moveAmount; i++)
                {
                    if (cratesStacks[fromCrate - 1].Count() <= 0)
                    {
                        continue;
                    }
                    
                    st.Push(cratesStacks[fromCrate - 1].Pop());
                }

                for (int i = 0; i < moveAmount; i++)
                {
                    if (st.Count() <= 0)
                    {
                        continue;
                    }
                    cratesStacks[toCrate - 1].Push(st.Pop());
                }   
            }

            string ans = "";
            for (int j = 0; j < crateAmount; j++)
            {
                ans += cratesStacks[j].Pop();
            }

            return ans;
        }
    }
}