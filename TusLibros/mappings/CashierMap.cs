using FluentNHibernate.Mapping;
using TusLibros.model;

namespace TusLibros.mappings
{
    public class CashierMap : ClassMap<Cashier>
    {
        public CashierMap()
        {
            Id(c => c.Id);
            References(c => c.AMerchantProcessor);
            
            HasMany(x => x.SalesRecord)
                .Inverse()
                .Cascade.All();

        }
    }
}
/*     public virtual Guid Id { get; protected set; }
        public virtual IList<Cart> SalesRecord { get; set; }
        public virtual Hashtable Catalog { get; set; }  por el momento no se como persistir hash
        public virtual MerchantProcessor AMerchantProcessor { get; set; }*/


