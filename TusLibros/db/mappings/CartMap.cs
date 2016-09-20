using FluentNHibernate.Mapping;
using TusLibros.model.entities;

namespace TusLibros.db.mappings
{
    public class CartMap : ClassMap<Cart>
    {
        public CartMap()
        {
            Id(x => x.Id);
            HasMany(x => x.Items)
                .KeyColumn("idCart")
                .Table("Books").Element("nameBook");
        }
    }
}


