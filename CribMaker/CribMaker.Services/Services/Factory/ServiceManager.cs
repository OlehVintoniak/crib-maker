#region

using CribMaker.Core.Repositories.Abstract;
using CribMaker.Core.Repositories.Factory;
using CribMaker.Services.Services.Impl;

#endregion

namespace CribMaker.Services.Services.Factory
{
    public class ServiceManager : IServiceManager
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IUnitOfWork _unitOfWork;

        #region Private Services Fields
        private IApplicationUserService _applicationUserService;
        #endregion

        public ServiceManager(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
        {
            _unitOfWork = unitOfWork;
            _repositoryManager = repositoryManager;
        }

        public IApplicationUserService ApplicationUserService
            => _applicationUserService ?? (_applicationUserService = new ApplicationUserService(_unitOfWork, _repositoryManager));
    }
}