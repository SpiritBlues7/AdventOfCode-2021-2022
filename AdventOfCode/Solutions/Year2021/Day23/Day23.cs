using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2021
{

    class Day23 : ASolution
    {


        public Day23() : base(23, 2021, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");
            var lines = input.SplitByNewline();

            int[] hallway = new int[11];
            int[] hall1 = new int[4];
            int[] hall2 = new int[4];
            int[] hall3 = new int[4];
            int[] hall4 = new int[4];

            for (int i = 0; i < lines.Length; i++)
            {
                if (i == 2)
                {
                    var hallOneElements = lines[i].Replace("#", "");
                    hall1[0] = hallOneElements[0] - 'A' + 1;
                    hall2[0] = hallOneElements[1] - 'A' + 1;
                    hall3[0] = hallOneElements[2] - 'A' + 1;
                    hall4[0] = hallOneElements[3] - 'A' + 1;
                }
                if (i == 3)
                {
                    var hallTwoElements = lines[i].Replace("#", "");
                    var hallTwoElementsFix = hallTwoElements.Replace(" ", "");
                    hall1[3] = 1;
                    hall2[3] = 2;
                    hall3[3] = 3;
                    hall4[3] = 4;
                }                
                if (i == 4)
                {
                    var hallTwoElements = lines[i].Replace("#", "");
                    var hallTwoElementsFix = hallTwoElements.Replace(" ", "");
                    hall1[2] = 1;
                    hall2[2] = 2;
                    hall3[2] = 3;
                    hall4[2] = 4;
                }                
                if (i == 5)
                {
                    var hallTwoElements = lines[i].Replace("#", "");
                    var hallTwoElementsFix = hallTwoElements.Replace(" ", "");
                    hall1[1] = hallTwoElementsFix[0] - 'A' + 1;
                    hall2[1] = hallTwoElementsFix[1] - 'A' + 1;
                    hall3[1] = hallTwoElementsFix[2] - 'A' + 1;
                    hall4[1] = hallTwoElementsFix[3] - 'A' + 1;
                }
            }

            var queue = new Queue<(List<int[]>, long, List<string>)>();

            Dictionary<string, long> totals = new Dictionary<string, long>();
            queue.Enqueue((new List<int[]>() { hallway, hall1, hall2, hall3, hall4 }, 0, new List<string>() { GetAsString(new List<int[]>() { hallway, hall1, hall2, hall3, hall4 }) }));

            
            totals[GetAsString(new List<int[]>() { hallway, hall1, hall2, hall3, hall4 })] = 0;

            int[] movement = new int[5];
            movement[1] = 1;
            movement[2] = 10;
            movement[3] = 100;
            movement[4] = 1000;


            long best = 9999999;
            List<string> bestPath = new List<string>();
            while (queue.Count > 0)
            {

                var positions = queue.Dequeue();
                var cHalls = positions.Item1;
                var cHallway = cHalls[0];
                var cTotal = positions.Item2;
                var cPath = positions.Item3;
                var cMemKey = GetAsString(positions.Item1);

                //Console.WriteLine(cHalls[1][0].ToString() + cHalls[1][1].ToString());
                if (cHalls[1][0] == 1 && cHalls[1][1] == 1 && cHalls[1][2] == 1 && cHalls[1][3] == 1 &&
                    cHalls[2][0] == 2 && cHalls[2][1] == 2 && cHalls[2][2] == 2 && cHalls[2][3] == 2 &&
                    cHalls[3][0] == 3 && cHalls[3][1] == 3 && cHalls[3][2] == 3 && cHalls[3][3] == 3 &&
                    cHalls[4][0] == 4 && cHalls[4][1] == 4 && cHalls[4][2] == 4 && cHalls[4][3] == 4)
                {
                    if (cTotal < best)
                    {
                        best = cTotal;
                        bestPath = cPath;
                    }
                }

                for (int h = 1; h < 5; h++)
                {
                    int currentHallSpot = 0;
                    if (cHalls[h][0] == 0)
                    {
                        currentHallSpot = 1;
                        if (cHalls[h][1] == 0)
                        {
                            currentHallSpot = 2;
                            if (cHalls[h][2] == 0)
                            {
                                currentHallSpot = 3;
                            }
                        }
                    }
                    if (cHalls[h][currentHallSpot] != h || (currentHallSpot < 3 && (
                        cHalls[h][Math.Min(3, currentHallSpot + 1)] != h || 
                        cHalls[h][Math.Min(3, currentHallSpot + 2)] != h || 
                        cHalls[h][Math.Min(3, currentHallSpot + 3)] != h)))
                    {

                        for (int j = 0; j < cHallway.Length; j++)
                        {
                            if (cHallway[j] == 0)
                            {

                                if (j % 2 == 0 && j != 0 && j != 10)
                                {
                                    continue;
                                }
                                else
                                {

                                    bool valid = true;
                                    for (int v = 1; v <= Math.Abs(j - 2*h); v++)
                                    {
                                        if (j > 2*h)
                                        {
                                            if (cHallway[j - v] != 0)
                                            {
                                                valid = false;
                                                break;
                                            }
                                        }                                        
                                        if (j < 2*h)
                                        {
                                            if (cHallway[j + v] != 0)
                                            {
                                                valid = false;
                                                break;
                                            }
                                        }
                                    }
                                    if (!valid)
                                    {
                                        continue;
                                    }

                                    int[] newHallway = new int[11];
                                    cHallway.CopyTo(newHallway, 0);
                                    int[] newHall = new int[4];
                                    cHalls[h].CopyTo(newHall, 0);

                                    newHallway[j] = newHall[currentHallSpot];
                                    newHall[currentHallSpot] = 0;
                                    long cNewTotal = cTotal + movement[cHalls[h][currentHallSpot]] * (Math.Abs(j - (h * 2)) + 1 + currentHallSpot);

                                    var queueItem = new List<int[]>() { newHallway, cHalls[1], cHalls[2], cHalls[3], cHalls[4] };
                                    queueItem[h] = newHall;

                                    var memKey = GetAsString(queueItem);


                                    if (totals.ContainsKey(memKey))
                                    {
                                        if (totals[memKey] <= cNewTotal)
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            totals[memKey] = cNewTotal;
                                        }
                                    }
                                    else
                                    {
                                        totals[memKey] = cNewTotal;
                                    }

                                    //Console.WriteLine(memKey + ": " + cTotal.ToString());
                                    var newPath = new List<string>(cPath);
                                    newPath.Add(memKey + ": " + (cNewTotal - cTotal).ToString());
                                    queue.Enqueue((queueItem, cNewTotal, newPath));



                                }
                            }
                        }

                    }
                }
                for (int i = 0; i < cHallway.Length; i++)
                {
                    if (cHallway[i] != 0)
                    {
                        int hallElem = cHallway[i];

                        int currentHallSpot2 = 0;
                        if (cHalls[hallElem][0] == 0 && (cHalls[hallElem][1] != hallElem || cHalls[hallElem][2] != hallElem || cHalls[hallElem][3] != hallElem))
                        {
                            currentHallSpot2 = 1;
                            if (cHalls[hallElem][1] == 0 && (cHalls[hallElem][2] != hallElem || cHalls[hallElem][3] != hallElem))
                            {
                                currentHallSpot2 = 2;
                                if (cHalls[hallElem][2] == 0 && cHalls[hallElem][3] != hallElem)
                                {
                                    currentHallSpot2 = 3;
                                }
                            }
                        }

                        if (cHalls[hallElem][currentHallSpot2] == 0 && (currentHallSpot2 == 3 || (
                            cHalls[hallElem][Math.Min(3, currentHallSpot2 + 1)] == hallElem &&
                            cHalls[hallElem][Math.Min(3, currentHallSpot2 + 2)] == hallElem &&
                            cHalls[hallElem][Math.Min(3, currentHallSpot2 + 3)] == hallElem)))
                        {
                            bool valid = true;
                            for (int v = 1; v <= Math.Abs(i - 2 * hallElem); v++)
                            {
                                if (i > 2 * hallElem)
                                {
                                    if (cHallway[i - v] != 0)
                                    {
                                        valid = false;
                                        break;
                                    }
                                }
                                if (i < 2 * hallElem)
                                {
                                    if (cHallway[i + v] != 0)
                                    {
                                        valid = false;
                                        break;
                                    }
                                }
                            }
                            if (!valid)
                            {
                                continue;
                            }

                            int[] newHallway = new int[11];
                            cHallway.CopyTo(newHallway, 0);
                            int[] newHall = new int[4];
                            cHalls[hallElem].CopyTo(newHall, 0);

                            newHallway[i] = 0;
                            newHall[currentHallSpot2] = hallElem;
                            long cNewTotal = cTotal + movement[hallElem] * (Math.Abs(i - (hallElem * 2)) + 1 + currentHallSpot2);



                            var queueItem = new List<int[]>() { newHallway, cHalls[1], cHalls[2], cHalls[3], cHalls[4] };
                            queueItem[hallElem] = newHall;


                            var memKey = GetAsString(queueItem);


                            if (totals.ContainsKey(memKey))
                            {
                                if (totals[memKey] <= cNewTotal)
                                {
                                    continue;
                                }
                                else
                                {
                                    totals[memKey] = cNewTotal;
                                }
                            }
                            else
                            {
                                totals[memKey] = cNewTotal;
                            }


                            //Console.WriteLine(memKey + ": " + cTotal.ToString());
                            var newPath = new List<string>(cPath);
                            newPath.Add(memKey + ": " + (cNewTotal - cTotal).ToString());
                            queue.Enqueue((queueItem, cNewTotal, newPath));
                        }
                    }
                }
                for (int h = 1; h < 5; h++)
                {
                    int currentHallSpot = -1;

                    if (cHalls[h][0] == 0 && (cHalls[h][1] == h && cHalls[h][2] == h && cHalls[h][3] == h))
                    {
                        currentHallSpot = 0;
                    }
                    if (cHalls[h][1] == 0 && (cHalls[h][2] == h && cHalls[h][3] == h))
                    {
                        currentHallSpot = 1;

                    }
                    if (cHalls[h][2] == 0 && (cHalls[h][3] == h))
                    {
                        currentHallSpot = 2;
                    }
                    if (cHalls[h][3] == 0)
                    {
                        currentHallSpot = 3;
                    }

                    if (currentHallSpot != -1 && cHalls[h][currentHallSpot] == 0)
                    {
                        for (int h2 = 1; h2 < 5; h2++)
                        {
                            if (h == h2)
                            {
                                continue;
                            }
                            int currentHallSpotOther = 0;
                            if (cHalls[h2][0] == 0)
                            {
                                currentHallSpotOther = 1;
                                if (cHalls[h2][1] == 0)
                                {
                                    currentHallSpotOther = 2;
                                    if (cHalls[h2][2] == 0)
                                    {
                                        currentHallSpotOther = 3;
                                    }
                                }
                            }

                            if (cHalls[h2][currentHallSpotOther] == h)
                            {

                                bool valid = true;
                                for (int v = 1; v <= Math.Abs(2 * h - 2 * h2); v++)
                                {
                                    if (2 * h > 2 * h2)
                                    {
                                        if (cHallway[2 * h - v] != 0)
                                        {
                                            valid = false;
                                            break;
                                        }
                                    }
                                    if (2 * h < 2 * h2)
                                    {
                                        if (cHallway[2 * h + v] != 0)
                                        {
                                            valid = false;
                                            break;
                                        }
                                    }
                                }
                                if (!valid)
                                {
                                    continue;
                                }

                                int[] newHallA = new int[4];
                                cHalls[h].CopyTo(newHallA, 0);
                                int[] newHallB = new int[4];
                                cHalls[h2].CopyTo(newHallB, 0);

                                newHallB[currentHallSpotOther] = 0;
                                newHallA[currentHallSpot] = h;
                                long cNewTotal = cTotal + movement[h] * (Math.Abs((h - h2) * 2) + 2 + currentHallSpot + currentHallSpotOther);

                                var queueItem = new List<int[]>() { cHallway, cHalls[1], cHalls[2], cHalls[3], cHalls[4] };
                                queueItem[h] = newHallA;
                                queueItem[h2] = newHallB;


                                var memKey = GetAsString(queueItem);

                                if (totals.ContainsKey(memKey))
                                {
                                    if (totals[memKey] <= cNewTotal)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        totals[memKey] = cNewTotal;
                                    }
                                }
                                else
                                {
                                    totals[memKey] = cNewTotal;
                                }
                                //Console.WriteLine(memKey + ": " + cTotal.ToString());
                                var newPath = new List<string>(cPath);
                                newPath.Add(memKey + ": " + (cNewTotal - cTotal).ToString());
                                queue.Enqueue((queueItem, cNewTotal, newPath));


                            }
                        }
                    }
                }

            }
            

            foreach (var path in bestPath)
            {
                //Console.WriteLine(path);
            }
            return best.ToString();
            
        }

        public string GetAsString(List<int[]> queueItem)
        {
            string result = "";
            for (int k = 0; k < queueItem.Count; k++)
            {
                for (int j = 0; j < queueItem[k].Length; j++)
                {
                    result += queueItem[k][j];
                }

            }
            return result;
        }

        protected override string SolvePartTwo(string input)
        {
            input = input.Replace("\r\n", "\n");
            var lines = input.SplitByNewline();

            int[] hallway = new int[11];
            int[] hall1 = new int[4];
            int[] hall2 = new int[4];
            int[] hall3 = new int[4];
            int[] hall4 = new int[4];

            for (int i = 0; i < lines.Length; i++)
            {
                if (i == 2)
                {
                    var hallOneElements = lines[i].Replace("#", "");
                    hall1[0] = hallOneElements[0] - 'A' + 1;
                    hall2[0] = hallOneElements[1] - 'A' + 1;
                    hall3[0] = hallOneElements[2] - 'A' + 1;
                    hall4[0] = hallOneElements[3] - 'A' + 1;
                }
                if (i == 3)
                {
                    var hallTwoElements = lines[i].Replace("#", "");
                    var hallTwoElementsFix = hallTwoElements.Replace(" ", "");
                    hall1[1] = hallTwoElementsFix[0] - 'A' + 1;
                    hall2[1] = hallTwoElementsFix[1] - 'A' + 1;
                    hall3[1] = hallTwoElementsFix[2] - 'A' + 1;
                    hall4[1] = hallTwoElementsFix[3] - 'A' + 1;
                }
                if (i == 4)
                {
                    var hallTwoElements = lines[i].Replace("#", "");
                    var hallTwoElementsFix = hallTwoElements.Replace(" ", "");
                    hall1[2] = hallTwoElementsFix[0] - 'A' + 1;
                    hall2[2] = hallTwoElementsFix[1] - 'A' + 1;
                    hall3[2] = hallTwoElementsFix[2] - 'A' + 1;
                    hall4[2] = hallTwoElementsFix[3] - 'A' + 1;
                }
                if (i == 5)
                {
                    var hallTwoElements = lines[i].Replace("#", "");
                    var hallTwoElementsFix = hallTwoElements.Replace(" ", "");
                    hall1[3] = hallTwoElementsFix[0] - 'A' + 1;
                    hall2[3] = hallTwoElementsFix[1] - 'A' + 1;
                    hall3[3] = hallTwoElementsFix[2] - 'A' + 1;
                    hall4[3] = hallTwoElementsFix[3] - 'A' + 1;
                }
            }

            var queue = new Queue<(List<int[]>, long, List<string>)>();

            Dictionary<string, long> totals = new Dictionary<string, long>();
            queue.Enqueue((new List<int[]>() { hallway, hall1, hall2, hall3, hall4 }, 0, new List<string>() { GetAsString(new List<int[]>() { hallway, hall1, hall2, hall3, hall4 }) }));


            totals[GetAsString(new List<int[]>() { hallway, hall1, hall2, hall3, hall4 })] = 0;

            int[] movement = new int[5];
            movement[1] = 1;
            movement[2] = 10;
            movement[3] = 100;
            movement[4] = 1000;


            long best = 9999999;
            List<string> bestPath = new List<string>();
            while (queue.Count > 0)
            {

                var positions = queue.Dequeue();
                var cHalls = positions.Item1;
                var cHallway = cHalls[0];
                var cTotal = positions.Item2;
                var cPath = positions.Item3;
                var cMemKey = GetAsString(positions.Item1);


                //Console.WriteLine(cHalls[1][0].ToString() + cHalls[1][1].ToString());
                if (cHalls[1][0] == 1 && cHalls[1][1] == 1 && cHalls[1][2] == 1 && cHalls[1][3] == 1 &&
                    cHalls[2][0] == 2 && cHalls[2][1] == 2 && cHalls[2][2] == 2 && cHalls[2][3] == 2 &&
                    cHalls[3][0] == 3 && cHalls[3][1] == 3 && cHalls[3][2] == 3 && cHalls[3][3] == 3 &&
                    cHalls[4][0] == 4 && cHalls[4][1] == 4 && cHalls[4][2] == 4 && cHalls[4][3] == 4)
                {
                    if (cTotal < best)
                    {
                        best = cTotal;
                        bestPath = cPath;
                    }
                }

                for (int h = 1; h < 5; h++)
                {
                    int currentHallSpot = 0;
                    if (cHalls[h][0] == 0)
                    {
                        currentHallSpot = 1;
                        if (cHalls[h][1] == 0)
                        {
                            currentHallSpot = 2;
                            if (cHalls[h][2] == 0)
                            {
                                currentHallSpot = 3;
                            }
                        }
                    }
                    if (cHalls[h][currentHallSpot] != h || (currentHallSpot < 3 && (
                        cHalls[h][Math.Min(3, currentHallSpot + 1)] != h ||
                        cHalls[h][Math.Min(3, currentHallSpot + 2)] != h ||
                        cHalls[h][Math.Min(3, currentHallSpot + 3)] != h)))
                    {

                        for (int j = 0; j < cHallway.Length; j++)
                        {
                            if (cHallway[j] == 0)
                            {

                                if (j % 2 == 0 && j != 0 && j != 10)
                                {
                                    continue;
                                }
                                else
                                {

                                    bool valid = true;
                                    for (int v = 1; v <= Math.Abs(j - 2 * h); v++)
                                    {
                                        if (j > 2 * h)
                                        {
                                            if (cHallway[j - v] != 0)
                                            {
                                                valid = false;
                                                break;
                                            }
                                        }
                                        if (j < 2 * h)
                                        {
                                            if (cHallway[j + v] != 0)
                                            {
                                                valid = false;
                                                break;
                                            }
                                        }
                                    }
                                    if (!valid)
                                    {
                                        continue;
                                    }

                                    int[] newHallway = new int[11];
                                    cHallway.CopyTo(newHallway, 0);
                                    int[] newHall = new int[4];
                                    cHalls[h].CopyTo(newHall, 0);

                                    newHallway[j] = newHall[currentHallSpot];
                                    newHall[currentHallSpot] = 0;
                                    long cNewTotal = cTotal + movement[cHalls[h][currentHallSpot]] * (Math.Abs(j - (h * 2)) + 1 + currentHallSpot);

                                    var queueItem = new List<int[]>() { newHallway, cHalls[1], cHalls[2], cHalls[3], cHalls[4] };
                                    queueItem[h] = newHall;

                                    var memKey = GetAsString(queueItem);


                                    if (totals.ContainsKey(memKey))
                                    {
                                        if (totals[memKey] <= cNewTotal)
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            totals[memKey] = cNewTotal;
                                        }
                                    }
                                    else
                                    {
                                        totals[memKey] = cNewTotal;
                                    }

                                    //Console.WriteLine(memKey + ": " + cTotal.ToString());
                                    var newPath = new List<string>(cPath);
                                    newPath.Add(memKey + ": " + (cNewTotal - cTotal).ToString());
                                    queue.Enqueue((queueItem, cNewTotal, newPath));



                                }
                            }
                        }

                    }
                }
                for (int i = 0; i < cHallway.Length; i++)
                {
                    if (cHallway[i] != 0)
                    {
                        int hallElem = cHallway[i];

                        int currentHallSpot2 = 0;
                        if (cHalls[hallElem][0] == 0 && (cHalls[hallElem][1] != hallElem || cHalls[hallElem][2] != hallElem || cHalls[hallElem][3] != hallElem))
                        {
                            currentHallSpot2 = 1;
                            if (cHalls[hallElem][1] == 0 && (cHalls[hallElem][2] != hallElem || cHalls[hallElem][3] != hallElem))
                            {
                                currentHallSpot2 = 2;
                                if (cHalls[hallElem][2] == 0 && cHalls[hallElem][3] != hallElem)
                                {
                                    currentHallSpot2 = 3;
                                }
                            }
                        }

                        if (cHalls[hallElem][currentHallSpot2] == 0 && (currentHallSpot2 == 3 || (
                            cHalls[hallElem][Math.Min(3, currentHallSpot2 + 1)] == hallElem &&
                            cHalls[hallElem][Math.Min(3, currentHallSpot2 + 2)] == hallElem &&
                            cHalls[hallElem][Math.Min(3, currentHallSpot2 + 3)] == hallElem)))
                        {
                            bool valid = true;
                            for (int v = 1; v <= Math.Abs(i - 2 * hallElem); v++)
                            {
                                if (i > 2 * hallElem)
                                {
                                    if (cHallway[i - v] != 0)
                                    {
                                        valid = false;
                                        break;
                                    }
                                }
                                if (i < 2 * hallElem)
                                {
                                    if (cHallway[i + v] != 0)
                                    {
                                        valid = false;
                                        break;
                                    }
                                }
                            }
                            if (!valid)
                            {
                                continue;
                            }

                            int[] newHallway = new int[11];
                            cHallway.CopyTo(newHallway, 0);
                            int[] newHall = new int[4];
                            cHalls[hallElem].CopyTo(newHall, 0);

                            newHallway[i] = 0;
                            newHall[currentHallSpot2] = hallElem;
                            long cNewTotal = cTotal + movement[hallElem] * (Math.Abs(i - (hallElem * 2)) + 1 + currentHallSpot2);



                            var queueItem = new List<int[]>() { newHallway, cHalls[1], cHalls[2], cHalls[3], cHalls[4] };
                            queueItem[hallElem] = newHall;


                            var memKey = GetAsString(queueItem);


                            if (totals.ContainsKey(memKey))
                            {
                                if (totals[memKey] <= cNewTotal)
                                {
                                    continue;
                                }
                                else
                                {
                                    totals[memKey] = cNewTotal;
                                }
                            }
                            else
                            {
                                totals[memKey] = cNewTotal;
                            }


                            //Console.WriteLine(memKey + ": " + cTotal.ToString());
                            var newPath = new List<string>(cPath);
                            newPath.Add(memKey + ": " + (cNewTotal - cTotal).ToString());
                            queue.Enqueue((queueItem, cNewTotal, newPath));
                        }
                    }
                }
                for (int h = 1; h < 5; h++)
                {
                    int currentHallSpot = -1;
                    if (cHalls[h][0] == 0 && (cHalls[h][1] == h && cHalls[h][2] == h && cHalls[h][3] == h))
                    {
                        currentHallSpot = 0;
                    }
                    if (cHalls[h][1] == 0 && (cHalls[h][2] == h && cHalls[h][3] == h))
                    {
                        currentHallSpot = 1;

                    }
                    if (cHalls[h][2] == 0 && (cHalls[h][3] == h))
                    {
                        currentHallSpot = 2;
                    }
                    if (cHalls[h][3] == 0)
                    {
                        currentHallSpot = 3;
                    }


                    if (currentHallSpot != -1 && cHalls[h][currentHallSpot] == 0)
                    {
                        for (int h2 = 1; h2 < 5; h2++)
                        {
                            if (h == h2)
                            {
                                continue;
                            }
                            int currentHallSpotOther = 0;
                            if (cHalls[h2][0] == 0)
                            {
                                currentHallSpotOther = 1;
                                if (cHalls[h2][1] == 0)
                                {
                                    currentHallSpotOther = 2;
                                    if (cHalls[h2][2] == 0)
                                    {
                                        currentHallSpotOther = 3;
                                    }
                                }
                            }

                            if (cHalls[h2][currentHallSpotOther] == h)
                            {

                                bool valid = true;
                                for (int v = 1; v <= Math.Abs(2 * h - 2 * h2); v++)
                                {
                                    if (2 * h > 2 * h2)
                                    {
                                        if (cHallway[2 * h - v] != 0)
                                        {
                                            valid = false;
                                            break;
                                        }
                                    }
                                    if (2 * h < 2 * h2)
                                    {
                                        if (cHallway[2 * h + v] != 0)
                                        {
                                            valid = false;
                                            break;
                                        }
                                    }
                                }
                                if (!valid)
                                {
                                    continue;
                                }

                                int[] newHallA = new int[4];
                                cHalls[h].CopyTo(newHallA, 0);
                                int[] newHallB = new int[4];
                                cHalls[h2].CopyTo(newHallB, 0);

                                newHallB[currentHallSpotOther] = 0;
                                newHallA[currentHallSpot] = h;
                                long cNewTotal = cTotal + movement[h] * (Math.Abs((h - h2) * 2) + 2 + currentHallSpot + currentHallSpotOther);

                                var queueItem = new List<int[]>() { cHallway, cHalls[1], cHalls[2], cHalls[3], cHalls[4] };
                                queueItem[h] = newHallA;
                                queueItem[h2] = newHallB;


                                var memKey = GetAsString(queueItem);

                                if (totals.ContainsKey(memKey))
                                {
                                    if (totals[memKey] <= cNewTotal)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        totals[memKey] = cNewTotal;
                                    }
                                }
                                else
                                {
                                    totals[memKey] = cNewTotal;
                                }
                                //Console.WriteLine(memKey + ": " + cTotal.ToString());
                                var newPath = new List<string>(cPath);
                                newPath.Add(memKey + ": " + (cNewTotal - cTotal).ToString());
                                queue.Enqueue((queueItem, cNewTotal, newPath));


                            }
                        }
                    }
                }

            }


            foreach (var path in bestPath)
            {
                //Console.WriteLine(path);
            }
            return best.ToString();
        }
    }
}