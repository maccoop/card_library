using System;
using System.Collections;
using thirdparty.card_library.data.Card;
using DG.Tweening;
using System.Collections.Generic;

namespace thirdparty.card_library.data.Match
{
    public class MatchPhase2 : IMatchUI
    {
        const float DURATION = 0.2f;
        const float DELAY = 0.1f;
        public CardUI[] cardOnHand;
        public Ease ease;

        int i = 0;
        public override void DoAnimation()
        {
            //InitCardUI();
            float delay = 0;
            for (i = 0; i < cardOnHand.Length; i++)
            {
                cardOnHand[i].transform.DOScaleX(1, DURATION).SetEase(ease).SetDelay(delay);
                delay += DELAY;
            }
        }

        private void InitCardUI()
        {
            for (i = 0; i < cardOnHand.Length; i++)
            {
                if (i >= Player.CardOnHand.Count)
                    break;
                cardOnHand[i].Init(Player.CardOnHand[i]);
            }
        }

        public override object[] SelectCards()
        {
            throw new System.NotImplementedException();
        }
    }
}
