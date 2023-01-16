using Boards.Classic.GameBoardCreation.ImageGenerators;
using Boards.Classic.GameBoardCreation.MeshGenerators;
using Boards.Classic.Squares;
using Squares;

namespace Boards.Classic.GameBoardCreation
{
    public class SquareGameObjectGeneratorFactory
    {
        private readonly GameBoard _gameBoard;
        private readonly float _squareWidth;
        private readonly float _squareHeight;

        private SquareGeometryGenerator _squareGeometryGeneratorValue;
        private SquareGeometryGenerator SquareGeometryGenerator => _squareGeometryGeneratorValue ??= new SquareGeometryGenerator(_squareHeight);

        private BorderGeometryGenerator _borderGeometryGeneratorValue;

        private BorderGeometryGenerator BorderGeometryGenerator => _borderGeometryGeneratorValue ??= new BorderGeometryGenerator(_squareHeight, _squareWidth);

        private ISquareImageGenerator _borderOtherSquareImageGeneratorValue;
        private ISquareImageGenerator _borderOtherSquareImageGenerator => _borderOtherSquareImageGeneratorValue ??= new BorderOtherSquareImageGenerator(_squareHeight, _squareWidth);

        private ISquareImageGenerator _borderPropertySquareImageGeneratorValue;
        private ISquareImageGenerator _borderPropertySquareImageGenerator => _borderPropertySquareImageGeneratorValue ??= new BorderPropertySquareImageGenerator(_squareHeight, _squareWidth);

        private ISquareImageGenerator _borderTrainStationImageGeneratorValue;
        private ISquareImageGenerator _borderTrainStationImageGenerator => _borderTrainStationImageGeneratorValue ??= new BorderTrainStationImageGenerator(_squareHeight, _squareWidth);

        private ISquareImageGenerator _borderChanceImageGeneratorValue;
        private ISquareImageGenerator _borderChanceImageGenerator => _borderChanceImageGeneratorValue ??= new BorderChanceImageGenerator(_squareHeight, _squareWidth);

        private ISquareImageGenerator _borderCommunityChestImageGeneratorValue;
        private ISquareImageGenerator _borderCommunityChestImageGenerator => _borderCommunityChestImageGeneratorValue ??= new BorderCommunityChestImageGenerator(_squareHeight, _squareWidth);

        private ISquareImageGenerator _borderElectricCompanyImageGeneratorValue;
        private ISquareImageGenerator _borderElectricCompanyImageGenerator => _borderElectricCompanyImageGeneratorValue ??= new BorderElectricCompanyImageGenerator(_squareHeight, _squareWidth);

        private ISquareImageGenerator _borderWaterWorksImageGeneratorValue;
        private ISquareImageGenerator _borderWaterWorksImageGenerator => _borderWaterWorksImageGeneratorValue ??= new BorderWaterWorksImageGenerator(_squareHeight, _squareWidth);

        private ISquareImageGenerator _borderIncomeTaxImageGeneratorValue;
        private ISquareImageGenerator _borderIncomeTaxImageGenerator => _borderIncomeTaxImageGeneratorValue ??= new BorderIncomeTaxImageGenerator(_squareHeight, _squareWidth);

        private ISquareImageGenerator _borderLuxuryTaxImageGeneratorValue;
        private ISquareImageGenerator _borderLuxuryTaxImageGenerator => _borderLuxuryTaxImageGeneratorValue ??= new BorderLuxuryTaxImageGenerator(_squareHeight, _squareWidth);

        private ISquareImageGenerator _freeParkingImageGeneratorValue;
        private ISquareImageGenerator _freeParkingImageGenerator => _freeParkingImageGeneratorValue ??= new FreeParkingImageGenerator(_squareHeight);

        private ISquareImageGenerator _goToJailImageGeneratorValue;
        private ISquareImageGenerator _goToJailImageGenerator => _goToJailImageGeneratorValue ??= new GoToJailImageGenerator(_squareHeight);

        private ISquareImageGenerator _goImageGeneratorValue;
        private ISquareImageGenerator _goImageGenerator => _goImageGeneratorValue ??= new GoImageGenerator(_squareHeight);

        private ISquareImageGenerator _jailImageGeneratorValue;
        private ISquareImageGenerator _jailImageGenerator => _jailImageGeneratorValue ??= new JailImageGenerator(_squareHeight);
    
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

        private IGeometryGenerator GetSquareMeshGenerator(Square square)
        {
            return SquareIsCorner(square) ? SquareGeometryGenerator : BorderGeometryGenerator;
        }

        private ISquareImageGenerator GetSquareImageGenerator(Square square)
        {
            return square switch
            {
                Property => _borderPropertySquareImageGenerator,
                TrainStation => _borderTrainStationImageGenerator,
                Chance => _borderChanceImageGenerator,
                CommunityChest => _borderCommunityChestImageGenerator,
                ElectricCompany => _borderElectricCompanyImageGenerator,
                WaterWorks => _borderWaterWorksImageGenerator,
                IncomeTax => _borderIncomeTaxImageGenerator,
                LuxuryTax => _borderLuxuryTaxImageGenerator,
                FreeParking => _freeParkingImageGenerator,
                GoToJail => _goToJailImageGenerator,
                Go => _goImageGenerator,
                Jail => _jailImageGenerator,
                _ => SquareIsCorner(square) ? _freeParkingImageGenerator : _borderOtherSquareImageGenerator
            };
        }

        private bool SquareIsCorner(Square square)
        {
            var squareIndex = _gameBoard.GetSquareIndex(square);
            var totalSquares = _gameBoard.Squares.Count;

            return squareIndex % (totalSquares/4) == 0;
        }
    }
}