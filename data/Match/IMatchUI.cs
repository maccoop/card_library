using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace thirdparty.card_library.data.Match
{
    public abstract class IMatchUI: MonoBehaviour, System.IDisposable
    {
        public UnityAction OnEnd;
        public IMatchPlayer Player { get; private set; }

        [Button]
        public abstract void DoAnimation();
        public abstract object[] SelectCards();
        public void SetPlayer(IMatchPlayer player)
        {
            this.Player = player;
        }

        public void Dispose()
        {
            OnEnd = null;
        }
    }
}
