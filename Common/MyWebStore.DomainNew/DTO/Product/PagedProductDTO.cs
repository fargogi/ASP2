using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebStore.DomainNew.DTO.Product
{
    public class PagedProductDTO
    {
        public IEnumerable<ProductDTO> Products { get; set; }

        public int TotalCount { get; set; }



    }
}
