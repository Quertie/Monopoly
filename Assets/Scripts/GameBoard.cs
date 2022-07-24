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
            new Property("Boulevard de Belleville", _brownColorGroup),
            new CommunityChestSquare("Caisse de communauté"),
            new Property("Rue Lecourbe", _brownColorGroup),
            new CommunityChestSquare("Impôts sur le revenu"),
            new CommunityChestSquare("Gare Montparnasse"),
            new Property("Rue de Vaugirard", _lightBlueColorGroup),
            new CommunityChestSquare("Chance"),
            new Property("Rue de Courcelles", _lightBlueColorGroup),
            new Property("Avenue de la République", _lightBlueColorGroup),
            new CommunityChestSquare("Prison"),
            new Property("Boulevard de la Villette", _violetColorGroup),
            new CommunityChestSquare("Compagnie de distribution d'électricité"),
            new Property("Avenue de Neuilly", _violetColorGroup),
            new Property("Rue de Paradis", _violetColorGroup),
            new CommunityChestSquare("Gare de Lyon"),
            new Property("Avenue Mozart", _orangeColorGroup),
            new CommunityChestSquare("Caisse de communauté"),
            new Property("Boulevard Saint Michel", _orangeColorGroup),
            new Property("Place Pigalle", _orangeColorGroup),
            new CommunityChestSquare("Parc Gratuit"),
            new Property("Avenue Matignon", _redColorGroup),
            new CommunityChestSquare("Chance"),
            new Property("Boulevard Malesherbes", _redColorGroup),
            new Property("Avenue Henri-Martin", _redColorGroup),
            new CommunityChestSquare("Gare du Nord"),
            new Property("Faubourg Saint-Honoré", _yellowColorGroup),
            new Property("Place de la Bourse", _yellowColorGroup),
            new CommunityChestSquare("Compagnie de distribution des eaux"),
            new Property("Rue la Fayette", _yellowColorGroup),
            new CommunityChestSquare("Allez en Prison"),
            new Property("Avenue de Breteuil", _greenColorGroup),
            new Property("Avenue Foch", _greenColorGroup),
            new CommunityChestSquare("Caisse de communauté"),
            new Property("Boulevard des Capucines", _greenColorGroup),
            new CommunityChestSquare("Gare Saint-Lazare"),
            new CommunityChestSquare("Chance"),
            new Property("Avenue des Champs-Elysées", _darkBlueColorGroup),
            new CommunityChestSquare("Taxe de luxe"),
            new Property("Rue de la Paix", _darkBlueColorGroup)
        };
    }
}
