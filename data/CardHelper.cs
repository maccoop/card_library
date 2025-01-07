using UnityEngine;

public static class CardHelper
{
    public static int GetCardValue(string[] cards, string[] supports)
    {
        MatchPoint point = MatchPoint.Create(0, 0);
        Card cache = null;
        foreach(var card in cards)
        {
            cache = Card.GetCard(card);
            point += cache.GetAddition();
        }
        point += SupportCardHelper.GetAdditionValue(cards, supports);
        return point.GetValue();
    }
}
