using System.Collections;
using UnityEngine;

namespace thirdparty.card_library.data.Match
{
    public abstract class IMatchUI: MonoBehaviour
    {
        public abstract IEnumerator DoAnimation();
        public abstract object[] SelectCards();
    }
}
