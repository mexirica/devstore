namespace DevStore.Orders.API.Application.DTO;

public class VoucherDTO
{
    public decimal? Percentage { get; set; }
    public decimal? Discount { get; set; }
    public string Code { get; set; }
    public int DiscountType { get; set; }
}