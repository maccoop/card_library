using UnityEngine;


public class Multi4H : SupportCard
{
    [SerializeField] private int _price = 6;
    const int VALUE = 4;
    protected string cardtype;
    public Multi4H()
    {
        cardtype = Card.H;
    }

    public override MatchPoint GetSupportValue(ICard[] cards)
    {
        MatchPoint result = new MatchPoint();
        foreach (var card in cards)
        {
            if (card.Type.ToUpper().Equals(cardtype.ToUpper()))
                result.Multi += VALUE;
        }
        return result;
    }
}

public class Multi4S : Multi4H
{
    public Multi4S()
    {
        cardtype = Card.S;
    }
}

public class Multi4C : Multi4H
{
    public Multi4C()
    {
        cardtype = Card.C;
    }
}

public class Multi4D : Multi4H
{
    public Multi4D()
    {
        cardtype = Card.D;
    }
}
