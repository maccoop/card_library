using UnityEngine;

public interface ISupportCard
{
    public enum Type
    {
        Effect, Consume
    }
    public string Description { get; }
    MatchPoint GetSupportValue(string[] cards);
    MatchPoint GetSupportValue(ICard[] cards);
}
