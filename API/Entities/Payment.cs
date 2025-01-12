using System;

namespace API.Entities;

public class Payment
{
    public int Id { get; set; }
    public required int UserId { get; set; }
    public required int Amount { get; set; }
    public required bool IsVerified { get; set; }
    public required DateTime PaymentDate { get; set; }
    public DateTime? VerificationDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
