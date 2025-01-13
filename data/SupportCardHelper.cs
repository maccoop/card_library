using System;
using System.Collections.Generic;
using System.Linq;

public struct MatchPoint
{
    public int Plus;
    public int Multi;

    private MatchPoint(int plus, int multi)
    {
        this.Plus = plus;
        Multi = multi;
    }

    public static MatchPoint Create(int plus, int multi)
    {
        return new MatchPoint(plus, multi);
    }

    public static MatchPoint operator +(MatchPoint a, MatchPoint b)
    {
        return MatchPoint.Create(a.Plus + b.Plus, a.Multi + b.Multi);
    }

    internal int GetValue()
    {
        return Plus * Multi;
    }
}

public static class SupportCardHelper
{
    // return {+,x}
    public static MatchPoint GetAdditionValue(string[] cards, string[] supportCards)
    {
        MatchPoint result = MatchPoint.Create(0, 0);
        MatchPoint cache = MatchPoint.Create(0, 0);
        foreach (var support in supportCards)
        {
            cache = SupportCardHelper.GetSupportValue(support, cards);
            result += result;
        }
        return result;
    }

    public static MatchPoint GetSupportValue(string supportCard, string[] cards)
    {

        if (!_supportCardLoaded.ContainsKey(supportCard))
        {
            Type foundType = AppDomain.CurrentDomain.GetAssemblies()
           .SelectMany(assembly => assembly.GetTypes())
           .FirstOrDefault(type => type.Name == supportCard);
            if (foundType == null)
                throw new ArgumentNullException(supportCard);
            var instance = Activator.CreateInstance(foundType);
            _supportCardLoaded.Add(supportCard, instance as SupportCard);
        }
        return _supportCardLoaded[supportCard].GetSupportValue(cards);
    }

    static Dictionary<string, SupportCard> _supportCardLoaded = new();
}

