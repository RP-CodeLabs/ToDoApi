using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Command
{
    public class PutTodoItemCommand : IPutTodoItemCommand
    {
        private readonly TodoContext _context;

        public PutTodoItemCommand(TodoContext context)
        {
            _context = context;
        }

        public IActionResult Execute(long id, TodoItem item)
        {
            if (item == null || item.Id == id)
            {
                return new BadRequestResult();
            }
            var todo = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return new NotFoundResult();
            }
            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;
            _context.TodoItems.Update(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
