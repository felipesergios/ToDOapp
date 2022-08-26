using api_app.Model;

namespace api_app.Repositories
{
    public interface ITodosRepository
    {
        Task<IEnumerable<Todo>> Get();
        Task<Todo> Get(int Id);
        Task<Todo> Create(Todo todo);
        Task Update(Todo todo);
        Task Delete(int Id);

    }
}

