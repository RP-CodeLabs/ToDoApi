using System;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.Command;

namespace TodoApi.Extensions
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddCommands(this IServiceCollection services) =>
            services.AddScoped<IGetTodoItemCommand, GetTodoItemCommand>()
                .AddScoped(x => new Lazy<IGetTodoItemCommand>(x.GetRequiredService<IGetTodoItemCommand>))
                .AddScoped<IPostTodoItemCommand, PostTodoItemCommand>()
                .AddScoped(x => new Lazy<IPostTodoItemCommand>(x.GetRequiredService<IPostTodoItemCommand>()))
                .AddScoped<IPutTodoItemCommand, PutTodoItemCommand>()
                .AddScoped(x => new Lazy<IPutTodoItemCommand>(x.GetRequiredService<IPutTodoItemCommand>()))
                .AddScoped<IDeleteTodoItemCommand, DeleteTodoItemCommand>()
                .AddScoped(x => new Lazy<IDeleteTodoItemCommand>(x.GetRequiredService<IDeleteTodoItemCommand>));
    }
}
