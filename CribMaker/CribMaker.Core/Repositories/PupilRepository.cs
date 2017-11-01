#region

using CribMaker.Core.Data;
using CribMaker.Core.Data.Entities;
using CribMaker.Core.Repositories.Abstract;

#endregion

namespace CribMaker.Core.Repositories
{
    public class PupilRepository : GenericRepository<Pupil>, IPupilRepository
    {
        public PupilRepository(ApplicationDbContext сontext) : base(сontext)
        {

        }
    }
}