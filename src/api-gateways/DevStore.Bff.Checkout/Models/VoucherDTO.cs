namespace DevStore.Bff.Checkout.Models;

public class VoucherDTO
{
    public decimal? Percentage { get; set; }
    public decimal? Discount { get; set; }
    public string Code { get; set; }
    public int DiscountType { get; set; }
}