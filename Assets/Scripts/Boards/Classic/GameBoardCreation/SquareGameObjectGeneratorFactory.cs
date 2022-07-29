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

    private ISquareImageGenerator _borderChanceImageGeneratorValue;
    private ISquareImageGenerator _borderChanceImageGenerator
    {
        get{
            if (_borderChanceImageGeneratorValue == null)
                _borderChanceImageGeneratorValue = new BorderChanceImageGenerator(_squareHeight, _squareWidth);
            return _borderChanceImageGeneratorValue;
        }
    }

    private ISquareImageGenerator _borderCommunityChestImageGeneratorValue;
    private ISquareImageGenerator _borderCommunityChestImageGenerator
    {
        get{
            if (_borderCommunityChestImageGeneratorValue == null)
                _borderCommunityChestImageGeneratorValue = new BorderCommunityChestImageGenerator(_squareHeight, _squareWidth);
            return _borderCommunityChestImageGeneratorValue;
        }
    }

    private ISquareImageGenerator _borderElectricCompanyImageGeneratorValue;
    private ISquareImageGenerator _borderElectricCompanyImageGenerator
    {
        get{
            if (_borderElectricCompanyImageGeneratorValue == null)
                _borderElectricCompanyImageGeneratorValue = new BorderElectricCompanyImageGenerator(_squareHeight, _squareWidth);
            return _borderElectricCompanyImageGeneratorValue;
        }
    }

    private ISquareImageGenerator _borderWaterWorksImageGeneratorValue;
    private ISquareImageGenerator _borderWaterWorksImageGenerator
    {
        get{
            if (_borderWaterWorksImageGeneratorValue == null)
                _borderWaterWorksImageGeneratorValue = new BorderWaterWorksImageGenerator(_squareHeight, _squareWidth);
            return _borderWaterWorksImageGeneratorValue;
        }
    }

    private ISquareImageGenerator _borderIncomeTaxImageGeneratorValue;
    private ISquareImageGenerator _borderIncomeTaxImageGenerator
    {
        get{
            if (_borderIncomeTaxImageGeneratorValue == null)
                _borderIncomeTaxImageGeneratorValue = new BorderIncomeTaxImageGenerator(_squareHeight, _squareWidth);
            return _borderIncomeTaxImageGeneratorValue;
        }
    }

    private ISquareImageGenerator _borderLuxuryTaxImageGeneratorValue;
    private ISquareImageGenerator _borderLuxuryTaxImageGenerator
    {
        get{
            if (_borderLuxuryTaxImageGeneratorValue == null)
                _borderLuxuryTaxImageGeneratorValue = new BorderLuxuryTaxImageGenerator(_squareHeight, _squareWidth);
            return _borderLuxuryTaxImageGeneratorValue;
        }
    }
    
    private ISquareImageGenerator _freeParkingImageGeneratorValue;
    private ISquareImageGenerator _freeParkingImageGenerator
    {
        get{
            if (_freeParkingImageGeneratorValue == null)
                _freeParkingImageGeneratorValue = new FreeParkingImageGenerator(_squareHeight);
            return _freeParkingImageGeneratorValue;
        }
    }

    private ISquareImageGenerator _goToJailImageGeneratorValue;
    private ISquareImageGenerator _goToJailImageGenerator
    {
        get{
            if (_goToJailImageGeneratorValue == null)
                _goToJailImageGeneratorValue = new GoToJailImageGenerator(_squareHeight);
            return _goToJailImageGeneratorValue;
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
        if (square is Property) return _borderPropertySquareImageGenerator;
        if (square is TrainStation) return _borderTrainStationImageGenerator;
        if (square is Chance) return _borderChanceImageGenerator;
        if (square is CommunityChest) return _borderCommunityChestImageGenerator;
        if (square is ElectricCompany) return _borderElectricCompanyImageGenerator;
        if (square is WaterWorks) return _borderWaterWorksImageGenerator;
        if (square is IncomeTax) return _borderIncomeTaxImageGenerator;
        if (square is LuxuryTax) return _borderLuxuryTaxImageGenerator;
        if (square is FreeParking) return _freeParkingImageGenerator;
        if (square is GoToJail) return _goToJailImageGenerator;
        if (SquareIsCorner(square, _gameBoard)) return _freeParkingImageGenerator;
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