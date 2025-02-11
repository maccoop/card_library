using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace thirdparty.card_library.data.Match
{
    public class Phase5Item : MonoBehaviour
    {
        const float DURATION = 0.2F;
        public CanvasGroup menu;
        public TMP_Text description;
        public Button buttonMenu;
        public Button buttonSelect;

        public void SetData(SupportCard data, MatchPhase5 phase)
        {
            buttonMenu.onClick.AddListener(ShowDes);
            buttonSelect.onClick.AddListener(()=>phase.Select(data));
            description.text = data.Description;
        }

        private void ShowDes()
        {
            menu.DOFade(1 - menu.alpha, DURATION);
        }
    }
}
