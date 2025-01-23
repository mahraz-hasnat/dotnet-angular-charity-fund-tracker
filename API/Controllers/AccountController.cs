using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
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

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == loginDto.Username.ToLower());

        if (user == null) return Unauthorized("Invalid username");

        if (String.IsNullOrEmpty(Encoding.UTF8.GetString(user.PasswordHash)) || String.IsNullOrEmpty(Encoding.UTF8.GetString(user.PasswordSalt)))
        {
            return Unauthorized("This user is not valid. Please register again.");
        }

        if (!VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt)) return Unauthorized("Invalid password");

        return new UserDto
        {
            Username = user.Username,
            Token = tokenService.CreateToken(user)
        };
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

    private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != passwordHash[i]) return false;
        }

        return true;
    }

    // [HttpPut("change-password")]
    // public async Task<ActionResult<User>> ChangePassword(ChangePasswordDto changePasswordDto)
    // {
    //     var user = await context.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == changePasswordDto.Username.ToLower());

    //     if (user == null) return NotFound("User not found");

    //     using var hmac = new HMACSHA512();

    //     user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(changePasswordDto.NewPassword));
    //     user.PasswordSalt = hmac.Key;

    //     context.Users.Update(user);
    //     await context.SaveChangesAsync();

    //     return Ok(user);
    // }

    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteUser(string username)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower());

        if (user == null) return NotFound("User not found");

        context.Users.Remove(user);
        await context.SaveChangesAsync();

        return Ok("User deleted");
    }

    // [HttpPut("forgot-password")]
    // public async Task<ActionResult<User>> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
    // {
    //     var user = await context.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == forgotPasswordDto.Username.ToLower());

    //     if (user == null) return NotFound("User not found");

    //     using var hmac = new HMACSHA512();

    //     user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(forgotPasswordDto.NewPassword));
    //     user.PasswordSalt = hmac.Key;

    //     context.Users.Update(user);
    //     await context.SaveChangesAsync();

    //     return Ok(user);
    // }

    private async Task<bool> UserExists(string username)
    {
        return await context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower());
    }
}
