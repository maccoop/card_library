using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICard
{
    int Value { get; }
    string Type { get; }
    MatchPoint GetAddition();
    Sprite GetIcon(string skin = "def");

}
