using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = System.Random;

public class MatchPlayer : MonoBehaviour, IMatchPlayer
{
    private UserData _userData;
    private IMatchPlayer.CardSelected _onCardSelected;
    private IMatchPlayer.SupportCardSelected _onSupportCardUsed;
    private IMatchPlayer.SupportCardSelected _onSupportCardSelected;
    private List<ICard> _cardOnTable;
    private List<ICard> _cardOnHand;
    private List<ISupportCard> _cardSupportOnTable;
    private ISupportCard[] _cardSupportSelected;
    private IMatchPlayerUI ui;

    public void KhoiTao()
    {
        if (UserData == null)
        {
            UserData = new UserData();
            UserData.Init();
        }
        CardOnTable = new List<ICard>();
        foreach (var e in UserData.cards)
        {
            CardOnTable.Add(Card.GetCard($"{e.Item1},{e.Item2}"));
        }
        StartCoroutine(ui.KhoiTao());
    }

    public void XaoBai()
    {
        Random rng = new Random();
        Shuffle(CardOnTable);
        void Shuffle<T>(List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]); // Hoán đổi vị trí
            }
        }
        StartCoroutine(ui.XaoBai());
    }

    public void ChiaBai()
    {
        CardOnHand = CardOnTable.GetRange(0, 9);
        StartCoroutine(ui.ChiaBai());
    }

    public void ChonBaiHoTro()
    {
        StartCoroutine(CoChonBaiHoTro());
        IEnumerator CoChonBaiHoTro()
        {
            var coroutine = ui.ChonBaiHoTro();
            string[] finalResult = null;
            while (coroutine.MoveNext())
            {
                if (coroutine.Current is string[] result)
                {
                    finalResult = result;
                    break;
                }
                yield return null;
            }
            _cardSupportOnTable = new();
            foreach(var spcard in finalResult)
            {
                _cardSupportOnTable.Add(SupportCardHelper.GetSupportCard(spcard));
            }
        }
    }

    public void ChonBai(int amountCardRequire)
    {
        StartCoroutine(CoChonBai());

        IEnumerator CoChonBai()
        {
            var coroutine = ui.ChonBai(CardOnHand, amountCardRequire);
            int[] finalResult = null;

            while (coroutine.MoveNext())
            {
                if (coroutine.Current is int[] result)
                {
                    finalResult = result;
                    break;
                }
                yield return null;
            }

            ICard[] cardSelected = new ICard[amountCardRequire];
            for (int i = 0; i < amountCardRequire; i++)
            {
                cardSelected[i] = CardOnHand[i];
            }
            CardOnHand.RemoveAll(card => cardSelected.Contains(card));
            _onCardSelected.Invoke(cardSelected);
        }
    }

    public void DungBaiHoTro()
    {
        StartCoroutine(CoDungBaiHoTro());

        IEnumerator CoDungBaiHoTro()
        {
            var coroutine = ui.DungBaiHoTro();
            int[] finalResult = null;

            while (coroutine.MoveNext())
            {
                if (coroutine.Current is int[] result)
                {
                    finalResult = result;
                    break;
                }
                yield return null;
            }

            _cardSupportSelected = new ISupportCard[finalResult.Length];
            for (int i = 0; i < finalResult.Length; i++)
            {
                _cardSupportSelected[i] = _cardSupportOnTable[finalResult[i]];
            }
            _cardSupportOnTable.RemoveAll(card => _cardSupportSelected.Contains(card));
            _onSupportCardSelected.Invoke(_cardSupportSelected);
        }

    }

    public void KetThuc(MatchControl.MatchPoint.KetQua ketQua)
    {
        StartCoroutine(ui.KetThuc(ketQua));
    }

    public IMatchPlayer.CardSelected OnCardSelected { get => _onCardSelected; set => _onCardSelected = value; }
    public IMatchPlayer.SupportCardSelected OnSupportCardUsed { get => _onSupportCardUsed; set => _onSupportCardUsed = value; }
    public IMatchPlayer.SupportCardSelected OnSupportCardSelected { get => _onSupportCardSelected; set => _onSupportCardSelected = value; }
    public UserData UserData { get => _userData; private set => _userData = value; }
    public List<ICard> CardOnTable { get => _cardOnTable; private set => _cardOnTable = value; }
    public List<ICard> CardOnHand { get => _cardOnHand; private set => _cardOnHand = value; }
}
