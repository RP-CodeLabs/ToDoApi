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
        public IEnumerable<TodoItem> GetAll()
        {
            return _todoContext.TodoItems.ToList();
        }

        [HttpGet("{id}", Name="GetTodo")]
        public IActionResult GetById(long id)
        {
            return _getTodoItemCommand.Value.Execute(id);
            //var todo = _todoContext.TodoItems.FirstOrDefault(x => x.Id == id);
            //if (todo == null)
            //{
            //    return NotFound();
            //}
            //return new ObjectResult(todo);
        }

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
        public IActionResult Create([FromBody] TodoItem item)
        {
            return _postTodoItemCommand.Value.Execute(item);
            //if (item == null)
            //{
            //    return BadRequest();
            //}
            //_todoContext.TodoItems.Add(item);
            //_todoContext.SaveChanges();
            //return CreatedAtRoute("GetTodo", new {id = item.Id}, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TodoItem item)
        {
            return _putTodoItemCommand.Value.Execute(id, item);
            //if (item == null || item.Id == id)
            //{
            //    return BadRequest();
            //}
            //var todo = _todoContext.TodoItems.FirstOrDefault(t => t.Id == id);
            //if (todo == null)
            //{
            //    return NotFound();
            //}
            //todo.IsComplete = item.IsComplete;
            //todo.Name = item.Name;
            //_todoContext.TodoItems.Update(todo);
            //_todoContext.SaveChanges();
            //return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return _deleteTodoItemCommand.Value.Execute(id);
            //var todo = _todoContext.TodoItems.FirstOrDefault(t => t.Id == id);
            //if (todo == null)
            //{
            //    return NotFound();
            //}
            //_todoContext.Remove(todo);
            //_todoContext.SaveChanges();
            //return new NoContentResult();
        }
    }
}
