using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

public class GameBoard
{

    private ColorGroup _brownColorGroup = new ColorGroup(MonopolyClassicTheme.BrownGroupColor);
    private ColorGroup _lightBlueColorGroup = new ColorGroup(MonopolyClassicTheme.LightBlueGroupColor);
    private ColorGroup _violetColorGroup = new ColorGroup(MonopolyClassicTheme.VioletGroupColor);
    private ColorGroup _orangeColorGroup = new ColorGroup(MonopolyClassicTheme.OrangeGroupColor);
    private ColorGroup _redColorGroup = new ColorGroup(MonopolyClassicTheme.RedGroupColor);
    private ColorGroup _yellowColorGroup = new ColorGroup(MonopolyClassicTheme.YellowGroupColor);
    private ColorGroup _greenColorGroup = new ColorGroup(MonopolyClassicTheme.GreenGroupColor);
    private ColorGroup _darkBlueColorGroup = new ColorGroup(MonopolyClassicTheme.DarkBlueGroupColor);

    public List<ColorGroup> ColorGroups {
        get
        {
            
            return new List<ColorGroup>() {_brownColorGroup, _lightBlueColorGroup, _violetColorGroup, _orangeColorGroup, _redColorGroup, _yellowColorGroup, _greenColorGroup, _darkBlueColorGroup};
        }
    }

    public List<Square> Squares {get;}

    public int GetSquareIndex(Square square)
    {
        return Squares.IndexOf(square);
    }


    public GameBoard()
    {
        Squares = CreateSquares();
    }

    private List<Square> CreateSquares()
    {
        return new List<Square> {
            new CommunityChestSquare("Go"),
            new Property("Boulevard de Belleville", _brownColorGroup, 60),
            new CommunityChestSquare("Caisse de communauté"),
            new Property("Rue Lecourbe", _brownColorGroup, 60),
            new CommunityChestSquare("Impôts sur le revenu"),
            new CommunityChestSquare("Gare Montparnasse"),
            new Property("Rue de Vaugirard", _lightBlueColorGroup, 100),
            new CommunityChestSquare("Chance"),
            new Property("Rue de Courcelles", _lightBlueColorGroup, 100),
            new Property("Avenue de la République", _lightBlueColorGroup, 120),
            new CommunityChestSquare("Prison"),
            new Property("Boulevard de la Villette", _violetColorGroup, 140),
            new CommunityChestSquare("Compagnie de distribution d'électricité"),
            new Property("Avenue de Neuilly", _violetColorGroup, 140),
            new Property("Rue de Paradis", _violetColorGroup, 160),
            new CommunityChestSquare("Gare de Lyon"),
            new Property("Avenue Mozart", _orangeColorGroup, 180),
            new CommunityChestSquare("Caisse de communauté"),
            new Property("Boulevard Saint Michel", _orangeColorGroup, 180),
            new Property("Place Pigalle", _orangeColorGroup, 200),
            new CommunityChestSquare("Parc Gratuit"),
            new Property("Avenue Matignon", _redColorGroup, 220),
            new CommunityChestSquare("Chance"),
            new Property("Boulevard Malesherbes", _redColorGroup, 220),
            new Property("Avenue Henri-Martin", _redColorGroup, 240),
            new CommunityChestSquare("Gare du Nord"),
            new Property("Faubourg Saint-Honoré", _yellowColorGroup, 260),
            new Property("Place de la Bourse", _yellowColorGroup, 260),
            new CommunityChestSquare("Compagnie de distribution des eaux"),
            new Property("Rue la Fayette", _yellowColorGroup, 280),
            new CommunityChestSquare("Allez en Prison"),
            new Property("Avenue de Breteuil", _greenColorGroup, 300),
            new Property("Avenue Foch", _greenColorGroup, 300),
            new CommunityChestSquare("Caisse de communauté"),
            new Property("Boulevard des Capucines", _greenColorGroup, 320),
            new CommunityChestSquare("Gare Saint-Lazare"),
            new CommunityChestSquare("Chance"),
            new Property("Avenue des Champs-Elysées", _darkBlueColorGroup, 350),
            new CommunityChestSquare("Taxe de luxe"),
            new Property("Rue de la Paix", _darkBlueColorGroup, 400)
        };
    }
}
