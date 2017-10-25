#region

using CribMaker.Core.Data.Entities;
using CribMaker.Core.Repositories;
using CribMaker.Core.Repositories.Abstract;
using CribMaker.Core.Repositories.Factory;
using CribMaker.Services.Services.Abstract;

#endregion

namespace CribMaker.Services.Services.Impl
{
    public class ApplicationUserService : EntityService<IApplicationUserRepository, ApplicationUser>
        , IApplicationUserService
    {
        public ApplicationUserService(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
            : base(unitOfWork, repositoryManager, repositoryManager.ApplicationUsers)
        {
        }

        public ApplicationUser GetByUserName(string userName)
        {
            return RepositoryManager.ApplicationUsers.GetByUserName(userName);
        }
    }
}