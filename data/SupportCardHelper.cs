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
        MatchPoint result = MatchPoint.Create(0,0);
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
        ISupportCard supportScript = null;
        switch (supportCard)
        {
            default:
                break;
        }
        return supportScript.GetSupportValue(cards);
    }
}

