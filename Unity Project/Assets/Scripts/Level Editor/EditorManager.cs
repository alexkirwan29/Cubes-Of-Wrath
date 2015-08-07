using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LevelManager))]
public class EditorManager : MonoBehaviour
{
    LevelManager level;
    [SerializeField]
    int brushSize;
    public enum EditorTool { Eraser, Brush, SquareBrush, Line };
    [SerializeField]
    EditorTool currTool;

    void Awake()
    {
        level = GetComponent<LevelManager>();
    }
    public void MouseDown(TileCoord pos)
    {
        
    }
    public void MouseUp(TileCoord pos)
    {

    }
    public void MouseHold(TileCoord pos)
    {
        Paint(pos);
    }
    void Paint(TileCoord pos)
    {
        switch (currTool)
        {
            case EditorTool.SquareBrush:
                SquareBrush(pos, brushSize);
                break;
            case EditorTool.Brush:
                Brush(pos, brushSize);
                break;
            case EditorTool.Eraser:
                Eraser(pos, brushSize);
                break;
                
        }
    }

    #region Tools
    void SquareBrush(TileCoord pos, int radius)
    {
        for (int y, x = pos.x - radius; x <= (pos.x + radius); x++)
        {
            for (y = pos.y - radius; y <= (pos.y + radius); y++)
            {
                level.CreateTile(new TileCoord(x,y),new Tile(0));
            }
        }
    }
    void Brush(TileCoord pos, int radius)
    {
        int cx, cy, sqrRadius = radius * radius;
        for (int y, x = pos.x - radius; x <= (pos.x + radius); x++)
        {
            for (y = pos.y - radius; y <= (pos.y + radius); y++)
            {
                cx = x - pos.x;
                cy = y - pos.y;
                if(cx * cx + cy * cy <= sqrRadius)
                    level.CreateTile(new TileCoord(x, y), new Tile(0));
            }
        }
    }
    void Eraser(TileCoord pos, int radius)
    {
        int cx, cy, sqrRadius = radius * radius;
        for (int y, x = pos.x - radius; x <= (pos.x + radius); x++)
        {
            for (y = pos.y - radius; y <= (pos.y + radius); y++)
            {
                cx = x - pos.x;
                cy = y - pos.y;
                if (cx * cx + cy * cy <= sqrRadius)
                    level.RemoveTile(new TileCoord(x, y));
            }
        }
    }
    #endregion

    TileCoord[] GetInsideSquare(TileCoord pos,int radius)
    {
        List<TileCoord> tiles = new List<TileCoord>();
        for (int y, x = pos.x - radius; x <= (pos.x + radius); x++)
        {
            for (y = pos.y - radius; y <= (pos.y + radius); y++)
            {
                tiles.Add(new TileCoord(x, y));
            }
        }

        return tiles.ToArray();
    }
    public void SetBrushSize(float value)
    {
        SetBrushSize((int)Mathf.FloorToInt(value));
    }
    public void SetBrushSize(int value)
    {
        brushSize = Mathf.Clamp(value, 0, 10);
    }
    public EditorTool currentTool
    {
        get { return currTool; }
        set { currTool = value; }
    }
    public void SetTool(int toolIndex)
    {
        if (System.Enum.IsDefined(typeof(EditorTool), toolIndex))
            currTool = (EditorTool)toolIndex;
        else
            Debug.LogError(string.Format("EditorTool does not have a value defined as {0}.", toolIndex));
    }
}