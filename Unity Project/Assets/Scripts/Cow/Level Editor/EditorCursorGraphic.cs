using UnityEngine;
using System.Collections.Generic;

using Cow.Editor.Brushes;
using Cow.Editor.Helpers;

namespace Cow.Editor
{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public class EditorCursorGraphic : MonoBehaviour
    {
        List<Vector3> vertices;
        List<int> triangles;
        public float height = 2f;
        public EditorManager editor;
        List<TileCoord> tiles;

        public void VisualiseBrush(Brush brush, int radius)
        {
            tiles = new List<TileCoord>( brush.GetBrush(radius, new TileCoord(), new Tile(0)).Keys);
            RenderTiles();
        }
        [System.Obsolete("Drawing lines through the CursorGraphic is not needed any-more")]
        public void VisualiseBrush(Brush brush, int radius, TileCoord start, TileCoord end)
        {
            tiles = new List<TileCoord>();
            foreach (TileCoord lineTile in editor.getTiles.AlongLine(start, end))
            {
                foreach (TileCoord tile in brush.GetBrush(radius, lineTile, new Cow.Tile(0)).Keys)
                {
                    if (!tiles.Contains(tile))
                        tiles.Add(tile);
                }
            }
            tiles = new List<TileCoord>(brush.GetBrush(radius, new TileCoord(), new Tile(0)).Keys);
            RenderTiles();
        }

        void RenderTiles()
        {
            vertices = new List<Vector3>();
            triangles = new List<int>();
            foreach (TileCoord tilePos in tiles)
            {
                Tile(tilePos);
            }
            MeshFilter mf = GetComponent<MeshFilter>();
            Mesh mesh = new Mesh();
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mf.mesh = mesh;
        }

        public void Tile(TileCoord pos)
        {
            //Create the top face
            Quad(
                new Vector3(pos.x + 0, height, pos.y + 0),
                new Vector3(pos.x + 0, height, pos.y + 1),
                new Vector3(pos.x + 1, height, pos.y + 1),
                new Vector3(pos.x + 1, height, pos.y + 0));
            if (!tiles.Contains(new TileCoord(pos.x - 1, pos.y)))
            {
                Quad(
                    new Vector3(pos.x + 0, height, pos.y + 1),
                    new Vector3(pos.x + 0, height, pos.y + 0),
                    new Vector3(pos.x + 0, 0, pos.y + 0),
                    new Vector3(pos.x + 0, 0, pos.y + 1));
            }
            if (!tiles.Contains(new TileCoord(pos.x + 1, pos.y)))
            {
                Quad(
                    new Vector3(pos.x + 1, height, pos.y + 0),
                    new Vector3(pos.x + 1, height, pos.y + 1),
                    new Vector3(pos.x + 1, 0, pos.y + 1),
                    new Vector3(pos.x + 1, 0, pos.y + 0));
            }
            if (!tiles.Contains(new TileCoord(pos.x, pos.y-1)))
            {
                Quad(
                    new Vector3(pos.x + 0, height, pos.y + 0),
                    new Vector3(pos.x + 1, height, pos.y + 0),
                    new Vector3(pos.x + 1, 0, pos.y + 0),
                    new Vector3(pos.x + 0, 0, pos.y + 0));
            }
            if (!tiles.Contains(new TileCoord(pos.x, pos.y + 1)))
            {
                Quad(
                    new Vector3(pos.x + 1, height, pos.y + 1),
                    new Vector3(pos.x + 0, height, pos.y + 1),
                    new Vector3(pos.x + 0, 0, pos.y + 1),
                    new Vector3(pos.x + 1, 0, pos.y + 1));
            }
        }
        void Quad(Vector3 v0, Vector3 v1,Vector3 v2, Vector3 v3)
        {
            vertices.Add(v0);
            vertices.Add(v1);
            vertices.Add(v2);
            vertices.Add(v3);

            triangles.Add(vertices.Count - 4);
            triangles.Add(vertices.Count - 3);
            triangles.Add(vertices.Count - 2);

            triangles.Add(vertices.Count - 4);
            triangles.Add(vertices.Count - 2);
            triangles.Add(vertices.Count - 1);
        }

        void Start()
        {
            if (editor == null)
                editor = FindObjectOfType<EditorManager>();
        }
    }
}