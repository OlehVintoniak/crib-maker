using System;
using System.Threading.Tasks;

namespace CribMaker.Core.Repositories.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
        Task<int> CommitAsync();
    }
}