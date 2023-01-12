using UnityEngine;

namespace Boards.Classic.GameBoardCreation.MeshGenerators
{
    public class BorderSquareMeshGenerator:ISquareMeshGenerator
    {
        private readonly float _squareHeight;
        private readonly float _squareWidth;

        public BorderSquareMeshGenerator(float squareHeight, float squareWidth)
        {
            _squareHeight = squareHeight;
            _squareWidth = squareWidth;
        }

        public Mesh GetMesh()
        {
            var mesh = new Mesh
            {
                name = "Square_Mesh",
                vertices = new [] {
                    new Vector3(-_squareWidth/2, 0f,-_squareHeight/2),
                    new Vector3(_squareWidth/2, 0f,-_squareHeight/2),
                    new Vector3(_squareWidth/2, 0f, _squareHeight/2),
                    new Vector3(-_squareWidth/2, 0f, _squareHeight/2)
                },
                uv = new[] {new Vector2(0,0), new Vector2(1,0), new Vector2(1,1), new Vector2(0,1)},
                triangles = new[] {0, 2, 1, 0, 3, 2}
            };
            mesh.RecalculateNormals();
            return mesh;
        }
    }
}