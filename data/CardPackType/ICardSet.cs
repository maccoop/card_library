using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardSet
{
    public enum Set
    {
        Normal = 1, Duo = 2, Tri = 3
    }
    MatchPoint GetSetValue(ICard[] cards);
    Set GetSetType(ICard[] cards);
}
