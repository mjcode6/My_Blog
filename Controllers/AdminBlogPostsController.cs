using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;

        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // Get tags from Repositories

            var tags = await tagRepository.GetAllAsync();

            var model = new AddBlogPostRequest
            {

                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })

            };




            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {

            // mapping view model to domain model
            var blogPost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                Author = addBlogPostRequest.Author,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublichedDate = addBlogPostRequest.PublichedDate,
                Visible = addBlogPostRequest.Visible,


            };

            // Map tags from selected tags

            var SelectedTags = new List<Tag>();


            foreach(var SelectedId in addBlogPostRequest.SelectedTags)
            {
                var selectedIdAsGuid = Guid.Parse(SelectedId);
                var existingTag = await tagRepository.GetAsync(selectedIdAsGuid);


                if(existingTag != null)
                {
                    SelectedTags.Add(existingTag);
                }
            }

            // mapping tag back to domain model

            blogPost.Tags = SelectedTags;

            await blogPostRepository.AddAsync(blogPost);

            return RedirectToAction("Add");
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            // call the repository

            var BlogPosts = await blogPostRepository.GetAllAsync();

            return View(BlogPosts);
        }

        [HttpGet]


        public async Task<IActionResult> Edit(Guid id)
        {
            // retrive the result from the repository


            var blogPost = await blogPostRepository.GetAsync(id);

            var tagsdomainModel = await tagRepository.GetAllAsync();


            if(blogPost != null)
            {

                // map a domain model to view model


                var model = new EditBlogPostRequest
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
                    Tags = tagsdomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()

                    }),
                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray()
                };


                return View(model);
            }


            // pass the data view
            return View(null);
        }




        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {

            // map view model back to domain model
            var blogPostDomainModel = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                PageTitle = editBlogPostRequest.PageTitle,
                Content = editBlogPostRequest.Content,
                Author = editBlogPostRequest.Author,
                ShortDescription = editBlogPostRequest.ShortDescription,
                UrlHandle = editBlogPostRequest.UrlHandle,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                PublichedDate = editBlogPostRequest.PublichedDate,
                Visible = editBlogPostRequest.Visible,
            };


            // map tags into domain model
            var selectedTags = new List<Tag>(); 

            foreach(var SelectedTags in editBlogPostRequest.SelectedTags)
            {
                if(Guid.TryParse(SelectedTags, out var tag))
                {
                    var foundTag = await tagRepository.GetAsync(tag);

                    if(foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }



            blogPostDomainModel.Tags = selectedTags;



            // submit information to repository to update

          var UpdatedBlog =   await blogPostRepository.UpdateAsync(blogPostDomainModel);

            if(UpdatedBlog != null)
            {
                // show success notif


                return RedirectToAction("Edit");
            }

            // show error notif
            return RedirectToAction("Edit");
        }


        [HttpPost]

        public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest)
        {
            // talk to repository to delete posts and tags

           var deletedBlogPost = await blogPostRepository.DeleteAsync(editBlogPostRequest.Id);


            if(deletedBlogPost != null)
            {
                // show  success notif
                return RedirectToAction("List");
            }
            // show error
            return RedirectToAction("Edit", new { Id = editBlogPostRequest.Id});
        }

    }
}
