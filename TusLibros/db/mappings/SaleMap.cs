using FluentNHibernate.Mapping;
using TusLibros.model.entities;

namespace TusLibros.db.mappings
{
    public class SaleMap : ClassMap<Sale>
    {
        public SaleMap()
        {
            Id(x => x.Id);
            //References(s => s.Client);
            //References(s => s.CreditCard);
            //HasMany<string,int>(x => x.BooksAndPrices).AsMap();

            

        }
    }
}


/*
 
 public class Customer : Entity
{        
    public IDictionary<string, Book> FavouriteBooks { get; set; }
}

public class Book : Entity
{
    public string Name { get; set; }
}
And then the map:

HasManyToMany<Book>(x => x.FavouriteBooks)
            .Table("FavouriteBooks")                
            .ParentKeyColumn("CustomerID")
            .ChildKeyColumn("BookID")
            .AsMap<string>("Nickname")                
            .Cascade.All();
Resulting xml:
     */
