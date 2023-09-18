﻿using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository userRepository;

        public AdminUsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<IActionResult> List()
        {

            var users = await userRepository.GetAll();


            var usersViewModel = new UserViewModel();

            usersViewModel.users = new List<User>();


            foreach(var user in users)
            {
                usersViewModel.users.Add(new Models.ViewModels.User
                {
                    Id = Guid.Parse(user.Id),
                    UserName = user.UserName,
                    EmailAddress = user.Email

                });
            }
          




            return View(usersViewModel);
        }
    }
}
