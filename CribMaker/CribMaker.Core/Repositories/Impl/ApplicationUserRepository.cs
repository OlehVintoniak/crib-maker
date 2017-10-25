#region

using System.Linq;
using CribMaker.Core.Data;
using CribMaker.Core.Data.Entities;
using CribMaker.Core.Repositories.Abstract;

#endregion

namespace CribMaker.Core.Repositories.Impl
{
    public class ApplicationUserRepository : GenericRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(ApplicationDbContext сontext)
            : base(сontext)
        {
        }

        public ApplicationUser GetByUserName(string userName)
        {
            return DbSet.FirstOrDefault(x => x.UserName == userName);
        }
    }
}