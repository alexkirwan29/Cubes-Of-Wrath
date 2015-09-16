using UnityEngine;
using System.Collections.Generic;
using Cow.Editor;

namespace Cow.Editor.Helpers
{
    public class GetTiles
    {
        LevelManager level;
        public GetTiles(LevelManager level)
        {
            this.level = level;
        }
        // Return an array of Tiles that fit inside a square the size of radius
        // with an offset.
        public TileCoord[] InsideSquare(int radius, TileCoord offset)
        {
            List<TileCoord> tiles = new List<TileCoord>();
            for (int y, x = offset.x - radius; x <= (offset.x + radius); x++)
            {
                for (y = offset.y - radius; y <= (offset.y + radius); y++)
                {
                    tiles.Add(new TileCoord(x, y));
                }
            }

            return tiles.ToArray();
        }

        // Return an array of Tiles that fit inside a square the size of radius
        // with an offset of the same type of 'type'.
        public TileCoord[] InsideSquareOfType(int radius, TileCoord offset, Tile type)
        {
            List<TileCoord> tiles = new List<TileCoord>();
            for (int y, x = offset.x - radius; x <= (offset.x + radius); x++)
            {
                for (y = offset.y - radius; y <= (offset.y + radius); y++)
                {
                    if (level.GetTile(new TileCoord(x, y)) == type)
                        tiles.Add(new TileCoord(x, y));
                }
            }

            return tiles.ToArray();
        }

        // Returns an array of tiles along a line from start to end.
        public TileCoord[] AlongLine(TileCoord start, TileCoord end)
        {
            // This if statement just swaps the start and end around if the start
            // is left of the end.
            if (start.x > end.x)
            {
                TileCoord swapTile = start;
                start = end;
                end = swapTile;
            }

            // Bresenham's line algorithm.
            // http://wki.pe/Bresenham's_line_algorithm
            // C.O.W. C# Implementation written by Tom Parker.
            List<TileCoord> tiles = new List<TileCoord>();

            float deltaX = end.x - start.x;
            float deltaY = end.y - start.y;
            float error = 0;
            float deltaError = Mathf.Abs(deltaY / deltaX);

            int y = start.y;

            for (int x = start.x; x < end.x; x++)
            {
                tiles.Add(new TileCoord(x, y));
                error += deltaError;
                while (error >= 0.5f)
                {
                    tiles.Add(new TileCoord(x, y));
                    y += (int)Mathf.Sign(end.y - start.y);
                    error -= 1;
                }
            }

            return tiles.ToArray();
        }
    }
}