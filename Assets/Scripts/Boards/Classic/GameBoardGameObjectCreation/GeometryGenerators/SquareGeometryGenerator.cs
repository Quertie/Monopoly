using System;
using System.Collections.Generic;
using UnityEngine;

namespace Boards.Classic.GameBoardGameObjectCreation.GeometryGenerators
{
    public class SquareGeometryGenerator:IGeometryGenerator
    {
        private readonly float _squareHeight;

        public SquareGeometryGenerator(float squareHeight)
        {
            _squareHeight = squareHeight;
        }

        public Mesh GetMesh()
        {
            var mesh = new Mesh
            {
                name = "Square_Mesh",
                vertices = new [] {
                    new Vector3(-_squareHeight/2, 0f,-_squareHeight/2),
                    new Vector3(_squareHeight/2, 0f,-_squareHeight/2),
                    new Vector3(_squareHeight/2, 0f, _squareHeight/2),
                    new Vector3(-_squareHeight/2, 0f, _squareHeight/2)
                },
                uv = new[] {new Vector2(0,0), new Vector2(1,0), new Vector2(1,1), new Vector2(0,1)},
                triangles = new[] {0, 2, 1, 0, 3, 2}
            };
            mesh.RecalculateNormals();
            return mesh;
        }

        public List<Vector3> GetTokenPositionMarkers(int numberOfPlayersOnSquare)
        {
            switch (numberOfPlayersOnSquare)
            {
                case 1:
                    return new List<Vector3> { new Vector3(0, 0, 0) };
                case 2:
                    return new List<Vector3>
                    {
                        new Vector3(0, 0, -_squareHeight / 4),
                        new Vector3(0, 0, _squareHeight / 4)
                    };
                case 3:
                    return new List<Vector3>
                    {
                        new Vector3(0, 0, -_squareHeight / 4),
                        new Vector3(-_squareHeight / 4, 0, _squareHeight / 4),
                        new Vector3(_squareHeight / 4, 0, _squareHeight / 4),
                    };
                case 4:
                    return new List<Vector3>
                    {
                        new Vector3(_squareHeight / 4, 0, -_squareHeight / 4),
                        new Vector3(-_squareHeight / 4, 0, -_squareHeight / 4),
                        new Vector3(-_squareHeight / 4, 0, _squareHeight / 4),
                        new Vector3(_squareHeight / 4, 0, _squareHeight / 4)
                    };
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
