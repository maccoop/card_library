using UnityEngine;

public static class CardHelper
{
    public static int GetCardValue(ICard[] cards, ISupportCard[] supports)
    {
        MatchPoint point = MatchPoint.Create(0, 0);
        foreach(var card in cards)
        {
            point += card.GetAddition();
        }
        point += SupportCardHelper.GetAdditionValue(cards, supports);
        return point.GetValue();
    }
}
