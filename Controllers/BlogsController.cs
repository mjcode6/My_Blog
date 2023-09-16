﻿using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repo;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogPostLikeRepository blogPostLikeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public BlogsController(IBlogPostRepository blogPostRepository, IBlogPostLikeRepository blogPostLikeRepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogPostLikeRepository = blogPostLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {


           var liked = false;

          var blogPost =  await blogPostRepository.GetByUrlHandleAsync(urlHandle);



            var blogDetailsViewModel = new BlogDetailsViewModel();



            if (blogPost != null)
            {
               var totalLikes =  await blogPostLikeRepository.GetTotalLikes(blogPost.Id);


                if (signInManager.IsSignedIn(User))
                {
                    // Get like for this user and this blog



                   var likesForBlog =  await blogPostLikeRepository.GetLikesForBlog(blogPost.Id);


                    var userId = userManager.GetUserId(User);

                    if(userId != null)
                    {
                        var likeFromUser = likesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));

                        liked = likeFromUser != null;
                    }
                }








                 blogDetailsViewModel = new BlogDetailsViewModel
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    ShortDescription = blogPost.ShortDescription,
                    PublichedDate = blogPost.PublichedDate,
                    Visible = blogPost.Visible,
                    Tags = blogPost.Tags,
                    TotalLikes = totalLikes,
                    Liked = liked

                };


            }

            return View(blogDetailsViewModel);
        }
    }
}
