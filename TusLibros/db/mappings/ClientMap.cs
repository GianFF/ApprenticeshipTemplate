using FluentNHibernate.Mapping;
using TusLibros.model.entities;

namespace TusLibros.db.mappings
{
    public class ClientMap : ClassMap<Client>
    {
        public ClientMap()
        {
            Id(x => x.Id);
            Map(x => x.UserName);
            Map(x => x.Password);
        }
    }
}