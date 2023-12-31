namespace DevStore.Billing.API.Models;

public class CreditCard
{
    protected CreditCard()
    {
    }

    public CreditCard(string holder, string cardNumber, string expirationDate, string securityCode)
    {
        Holder = holder;
        CardNumber = cardNumber;
        ExpirationDate = expirationDate;
        SecurityCode = securityCode;
    }

    public string Holder { get; set; }
    public string CardNumber { get; set; }
    public string ExpirationDate { get; set; }
    public string SecurityCode { get; set; }
}