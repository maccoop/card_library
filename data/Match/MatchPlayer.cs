using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace thirdparty.card_library.data.Match
{
    public class MatchPlayer : MonoBehaviour, IMatchPlayer
    {
        private UserDataSingle _userData;
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
            //UserData = ServiceLocator.Instance.GetService<UserDataSingle>();
            CardOnTable = new List<ICard>();
            foreach (var e in UserData.cards)
            {
                CardOnTable.Add(Card.Card.GetCard($"{e.Item1},{e.Item2}"));
            }
            ui.KhoiTao();
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
            ui.XaoBai();
        }

        public void ChiaBai()
        {
            CardOnHand = CardOnTable.GetRange(0, 9);
            ui.ChiaBai();
        }

        public void ChonBaiHoTro()
        {
            ui.ChonBaiHoTro(OnEnd);

            void OnEnd(object[] selected)
            {
                _cardSupportOnTable = new();
                foreach (var spcard in selected)
                {
                    _cardSupportOnTable.Add(SupportCardHelper.GetSupportCard(spcard as string));
                }
            }
        }

        public void ChonBai(int amountCardRequire)
        {
            ui.ChonBai(amountCardRequire, OnComplete);
            void OnComplete(object[] objects)
            {
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
            ui.DungBaiHoTro(OnEnd);

            void OnEnd(object[] finalResult)
            {
                _cardSupportSelected = new ISupportCard[finalResult.Length];
                int index = 0;
                for (int i = 0; i < finalResult.Length; i++)
                {
                    index = int.Parse(finalResult[i].ToString());
                    _cardSupportSelected[i] = _cardSupportOnTable[index];
                }
                _cardSupportOnTable.RemoveAll(card => _cardSupportSelected.Contains(card));
                _onSupportCardSelected.Invoke(_cardSupportSelected);
            }

        }

        public void KetThuc(MatchControl.MatchPoint.KetQua ketQua)
        {
            ui.KetThuc(ketQua);
        }

        public IMatchPlayer.CardSelected OnCardSelected { get => _onCardSelected; set => _onCardSelected = value; }
        public IMatchPlayer.SupportCardSelected OnSupportCardUsed { get => _onSupportCardUsed; set => _onSupportCardUsed = value; }
        public IMatchPlayer.SupportCardSelected OnSupportCardSelected { get => _onSupportCardSelected; set => _onSupportCardSelected = value; }
        public UserDataSingle UserData { get => _userData; private set => _userData = value; }
        public List<ICard> CardOnTable { get => _cardOnTable; private set => _cardOnTable = value; }
        public List<ICard> CardOnHand { get => _cardOnHand; private set => _cardOnHand = value; }
    }
}
