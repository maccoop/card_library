using System.Collections;
using System.Collections.Generic;

namespace thirdparty.card_library.data.Match
{
    public delegate void OnSelected(object[] selected);
    public interface IMatchPlayerUI
    {
        
        void XaoBai();
        void ChiaBai();
        void KhoiTao();
        void ChonBaiHoTro(OnSelected onSelected);
        void ChonBai(int amountCardRequire, OnSelected onSelected);
        void DungBaiHoTro(OnSelected onSelected);
        void KetThuc(MatchControl.MatchPoint.KetQua ketQua);
    }
}
