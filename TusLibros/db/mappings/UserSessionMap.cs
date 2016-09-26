using FluentNHibernate.Mapping;
using TusLibros.model;

namespace TusLibros.db.mappings
{
    public class UserSessionMap : ClassMap<UserSession>
    {
        public UserSessionMap()
        {
            Id(u => u.Id);
            References(u => u.Cart).Cascade.All();
            References(u => u.Client);
            Map(u => u.Date);
        }
    }
}


