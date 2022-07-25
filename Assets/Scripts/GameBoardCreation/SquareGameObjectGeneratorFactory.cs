using System;
using UnityEngine;

public class SquareGameObjectGeneratorFactory
{
    private readonly GameBoard _gameBoard;
    private readonly float _squareWidth;
    private readonly float _squareHeight;

    private CornerSquareMeshGenerator _cornerSquareMeshGeneratorValue;
    private CornerSquareMeshGenerator _cornerSquareMeshGenerator 
    {
        get
        {
            if (_cornerSquareMeshGeneratorValue == null)
                _cornerSquareMeshGeneratorValue = new CornerSquareMeshGenerator(_squareHeight);
            return _cornerSquareMeshGeneratorValue;
        }
    }

    private BorderSquareMeshGenerator _borderSquareMeshGeneratorValue;

    private BorderSquareMeshGenerator _borderSquareMeshGenerator 
    {
        get
        {
            if (_borderSquareMeshGeneratorValue == null)
                _borderSquareMeshGeneratorValue = new BorderSquareMeshGenerator(_squareHeight, _squareWidth);
            return _borderSquareMeshGeneratorValue;
        }
    }

    private ISquareImageGenerator _borderOtherSquareImageGeneratorValue;
    private ISquareImageGenerator _borderOtherSquareImageGenerator
    {
        get{
            if (_borderOtherSquareImageGeneratorValue == null)
                _borderOtherSquareImageGeneratorValue = new BorderOtherSquareImageGenerator(_squareHeight, _squareWidth);
            return _borderOtherSquareImageGeneratorValue;
        }
    }
    
    private ISquareImageGenerator _borderPropertySquareImageGeneratorValue;
    private ISquareImageGenerator _borderPropertySquareImageGenerator
    {
        get{
            if (_borderPropertySquareImageGeneratorValue == null)
                _borderPropertySquareImageGeneratorValue = new BorderPropertySquareImageGenerator(_squareHeight, _squareWidth);
            return _borderPropertySquareImageGeneratorValue;
        }
    }

    private ISquareImageGenerator _borderTrainStationImageGeneratorValue;
    private ISquareImageGenerator _borderTrainStationImageGenerator
    {
        get{
            if (_borderTrainStationImageGeneratorValue == null)
                _borderTrainStationImageGeneratorValue = new BorderTrainStationImageGenerator(_squareHeight, _squareWidth);
            return _borderTrainStationImageGeneratorValue;
        }
    }

    private ISquareImageGenerator _cornerSquareImageGeneratorValue;
    private ISquareImageGenerator _cornerSquareImageGenerator
    {
        get{
            if (_cornerSquareImageGeneratorValue == null)
                _cornerSquareImageGeneratorValue = new CornerSquareImageGenerator();
            return _cornerSquareImageGeneratorValue;
        }
    }
    
    public SquareGameObjectGeneratorFactory(GameBoard gameBoard, float squareWidth, float squareHeight)
    {
        _gameBoard = gameBoard;
        _squareWidth = squareWidth;
        _squareHeight = squareHeight;
    }

    public SquareGameObjectGenerator GetGameObjectGenerator(Square square)
    {
        var imageGenerator = GetSquareImageGenerator(square);
        var meshGenerator = GetSquareMeshGenerator(square);
        return new SquareGameObjectGenerator(imageGenerator, meshGenerator);
    }

    private ISquareMeshGenerator GetSquareMeshGenerator(Square square)
    {
        return SquareIsCorner(square, _gameBoard) ? (ISquareMeshGenerator)_cornerSquareMeshGenerator : (ISquareMeshGenerator)_borderSquareMeshGenerator;
    }

    private ISquareImageGenerator GetSquareImageGenerator(Square square)
    {
        if (SquareIsCorner(square, _gameBoard)) return _cornerSquareImageGenerator;
        if (square is Property) return _borderPropertySquareImageGenerator;
        if (square is TrainStation) return _borderTrainStationImageGenerator;
        return _borderOtherSquareImageGenerator;
    }

    private bool SquareIsCorner(Square square, GameBoard gameBoard)
    {
        var squareIndex = _gameBoard.GetSquareIndex(square);
        var totalSquares = _gameBoard.Squares.Count;

        if (squareIndex % (totalSquares/4) == 0)
            return true;
        return false;
    }
}