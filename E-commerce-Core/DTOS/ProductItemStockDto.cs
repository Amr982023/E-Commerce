using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.DTOS
{
    public class ProductItemStockDto
    {
        public int ProductId { get; set; }
        public int TotalStock { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }   

    }
}
