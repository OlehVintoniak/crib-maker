using CribMaker.Core.Data;
using CribMaker.Core.Repositories.Impl;

namespace CribMaker.Core.Repositories.Factory
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _context;

        #region Private Repository fields
        private IApplicationUserRepository _applicationUserRepository;

        #endregion

        public RepositoryManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public IApplicationUserRepository ApplicationUsers
            => _applicationUserRepository ?? (_applicationUserRepository = new ApplicationUserRepository(_context));
    }
}
