using System.Collections.Generic;
using System.Drawing;

namespace Final
{
    // 商品資料結構，所有 Form 都能存取
    public class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Image Image { get; set; }
    }

    // 靜態購物車，跨表單共用
    public static class CartData
    {
        public static List<Product> Items { get; } = new List<Product>();
    }
}
