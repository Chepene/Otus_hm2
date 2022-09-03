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
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
           return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            return await Mediator.Send(new Details.Query{Id = id}); 
        }
    }
}