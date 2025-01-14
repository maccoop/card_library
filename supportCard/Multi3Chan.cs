using UnityEngine;

[System.Serializable]
public class Multi3Chan : SupportCard
{
    [SerializeField] private int _price;
    [SerializeField] private Sprite _texture;
    const int VALUE = 3;
    protected bool type;
    public Multi3Chan()
    {
        type = true;
    }
    public override int Price => _price;

    public override MatchPoint GetSupportValue(string[] cards)
    {
        MatchPoint result = new MatchPoint();
        Card card;
        foreach (var e in cards)
        {
            card = Card.GetCard(e);
            if ((card.Value % 2 == 0) == type)
                result.Multi += VALUE;
        }
        return result;
    }
}

[System.Serializable]
public class Multi3Le : Multi3Chan
{
    public Multi3Le()
    {
        type = false;
    }
}
