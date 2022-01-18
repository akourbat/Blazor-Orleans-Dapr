using Orleans.Concurrency;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorServer.Models
{
    [Immutable]
    [Serializable]
    public record class TodoItem
    {
        public TodoItem(Guid key, string title, bool isDone, Guid ownerKey)
            : this(key, title, isDone, ownerKey, DateTime.UtcNow) { }

        private TodoItem(Guid key, string title, bool isDone, Guid ownerKey, DateTime timestamp) =>
            (Key, Title, IsDone, OwnerKey, Timestamp) = (key, title, isDone, ownerKey, timestamp);

        public Guid Key { get; init; }
        public string Title { get; init; }
        public bool IsDone { get; init; }
        public Guid OwnerKey { get; init; }
        public DateTime Timestamp { get; init; }
    }

    //public record class TodoItem1(Guid Key, string Title, bool IsDone, Guid OwnerKey)
    //{
    //    private TodoItem1(Guid key, string title, bool isDone, Guid ownerKey, DateTime timestamp) { }

    //    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    //}
}