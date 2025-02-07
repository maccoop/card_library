using System.Collections.Generic;
using thirdparty.card_library.data.Card;
using UnityEngine;

[System.Serializable]
public class UserDataSingle
{
    const string KEY_SAVE = "userdata";
    const string CARD_FRAME_DEF = "def";
    public List<(string, string)> cards;
    public List<(string, string)> supportCards;

    public void Init()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int value = 2; value <= 14; value++)
            {
                cards.Add(($"{value},{Card.GetCardType(i)}", CARD_FRAME_DEF));
            }
        }
        Save();
    }

    public void Save()
    {
        PlayerPrefs.SetString(KEY_SAVE, JsonUtility.ToJson(this));
        PlayerPrefs.Save();
    }
}
