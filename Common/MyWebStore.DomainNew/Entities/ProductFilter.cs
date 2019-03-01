using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebStore.DomainEntities.Entities
{
    /// <summary>Класс для фильтрации продуктов</summary>
    public class ProductFilter
    {
        /// <summary>Секция, которой принадлежит товар</summary>
        public int? SectionId { get; set; }

        /// <summary>Бренд</summary>
        public int? BrandId { get; set; }

        public IEnumerable<int> Ids { get; set; }
    }
}
