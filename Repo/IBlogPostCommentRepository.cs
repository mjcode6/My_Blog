using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repo
{
    public interface IBlogPostCommentRepository
    {

        Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);

        Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogPostId);
    }
}
