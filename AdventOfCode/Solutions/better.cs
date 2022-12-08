using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    public static class Better
    {
        public static List<long> InputTo1dLong(string input, List<string> delimiters, int groupCount, bool shouldTrim = true, bool asDigits = true, bool filterRemainingCharacter = true)
        {
            var retval = new List<long>();
            bool eachChar = delimiters.Contains("");
            delimiters.Remove("");
            var splitInput = input.Split(delimiters.ToArray(), shouldTrim ? (StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) : StringSplitOptions.None).Select(s => s).ToList();
            if (eachChar)
            {
                for (int i = 0; i < splitInput.Count; i++)
                {
                    if (splitInput[i].Length > 1)
                    {
                        var tempInput = splitInput[i];
                        splitInput[i] = tempInput[0].ToString();
                        for (int j = 1; j < tempInput.Length; j++)
                        {
                            splitInput.Insert(i + j, tempInput[j].ToString());
                        }
                        i = i + tempInput.Length - 1;
                    }
                }
            }

            if (filterRemainingCharacter)
            {
                splitInput = splitInput.Select(s => new string(s.Where(char.IsNumber).ToArray())).ToList();
            }


            retval = splitInput.Select(s => s.ToLong()).ToList();


            if (groupCount > 1)
            {
                var tempRetval = new List<long>();
                for (int i = 0; i < retval.Count; i += groupCount)
                {
                    var str = "";
                    long sum = 0;
                    for (int j = 0; j < groupCount; j++)
                    {
                        if (retval.Count > i + j)
                        {
                            str += retval[i + j].ToString();
                            sum += retval[i + j];
                        }
                    }

                    if (asDigits)
                    {
                        tempRetval.Add(long.Parse(str));
                    }
                    else
                    {
                        tempRetval.Add(sum);
                    }
                }

                retval = tempRetval;
            }
            return retval;
        }

        public static List<List<long>> InputTo2dLong(string input, List<string> firstDelimiters, List<string> secondDelimiters, int groupCount, bool shouldTrim = true, bool asDigits = true, bool filterRemainingCharacter = true)
        {
            var retval = new List<List<long>>();
            var firstLevelProcessing = InputTo1dString(input, firstDelimiters, 1, shouldTrim);
            foreach (var item in firstLevelProcessing)
            {
                retval.Add(InputTo1dLong(item, new List<string>(secondDelimiters), groupCount, shouldTrim, asDigits, filterRemainingCharacter));
            }
            return retval;
        }

        public static List<string> InputTo1dString(string input, List<string> delimiters, int groupCount, bool shouldTrim = true)
        {
            bool eachChar = delimiters.Contains("");
            delimiters.Remove("");
            var splitInput = input.Split(delimiters.ToArray(), shouldTrim ? (StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) : StringSplitOptions.None).Select(s => s).ToList();
            if (eachChar)
            {
                for (int i = 0; i < splitInput.Count; i++)
                {
                    if (splitInput[i].Length > 1)
                    {
                        var tempInput = splitInput[i];
                        splitInput[i] = tempInput[0].ToString();
                        for (int j = 1; j < tempInput.Length; j++)
                        {
                            splitInput.Insert(i + j, tempInput[j].ToString());
                        }
                        i = i + tempInput.Length - 1;
                    }
                }
            }

            if (groupCount > 1)
            {
                var tempRetval = new List<string>();
                for (int i = 0; i < splitInput.Count; i += groupCount)
                {
                    var str = "";
                    long sum = 0;
                    for (int j = 0; j < groupCount; j++)
                    {
                        if (splitInput.Count > i + j)
                        {
                            str += splitInput[i + j];

                        }
                    }
                    tempRetval.Add(str);
                }

                splitInput = tempRetval;
            }
            return splitInput;
        }

        public static List<List<string>> InputTo2dString(string input, List<string> firstDelimiters, List<string> secondDelimiters, int groupCount, bool shouldTrim = true)
        {
            var retval = new List<List<string>>();
            var firstLevelProcessing = InputTo1dString(input, firstDelimiters, 1, shouldTrim);
            foreach (var item in firstLevelProcessing)
            {
                retval.Add(InputTo1dString(item, new List<string>(secondDelimiters), groupCount, shouldTrim));
            }
            return retval;
        }

        public static List<decimal> InputTo1dDecimal(string input, List<string> delimiters, int groupCount, bool shouldTrim = true, bool asDigits = true, bool filterRemainingCharacter = true)
        {
            var retval = new List<decimal>();
            bool eachChar = delimiters.Contains("");
            delimiters.Remove("");
            var splitInput = input.Split(delimiters.ToArray(), shouldTrim ? (StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) : StringSplitOptions.None).Select(s => s).ToList();
            if (eachChar)
            {
                for (int i = 0; i < splitInput.Count; i++)
                {
                    if (splitInput[i].Length > 1)
                    {
                        var tempInput = splitInput[i];
                        splitInput[i] = tempInput[0].ToString();
                        for (int j = 1; j < tempInput.Length; j++)
                        {
                            splitInput.Insert(i + j, tempInput[j].ToString());
                        }
                        i = i + tempInput.Length - 1;
                    }
                }
            }

            if (filterRemainingCharacter)
            {
                splitInput = splitInput.Select(s => new string(s.Where(char.IsNumber).ToArray())).ToList();
            }


            retval = splitInput.Select(s => s.ToDecimal()).ToList();


            if (groupCount > 1)
            {
                var tempRetval = new List<decimal>();
                for (int i = 0; i < retval.Count; i += groupCount)
                {
                    var str = "";
                    decimal sum = 0;
                    for (int j = 0; j < groupCount; j++)
                    {
                        if (retval.Count > i + j)
                        {
                            str += retval[i + j].ToString();
                            sum += retval[i + j];
                        }
                    }

                    if (asDigits)
                    {
                        tempRetval.Add(decimal.Parse(str));
                    }
                    else
                    {
                        tempRetval.Add(sum);
                    }
                }

                retval = tempRetval;
            }
            return retval;
        }
        public static List<List<decimal>> InputTo2dDecimal(string input, List<string> firstDelimiters, List<string> secondDelimiters, int groupCount, bool shouldTrim = true, bool asDigits = true, bool filterRemainingCharacter = true)
        {
            var retval = new List<List<decimal>>();
            var firstLevelProcessing = InputTo1dString(input, firstDelimiters, 1, shouldTrim);
            foreach (var item in firstLevelProcessing)
            {
                retval.Add(InputTo1dDecimal(item, new List<string>(secondDelimiters), groupCount, shouldTrim, asDigits, filterRemainingCharacter));
            }
            return retval;
        }

        public static long ToLong(this string value)
        {
            long retval = 0;
            long.TryParse(value, out retval);
            return retval;
        }

        public static decimal ToDecimal(this string value)
        {
            decimal retval = 0;
            decimal.TryParse(value, out retval);
            return retval;
        }

        public static (long increasing, long decreasing, long same) CountIncreasing(List<long> values)
        {
            var increasing = 0L;
            var decreasing = 0L;
            var same = 0L;
            for (int i = 1; i < values.Count; i++)
            {
                if (values[i - 1] > values[i])
                {
                    decreasing++;
                }
                else if (values[i - 1] < values[i])
                {
                    increasing++;
                }
                else
                {
                    same++;
                }
            }
            return (increasing, decreasing, same);
        }

        public static (long increasing, long decreasing, long same) CountIncreasing(List<long> values, long target)
        {
            var increasing = 0L;
            var decreasing = 0L;
            var same = 0L;
            for (int i = 0; i < values.Count; i++)
            {
                if (target > values[i])
                {
                    decreasing++;
                }
                else if (target < values[i])
                {
                    increasing++;
                }
                else
                {
                    same++;
                }
            }
            return (increasing, decreasing, same);
        }

        public static List<long> SumSliding(List<long> values, int slideCount)
        {
            var retval = new List<long>();
            for (int i = 0; i < values.Count; i++)
            {
                var sum = 0L;
                for (int j = i; j < i + slideCount; j++)
                {
                    if (values.Count > j)
                    {
                        sum += values[j];
                    }
                }
                retval.Add(sum);
            }
            return retval;
        }

        public static List<long> SumShifting(List<long> values, int slideCount)
        {
            var retval = new List<long>();
            for (int i = 0; i < values.Count; i = i + slideCount)
            {
                var sum = 0L;
                for (int j = i; j < i + slideCount; j++)
                {
                    if (values.Count > j)
                    {
                        sum += values[j];
                    }
                }
                retval.Add(sum);
            }
            return retval;
        }

        public static List<LongSum> FindEntriesThatSumToValue(List<long> values, long target, int entryCount)
        {
            List<LongSum> retval = new List<LongSum>();
            List<LongSum> currentList = new List<LongSum>();
            List<LongSum> nextList = new List<LongSum>();

            for (int i = 0; i < values.Count; i++)
            {
                nextList.Add(new LongSum(new List<long>() { values[i] }, new List<int>() { i }, values[i]));
            }


            int depth = 1;
            while (nextList.Count > 0 && depth < entryCount)
            {
                depth++;
                currentList = nextList;
                nextList = new List<LongSum>();

                for (int i = 0; i < currentList.Count; i++)
                {
                    for (int j = currentList[i].Indicies.Last() + 1; j < values.Count; j++)
                    {
                        var newSum = currentList[i].Sum + values[j];
                        var newItems = new List<long>(currentList[i].Values);
                        newItems.Add(values[j]);
                        var newIndicies = new List<int>(currentList[i].Indicies);
                        newIndicies.Add(j);

                        var nextLongSum = new LongSum(newItems, newIndicies, newSum);

                        if (newSum == target && newIndicies.Count == entryCount)
                        {
                            retval.Add(nextLongSum);
                        }
                        else if (newSum < target)
                        {
                            nextList.Add(nextLongSum);
                        }
                    }
                }
            }

            return retval;
        }
    }

    public class LongSum
    {
        public List<long> Values { get; set; } = new List<long>();
        public List<int> Indicies { get; set; } = new List<int>();
        public long Sum { get; set; }

        public LongSum(List<long> values, List<int> indicies, long sum)
        {
            Values = values;
            Indicies = indicies;
            Sum = sum;
        }
    }
}