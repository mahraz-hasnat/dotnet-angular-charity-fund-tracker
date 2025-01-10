using System;
using API.Controllers;

namespace API.DTOs;

public class UpdateDto
{
    public required string Username { get; set; }
    public required string MobileNo { get; set; }
    public string? Email { get; set; }
}
