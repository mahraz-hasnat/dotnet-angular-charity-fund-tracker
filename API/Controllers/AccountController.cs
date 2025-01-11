using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(RegisterDto user)
    {
        if (await UserExists(user.Username)) return BadRequest("Username is taken");

        using var hmac = new HMACSHA512();

        var newUser = new User
        {
            Username = user.Username,
            MobileNo = user.MobileNo,
            Email = user.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password)),
            PasswordSalt = hmac.Key
        };

        context.Users.Add(newUser);
        await context.SaveChangesAsync();

        return Created("User created", newUser);
    }

    [HttpPut("update")]
    public async Task<ActionResult<User>> UpdateUser(UpdateDto updateDto)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == updateDto.Username.ToLower());

        if (user == null) return NotFound("User not found");

        user.MobileNo = updateDto.MobileNo;
        user.Email = updateDto.Email;

        context.Users.Update(user);
        await context.SaveChangesAsync();

        return Ok(user);
    }

    private async Task<bool> UserExists(string username)
    {
        return await context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower());
    }
}
