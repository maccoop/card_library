using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace thirdparty.card_library.data.Match
{
    public class PlayerMatchUI : MonoBehaviour, IMatchPlayerUI
    {
        private IMatchUI[] phases;

        private void Start()
        {
            throw new NotImplementedException();
        }

        private IMatchUI GetPhases(int index)
        {
            return phases[index];
        }
    
        public IEnumerator XaoBai()
        {
            yield return GetPhases(0).DoAnimation();
        }

        public IEnumerator ChiaBai()
        {
            yield return GetPhases(1).DoAnimation();
        }

        public IEnumerator KhoiTao()
        {
            yield return GetPhases(2).DoAnimation();
        }

        public IEnumerator<string[]> ChonBaiHoTro()
        {
            StartCoroutine(GetPhases(3).DoAnimation());
            yield return GetPhases(3).SelectCards().Cast<string>().ToArray();
        }

        public IEnumerator<int[]> ChonBai(List<ICard> cardOnHand, int amountCardRequire)
        {
            StartCoroutine(GetPhases(4).DoAnimation());
            yield return GetPhases(4).SelectCards().Cast<int>().ToArray();
        }

        public IEnumerator<int[]> DungBaiHoTro()
        {
            StartCoroutine(GetPhases(5).DoAnimation());
            yield return GetPhases(5).SelectCards().Cast<int>().ToArray();
        }

        public IEnumerator KetThuc(MatchControl.MatchPoint.KetQua ketQua)
        {
            yield return GetPhases(6).DoAnimation();
        }
    }
}
