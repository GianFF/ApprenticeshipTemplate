using FluentNHibernate.Mapping;
using TusLibros.model.Entitys;

namespace TusLibros.model.mappings
{
    public class MerchantProcessorMap : ClassMap<MerchantProcessor>
    {
        public MerchantProcessorMap()
        {
            Id(m => m.Id);
            
        }
    }
}
/*            public virtual Guid Id { get; protected set; }
        public virtual Hashtable SuccessfulOperations { get; protected set; }}  no se persistir hash aun
        */


