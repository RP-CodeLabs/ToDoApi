using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Command
{
    public interface IPutTodoItemCommand
    {
        IActionResult Execute(long id, TodoItem item);
    }
}
