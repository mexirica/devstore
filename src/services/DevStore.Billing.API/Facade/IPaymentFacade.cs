using System.Threading.Tasks;
using DevStore.Billing.API.Models;

namespace DevStore.Billing.API.Facade;

public interface IPaymentFacade
{
    Task<Transaction> AuthorizePayment(Payment payment);
    Task<Transaction> CapturePayment(Transaction transaction);
    Task<Transaction> CancelAuthorization(Transaction transaction);
}