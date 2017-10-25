using CribMaker.Core.Data.Entities;
using CribMaker.Core.Repositories.Abstract;

namespace CribMaker.Core.Repositories
{
    public interface IApplicationUserRepository : IGenericRepository<ApplicationUser>
    {
        ApplicationUser GetByUserName(string userName);
    }
}