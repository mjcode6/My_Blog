using Microsoft.AspNetCore.Identity;

namespace Bloggie.Web.Repo
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>>GetAll();
    }
}
