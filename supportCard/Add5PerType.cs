using UnityEngine;

[System.Serializable]
public class Add5H : SupportCard
{
    [SerializeField] private Sprite _texture;
    [SerializeField] private int _price;
    const int VALUE = 5;
    protected string cardtype;
    public Add5H()
    {
        cardtype = Card.H;
    }

    public override int Price => _price;

    public override MatchPoint GetSupportValue(Card[] cards)
    {
        MatchPoint result = new MatchPoint();
        foreach (var card in cards)
        {
            if (card.Type == cardtype)
                result.Plus += VALUE;
        }
        return result;
    }
}

[System.Serializable]
public class Add5S : Add5H
{
   public Add5S()
    {
        cardtype = Card.S;
    }
}

[System.Serializable]
public class Add5C : Add5H
{
   public Add5C()
    {
        cardtype = Card.C;
    }
}

[System.Serializable]
public class Add5D : Add5H
{
   public Add5D()
    {
        cardtype = Card.D;
    }
}
