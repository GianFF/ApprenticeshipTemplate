using FluentNHibernate.Mapping;
using TusLibros.model.entities;

namespace TusLibros.db.mappings
{
    public class SaleDetailMap: ClassMap<SaleDetail>
    {
        public SaleDetailMap()
        {
            Id(x => x.Id);
            Map(x => x.Book);
            Map(x => x.Price);
            Map(x => x.Quantity);
        }
    }
}