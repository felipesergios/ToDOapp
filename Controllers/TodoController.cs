using Microsoft.AspNetCore.Mvc;
using api_app.Repositories;
using api_app.Model;

namespace api_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodosRepository _todosRepository;
        public TodoController(ITodosRepository todosRepository)
        {
            _todosRepository = todosRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<Todo>> GetTodos()
        {
            return await _todosRepository.Get();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodos(int id)
        {
            return await _todosRepository.Get(id);
        }
        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo([FromBody] Todo todo)
        {
            var newTodo = await _todosRepository.Create(todo);
            return CreatedAtAction(nameof(GetTodos),new {id = newTodo.id},newTodo);
        }
        [HttpDelete]
        public async Task<ActionResult<Todo>> Delete(int id)
        {
            var todoFromDelete = await _todosRepository.Get(id);
            if(todoFromDelete != null){
                await _todosRepository.Delete(todoFromDelete.id);
                return NoContent();
            }
            return NotFound();
        }
        [HttpPut]
         public async Task<ActionResult<Todo>> PutTodo(int id,[FromBody] Todo todo)
         {
            if(id == todo.id){
                await _todosRepository.Update(todo);
                return NoContent();
            }
            return BadRequest();
         }



}

}