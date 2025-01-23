

namespace thirdparty.card_library.data.Match
{
    public interface IMatchPlayer
    {
        public delegate void CardSelected(ICard[] cards);
        public delegate void SupportCardSelected(ISupportCard[] cards);
        CardSelected OnCardSelected { get; set; }
        SupportCardSelected OnSupportCardUsed { get; set; }
        SupportCardSelected OnSupportCardSelected { get; set; }
        void KhoiTao();
        void XaoBai();
        void ChiaBai();
        void DungBaiHoTro();
        void ChonBaiHoTro();
        void ChonBai(int amountCardRequire);
        void KetThuc(MatchControl.MatchPoint.KetQua ketQua);
    }
}
