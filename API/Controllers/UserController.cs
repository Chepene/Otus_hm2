using Application.Users;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistnce;

namespace API.Controllers
{
    public class UsersController : BasicApiController
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
           
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
           return await _mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id) => Ok();
    }
}