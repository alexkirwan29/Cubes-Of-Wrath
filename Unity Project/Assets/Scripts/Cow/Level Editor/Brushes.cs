using System.Collections.Generic;

namespace Cow.Editor.Brushes
{
    public abstract class Brush
    {
        public abstract Dictionary<TileCoord, Tile> GetBrush(int radius, TileCoord pos, Tile fill);
    }
    public class CircleBrush : Brush
    {
        public override Dictionary<TileCoord, Tile> GetBrush(int radius, TileCoord pos, Tile fill)
        {
            Dictionary<TileCoord, Tile> brush = new Dictionary<TileCoord, Tile>();

            TileCoord centre;
            int sqrRadius = radius * radius;

            for (int y, x = pos.x - radius; x <= (pos.x + radius); x++)
            {
                for (y = pos.y - radius; y <= (pos.y + radius); y++)
                {
                    centre.x = x - pos.x;
                    centre.y = y - pos.y;

                    if(centre.x * centre.x + centre.y * centre.y <= sqrRadius)
                        brush.Add(new TileCoord(x, y), fill);
                }
            }
            return brush;
        }
    }
    public class SquareBrush : Brush
    {
        public override Dictionary<TileCoord, Tile> GetBrush(int radius, TileCoord pos, Tile fill)
        {
            Dictionary<TileCoord, Tile> brush = new Dictionary<TileCoord, Tile>();
            for (int y, x = pos.x - radius; x <= (pos.x + radius); x++)
            {
                for (y = pos.y - radius; y <= (pos.y + radius); y++)
                {
                    brush.Add(new TileCoord(x, y), fill);
                }
            }
            return brush;
        }
    }
}