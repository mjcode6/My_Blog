using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repo
{
    public interface IBlogPostRepository
    {


        Task<IEnumerable<BlogPost>> GetAllAsync();
             Task<BlogPost?> GetAsync(Guid id);

        Task<BlogPost> AddAasync(BlogPost blogPost);
        Task<BlogPost?> UpdateAasync(BlogPost blogPost);
        Task<BlogPost?> DeleteAasync(Guid id);
       
    }
}
