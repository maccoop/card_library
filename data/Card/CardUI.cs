using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace thirdparty.card_library.data.Card
{
    public class CardUI : MonoBehaviour
    {
        public global::thirdparty.card_library.data.Card.Card data;
        public Image icon;
        public Image background;
        public TMP_Text title, description;
        public TMP_Text point1, point2;
    
        public UserDataSingle userData;

        public void Init(ICard data)
        {
            this.background.sprite = data.GetIcon();
            this.icon.sprite = data.GetIcon();
            this.title.text = data.Value.ToString().ToUpper();
            this.description.text = data.Type;
        }
    }
}
