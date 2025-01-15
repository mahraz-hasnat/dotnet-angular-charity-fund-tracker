using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    //[Authorize]
    [AllowAnonymous]
    public class PaymentsController(DataContext context) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Payment>>> GetPayments()
        {
            var payments = await context.Payments.ToListAsync();
            return Ok(payments);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<Payment>>> GetPayment(int id)
        {
            var payment = await context.Payments.FirstOrDefaultAsync(x => x.Id == id);
            if (payment == null) return NotFound();
            return Ok(payment);
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreatePayment(PaymentDto paymentDto)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == paymentDto.UserId);
            if (paymentDto.Amount <= 0) return BadRequest("Amount must be greater than 0");
            if (user == null) return BadRequest("Invalid User");
            //if (paymentDto.UserId != context.Users.FirstOrDefaultAsync(x => )) return BadRequest("Invalid User");
            //if (paymentDto.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) return Unauthorized();
            var payment = new Payment
            {
                UserId = paymentDto.UserId,
                Amount = paymentDto.Amount,
                IsVerified = false,
                PaymentDate = DateTime.Now
            };
            context.Payments.Add(payment);
            await context.SaveChangesAsync();
            return Ok(payment);
        }

        [HttpPut("verify/{id:int}")]
        public async Task<ActionResult> VerifyPayment(int id)
        {
            var payment = await context.Payments.FirstOrDefaultAsync(x => x.Id == id);
            if (payment == null) return NotFound();
            payment.IsVerified = true;
            payment.VerificationDate = DateTime.Now;
            await context.SaveChangesAsync();
            return Ok(payment);
        }

        [HttpPut("update/{id:int}")]
        public async Task<ActionResult> UpdatePayment(int id, PaymentDto paymentDto)
        {
            var payment = await context.Payments.FirstOrDefaultAsync(x => x.Id == id);
            if (payment == null) return NotFound();
            if (payment.UserId != paymentDto.UserId) return BadRequest("You don't have access to update this payment details");
            if (payment.IsVerified) return BadRequest("Cannot update a verified payment");
            payment.Amount = paymentDto.Amount;
            payment.UpdatedDate = DateTime.Now;
            await context.SaveChangesAsync();
            return Ok(payment);
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult> DeletePayment(int id)
        {
            var payment = await context.Payments.FirstOrDefaultAsync(x => x.Id == id);
            if (payment == null) return NotFound();
            if (payment.IsVerified) return BadRequest("Only Admin can delete a verified payment");
            context.Payments.Remove(payment);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
