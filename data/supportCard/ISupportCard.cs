using UnityEngine;
//using Assets.SimpleLocalization.Scripts;

public interface ISupportCard
{
    string Name { get; }
    int Price { get; }
    Sprite Texture { get; }
    string Description { get; }
    MatchPoint GetSupportValue(ICard[] cards);
}

public abstract class SupportCard : ISupportCard
{
    public string Name => GetType().Name;
    public int Price
    {
        get
        {
            if (_price == 0)
            {
                _price = 0;//ServiceLocator.Instance.GetService<PriceLocalizeService>().GetPrice(Name);
            }
            return _price;
        }
    }
    public Sprite Texture
    {
        get
        {
            if (_texture == null)
            {
                _texture = Resources.Load<Sprite>("support/" + GetType().Name);
            }
            return _texture;
        }
    }
    public string Description
    {
        get
        {
            if (_description != null)
            {
                //LocalizationManager.Localize(Name);
            }
            return _description;
        }
    }
    public abstract MatchPoint GetSupportValue(ICard[] cards);
    private Sprite _texture;
    private string _description;
    private int _price;
}
