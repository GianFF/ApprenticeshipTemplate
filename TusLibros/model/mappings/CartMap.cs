using FluentNHibernate.Mapping;
using TusLibros.model.Entitys;

namespace TusLibros.model.mappings
{
    public class CartMap : ClassMap<Cart>
    {
        public CartMap()
        {
            Id(x => x.Id);
            //HasMany(x => x.Items);
            HasMany(x => x.Items)
                .KeyColumn("idCart")
                .Table("Books").Element("nameBook");
        }
    }
}


