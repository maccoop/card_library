using System;
using UnityEngine;
using IEnumerator = System.Collections.IEnumerator;

public class MatchControl : UnityEngine.MonoBehaviour
{
    [System.Serializable]
    public enum Phase
    {
        WaitingPlayer, KhoiTao, ChiaBai, ChonBaiBoTro, PhuDau, ChonBaiChinh, ChonBaiBotro, MoBai, KetThuc
    }
    private WaitForSeconds _waitSwitchPhase = new WaitForSeconds(MatchSetting.SecondSwitchPhase);
    private WaitForSeconds _waitPlayerSelected = new WaitForSeconds(MatchSetting.SecondSwitchPhase);
    private WaitForFixedUpdate _waitUpdate = new WaitForFixedUpdate();
    public IMatchPlayer Player1 { get; private set; }
    public IMatchPlayer Player2 { get; private set; }
    public Phase MatchPhase { get; private set; }
    public SelectedCardData SupportCards{ get; private set; }
    public bool Join(IMatchPlayer player)
    {
        if (MatchPhase != Phase.WaitingPlayer)
            return false;
        if (Player1 == null)
        {
            Player1 = player;
            return true;
        }
        else
        {
            Player2 = player;
            MatchPhase = Phase.KhoiTao;
            StartCoroutine(CoInit());
            return true;
        }
    }
    public IEnumerator CoInit()
    {
        if (MatchPhase != Phase.KhoiTao)
            yield break;
        /// tạo bài
        /// 
        Player1.Init();
        Player2.Init();
        // tráo bài
        Player1.Suffle();
        Player2.Suffle();
        // next
        MatchPhase = Phase.ChiaBai;
    }

    public IEnumerator ChiaBai()
    {
        yield return _waitSwitchPhase;
        Player1.DealCard();
        Player2.DealCard();
        MatchPhase = Phase.ChonBaiBoTro;
    }

    public IEnumerator ChonBaiBoTro()
    {
        yield return _waitSwitchPhase;
        Player1.SelectSupportCard();
        Player2.SelectSupportCard(); 
        bool player1Done = false;
        bool player2Done = false;
        Player1.OnSupportCardSelected += (cards) =>
        {

        };
        while (!PlayerSelectCardDone())
        {
            yield return _waitPlayerSelected;
        }
        MatchPhase = Phase.PhuDau;

        bool PlayerSelectCardDone()
        {
            return player1Done && player2Done;
        }
    }

    public IEnumerator PhuDau()
    {
        yield return _waitSwitchPhase;
        SelectedCardData phaseData = new();
        bool player1Done = false;
        bool player2Done = false;
        Player1.SelectCard(1);
        Player2.SelectCard(1);
        Player1.OnCardSelected += (cards) =>
        {
            phaseData.Player1Card = new string[] { cards[0] };
            player1Done = true;
        }; 
        Player2.OnCardSelected += (cards) =>
        {
            phaseData.Player2Card = new string[] { cards[0] };
            player2Done = true;
        };
        // đợi người chơi chọn xong
        while (!PlayerSelectCardDone())
        {
            yield return _waitPlayerSelected;
        }
        // Tính điểm
        int supportValue1 = CardHelper.GetCardValue(phaseData.Player1Card, SupportCards.Player1Card);
        int supportValue2 = CardHelper.GetCardValue(phaseData.Player2Card, SupportCards.Player2Card);
        bool PlayerSelectCardDone()
        {
            return player1Done && player2Done;
        }
    }

    public class SelectedCardData
    {
        public string[] Player1Card;
        public string[] Player2Card;
    }

}
