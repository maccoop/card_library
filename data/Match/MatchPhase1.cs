using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace thirdparty.card_library.data.Match
{
    public class MatchPhase1 : IMatchUI
    {
        public Transform Target;
        public GameObject prefab;
        public AnimationCurve curveX, curveY;
        public override IEnumerator DoAnimation()
        {
            prefab.transform.DOMoveX(Target.position.x, 0.5f, true).SetEase(curveX);
            prefab.transform.DOMoveY(Target.position.y, 0.5f, true).SetEase(curveY);
            yield break;
        }

        public override object[] SelectCards()
        {
            throw new System.NotImplementedException();
        }
    }
}
