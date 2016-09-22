using FluentNHibernate.Mapping;
using TusLibros.model.entities;

namespace TusLibros.db.mappings
{
    public class SaleMap : ClassMap<Sale>
    {
        public SaleMap()
        {
            Id(x => x.Id);
            References(s => s.Client);
            References(s => s.Client).Cascade.All();
        }
    }
}