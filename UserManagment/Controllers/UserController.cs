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
    public class UserController : Controller
    {
        private IMediator _mediatorInstance;
        protected IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
        [HttpGet("/User/Index")]
        public async Task<IActionResult> Index()
        {
            var response = await Mediator.Send(new GetUserListQuery());
            return View(response);
        }
        [HttpGet("/User/Create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost("/User/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] AddUserCommand command)
        {
            var result= await Mediator.Send(command);
            if (result == "Created Successfully")
                return RedirectToAction("Index");
            else
                return View(command);
                

        }
        [HttpGet("/User/Edit/{id:int}")]
        public async Task<IActionResult> Edit([FromRoute]int id)
        {
            var user = await Mediator.Send(new GetUserByIdQuery(id));
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost("/User/Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] EditUserCommand command)
        {
            var result = await Mediator.Send(command);
            if (result == "Edited Successfully")
                return RedirectToAction("Index");
            else
                return View(command);
        }
        
        [HttpGet("/User/Delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
           var result= await Mediator.Send(new DeleteUserCommand(id));
            if (result == "Deleted Successfully")
                return RedirectToAction("Index");
            else
                return BadRequest();
        }
    }
}
