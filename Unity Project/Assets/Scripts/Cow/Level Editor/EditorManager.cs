using UnityEngine;
using System.Collections.Generic;
using Cow.Editor.Brushes;

namespace Cow.Editor
{
    // Make dam sure that a LevelManager is on this GameObject!
    [RequireComponent(typeof(LevelManager))]
    public class EditorManager : MonoBehaviour
    {
        // ========= Variables! ==========  // Whoo!
        LevelManager level;                 // The reference to the level part of the editor.

        int brushSize;                      // The brush size.
        int currBrush;                      // The current brush.
        Tile currTile;                      // The current tile.

        Brush[] brushes = new Brush[] {     // The list of brushes.
            new SquareBrush(),              // - A square brush.
            new CircleBrush()               // - A Circle brush.
        };                                  // LOL, only two brushes! but we can add more later on.

        TileCoord lastPos;                  // The last position the cursor was at. this includes the mouse down pos.
        public EditorCursor cursor;         // This should not be here but I added the cursor visualiser lazily, that's why it's here. Cut me some slack it WAS 2:00 AM. :P
        void Awake()
        {
            // Get the LevelManager because it's very important... 'level-editor'
            level = GetComponent<LevelManager>();
        }
        void Start()
        {
            // Set the defaults.
            BrushSize = 0;
            ActiveTile = new Tile(0);

            // Windge at you if some things do not look right
            if (level == null)
                Debug.LogWarning("<b>Ohhh Noooh:</b> The <b>level</b> part of the level editor is missing!");
            if (currTile == null)
                Debug.LogWarning("<b>Ohhh Noooh:</b> The current tile is an Eraser, is that intentional?");
            if (brushes == null || brushes.Length == 0)
                Debug.LogWarning("<b>Ohhh Noooh:</b> We have no brushes!");
        }
        #region Mouse / Cursor stuf
        public void MouseDown(TileCoord pos)
        {
            lastPos = pos;
        }
        public void MouseUp(TileCoord pos)
        {

        }
        public void MouseHold(TileCoord pos)
        {
            // Only paint a line if the cursor has moved more than one unit else
            // just paint a single tile at the new cursor pos. Also set the lastPos
            // to the current pos for the next time the cursor moves.
            if (pos.x > lastPos.x + 1 || pos.x < lastPos.x - 1 || pos.y > lastPos.y + 1 || pos.y < lastPos.y - 1)
                // Just loop through each tile in the line and paint a brush over
                // the tile.
                foreach (TileCoord posInLine in GetTilesAlongLine(lastPos, pos))
                    Paint(posInLine, currTile);
            else
                Paint(pos, currTile);

            lastPos = pos;
        }
        #endregion

        // === Paint Methods ===
        public void Paint(TileCoord pos) { Paint(pos, currTile, currBrush); }
        public void Paint(TileCoord pos,Tile tile) { Paint(pos, tile, currBrush); }
        public void Paint(TileCoord pos, Tile tile, int brushIndex)
        {
            // Apply a brush to the level.
            if (brushes != null && brushes[currBrush] != null)
                level.SetTiles(brushes[currBrush].GetBrush(brushSize, pos, tile));
            else
                Debug.LogWarning("Brush out of range! did you add at least one brush?");
        }

        #region Helpers
        TileCoord[] GetInsideSquare(int radius, TileCoord offset)
        {
            // Return an array of Tiles that fit inside a square the size of radius
            // with an offset.
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
        TileCoord[] GetInsideSquare(int radius, TileCoord offset, Tile type)
        {
            // Return an array of Tiles that fit inside a square the size of radius
            // with an offset of the same type of 'type'.
            List<TileCoord> tiles = new List<TileCoord>();
            for (int y, x = offset.x - radius; x <= (offset.x + radius); x++)
            {
                for (y = offset.y - radius; y <= (offset.y + radius); y++)
                {
                    if(level.GetTile(new TileCoord(x,y)) == type)
                        tiles.Add(new TileCoord(x, y));
                }
            }

            return tiles.ToArray();
        }
        TileCoord[] GetTilesAlongLine(TileCoord start, TileCoord end)
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

            for(int x = start.x; x < end.x; x++)
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

        // These are used to change the brush, the size of the brush and the
        // selected tile type
        public int BrushSize
        {
            get { return brushSize; }
            set
            {
                brushSize = Mathf.Clamp(value, 0, 10);
                OnBrushChange(SelectedBrush);
            }
        }
        public int SelectedBrush
        {
            get { return currBrush; }
            set
            {
                if (value < brushes.Length && value >= 0)
                {
                    currBrush = value;
                    OnBrushChange(value);
                }
                else
                    Debug.LogError(string.Format("The Brush {0} does not exist.", value));
            }
        }
        public Tile ActiveTile
        {
            get { return currTile; }
            set { currTile = value; OnTileChange(); }
        }

        // These are called when the brush is changed or the tile is changed.
        void OnBrushChange(int index)
        {
            Debug.Log(string.Format("Updated brush {0}.",index));
            EditorCursor.Instance.graphic.VisualiseBrush(brushes[currBrush],brushSize);
        }
        void OnTileChange()
        {
            Debug.Log(string.Format("Active tile changed to <i>{0}</i>",ActiveTile));
        }
        #endregion
    }
}