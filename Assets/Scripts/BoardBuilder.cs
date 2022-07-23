using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

public class BoardBuilder
{

    private const float squareSize = 1f;

    private Bitmap firstSquareImage {get;set;}

    private SquareImageGenerator _squareImageGenerator;
    private GameBoard _gameBoard;

    public BoardBuilder(GameBoard gameBoard, SquareImageGenerator squareImageGenerator)
    {
        _gameBoard = gameBoard;
        _squareImageGenerator = squareImageGenerator;
    }

    public void BuildBoard()
    {
        var totalSquares = _gameBoard.Squares.Count();
        for (int i = 0; i < totalSquares; i++)
        {
            var square = _gameBoard.Squares[i];
            var squareGameObject = CreateSquare(square);
            MoveSquareToPosition(squareGameObject, i);
        }
    }

    private GameObject CreateSquare(Square square)
    {
        firstSquareImage = _squareImageGenerator.GetImage(square);
        var texture = TextureFromBitmap(firstSquareImage);

        var squareObject = CreateSquareGameObject();
        squareObject.GetComponent<Renderer>().material.mainTexture = texture;
        return squareObject;
    }

    private Texture2D TextureFromBitmap(Bitmap bmp)
    {
        var units = GraphicsUnit.Point;
        var bounds = bmp.GetBounds(ref units);
        var texture = new Texture2D((int)bounds.X, (int)bounds.Y);
        for (int x = 0; x < bounds.X; x++)
        {
            for (int y = 0; y < bounds.Y; y++)
            {
                var color = bmp.GetPixel(x,y);
                var unityColor = new UnityEngine.Color(color.R, color.G, color.B);
                texture.SetPixel(x, y, unityColor);
            }
        }
        return texture;
    }

    private GameObject CreateSquareGameObject()
    {
        var mesh = new Mesh();
        mesh.name = "Square_Mesh";
        mesh.vertices = new [] {
            new Vector3(-squareSize/2, 0f,-squareSize/2),
            new Vector3(squareSize/2, 0f,-squareSize/2),
            new Vector3(squareSize/2, 0f, squareSize/2),
            new Vector3(-squareSize/2, 0f, squareSize/2)
        };
        mesh.uv = new[] {new Vector2(0,0), new Vector2(0,1), new Vector2(1,1), new Vector2(1,0)};
        mesh.triangles = new[] {0, 2, 1, 0, 3, 2};
        mesh.RecalculateNormals();
        var gameObject = new GameObject("Square", typeof(MeshRenderer), typeof(MeshFilter), typeof(MeshCollider));
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        return gameObject;
    }

    private void MoveSquareToPosition(GameObject squareGameObject, int i)
    {
        var totalSquares = _gameBoard.Squares.Count();
        float x,z;
        if (i < totalSquares/4)
        {
            x = -squareSize * i;
            z = 0;
        }
        else if (i < totalSquares / 2)
        {
            x = -squareSize * totalSquares/4;
            z = squareSize * (i%(totalSquares/4));
        }
        else if (i < 3*totalSquares/4)
        {
            x = -squareSize * (totalSquares/4 - (i%(totalSquares/2)));
            z = squareSize * totalSquares/4;
        }
        else
        {
            x = 0;
            z = squareSize * (totalSquares/4 - (i%(3 * totalSquares/4)));
        }
        squareGameObject.transform.Translate(x, 0f, z);
    }
}
