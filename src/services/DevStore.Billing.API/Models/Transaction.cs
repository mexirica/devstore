using System;
using DevStore.Core.DomainObjects;

namespace DevStore.Billing.API.Models;

public class Transaction : Entity
{
    public string AuthorizationCode { get; set; }
    public string CreditCardCompany { get; set; }
    public DateTime? TransactionDate { get; set; }
    public decimal Amount { get; set; }
    public decimal TransactionCost { get; set; }
    public TransactionStatus TransactionStatus { get; set; }
    public string TID { get; set; } // Id
    public string NSU { get; set; } // Meio (paypal)

    public Guid PaymentId { get; set; }

    // EF Relation
    public Payment Payment { get; set; }
}