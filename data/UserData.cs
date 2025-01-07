using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    const string KEY_SAVE = "userdata";
    const string CARD_FRAME_DEF = "def";
    public List<(string, string)> cards;
    public void Init()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int value = 2; value <= 14; value++)
            {
                cards.Add(($"{value},{Card.GetCardType(i)}", CARD_FRAME_DEF));
            }
        }
    }

    public void Save()
    {
        PlayerPrefs.SetString("", JsonUtility.ToJson(this));
        PlayerPrefs.Save();
    }

    public void Add(int value, int type)
    {
    }
}
