using System;
using System.Data.Common;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public required DbSet<User> Users { get; set; }
    public required DbSet<Payment> Payments { get; set; }
}
