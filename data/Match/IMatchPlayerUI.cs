using System.Collections;
using System.Collections.Generic;

namespace thirdparty.card_library.data.Match
{
    public interface IMatchPlayerUI
    {
        IEnumerator XaoBai();
        IEnumerator ChiaBai();
        IEnumerator KhoiTao();
        IEnumerator<string[]> ChonBaiHoTro();
        IEnumerator<int[]> ChonBai(List<ICard> cardOnHand, int amountCardRequire);
        IEnumerator<int[]> DungBaiHoTro();
        IEnumerator KetThuc(MatchControl.MatchPoint.KetQua ketQua);
    }
}
