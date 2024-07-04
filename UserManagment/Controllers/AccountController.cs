using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Components.Routing;
using UserManagment.Application.Features.Users.Commands.Models;
using UserManagment.Application.Features.Users.Queries.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Http.HttpResults;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UserManagment.Controllers
{

    
   
    public class AccountController : Controller
    {
        private IMediator _mediatorInstance;
        protected IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm]LoginUserCommand command)
        {
            var result = await Mediator.Send(command);
            if (result == true)
                return RedirectToAction("Index", "User");
            else
                ModelState.AddModelError(string.Empty, "Incorrect username or password.");
            return View(command);
        }
    }
}
