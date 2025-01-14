using UnityEngine;

[System.Serializable]
public class Add10Chan : SupportCard
{
    [SerializeField] private Sprite _texture;
    [SerializeField] private int _price;
    const int VALUE = 10;
    protected bool type;
    public Add10Chan()
    {
        type = true;
    }
    public override int Price => _price;

    public override MatchPoint GetSupportValue(Card[] cards)
    {
        MatchPoint result = new MatchPoint();
        foreach (var card in cards)
        {
            if ((card.Value % 2 == 0) == type)
                result.Plus += VALUE;
        }
        return result;
    }
}

[System.Serializable]
public class Add10Le: Add10Chan
{
    public Add10Le()
    {
        type = false;
    }
}
