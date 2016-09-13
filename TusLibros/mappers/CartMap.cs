using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Bytecode.CodeDom;
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
