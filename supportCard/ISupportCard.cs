using UnityEngine;

public interface ISupportCard
{
    int Price { get; }
    Sprite Texture { get; }
    string Description { get; }
    MatchPoint GetSupportValue(string[] cards);
}

public abstract class SupportCard: ISupportCard
{
    public abstract int Price { get; }
    public Sprite Texture
    {
        get
        {
            if(_texture == null)
            {
                _texture = Resources.Load<Sprite>("support/" + GetType().Name);
            }
            return _texture;
        }
    }
    public abstract string Description { get; }
    public abstract MatchPoint GetSupportValue(string[] cards);
    private Sprite _texture;
}
