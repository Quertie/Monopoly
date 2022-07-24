using UnityEngine;

public class CornerSquareMeshGenerator:ISquareMeshGenerator
{
    private readonly float _squareHeight;

    public CornerSquareMeshGenerator(float squareHeight)
    {
        _squareHeight = squareHeight;
    }

    public Mesh GetMesh()
    {
        var mesh = new Mesh();
        mesh.name = "Square_Mesh";
        mesh.vertices = new [] {
            new Vector3(-_squareHeight/2, 0f,-_squareHeight/2),
            new Vector3(_squareHeight/2, 0f,-_squareHeight/2),
            new Vector3(_squareHeight/2, 0f, _squareHeight/2),
            new Vector3(-_squareHeight/2, 0f, _squareHeight/2)
        };
        mesh.uv = new[] {new Vector2(1,0), new Vector2(0,0), new Vector2(0,1), new Vector2(1,1)};
        mesh.triangles = new[] {0, 2, 1, 0, 3, 2};
        mesh.RecalculateNormals();
        return mesh;
    }
}
