using Orleans.Concurrency;
using System;

namespace BlazorServer.Models
{
    [Immutable]
    [Serializable]
    public abstract record class TodoEvent;

    [Immutable]
    [Serializable]
    public record class TodoNotification(Guid ItemKey, TodoItem Item) : TodoEvent;
}