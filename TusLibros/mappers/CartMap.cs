using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using TusLibros.lib;

namespace TusLibros.mappers
{
    class CartMap : ClassMapping<Cart>
    {
        public CartMap()
        {
            Id(x => x.Id, map => map.Generator(Generators.HighLow, x => x.Params(new {max_low = 100})));
        }
    }
}
