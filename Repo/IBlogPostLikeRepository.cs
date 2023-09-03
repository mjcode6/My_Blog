using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repo
{
    public interface IBlogPostLikeRepository
    {
        Task<int>GetTotalLikes(Guid blogPostId);

        Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);
    }
}
