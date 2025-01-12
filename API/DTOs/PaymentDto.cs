using System;

namespace API.DTOs;

public class PaymentDto
{
    public required int UserId { get; set; }
    public required int Amount { get; set; }
}
