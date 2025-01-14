﻿using UnityEngine;
using System;
using System.Linq;
using System.Text.RegularExpressions;

[Serializable]
public class Card
{
    public const string H = "H";
    public const string D = "D";
    public const string C = "C";
    public const string S = "S";

    internal MatchPoint GetAddition()
    {
        if (Value > 10)
            return MatchPoint.Create(10,0);
        return MatchPoint.Create(Value, 0);
    }

    /// <summary>
    /// key có dạng '[giá trị][loại]'
    /// ex: 4c, 3c, 5d
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
        Value = int.Parse(Regex.Match(key, @"^\d+").Value);
        if (Value > 14)
            Value = 14;
        Type = Regex.Match(key, @"\D").Value.ToUpper();
    }

    public static string GetCardType(int value)
    {
        if (value is 0) return nameof(H);
        if (value is 1) return nameof(D);
        if (value is 2) return nameof(C);
        if (value is 3) return nameof(S);
        return null;
    }

    public static Card GetCard(string card)
    {
        var result = new Card() { key = card };
        result.Init();
        return result;
    }
}

