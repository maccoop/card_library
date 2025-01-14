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

    public static int GetCardValue(string[] cards, SupportCard[] supports)
    {
        return GetCardValue(cards.GetCards(), supports);
    }

    public static int GetCardValue(string[] cards, string[] supports)
    {
        return GetCardValue(cards, supports.GetSupportCards());
    }

    public static Card[] GetCards(this string[] cards)
    {
        Card[] result = new Card[cards.Length];
        int i = 0;
        foreach (var card in cards)
        {
            result[i++] = Card.GetCard(card);
        }
        return result;
    }
}
