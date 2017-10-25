using CribMaker.Core.Data.Entities;

namespace CribMaker.Services.Services
{
    public interface IApplicationUserService
    {
        ApplicationUser GetByUserName(string userName);
    }
}