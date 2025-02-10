using System;
using System.Collections;
using thirdparty.card_library.data.Card;
using DG.Tweening;
using System.Collections.Generic;

namespace thirdparty.card_library.data.Match
{
    public class MatchPhase3 : IMatchUI
    {
        const float DURATION = 0.2f;
        const float DELAY = 0.1f;
        public CardUI[] cardOnHand;
        public Ease ease;

        int i = 0;
        public override void DoAnimation()
        {
        }

        public override object[] SelectCards()
        {
            throw new System.NotImplementedException();
        }
    }
}
