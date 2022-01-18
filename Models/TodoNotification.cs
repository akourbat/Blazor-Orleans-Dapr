using System;

namespace BlazorServer.Models
{
    public record class TodoNotification(Guid ItemKey, TodoItem Item);
}