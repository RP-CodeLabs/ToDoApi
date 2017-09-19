using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Command
{
    public class DeleteTodoItemCommand : IDeleteTodoItemCommand
    {
        private readonly TodoContext _context;

        public DeleteTodoItemCommand(TodoContext context) => _context = context; 

        public IActionResult Execute(long id)
        {
            var todo = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return new NotFoundResult();
            }
            _context.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
