using UnityEngine;

[System.Serializable]
public class Multi2H : SupportCard
{
    [SerializeField] private Sprite _texture;
    [SerializeField] private int _price;
    const int VALUE = 2;
    protected string cardtype;
    public Multi2H()
    {
        cardtype = Card.H;
    }

    public override int Price => _price;

    public override MatchPoint GetSupportValue(string[] cards)
    {
        MatchPoint result = new MatchPoint();
        Card card;
        foreach (var e in cards)
        {
            card = Card.GetCard(e);
            if (card.Type == cardtype)
                result.Multi += VALUE;
        }
        return result;
    }
}

[System.Serializable]
public class Multi2S : Multi2H
{
    public Multi2S()
    {
        cardtype = Card.S;
    }
}

[System.Serializable]
public class Multi2C : Multi2H
{
    public Multi2C()
    {
        cardtype = Card.C;
    }
}

[System.Serializable]
public class Multi2D : Multi2H
{
    public Multi2D()
    {
        cardtype = Card.D;
    }
}
