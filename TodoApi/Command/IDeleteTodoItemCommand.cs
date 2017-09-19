using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Command
{
    public interface IDeleteTodoItemCommand
    {
        IActionResult Execute(long id);
    }
}
