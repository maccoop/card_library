using System;
using System.Linq;
using UnityEngine;
using IEnumerator = System.Collections.IEnumerator;

public class MatchControl : UnityEngine.MonoBehaviour
{
    [System.Serializable]
    public enum MatchPhase
    {
        WaitingPlayer, KhoiTao, ChiaBai, ChonBaiBoTro, PhuDau, ChonBaiChinh, SuDungTheBoTro, MoBai, KetThuc
    }
    private WaitForSeconds _waitSwitchPhase = new WaitForSeconds(MatchSetting.SecondSwitchPhase);
    private WaitForSeconds _waitPlayerSelected = new WaitForSeconds(MatchSetting.SecondWaitPlayerSelected);
    private WaitForFixedUpdate _waitUpdate = new WaitForFixedUpdate();
    private SelectedCardData phaseData;
    private MatchPhase lastPhase;

    public IMatchPlayer Player1 { get; private set; }
    public IMatchPlayer Player2 { get; private set; }
    public MatchPhase _MatchPhase { get; private set; }
    public SelectedSPCardData SupportCards { get; private set; }
    public SelectedSPCardData UseSupportCard { get; private set; }
    public bool player1First { get; private set; }
    public int Phase { get; private set; }
    public MatchPoint _MatchPoint { get; private set; }
    public bool Join(IMatchPlayer player)
    {
        if (_MatchPhase != MatchPhase.WaitingPlayer)
            return false;
        if (Player1 == null)
        {
            Player1 = player;
            return true;
        }
        else
        {
            Player2 = player;
            _MatchPhase = MatchPhase.KhoiTao;
            return true;
        }
    }

    public IEnumerator Khoitao()
    {
        if (_MatchPhase != MatchPhase.KhoiTao)
            yield break;
        /// tạo bài
        /// 
        Player1.KhoiTao();
        Player2.KhoiTao();
        // tráo bài
        Player1.XaoBai();
        Player2.XaoBai();
        // next
        _MatchPhase = MatchPhase.ChiaBai;
    }

    public IEnumerator ChiaBai()
    {
        yield return _waitSwitchPhase;
        Player1.ChiaBai();
        Player2.ChiaBai();
        _MatchPhase = MatchPhase.ChonBaiBoTro;
    }

    public IEnumerator ChonBaiBoTro()
    {
        yield return _waitSwitchPhase;
        Player1.ChonBaiHoTro();
        Player2.ChonBaiHoTro();
        bool player1Done = false;
        bool player2Done = false;
        Player1.OnSupportCardSelected += (cards) =>
        {
            SupportCards.Player1 = cards;
            player1Done = true;
        };
        Player2.OnSupportCardSelected += (cards) =>
        {
            SupportCards.Player2 = cards;
            player2Done = true;
        };
        while (!PlayerSelectCardDone())
        {
            yield return _waitPlayerSelected;
        }
        _MatchPhase = MatchPhase.PhuDau;

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
        Player1.ChonBai(1);
        Player2.ChonBai(1);
        Player1.OnCardSelected += (cards) =>
        {
            phaseData.Player1 = new ICard[] { cards[0] };
            player1Done = true;
        };
        Player2.OnCardSelected += (cards) =>
        {
            phaseData.Player2 = new ICard[] { cards[0] };
            player2Done = true;
        };
        // đợi người chơi chọn xong
        while (!PlayerSelectCardDone())
        {
            yield return _waitPlayerSelected;
        }
        // Tính điểm
        int point1 = CardHelper.GetCardValue(phaseData.Player1, SupportCards.Player1);
        int point2 = CardHelper.GetCardValue(phaseData.Player2, SupportCards.Player2);
        player1First = point1 >= point2;
        Debug.Log(player1First ? "Player 1" : "Player2" + "is first play");
        _MatchPhase = MatchPhase.ChonBaiChinh;
        Phase = 1;
        bool PlayerSelectCardDone()
        {
            return player1Done && player2Done;
        }
    }

