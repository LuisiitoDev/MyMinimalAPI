using Microsoft.EntityFrameworkCore;
using MyMinimalAPI.Model;

namespace MyMinimalAPI.Context
{
    internal class TodoDB : DbContext
    {
        public DbSet<TodoModel> Todos => Set<TodoModel>();
        public TodoDB(DbContextOptions<TodoDB> options) : base(options) { }

    }
}
