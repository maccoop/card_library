using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.SimpleLocalization.Scripts;
using System;

public class PriceLocalizeService : MonoBehaviour, IEService
{
    public TextAsset asset;
    Dictionary<string, int> prices;

    private void Awake()
    {
        ServiceLocator.Instance.Register(this);
    }

    private void Start()
    {
        prices = new();
        List<string> list = LocalizationManager.GetLines(asset.text);
        string[] item;

        foreach (var e in list)
        {
            try
            {
                item = e.Split(',');
                prices.Add(item[0], int.Parse(item[1]));
            }
            catch (Exception es)
            {
                throw es;
            }
        }
    }

    internal int GetPrice(string name)
    {
        return prices[name];
    }
}
