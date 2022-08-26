
using api_app.Model;
using Microsoft.EntityFrameworkCore;

namespace api_app.Repositories
{
    public class TodoRepository : ITodosRepository
    {
        public readonly TodoContext  _context;
        public TodoRepository(TodoContext context)
        {
            _context = context;
        }
        public async Task<Todo> Create(Todo todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task Delete(int id)
        {
            var todoDelete = await _context.Todos.FindAsync(id);
            _context.Todos.Remove(todoDelete);
            await _context.SaveChangesAsync();
            
            
        }

        public async Task<IEnumerable<Todo>> Get()
        {
           return await _context.Todos.ToListAsync();
        }

        public async Task<Todo> Get(int id)
        {
             return await _context.Todos.FindAsync(id);
        }

        public async Task Update(Todo todo)
        {
            _context.Entry(todo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}