    public IEnumerator ChonBaiChinh()
    {
        yield return _waitSwitchPhase;
        phaseData = new SelectedCardData();
        bool player1Done = false;
        bool player2Done = false;
        var fPlayer = player1First ? Player1 : Player2;
        var sPlayer = player1First ? Player2 : Player1;
        fPlayer.ChonBai(3);
        fPlayer.OnCardSelected += (cards) =>
        {
            if (player1First)
                phaseData.Player1 = cards;
            else
                phaseData.Player2 = cards;
            player1Done = true;
            sPlayer.ChonBai(3);
            fPlayer.OnCardSelected += (cards) =>
            {
                if (player1First)
                    phaseData.Player2 = cards;
                else
                    phaseData.Player1 = cards;
                player2Done = true;
            };
        };
        // đợi người chơi chọn xong
        while (!PlayerSelectCardDone())
        {
            yield return _waitPlayerSelected;
        }
        _MatchPhase = MatchPhase.SuDungTheBoTro;
        bool PlayerSelectCardDone()
        {
            return player1Done && player2Done;
        }
    }

    public IEnumerator SuDungTheBoTro()
    {
        yield return _waitSwitchPhase; 
        Player1.DungBaiHoTro();
        Player2.DungBaiHoTro();
        bool player1Done = false;
        bool player2Done = false;
        UseSupportCard = new();
        Player1.OnSupportCardUsed += (cards) =>
        {
            player1Done = true;
            UseSupportCard.Player1 = cards;
        };
        Player2.OnSupportCardUsed += (cards) =>
        {
            player2Done = true;
            UseSupportCard.Player2 = cards;
        };
        while (!PlayerSelectCardDone())
        {
            yield return _waitPlayerSelected;
        }
        _MatchPhase = MatchPhase.MoBai;
        bool PlayerSelectCardDone()
        {
            return player1Done && player2Done;
        }
    }

    public IEnumerator MoBai()
    {
        yield return _waitSwitchPhase;
        ISupportCard[] useSPCard1 = SupportCards.Player1.Concat(UseSupportCard.Player1).ToArray();
        ISupportCard[] useSPCard2 = SupportCards.Player2.Concat(UseSupportCard.Player2).ToArray();
        int point1 = CardHelper.GetCardValue(phaseData.Player1, useSPCard1);
        int point2 = CardHelper.GetCardValue(phaseData.Player2, useSPCard2);
        _MatchPoint.player1 += point1 >= point2 ? 1 : 0;
        _MatchPoint.player2 += point2 >= point1 ? 1 : 0;
        Phase++;
        if(Phase > 3)
        {
            _MatchPhase = MatchPhase.ChonBaiChinh;
        }
        else
        {
            _MatchPhase = MatchPhase.KetThuc;
        }
    }

    public IEnumerator KetThuc()
    {
        yield return _waitSwitchPhase;
        Player1.KetThuc(_MatchPoint.GetKQPlayer1());
        Player2.KetThuc(_MatchPoint.GetKQPlayer2());
    }

    private void Update()
    {
        if(lastPhase != _MatchPhase)
        {
            lastPhase = _MatchPhase;
            switch (_MatchPhase)
            {
                case MatchPhase.ChiaBai:
                    StartCoroutine(this.ChiaBai());
                    break;
                case MatchPhase.ChonBaiBoTro:
                    StartCoroutine(this.ChonBaiBoTro());
                    break;
                case MatchPhase.ChonBaiChinh:
                    StartCoroutine(this.ChonBaiChinh());
                    break;
                case MatchPhase.KetThuc:
                    StartCoroutine(this.KetThuc());
                    break;
                case MatchPhase.KhoiTao:
                    StartCoroutine(this.Khoitao());
                    break;
                case MatchPhase.MoBai:
                    StartCoroutine(this.MoBai());
                    break;
                case MatchPhase.PhuDau:
                    StartCoroutine(this.PhuDau());
                    break;
                case MatchPhase.SuDungTheBoTro:
                    StartCoroutine(this.SuDungTheBoTro());
                    break;
            }
        }
        
    }

    public class SelectedCardData
    {
        public ICard[] Player1;
        public ICard[] Player2;
    }
    public class SelectedSPCardData
    {
        public ISupportCard[] Player1;
        public ISupportCard[] Player2;
    }

    public class MatchPoint
    {
        public int player1;
        public int player2;

        public enum KetQua
        {
            Thang, Hoa, Thua
        }

        public KetQua GetKQPlayer1()
        {
            return player1 > player2 ? KetQua.Thang : player1 == player2 ? KetQua.Hoa : KetQua.Thua;
        }

        public KetQua GetKQPlayer2()
        {
            return player2 > player1 ? KetQua.Thang : player1 == player2 ? KetQua.Hoa : KetQua.Thua;
        }
    }
}
