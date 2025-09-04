using ECommerce.Core.Enums;

namespace ECommerce.Core.DTO;

public class ProductDto
{
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public Categories Categories { get; set; }
    public decimal UnitPrice { get; set; }
    public int Stock { get; set; }
}