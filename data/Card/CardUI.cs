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

        private void Start()
        {
            icon.sprite = data.GetIcon();
        }

        public void Init(global::thirdparty.card_library.data.Card.Card data)
        {
            this.background.sprite = data.GetIcon();
        }
    }
}
