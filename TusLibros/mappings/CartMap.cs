using FluentNHibernate.Mapping;
using TusLibros.model;

namespace TusLibros.mappings
{
    public class CartMap : ClassMap<Cart>
    {
        public CartMap()
        {
            Id(x => x.Id);
            //HasMany(x => x.Items);
            HasMany(x => x.Items)
                .KeyColumn("PostID")
                .Table("Books").Element("nameBook");
        }
    }
}


