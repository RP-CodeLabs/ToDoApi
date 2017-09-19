using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Command
{
    public interface IGetTodoItemCommand
    {
        IActionResult Execute(long id);
    }
}