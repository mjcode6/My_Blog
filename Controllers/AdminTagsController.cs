using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Bloggie.Web.Controllers
{

    [Authorize(Roles = "Admin")]

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
            validateAddTagRequest(addTagRequest);

            if (ModelState.IsValid == false)
            {
                return View();
            }


            var tag = new Tag
            {
                Name = addTagRequest.Name,
            DisplayName = addTagRequest.DisplayName

            };

            await tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }

        private void ValidateAddTagRequest(AddTagRequest addTagRequest)
        {
            throw new NotImplementedException();
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



        private void validateAddTagRequest(AddTagRequest request)
        {
            if(request.Name is not null && request.DisplayName is not null)
            {
                if(request.Name == request.DisplayName)
                {
                    ModelState.AddModelError("DisplayName", "Name cannot be same as display Name");
                }
            }
        }

    }
}