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

        private CornerSquareMeshGenerator _cornerSquareMeshGeneratorValue;
        private CornerSquareMeshGenerator _cornerSquareMeshGenerator => _cornerSquareMeshGeneratorValue ??= new CornerSquareMeshGenerator(_squareHeight);

        private BorderSquareMeshGenerator _borderSquareMeshGeneratorValue;

        private BorderSquareMeshGenerator _borderSquareMeshGenerator => _borderSquareMeshGeneratorValue ??= new BorderSquareMeshGenerator(_squareHeight, _squareWidth);

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

        private ISquareMeshGenerator GetSquareMeshGenerator(Square square)
        {
            return SquareIsCorner(square) ? _cornerSquareMeshGenerator : _borderSquareMeshGenerator;
        }

        private ISquareImageGenerator GetSquareImageGenerator(Square square)
        {
            switch (square)
            {
                case Property:
                    return _borderPropertySquareImageGenerator;
                case TrainStation:
                    return _borderTrainStationImageGenerator;
                case Chance:
                    return _borderChanceImageGenerator;
                case CommunityChest:
                    return _borderCommunityChestImageGenerator;
                case ElectricCompany:
                    return _borderElectricCompanyImageGenerator;
                case WaterWorks:
                    return _borderWaterWorksImageGenerator;
                case IncomeTax:
                    return _borderIncomeTaxImageGenerator;
                case LuxuryTax:
                    return _borderLuxuryTaxImageGenerator;
                case FreeParking:
                    return _freeParkingImageGenerator;
                case GoToJail:
                    return _goToJailImageGenerator;
                case Go:
                    return _goImageGenerator;
                case Jail:
                    return _jailImageGenerator;
            }

            return SquareIsCorner(square) ? _freeParkingImageGenerator : _borderOtherSquareImageGenerator;
        }

        private bool SquareIsCorner(Square square)
        {
            var squareIndex = _gameBoard.GetSquareIndex(square);
            var totalSquares = _gameBoard.Squares.Count;

            return squareIndex % (totalSquares/4) == 0;
        }
    }
}