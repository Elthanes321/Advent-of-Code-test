using Advent_of_Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code
{
    public class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Cost { get; set; }
        public int Distance { get; set; }
        public int CostDistance => Cost + Distance;
        public Tile Parent { get; set; }

        //The distance is essentially the estimated distance, ignoring walls to our target. 
        //So how many tiles left and right, up and down, ignoring walls, to get there. 
        public void SetDistance(int targetX, int targetY)
        {
            this.Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
        }
    }

    public class Heightmap
    {
        List<string> map;
        Tile end;
        Tile start;
        object o;
        List<Tile> activeTiles;
        List<Tile> visitedTiles;
        public Heightmap(string input)
        {
            map = new();
            var divide = input.Split(Environment.NewLine);
            for (int i = 0; i < divide.Length; i++)
            {
                map.Add(divide[i]);
            }
            start = new();
            start.Y = map.FindIndex(x => x.Contains('S'));
            start.X = map[start.Y].IndexOf('S');
            map[start.Y] = map[start.Y].Replace('S', 'a');

            end = new();
            end.Y = map.FindIndex(x => x.Contains('E'));
            end.X = map[end.Y].IndexOf('E');
            map[end.Y] = map[end.Y].Replace('E', 'z');

            start.SetDistance(end.X, end.Y);
        }

        public int FindMinimal()
        {
            activeTiles = new List<Tile>();
            activeTiles.Add(start);
            visitedTiles = new List<Tile>();
            while (activeTiles.Any())
            {
                var checkTile = activeTiles.OrderBy(x => x.CostDistance).First();
                if (checkTile.X == end.X && checkTile.Y == end.Y)
                {
                    return checkTile.Cost;
                }

                visitedTiles.Add(checkTile);
                activeTiles.Remove(checkTile);
                var walkableTiles = GetWalkableTiles(checkTile, end);
                foreach(Tile wTile in walkableTiles)
                {
                    if (visitedTiles.Any(x => x.X == wTile.X && x.Y == wTile.Y))
                        continue;
                    if(activeTiles.Any(x=> x.X ==wTile.X && x.Y == wTile.Y))
                    {
                        var eTile = activeTiles.First(x => x.X == wTile.X && x.Y == wTile.Y);
                        if(eTile.CostDistance > checkTile.CostDistance)
                        {
                            activeTiles.Remove(eTile);
                            activeTiles.Add(wTile);
                        }
                    }
                    else
                    {
                        activeTiles.Add(wTile);
                    }
                }
            }


            return 9999;
        }

        public int FindMinimalMinimal()
        {
            List<Tile> lowest = new();
            for(int i=0;i<map.Count;i++)
            {
                for(int j = 0; j < map[0].Length;j++)
                    if (map[i][j] == 'a')
                    {
                        Tile potential = new();
                        potential.X = j;
                        potential.Y = i;
                        potential.SetDistance(end.X, end.Y);
                        lowest.Add(potential);
                    }
            }
            List<int> results = new();
            foreach(var tile in lowest)
            {
                start = tile;
                results.Add(FindMinimal());
            }


            return results.Min();
        }

        private List<Tile> GetWalkableTiles(Tile currentTile, Tile targetTile)
        {
            var possibleTiles = new List<Tile>(){
            new Tile { X = currentTile.X, Y = currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
            new Tile { X = currentTile.X, Y = currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1},
            new Tile { X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
            new Tile { X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
        };

            possibleTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));

            var maxX = map.First().Length - 1;
            var maxY = map.Count - 1;

            return possibleTiles
                    .Where(tile => tile.X >= 0 && tile.X <= maxX)
                    .Where(tile => tile.Y >= 0 && tile.Y <= maxY)
                    .Where(tile => map[tile.Y][tile.X] - map[currentTile.Y][currentTile.X] < 2)
                    .ToList();
        }
    }
}
