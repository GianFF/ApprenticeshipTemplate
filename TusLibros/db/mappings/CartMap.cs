using FluentNHibernate.Mapping;
using NHibernate.Type;
using TusLibros.model.entities;

namespace TusLibros.db.mappings
{
    public class CartMap : ClassMap<Cart>
    {
        public CartMap()
        {
            Id(x => x.Id);
            HasMany(x => x.Items)
                .AsMap<int>("keyColumn")
                .Element("valueColumn", c => c.Type<string>());
        }
    }
}


