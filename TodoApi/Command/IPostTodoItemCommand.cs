using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Command
{
    public interface IPostTodoItemCommand
    {
        IActionResult Execute(TodoItem item);
    }
}
