using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api_app.Model
{
    public class TodoContext : IdentityDbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Todo> Todos {get;set;}
    }
}