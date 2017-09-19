using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Command
{
    public class PostTodoItemCommand : IPostTodoItemCommand
    {
        private readonly TodoContext _context;

        public PostTodoItemCommand(TodoContext context) => _context = context; 

        public IActionResult Execute(TodoItem item)
        {
            if (item == null)
            {
                return new NotFoundResult();
            }
            _context.TodoItems.Add(item);
            _context.SaveChanges();
            return new CreatedAtRouteResult("GetTodo", new { id= item.Id }, item);
        }
    }
}
