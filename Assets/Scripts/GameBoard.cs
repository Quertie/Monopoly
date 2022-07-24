using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameBoard
{
    public List<Square> Squares {get; }

    private List<string> Names = new List<string> {
                "Go",
                "Boulevard de Belleville",
                "Caisse de communauté",
                "Rue Lecourbe",
                "Impôts sur le revenu",
                "Gare Montparnasse",
                "Rue de Vaugirard",
                "Chance",
                "Rue de Courcelles",
                "Avenue de la République",
                "Prison",
                "Boulevard de la Villette",
                "Compagnie de distribution d'électricité",
                "Avenue de Neuilly",
                "Rue de Paradis",
                "Gare de Lyon",
                "Avenue Mozart",
                "Caisse de communauté",
                "Boulevard Saint Michel",
                "Place Pigalle",
                "Parc Gratuit",
                "Avenue Matignon",
                "Chance",
                "Boulevard Malesherbes",
                "Avenue Henri-Martin",
                "Gare du Nord",
                "Faubourg Saint-Honoré",
                "Place de la Bourse",
                "Compagnie de distribution des eaux",
                "Rue la Fayette",
                "Allez en Prison",
                "Avenue de Breteuil",
                "Avenue Foch",
                "Caisse de communauté",
                "Boulevard des Capucines",
                "Gare Saint-Lazare",
                "Chance",
                "Avenue des Champs-Elysées",
                "Taxe de luxe",
                "Rue de la Paix"};

    public GameBoard()
    {
        Squares = Names.Select(n => new Square(n)).ToList();
    }

    public int GetSquareIndex(Square square)
    {
        return Squares.IndexOf(square);
    }
}