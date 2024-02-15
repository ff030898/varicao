using Microsoft.EntityFrameworkCore;

namespace VariacaoApi.Models;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems { get; set; } = null!;
        
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");
}

