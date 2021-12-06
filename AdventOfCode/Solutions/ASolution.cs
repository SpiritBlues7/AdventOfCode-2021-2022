using System;
using System.Collections.Generic;
using System.Diagnostics;
using AdventOfCode.Infrastructure.Exceptions;
using AdventOfCode.Infrastructure.Helpers;
using AdventOfCode.Infrastructure.Models;

namespace AdventOfCode.Solutions
{
    abstract class ASolution
    {
        Lazy<string> _input, _debugInput;
        Lazy<SolutionResult> _part1, _part2;

        public int Day { get; }
        public int Year { get; }
        public string Title { get; }
        public string Input => Debug ? DebugInput : _input.Value ?? null;
        public SolutionResult Part1 => _part1.Value;
        public SolutionResult Part2 => _part2.Value;

        public string DebugInput => _debugInput.Value ?? null;
        public bool Debug { get; set; }

        private protected ASolution(int day, int year, string title, bool useDebugInput = false)
        {
            Day = day;
            Year = year;
            Title = title;
            Debug = useDebugInput;
            _input = new Lazy<string>(() => InputHelper.LoadInput(Day, Year));
            _debugInput = new Lazy<string>(() => InputHelper.LoadDebugInput(Day, Year));
            _part1 = new Lazy<SolutionResult>(() => SolveSafely(SolvePartOne));
            _part2 = new Lazy<SolutionResult>(() => SolveSafely(SolvePartTwo));
        }

        public void Solve(int part = 0)
        {

            if (Input == null) return;

            bool doOutput = false;
            string output = $"--- Day {Day}: {Title} --- \n";
            if (DebugInput != null)
            {
                output += $"!!! DebugInput used: {DebugInput}\n";
            }

            if (part != 2)
            {
                if (!string.IsNullOrEmpty(_part1.Value.Answer))
                {
                    output += $"Part 1: {_part1.Value.Answer}\n";
                    doOutput = true;
                }
                else
                {
                    output += "Part 1: Unsolved\n";
                    if (part == 1) doOutput = true;
                }
            }
            if (part != 1)
            {
                if (!string.IsNullOrEmpty(_part2.Value.Answer))
                {
                    output += $"Part 2: {_part2.Value.Answer}\n";
                    doOutput = true;
                }
                else
                {
                    output += "Part 2: Unsolved\n";
                    if (part == 2) doOutput = true;
                }
            }

            if (doOutput) Console.WriteLine(output);

            if (!string.IsNullOrWhiteSpace(DebugInput))
            {
                Console.WriteLine($"Debug Part 1: {SolvePartOne(DebugInput)}");
            }
            Console.WriteLine($"Real Part 1: {SolvePartOne(_input.Value)}");

            if (!string.IsNullOrWhiteSpace(DebugInput))
            {
                Console.WriteLine($"Debug Part 2: {SolvePartTwo(DebugInput)}");
            }
            Console.WriteLine($"Real Part 2: {SolvePartTwo(_input.Value)}");
        }

        SolutionResult SolveSafely(Func<string> SolverFunction)
        {
            if (Debug)
            {
                if (string.IsNullOrEmpty(DebugInput))
                {
                    throw new InputException("DebugInput is null or empty");
                }
            }
            else if (string.IsNullOrEmpty(Input))
            {
                throw new InputException("Input is null or empty");
            }

            try
            {
                var then = DateTime.Now;
                var result = SolverFunction();
                var now = DateTime.Now;
                return string.IsNullOrEmpty(result) ? SolutionResult.Empty : new SolutionResult { Answer = result, Time = now - then };
            }
            catch (Exception)
            {
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                    return SolutionResult.Empty;
                }
                else
                {
                    throw;
                }
            }
        }

        protected virtual string SolvePartOne()
        {
            return "";
        }
        protected virtual string SolvePartTwo()
        {
            return "";
        }

        protected virtual string SolvePartOne(string input)
        {
            return "";

        }

        protected virtual string SolvePartTwo(string input)
        {
            return "";
        }
    }
}
