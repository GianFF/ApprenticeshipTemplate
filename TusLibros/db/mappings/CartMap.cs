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
            //HasMany(x => x.Items)
            //  .KeyColumn("idCart")
            //.Table("Books").Element("nameBook");

            //HasMany(u => u.Items)
              //  .Table("Items")
                //.AsMap("books")
                //.Element("preferenceType", e => e.Column("preferenceValue").Type<Int32Type>());


            HasMany(u => u.Items)
               // these are most likely by convention
               // .Table("tbl_AccessPointPosition") 
               // .KeyColumn("Transport_id")
               // ...
               .AsEntityMap("Endpoint_id")
               .Element("string_col", part => part.Type<string>())
               .Element("integer_col", part => part.Type<int>());
        }
    }
}


