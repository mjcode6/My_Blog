using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Bloggie.Web.Controllers
{



    public class AdminTagsController : Controller
    {
        private readonly BloggieDbContext bloggieDbContext;
        public AdminTagsController(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }





        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Add(AddTagRequest addTagRequest)
        {

            var tag = new Tag
            {
                Name = addTagRequest.Name,
            DisplayName = addTagRequest.DisplayName

            };

            bloggieDbContext.Tags.Add(tag);
            bloggieDbContext.SaveChanges();

            return RedirectToAction("List");
        }


        [HttpGet]
        [ActionName("List")]
        
        public IActionResult List()
        {

            // use db context for read the tags
            var tags = bloggieDbContext.Tags.ToList();

            return View(tags);
        }



        [HttpGet]
        public IActionResult Edit(Guid id)
        {

            var tag = bloggieDbContext.Tags.FirstOrDefault(x => x.Id == id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }


            return View(null);
        }

        [HttpPost]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var existingTag = bloggieDbContext.Tags.Find(tag.Id);


            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;


                // save changes
                bloggieDbContext.SaveChanges();

                // success notification
                return RedirectToAction("Edit", new { id = editTagRequest.Id });

            }

            // failure notification

            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }


        [HttpPost]
        public IActionResult Delete(EditTagRequest editTagRequest)
        {
            var tag = bloggieDbContext.Tags.Find(editTagRequest.Id);

            if (tag != null)
            {
                bloggieDbContext.Tags.Remove(tag);
                bloggieDbContext.SaveChanges();


                // show sucess notification
                return RedirectToAction("List");
            }

            // show an error notification

            return RedirectToAction("Edit", new {id = editTagRequest.Id});
        }





    }
}