using FluentNHibernate.Mapping;
using TusLibros.model.entities;

namespace TusLibros.db.mappings
{
    public class CreditCardMap : ClassMap<CreditCard>
    {
        public CreditCardMap()
        {
            Id(x => x.Id);
            Map(u => u.ExpirationDate);
            Map(x => x.CardNumber);
        }
    }
}