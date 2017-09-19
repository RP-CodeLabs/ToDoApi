using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Command;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly TodoContext _todoContext;

        private readonly Lazy<IGetTodoItemCommand> _getTodoItemCommand;
        private readonly Lazy<IPostTodoItemCommand> _postTodoItemCommand;
        private readonly Lazy<IPutTodoItemCommand> _putTodoItemCommand;
        private readonly Lazy<IDeleteTodoItemCommand> _deleteTodoItemCommand;

        public TodoController(TodoContext context, Lazy<IGetTodoItemCommand> getTodoItemCommand, Lazy<IPostTodoItemCommand> postTodoItemCommand, Lazy<IPutTodoItemCommand> putTodoItemCommand, Lazy<IDeleteTodoItemCommand> deleteTodoItemCommand)
        {
            _todoContext = context;
            _getTodoItemCommand = getTodoItemCommand;
            _postTodoItemCommand = postTodoItemCommand;
            _putTodoItemCommand = putTodoItemCommand;
            _deleteTodoItemCommand = deleteTodoItemCommand;
            if (_todoContext.TodoItems.Any()) return;
            _todoContext.TodoItems.Add(new TodoItem() {Name = "Item1"});
            _todoContext.SaveChanges();
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll() => _todoContext.TodoItems.ToList(); 

        [HttpGet("{id}", Name="GetTodo")]
        public IActionResult GetById(long id) => _getTodoItemCommand.Value.Execute(id);

        [HttpGet("{id1}/{id2}")]
        public IActionResult GetByIds(long id1, long id2)
        {
            var todos = new List<TodoItem>
            {
                _todoContext.TodoItems.FirstOrDefault(t => t.Id == id1),
                _todoContext.TodoItems.FirstOrDefault(t => t.Id == id2)
            };
            return new ObjectResult(todos);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item) => _postTodoItemCommand.Value.Execute(item);

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TodoItem item) => _putTodoItemCommand.Value.Execute(id, item);

        [HttpDelete("{id}")]
        public IActionResult Delete(long id) => _deleteTodoItemCommand.Value.Execute(id);
    }
}
