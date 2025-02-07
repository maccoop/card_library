using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace thirdparty.card_library.data.Match
{
    public abstract class IMatchUI: MonoBehaviour
    {
        public UnityAction OnAnimationEnd;

        [Button]
        public abstract void DoAnimation();
        public abstract object[] SelectCards();
    }
}
