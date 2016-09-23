using FluentNHibernate.Mapping;
using TusLibros.model.entities;

namespace TusLibros.db.mappings
{
    public class SaleMap : ClassMap<Sale>
    {
        public SaleMap()
        {
            Id(x => x.Id);
            References(s => s.Client).Cascade.All(); //TODO: no va el cascade All. Sacarlo cuando se registren usuarios al crear carritos.
            References(s => s.CreditCard).Cascade.All();
        }
    }
}