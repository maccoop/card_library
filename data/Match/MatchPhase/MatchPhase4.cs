using System;
using System.Collections;
using thirdparty.card_library.data.Card;
using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace thirdparty.card_library.data.Match
{
    /// <summary>
    /// chọn bài đánh
    /// </summary>
    public class MatchPhase4 : IMatchUI
    {
        const float DURATION = 0.1f;
        const float DELAY = 0.1f;
        public int MaxValue { get; set; } = 3;
        public CardUI[] r1;
        public Ease ease;
        public Button btn1;

        private bool[] r2 = new bool[5];
        private Phase4Item[] r3 = new Phase4Item[5];
        private int i = 0;
        private bool b1;
        private int count;

        public override void DoAnimation()
        {
            b1 = true;
            for (int i = 0; i < r1.Length; i++)
            {
                var item = r1[i].gameObject.AddComponent<Phase4Item>();
                item.SetParent(this);
                item.SetIndex(i);
                r3[i] = item;
            }
            btn1.onClick.AddListener(OnEnd);
            btn1.gameObject.SetActive(true);
            btn1.interactable = false;
        }

        private void Update()
        {
            if (b1)
            {
                count = 0;
                for (i = 0; i < r2.Length; i++)
                {
                    if (r2[i])
                    {
                        count++;
                    }
                }
                btn1.interactable = count == MaxValue;
            }
        }
        public override object[] SelectCards()
        {
            List<object> result = new();
            for (i = 0; i < r2.Length; i++)
            {
                if (r2[i])
                {
                    result.Add(i);
                }
                Destroy(r3[i].GetComponent<Phase4Item>());
            }
            var value = result.ToArray();
            return value;
        }
        internal bool CheckEnable(int index)
        {
            int count = 0;
            for (i = 0; i < r2.Length; i++)
            {
                if (r2[i])
                {
                    if (i == index)
                    {
                        r2[i] = false;
                        return false;
                    }
                    ++count;
                    if (count >= MaxValue)
                    {
                        return false;
                    }
                }
            }
            r2[index] = true;
            return true;
        }
    }

    public class Phase4Item : MonoBehaviour
    {
        const float DURATION = 0.1f;
        private int index = 0;
        private MatchPhase4 phase;
        private RectTransform rect;
        private float a;
        private float b;

        public void OnClick()
        {
            if (phase.CheckEnable(index))
            {
                rect.DOAnchorPosY(b, DURATION);
            }
            else
            {
                rect.DOAnchorPosY(a, DURATION);
            }
        }
        internal void SetIndex(int i)
        {
            this.index = i;
            GetComponent<Button>().onClick.AddListener(OnClick);
        }
        internal void SetParent(MatchPhase4 p)
        {
            this.phase = p;
            rect = GetComponent<RectTransform>();
            a = rect.anchoredPosition.y;
            b = a + 200;
        }
    }
}
