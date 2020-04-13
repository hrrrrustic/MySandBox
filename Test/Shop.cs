using System.ComponentModel.DataAnnotations;

namespace Test
{
    public class Shop
    {
        [Key] public int ShopId { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
    }
}