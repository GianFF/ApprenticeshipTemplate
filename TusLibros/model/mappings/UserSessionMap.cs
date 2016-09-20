using FluentNHibernate.Mapping;

namespace TusLibros.model.mappings
{
    public class UserSessionMap : ClassMap<UserSession>
    {
        public UserSessionMap()
        {
            Id(u => u.Id);
            References(u => u.Cart).Cascade.All();
            Map(u => u.Date);
        }
    }
}


