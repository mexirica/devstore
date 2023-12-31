using System.Threading.Tasks;
using DevStore.Core.Data;

namespace DevStore.Orders.Domain.Vouchers;

public interface IVoucherRepository : IRepository<Voucher>
{
    Task<Voucher> GetVoucherByCode(string code);
    void Update(Voucher voucher);
}