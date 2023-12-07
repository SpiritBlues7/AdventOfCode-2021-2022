using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel.Design;

namespace AdventOfCode.Solutions.Year2022
{

    class Day25 : ASolution
    {

        public Day25() : base(25, 2022, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();

            double total = 0;
            foreach (string line in lines)
            {
                double tempTotal = 0;
                int p = 0;
                for (int i = line.Length - 1;i >= 0; i--)
                {
                    int val = 0;
                    if (line[i] == '1' || line[i] == '2' || line[i] == '0')
                    {
                        val = int.Parse(line[i].ToString());
                    } else if (line[i] == '-')
                    {
                        val = -1;
                    } else
                    {
                        val = -2;
                    }
                    double newVal = Math.Pow(5, line.Length - i - 1) * val;
                    tempTotal += newVal;
                }
                total += tempTotal;

            }

            double answer = 0;
            double tempValue = 1;
            List<double> system = new List<double>();
            int count = 0;
            system.Add(tempValue);
            while (tempValue < total)
            {
                tempValue *= 5;
                system.Add(tempValue);
                count++;
            }


            List<double> nums = new List<double>();
            for (int i = 0; i < count; i++)
            {
                double remainder = total;
                answer = Math.Floor(total / system[count - 1 - i]);
                nums.Add(answer);
                if (answer > 2)
                {
                    int j = 0;
                    while(nums[i - 1 - j] + 1 > 2)
                    {
                        nums[i - 1 - j] += 1;
                        nums[i - j] = -(5 - nums[i - j]);
                        j++;
                    }
                    nums[i - 1 - j] += 1;
                    nums[i - j] = -(5 - nums[i - j]);


                }

                remainder = total % system[count - 1 - i];
                total = remainder;

            }

            string ans = "";
            foreach (double item in nums)
            {
                if (item == -1)
                {
                    ans += "-";
                } else if (item == -2)
                {
                    ans += "=";
                } else
                {
                    ans += item.ToString();
                }
                
            }

            return ans;
        }

        protected override string SolvePartTwo(string input)
        {
            return null;
        }
    }
}