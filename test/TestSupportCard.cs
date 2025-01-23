using System.Collections.Generic;
using thirdparty.card_library.data.Card;
using UnityEngine;

public class TestSupportCard : MonoBehaviour
{
    string arraySPCard = "";
    string arrayCard = "";
    MatchPoint point;
    List<SupportCard> spCard;
    List<Card> cards;
    private void OnGUI()
    {
        int i = 0;
        int rowCount = 10;
        int spacing = 10;
        int width = Screen.width / 2;
        int height = 40;
        // title
        GUI.Label(GetRect(), "Test Card");
        
        // input hand card
        GUI.Label(GetRect(), "Input array hand card: ");
        arrayCard = GUI.TextField(GetRect(), arrayCard);

        //input sp card
        GUI.Label(GetRect(), "Input array support card: ");
        arraySPCard = GUI.TextField(GetRect(), arraySPCard);

        // button action
        if (GUI.Button(GetRect(), "GET Total Score"))
        {
            spCard = new List<SupportCard>();
            foreach(var spname in arraySPCard.Split(","))
            {
                spCard.Add(SupportCardHelper.GetSupportCard(spname));
            }
            cards = new();
            foreach(var card in arrayCard.Split(","))
            {
                cards.Add(Card.GetCard(card));
            }
        }

        //tính điểm
        if(cards != null)
        {
            point.Multi = 1;
            point.Plus = 0;
            foreach (var card in cards)
            {
                point += card.GetAddition();
            }
            // cards 
            string logC = "";
            foreach (var card in cards)
            {
                logC += "(" + card.Value + ":" + card.Type + "), ";
            }
            GUI.Label(GetRect(), logC);
        }
        if(spCard != null)
        {
            // sp cards
            string arrayName = "";
            foreach (var e in spCard)
            {
                arrayName += "," + e.Name;
                point += e.GetSupportValue(cards.ToArray());
            }
            GUI.Label(GetRect(), arrayName);
        }
        GUI.Label(GetRect(), $"{point.Plus} : {point.Multi}");
        GUI.Label(GetRect(), "Điểm nhận: " + point.GetValue());

        //end

        Rect GetRect(int height = 40)
        {
            var result = new Rect(width / 2, GetPosition(), width, height);
            ++i;
            return result;
        }
        int GetPosition() => i * height + i * spacing;
    }
}
