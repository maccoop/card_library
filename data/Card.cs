using UnityEngine;
using System;

[Serializable]
public class Card: ICard
{
    public const string H = "Hearts";
    public const string D = "Diamonds";
    public const string C = "Clubs";
    public const string S = "Spades";

    /// <summary>
    /// key có dạng 'giá trị, loại'
    /// giá trị: 2->14
    /// loại:   Hearts - H
    ///         Diamonds – D
    ///         Clubs – C
    ///         Spades – S
    /// </summary>
    private string key;
    public int Value { get; private set; }
    public string Type { get; private set; }

    private Card()
    {

    }

    private void Init()
    {
        Value = int.Parse(key.Split(',')[0]);
        Type = GetCardType(int.Parse(key.Split(',')[1]));
    }
    public MatchPoint GetAddition()
    {
        if (Value > 10)
            return MatchPoint.Create(10, 0);
        return MatchPoint.Create(Value, 0);
    }

    public static string GetCardType(int value)
    {
        if (value is 0) return nameof(H);
        if (value is 1) return nameof(D);
        if (value is 2) return nameof(C);
        if (value is 3) return nameof(S);
        return null;
    }
    public static string GetCardName(int value)
    {
        if (value is 0) return H;
        if (value is 1) return D;
        if (value is 2) return C;
        if (value is 3) return S;
        return null;
    }
    public static Card GetCard(string card)
    {
        var result = new Card() { key = card };
        result.Init();
        return result;
    }
}

