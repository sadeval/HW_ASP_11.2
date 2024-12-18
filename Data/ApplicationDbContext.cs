using HW_ASP_11._2.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HW_ASP_11._2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
