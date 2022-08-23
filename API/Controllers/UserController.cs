using Core;
using Core.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UsersController : BasicApiController
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
           return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<User> GetUser(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}