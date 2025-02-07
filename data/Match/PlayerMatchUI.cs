using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace thirdparty.card_library.data.Match
{
    public class PlayerMatchUI : MonoBehaviour, IMatchPlayerUI
    {
        [SerializeField] private IMatchUI[] phases;

        private void Start()
        {
        }

        private IMatchUI GetPhases(int index)
        {
            return phases[index];
        }

        public IEnumerator XaoBai()
        {
            GetPhases(0).DoAnimation();
            yield break;
        }

        public IEnumerator ChiaBai()
        {
            GetPhases(1).DoAnimation();
            yield break;
        }

        public IEnumerator KhoiTao()
        {
            GetPhases(2).DoAnimation();
            yield break;
        }

        public IEnumerator<string[]> ChonBaiHoTro()
        {
            GetPhases(3).DoAnimation();
            yield return GetPhases(3).SelectCards().Cast<string>().ToArray();
        }

        public IEnumerator<int[]> ChonBai(List<ICard> cardOnHand, int amountCardRequire)
        {
            GetPhases(4).DoAnimation();
            yield return GetPhases(4).SelectCards().Cast<int>().ToArray();
        }

        public IEnumerator<int[]> DungBaiHoTro()
        {
            GetPhases(5).DoAnimation();
            yield return GetPhases(5).SelectCards().Cast<int>().ToArray();
        }

        public IEnumerator KetThuc(MatchControl.MatchPoint.KetQua ketQua)
        {
            GetPhases(6).DoAnimation();
            yield break;
        }
    }
}
