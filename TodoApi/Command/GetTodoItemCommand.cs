using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Command
{
    public class GetTodoItemCommand : IGetTodoItemCommand
    {
        private readonly TodoContext _context;

        public GetTodoItemCommand(TodoContext context)
        {
            _context = context;
        }

        public IActionResult Execute(long id)
        {
            var todo = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return new NotFoundResult();
            }
            return new ObjectResult(todo);
        }
    }
}
