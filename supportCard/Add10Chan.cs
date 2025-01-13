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
    public override string Description => $"Thêm {VALUE} điểm cộng với mỗi bài {(type ? "Chẵn":"Lẻ")} đánh ra";
    public override int Price => _price;

    public override MatchPoint GetSupportValue(string[] cards)
    {
        MatchPoint result = new MatchPoint();
        Card card;
        foreach (var e in cards)
        {
            card = Card.GetCard(e);
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
