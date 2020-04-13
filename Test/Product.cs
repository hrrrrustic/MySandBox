using System.ComponentModel.DataAnnotations;

namespace Test
{
    public class Product
    {
        [Key] public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
}