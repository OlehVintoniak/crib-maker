#region

using CribMaker.Core.Data.Entities;
using CribMaker.Core.Repositories;
using CribMaker.Core.Repositories.Abstract;
using CribMaker.Core.Repositories.Factory;
using CribMaker.Services.Services.Abstract;
using System.Collections.Generic;
using System.Linq;

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

        public List<ApplicationUser> GetAllUsersWhichIsNotPupils()
        {
            return RepositoryManager.ApplicationUsers.GetAll()
                .Where(u => u.PupilId == null).ToList();
        }

        public bool CanBePupil(string email)
        {
            var user = GetByUserName(email);
            return user != null && user.PupilId == null;
        }
    }
}