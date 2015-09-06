using UnityEngine;
using System.Collections.Generic;

using Cow.Editor.Brushes;

namespace Cow.Editor
{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public class EditorCursorGraphic : MonoBehaviour
    {
        List<Vector3> vertices;
        List<int> triangles;
        public float height = 2f;

        List<TileCoord> tiles;

        public void VisualiseBrush(Brush brush, int radius)
        {
            vertices = new List<Vector3>();
            triangles = new List<int>();

            tiles = new List<TileCoord>( brush.GetBrush(radius, new TileCoord(), new Tile(0)).Keys);

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

        }
    }
}