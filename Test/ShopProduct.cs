using System.ComponentModel.DataAnnotations.Schema;

namespace Test
{
    public class ShopProduct
    {
        [ForeignKey("ShopId")]
        public Shop Shop { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int ProductId { get; set; }
        public int ShopId { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}