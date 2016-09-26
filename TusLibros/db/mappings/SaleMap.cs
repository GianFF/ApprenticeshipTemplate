using FluentNHibernate.Mapping;
using TusLibros.model.entities;

namespace TusLibros.db.mappings
{
    public class SaleMap : ClassMap<Sale>
    {
        public SaleMap()
        {
            Id(s => s.Id);
            Map(s => s.Date);
            References(s => s.Client);
            References(s => s.CreditCard).Cascade.All();
        }
    }
}