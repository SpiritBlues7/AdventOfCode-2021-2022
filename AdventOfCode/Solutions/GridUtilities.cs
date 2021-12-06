using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions
{
    public class GridUtilities
    {
        public Dictionary<Point, GridItem> grid = new Dictionary<Point, GridItem>();
        private int height;
        private int width;

        public void SetValue(int x, int y, GridItem newItem) {
            Point point = new Point(x, y);
            SetValue(point, newItem);
        }

        public void SetValue(Point point, GridItem newItem) {
            grid[point] = newItem;
        }

        public void SetValue(int x, int y, char value) {
            Point point = new Point(x, y);
            SetValue(point, value);
        }

        public void SetValue(Point point, char value) {
            GridItem newItem = grid.GetValueOrDefault(point);
            if (newItem == null) {
                newItem = new GridItem();
            } else {
                newItem = grid[point];
            }
         
            newItem.character = value;
            grid[point] = newItem;
        }

        public void SetValue(int x, int y, int value) {
            Point point = new Point(x, y);
            SetValue(point, value);
        }

        public void SetValue(Point point, int value) {
            GridItem newItem = grid.GetValueOrDefault(point);
            if (newItem == null) {
                newItem = new GridItem();
            } else {
                newItem = grid[point];
            }
         
            newItem.number = value;
            grid[point] = newItem;
        }

        public GridItem GetValue(int x, int y) {
            Point point = new Point(x, y);
            return GetValue(point);
        }

        public GridItem GetValue(Point point) {
            GridItem value = grid.GetValueOrDefault(point);

            if (value == null) {
                return new GridItem();
            }
            return value;
        }

        public Dictionary<Point, GridItem> GetGrid() {
            return grid;
        }

        public void SetHeight(int value) {
            height = value;
        }

        public void SetWidth(int value) {
            width = value;
        }

        public int GetHeight(bool autoSet) {
            if (autoSet) {
                int maxY = grid.Keys.Max(point => point.Y);
                int minY = grid.Keys.Min(point => point.Y);
                return maxY - minY + 1;
            } else {
                return height;
            }
        }

        public int GetWidth(bool autoSet) {
            if (autoSet) {
                int maxX = grid.Keys.Max(point => point.X);
                int minX = grid.Keys.Min(point => point.X);
                return maxX - minX + 1;
            } else {
                return width;
            }
        }

        // type = char or distance
        public void Draw(string type = "char") {
            // +1 and -1 is just to give a border
            int maxX = grid.Keys.Max(point => point.X) + 1;
            int maxY = grid.Keys.Max(point => point.Y) + 1;
            int minX = grid.Keys.Min(point => point.X) - 1;
            int minY = grid.Keys.Min(point => point.Y) - 1;

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    Point point = new Point(x, y);
                    
                    char printValue;
                    GridItem value = GetValue(point);
                    if (type == "distance") {
                        printValue = (value.distance % 10).ToString()[0];
                    } else {
                        printValue = value.character;
                    }
                    

                    if ((x == maxX && y == maxY) || 
                        (x == maxX && y == minY) ||
                        (x == minX && y == maxY) || 
                        (x == minX && y == minY)) {
                        printValue = '+';
                    } 
                    else if (x == maxX || x == minX) {
                        printValue = '|';
                    }
                    else if (y == maxY || y == minY) {
                        printValue = '—';
                    }
                    System.Console.Write(printValue);

                }
                System.Console.Write('\n');
            }
        }


        public static long CalculateManhattanDistance(long x1, long y1, long x2, long y2) {

            long xDistance = Math.Abs(x1 - x2);
            long yDistance = Math.Abs(y1 - y2);

            return xDistance + yDistance;
        }


        public List<Point> GetNeighbours4Dir(int x, int y, int radius)
        {
            int maxX = grid.Keys.Max(point => point.X);
            int maxY = grid.Keys.Max(point => point.Y);
            int minX = grid.Keys.Min(point => point.X);
            int minY = grid.Keys.Min(point => point.Y);
            var retval = new List<Point>();
            for (int x2 = x - radius; x2 <= x + radius; x2++)
            {
                if (x2 == x)
                {
                    continue;
                }
                if (x2 >= minX && x2 <= maxX)
                {
                    retval.Add(new Point(x2, y));
                }
              
            }
            for (int y2 = y - radius; y2 <= y + radius; y2++)
            {
                if ( y2 == y)
                {
                    continue;
                }
                if (y2 >= minY && y2 <= maxY)
                {
                    retval.Add(new Point(x, y2));
                }
            }
            return retval;
        }
        
        public List<Point> GetNeighbours8Dir(int x, int y, int radius)
        {
            int maxX = grid.Keys.Max(point => point.X);
            int maxY = grid.Keys.Max(point => point.Y);
            int minX = grid.Keys.Min(point => point.X);
            int minY = grid.Keys.Min(point => point.Y);
            var retval = new List<Point>();
            for (int x2 = x - radius; x2 <= x + radius; x2++)
            {
                for (int y2 = y - radius; y2 <= y + radius; y2++)
                {
                    if (x2 == x && y2 == y)
                    {
                        continue;
                    }
                    if (x2 >= minX && x2 <= maxX && y2 >= minY && y2 <= maxY)
                    {
                        retval.Add(new Point(x2, y2));
                    }
                }
            }
            return retval;

        }

        public List<Point> GetLineOfSight4Dir(int x, int y, List<char> ignoreChars)
        {
            int maxX = grid.Keys.Max(point => point.X);
            int maxY = grid.Keys.Max(point => point.Y);
            int minX = grid.Keys.Min(point => point.X);
            int minY = grid.Keys.Min(point => point.Y);

            var retval = new List<Point>();
            var top = new Point(x, y - 1);
            var left = new Point(x-1, y);
            var right = new Point(x + 1, y);
            var bottom = new Point(x, y + 1);

            while (top.Y > minY && ignoreChars.Contains(grid[new Point(top.X, top.Y)].character))
            {
                top.Y--;
            }

            while (bottom.Y <= maxY - 1 && ignoreChars.Contains(grid[new Point(bottom.X, bottom.Y)].character))
            {
                bottom.Y++;
            }

            while (left.X > minX && ignoreChars.Contains(grid[new Point(left.X, left.Y)].character))
            {
                left.X--;
            }

            while (right.X <= maxX - 1 && ignoreChars.Contains(grid[new Point(right.X, right.Y)].character))
            {
                right.X++;
            }

            retval.Add(top);
            retval.Add(bottom);
            retval.Add(left);
            retval.Add(right);
            retval = retval.Where(p => p.X >= minX && p.X <= maxX && p.Y >= minY && p.Y <= maxY).ToList();
           
            return retval;

        }

        public List<Point> GetLineOfSight8Dir(int x, int y, List<char> ignoreChars)
        {
            var retval = GetLineOfSight4Dir(x, y, ignoreChars);

            int maxX = grid.Keys.Max(point => point.X);
            int maxY = grid.Keys.Max(point => point.Y);
            int minX = grid.Keys.Min(point => point.X);
            int minY = grid.Keys.Min(point => point.Y);

            var topLeft = new Point(x-1, y - 1);
            var topRight = new Point(x + 1, y - 1);
            var bottomLeft = new Point(x - 1, y + 1);
            var bottomRight = new Point(x + 1, y + 1);

            while (topLeft.Y > minY && topLeft.X > minX && ignoreChars.Contains(grid[new Point(topLeft.X, topLeft.Y)].character))
            {
                topLeft.X--;
                topLeft.Y--;
            }

            while (topRight.Y > minY && topRight.X <= maxX - 1 && ignoreChars.Contains(grid[new Point(topRight.X, topRight.Y)].character))
            {
                topRight.Y--;
                topRight.X++;
            }

            while (bottomLeft.Y <= maxY - 1&& bottomLeft.X > minX && ignoreChars.Contains(grid[new Point(bottomLeft.X, bottomLeft.Y)].character))
            {
                bottomLeft.Y++;
                bottomLeft.X--;
            }

            while (bottomRight.Y <= maxY - 1 && bottomRight.X <= maxX - 1 && ignoreChars.Contains(grid[new Point(bottomRight.X, bottomRight.Y)].character))
            {
                bottomRight.Y++;
                bottomRight.X++;
            }

            retval.Add(topLeft);
            retval.Add(topRight);
            retval.Add(bottomLeft);
            retval.Add(bottomRight);
            retval = retval.Where(p => p.X >= minX && p.X <= maxX && p.Y >= minY && p.Y <= maxY).ToList();

            return retval;

        }

        public static double CalculateLineOfSight(int X1, int Y1, int X2, int Y2)
        {
            return ((Math.Atan2(Y1 - Y2, X1 - X2) * 180.0 / Math.PI) + 270) % 360;
        }
        
        public List<(Point item, double angle)> GetLineOfSightAngle(int x, int y, List<char> ignoreChars)
        {
            int maxX = grid.Keys.Max(point => point.X);
            int maxY = grid.Keys.Max(point => point.Y);
            int minX = grid.Keys.Min(point => point.X);
            int minY = grid.Keys.Min(point => point.Y);

            var items = new List<(int distance, double angle, Point item)>();
            for (int x2 = minX; x2 <= maxX; x2++)
            {
                for (int y2 = minY; y2 <= maxY; y2++)
                {
                    if (ignoreChars.Contains(grid[new Point(x2,y2)].character))
                    {
                        continue;
                    }

                    var distance = Utilities.ManhattanDistance((x, y), (x2, y2));
                    var angle = CalculateLineOfSight(x, y, x2, y2);
                    items.Add((distance, angle, new Point(x2, y2)));
                }
            }
            var groups = items.GroupBy(i => i.angle);
            var retval = new List<(Point item, double angle)>();
            foreach (var group in groups)
            {
                var first = group.OrderBy(g => g.distance).First();
                retval.Add((first.item, first.angle));
            }
            return retval;

        }

        public void GenerateDistanceMap4Dir(int x, int y, List<char> wallChars)
        {
            int maxX = grid.Keys.Max(point => point.X);
            int maxY = grid.Keys.Max(point => point.Y);
            int minX = grid.Keys.Min(point => point.X);
            int minY = grid.Keys.Min(point => point.Y);
            int height = maxY - minY + 1;
            int width = maxX - minX + 1;

            for (int x2 = minX; x2 <= maxX; x2++)
            {
                for (int y2 = minY; y2 <= maxY; y2++)
                {
                    grid[new Point(x2, y2)].distance = -1;
                }
            }
            int count = 0;
            var currentPoints = new List<(int x, int y)>() ;
            var nextPoints = new List<(int x, int y)>() { (x, y) };
            while (nextPoints.Count > 0)
            {
                currentPoints = nextPoints;
                nextPoints = new List<(int x, int y)>();

                for (int i = 0;i < currentPoints.Count; i++)
                {
                    var point = currentPoints[i];
                    grid[new Point(point.x, point.y)].distance = count;
                    var neighbours = GetNeighbours4Dir(point.x, point.y, 1);
                    foreach (var neighbour in neighbours)
                    {
                        if (!wallChars.Contains(grid[new Point(neighbour.X, neighbour.Y)].character) && grid[new Point(neighbour.X, neighbour.Y)].distance == -1 &&
                         !currentPoints.Contains((neighbour.X, neighbour.Y)) && !nextPoints.Contains((neighbour.X, neighbour.Y)))
                        {
                            nextPoints.Add((neighbour.X, neighbour.Y));
                        }
                    }
                }
                count++;
            }
        }

        
        public void GenerateDistanceMap8Dir(int x, int y, List<char> wallChars)
        {
            int maxX = grid.Keys.Max(point => point.X);
            int maxY = grid.Keys.Max(point => point.Y);
            int minX = grid.Keys.Min(point => point.X);
            int minY = grid.Keys.Min(point => point.Y);
            int height = maxY - minY + 1;
            int width = maxX - minX + 1;

            for (int x2 = minX; x2 <= maxX; x2++)
            {
                for (int y2 = minY; y2 <= maxY; y2++)
                {
                    grid[new Point(x2, y2)].distance = -1;
                }
            }
            int count = 0;
            var currentPoints = new List<(int x, int y)>();
            var nextPoints = new List<(int x, int y)>() { (x, y) };
            while (nextPoints.Count > 0)
            {
                System.Console.WriteLine(nextPoints.Count());
                currentPoints = nextPoints;
                nextPoints = new List<(int x, int y)>();
                

                for (int i = 0; i < currentPoints.Count; i++)
                {
                    var point = currentPoints[i];
                    grid[new Point(point.x, point.y)].distance = count;
                    var neighbours = GetNeighbours8Dir(point.x, point.y, 1);
                    foreach (var neighbour in neighbours)
                    {
                        if (!wallChars.Contains(grid[new Point(neighbour.X, neighbour.Y)].character) && grid[new Point(neighbour.X, neighbour.Y)].distance == -1 && 
                        !currentPoints.Contains((neighbour.X, neighbour.Y)) && !nextPoints.Contains((neighbour.X, neighbour.Y)))
                        {
                            nextPoints.Add((neighbour.X, neighbour.Y));
                        }
                    }
                }
                count++;
            }
        }

        public long Count(char value)
        {
            var count = 0; 
            int maxX = grid.Keys.Max(point => point.X);
            int maxY = grid.Keys.Max(point => point.Y);
            int minX = grid.Keys.Min(point => point.X);
            int minY = grid.Keys.Min(point => point.Y);

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    if (grid[new Point(x,y)].character == value)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

    }




    public class GridItem {
        public int number = '0';
        public char character = '.';
        public long distance = 0;

        public Dictionary<char, int> distances = new Dictionary<char, int>();
    }



}