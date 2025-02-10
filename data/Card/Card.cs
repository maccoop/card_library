using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace thirdparty.card_library.data.Card
{
    [Serializable]
    public class Card: ICard
    {
        public const string H = "H";
        public const string D = "D";
        public const string C = "C";
        public const string S = "S";

        /// <summary>
        /// key có dạng '[giá trị][loại]'
        /// ex: 4c, 3c, 5d
        /// giá trị: 2->14
        /// loại:   Hearts - H
        ///         Diamonds – D
        ///         Clubs – C
        ///         Spades – S
        /// </summary>
        private string key;
        public int Value { get; private set; }
        public string Type { get; private set; }

        private Card()
        {

        }

        private void Init()
        {
            Value = int.Parse(Regex.Match(key, @"^\d+").Value);
            if (Value > 14)
                Value = 14;
            Type = Regex.Match(key, @"\D").Value.ToUpper();
        }
        public MatchPoint GetAddition()
        {
            if (Value > 10)
                return MatchPoint.Create(10, 0);
            return MatchPoint.Create(Value, 0);
        }

        public static string GetCardType(int value)
        {
            if (value is 0) return nameof(H);
            if (value is 1) return nameof(D);
            if (value is 2) return nameof(C);
            if (value is 3) return nameof(S);
            return null;
        }
        public static string GetCardName(int value)
        {
            if (value is 0) return H;
            if (value is 1) return D;
            if (value is 2) return C;
            if (value is 3) return S;
            return null;
        }
        public static Card GetCard(string card)
        {
            var result = new Card() { key = card };
            result.Init();
            return result;
        }
        public Sprite GetIcon(string skin = "def")
        {
            key = $"card/{skin}/{Value}{Type}";
            return ServiceLocator.Instance.GetService<SpriteManagerService>().GetSprite(key);
        }
    }
}

