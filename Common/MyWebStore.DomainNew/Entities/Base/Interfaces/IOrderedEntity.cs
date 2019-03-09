using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebStore.DomainNew.Entities.Base.Interfaces
{
    /// <summary>Упорядоченная сущность</summary>
    public interface IOrderedEntity
    {
        /// <summary>Порядок</summary>
        int Order { get; set; }
    }
}
