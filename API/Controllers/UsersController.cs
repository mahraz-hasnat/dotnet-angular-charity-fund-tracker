using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace API.Controllers
{
    //[Authorize]
    [AllowAnonymous]
    public class UsersController(DataContext context) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersDto>>> GetUsersAsync()
        {
            var users = await context.Users.ToListAsync();
            var usersDto = users.Select(user => new UsersDto
            {
                Id = user.Id,
                Username = user.Username,
                MobileNo = user.MobileNo,
                Email = user.Email
            });

            return Ok(usersDto);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUserAsync(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null) return NotFound();
            return user;
        }
    }
}
