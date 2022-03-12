using Microsoft.EntityFrameworkCore;
using MyMinimalAPI.Context;
using MyMinimalAPI.Model;

namespace MyMinimalAPI.Services
{
    internal class TodoService
    {
        private readonly TodoDB _db;

        public TodoService(TodoDB db)
        {
            _db = db;
        }

        public async Task<TodoModel> Add(TodoModel model)
        {
            await _db.Todos.AddAsync(model);
            await _db.SaveChangesAsync();
            return model;
        }

        public async Task<TodoModel?> Delete(int id)
        {
            if (await _db.Todos.FindAsync(id) is TodoModel todo)
            {
                _db.Todos.Remove(todo);
                await _db.SaveChangesAsync();
                return todo;
            }

            return default(TodoModel);
        }

        public async Task<TodoModel?> GetById(int id) => await _db.Todos.FindAsync(id);

        public async Task<List<TodoModel>> Get() => await _db.Todos.ToListAsync();
    }
}
