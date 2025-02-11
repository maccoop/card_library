using System;
using System.Collections;
using thirdparty.card_library.data.Card;
using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace thirdparty.card_library.data.Match
{
    /// <summary>
    /// chọn bài hỗ trợ
    /// </summary>
    public class MatchPhase5 : IMatchUI
    {
        const float DURATION = 0.2F;
        public RectTransform _base;
        public Transform content;
        public Phase5Item prefab;
        public Vector2 targetPos;

        private List<Phase5Item> items = new();
        private Vector2 basePos;
        private bool enable = false;
        UserDataSingle userdata;
        SupportCard selected;

        private void Start()
        {
            basePos = _base.anchoredPosition;
        }

        public override void DoAnimation()
        {
            if (userdata.supportCards.Count == 0)
            {
                OnEnd.Invoke();
                return;
            }
            enable = true;
            _base.DOAnchorPos(targetPos, DURATION);
            for (int i = 0; i < userdata.supportCards.Count; i++)
            {
                var item = Instantiate(prefab, content);
                var data = SupportCardHelper.GetSupportCard(userdata.supportCards[i].Item1);
                item.SetData(data, this);
                items.Add(item);
            }
        }

        internal void Select(SupportCard data)
        {
            selected = data;
            enable = false;
            _base.DOAnchorPos(basePos, DURATION);
            OnEnd.Invoke();
        }

        public override object[] SelectCards()
        {
            return new object[1] { selected };
        }
    }
}
