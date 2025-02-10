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

        public void XaoBai()
        {
            GetPhases(0).DoAnimation();
        }

        public void ChiaBai()
        {
            GetPhases(1).DoAnimation();
        }

        public void KhoiTao()
        {
            GetPhases(2).DoAnimation();
        }

        public void ChonBaiHoTro(OnSelected onSelected)
        {
            using (var phase = GetPhases(3))
            {
                phase.DoAnimation();
                phase.OnEnd += () =>
                {
                    onSelected.Invoke(phase.SelectCards().Cast<string>().ToArray());
                };
            }
        }

        public void ChonBai(int amountCardRequire, OnSelected onSelected)
        {
            using (var phase = GetPhases(4) as MatchPhase4)
            {
                phase.DoAnimation();
                phase.MaxValue = amountCardRequire;
                phase.OnEnd += () =>
                {
                    onSelected.Invoke(phase.SelectCards().Cast<string>().ToArray());
                };
            }
        }

        public void DungBaiHoTro(OnSelected onSelected)
        {

            using (var phase = GetPhases(5))
            {
                phase.DoAnimation();
                phase.OnEnd += () =>
                {
                    onSelected.Invoke(phase.SelectCards().Cast<string>().ToArray());
                };
            }
        }

        public void KetThuc(MatchControl.MatchPoint.KetQua ketQua)
        {
            GetPhases(6).DoAnimation();
        }
    }
}
