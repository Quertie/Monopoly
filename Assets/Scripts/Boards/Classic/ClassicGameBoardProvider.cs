using System.Collections.Generic;
using Boards.Classic.Squares;
using Boards.Classic.Style;
using Squares;

namespace Boards.Classic
{
    public class ClassicGameBoardProvider
    {
        private readonly ColorGroup _brownColorGroup = new ColorGroup(MonopolyClassicTheme.BrownGroupColor);
        private readonly ColorGroup _lightBlueColorGroup = new ColorGroup(MonopolyClassicTheme.LightBlueGroupColor);
        private readonly ColorGroup _violetColorGroup = new ColorGroup(MonopolyClassicTheme.VioletGroupColor);
        private readonly ColorGroup _orangeColorGroup = new ColorGroup(MonopolyClassicTheme.OrangeGroupColor);
        private readonly ColorGroup _redColorGroup = new ColorGroup(MonopolyClassicTheme.RedGroupColor);
        private readonly ColorGroup _yellowColorGroup = new ColorGroup(MonopolyClassicTheme.YellowGroupColor);
        private readonly ColorGroup _greenColorGroup = new ColorGroup(MonopolyClassicTheme.GreenGroupColor);
        private readonly ColorGroup _darkBlueColorGroup = new ColorGroup(MonopolyClassicTheme.DarkBlueGroupColor);

        private readonly List<ColorGroup> _colorGroups;
        
         private readonly List<Square> _squares;
         private readonly int _numberOfPlayers;

         public ClassicGameBoardProvider(int numberOfPlayers)
         {
             _numberOfPlayers = numberOfPlayers;
             _colorGroups = new List<ColorGroup>() { _brownColorGroup, _lightBlueColorGroup, _violetColorGroup, _orangeColorGroup, _redColorGroup, _yellowColorGroup, _greenColorGroup, _darkBlueColorGroup };
             _squares = new List<Square> {
                new Go("Départ"),
                new Property("Boulevard de Belleville", _brownColorGroup, 60),
                new CommunityChest("Caisse de communauté"),
                new Property("Rue Lecourbe", _brownColorGroup, 60),
                new IncomeTax("Impôts sur le revenu", 200),
                new TrainStation("Gare Montparnasse", 200),
                new Property("Rue de Vaugirard", _lightBlueColorGroup, 100),
                new Chance("Chance"),
                new Property("Rue de Courcelles", _lightBlueColorGroup, 100),
                new Property("Avenue de la République", _lightBlueColorGroup, 120),
                new Jail("En prison"),
                new Property("Boulevard de la Villette", _violetColorGroup, 140),
                new ElectricCompany("Compagnie de distribution d'électricité", 150),
                new Property("Avenue de Neuilly", _violetColorGroup, 140),
                new Property("Rue de Paradis", _violetColorGroup, 160),
                new TrainStation("Gare de Lyon", 200),
                new Property("Avenue Mozart", _orangeColorGroup, 180),
                new CommunityChest("Caisse de communauté"),
                new Property("Boulevard Saint Michel", _orangeColorGroup, 180),
                new Property("Place Pigalle", _orangeColorGroup, 200),
                new FreeParking("Parc Gratuit"),
                new Property("Avenue Matignon", _redColorGroup, 220),
                new Chance("Chance"),
                new Property("Boulevard Malesherbes", _redColorGroup, 220),
                new Property("Avenue Henri-Martin", _redColorGroup, 240),
                new TrainStation("Gare du Nord", 200),
                new Property("Faubourg Saint-Honoré", _yellowColorGroup, 260),
                new Property("Place de la Bourse", _yellowColorGroup, 260),
                new WaterWorks("Compagnie de distribution des eaux", 150),
                new Property("Rue la Fayette", _yellowColorGroup, 280),
                new GoToJail("Allez en Prison"),
                new Property("Avenue de Breteuil", _greenColorGroup, 300),
                new Property("Avenue Foch", _greenColorGroup, 300),
                new CommunityChest("Caisse de communauté"),
                new Property("Boulevard des Capucines", _greenColorGroup, 320),
                new TrainStation("Gare Saint-Lazare", 200),
                new Chance("Chance"),
                new Property("Avenue des Champs-Elysées", _darkBlueColorGroup, 350),
                new LuxuryTax("Taxe de luxe", 100),
                new Property("Rue de la Paix", _darkBlueColorGroup, 400)
            };
         }

         public IGameBoard GetBoard()
         {
             return new GameBoard(_squares, _colorGroups, _numberOfPlayers);
         }
    }
}