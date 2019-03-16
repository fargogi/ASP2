using MyWebStore.DomainNew.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Interfaces.Services
{
    public interface ICartStore
    {
        Cart Cart { get; set; }
    }
}
