using System.Collections.Generic;

namespace RealWorldMvc3.Models
{
    public class BrowseProducts
    {
        public string CategoryName { get; set; }

        public IEnumerable<ProductDetail> Products { get; set; }
    }
}