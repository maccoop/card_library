

public interface IMatchPlayer
{
    public delegate void CardSelected(string[] cards);
    public delegate void SupportCardSelected(string[] cards);
    CardSelected OnCardSelected { get; set; }
    SupportCardSelected OnSupportCardSelected { get; set; }
    SupportCardSelected OnSupportCardUsed { get; set; }
    void Init();
    void Suffle();
    void DealCard();
    void SelectCard(int amountCardRequire);
    void SelectSupportCard();
    void UseSupportCard();
}
