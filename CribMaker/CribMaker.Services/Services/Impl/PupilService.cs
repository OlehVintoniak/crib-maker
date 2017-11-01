#region

using CribMaker.Core.Data.Entities;
using CribMaker.Core.Repositories;
using CribMaker.Core.Repositories.Abstract;
using CribMaker.Core.Repositories.Factory;
using CribMaker.Services.Services.Abstract;

#endregion

namespace CribMaker.Services.Services.Impl
{
    public class PupilService : EntityService<IPupilRepository, Pupil>, IPupilService
    {
        public PupilService(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager) 
            : base(unitOfWork, repositoryManager, repositoryManager.Pupils)
        {
        }

        public void Create(Pupil pupil)
        {
            Add(pupil);
            pupil.ApplicationUser.PupilId = pupil.Id;
            UnitOfWork.Commit();
        }

        public void Remove(Pupil pupil)
        {
            var user = pupil.ApplicationUser;
            user.PupilId = null;
            Delete(pupil);
            UnitOfWork.Commit();
        }

        public Pupil GetById(int id)
        {
            return FindById(id);
        }
    }
}