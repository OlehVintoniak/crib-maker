namespace CribMaker.Core.Repositories.Factory
{
    public interface IRepositoryManager
    {
        IApplicationUserRepository ApplicationUsers { get; }

        IPupilRepository Pupils { get; }
    }
}