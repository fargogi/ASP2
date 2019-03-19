using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebStore.DomainNew.ViewModels.Product
{
    public class BrandCompleteViewModel
    {
        public IEnumerable<BrandViewModel> Brands { get; set; }

        public int? CurrentBrandId { get; set; }


    }
}
