using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repo
{


    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }



        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
           await bloggieDbContext.AddAsync(blogPost);
            await bloggieDbContext.SaveChangesAsync();
            return blogPost;
        }

       

        public Task<BlogPost?> DeleteAasync()
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> DeleteAasync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> GetAasync()
        {
            throw new NotImplementedException();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await bloggieDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await bloggieDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public Task<BlogPost?> UpdateAasync()
        {
            throw new NotImplementedException();
        }

        public async Task<BlogPost?> UpdateAasync(BlogPost blogPost)
        {
            var existingBlog = await bloggieDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
        }

        public Task<BlogPost?> GetAasync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost> AddAasync(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }

      
    }
}
