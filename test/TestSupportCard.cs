using UnityEngine;

public class TestSupportCard : MonoBehaviour
{
    string nameofSP = "";
    SupportCard spCard;
    private void OnGUI()
    {
        int i = 0;
        int rowCount = 10;
        int spacing = 10;
        int width = Screen.width / 2;
        int height = 40;
        i++;
        GUI.Label(GetRect(), "Test Get Support Card");
        i++;
        GUI.Label(GetRect(), "Input name support card: ");
        i++;
        nameofSP = GUI.TextField(GetRect(), nameofSP);
        i++;
        if (GUI.Button(GetRect(), "Test Get Support Card"))
        {
            spCard = SupportCardHelper.GetSupportCard(nameofSP);
        }
        i++;
        if (spCard != null)
        {
            GUI.Label(GetRect(), "Suppport Card Name: " + spCard.GetType().Name);
            i++;
            GUI.Label(GetRect(), "Suppport Card Des: " + spCard.Description);
            i++;
        }
        string arrayName = "";
        foreach(var e in SupportCardHelper._supportCardLoaded)
        {
            arrayName += "," + e.Key;
        }
        GUI.Label(GetRect(), arrayName);


        Rect GetRect() => new Rect(width/2, GetPosition(), width, height);
        int GetPosition() => i * height + i * spacing;
    }
}
