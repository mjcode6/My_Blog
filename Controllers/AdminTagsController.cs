using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Bloggie.Web.Controllers
{



    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }





        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }



        [HttpPost]
        public async  Task<IActionResult> Add(AddTagRequest addTagRequest)
        {

            var tag = new Tag
            {
                Name = addTagRequest.Name,
            DisplayName = addTagRequest.DisplayName

            };

            await tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }


        [HttpGet]
        [ActionName("List")]
        
        public async Task<IActionResult> List()
        {

            // use db context for read the tags
            var tags = await tagRepository.GetAllAsync();

            return View(tags);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

            var tag = await tagRepository.GetAsync(id);

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
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

           var UpdatedTag = await tagRepository.UpdateAsync(tag);

            if(UpdatedTag != null)
            {
                // show success notification
            }else
            {
                // failure notification

            }


            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var DeletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

            if(DeletedTag != null)
            {
                // show an success notification
                return RedirectToAction("List");
            }

            // show an error notification

            return RedirectToAction("Edit", new {id = editTagRequest.Id});
        }





    }
}