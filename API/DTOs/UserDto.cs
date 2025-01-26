using System;

namespace API.DTOs;

public class UserDto
{
    public required string Username { get; set; }
    public required string Token { get; set; }
}

public class UsersDto
{
    public required int Id { get; set; }
    public required string Username { get; set; }
    public required string MobileNo { get; set; }
    public string? Email { get; set; }
}