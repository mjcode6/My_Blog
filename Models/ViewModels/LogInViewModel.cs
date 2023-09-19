using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Bloggie.Web.Models.ViewModels
{
    public class LogInViewModel
    {
        [Required]
        public string Username{ get; set; }
        [Required]
        [MinLength(6,ErrorMessage = "Password has to be minimum 6 charactres")]
        public string Password { get; set; }
        [Required]
        public string? ReturnUrl { get; set; }


    }
}
