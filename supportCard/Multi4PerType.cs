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
    public override string Description => $"Thêm {VALUE} điểm nhân với mỗi bài {cardtype} đánh ra";

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
