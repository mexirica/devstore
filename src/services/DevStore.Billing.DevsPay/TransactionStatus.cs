namespace DevStore.Billing.DevsPay;

public enum TransactionStatus
{
    Authorized = 1,
    Paid,
    Refused,
    Chargeback,
    Cancelled
}