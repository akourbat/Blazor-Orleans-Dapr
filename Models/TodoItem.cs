using Orleans.Concurrency;
using System;

namespace BlazorServer.Models
{
    [Immutable]
    [Serializable]
    public record class TodoItem : IEquatable<TodoItem>
    {
        public TodoItem(Guid key, string title, bool isDone, Guid ownerKey)
            : this(key, title, isDone, ownerKey, DateTime.UtcNow)
        {
        }
        private TodoItem(Guid key, string title, bool isDone, Guid ownerKey, DateTime timestamp)
        {
            Key = key;
            Title = title;
            IsDone = isDone;
            OwnerKey = ownerKey;
            Timestamp = timestamp;
        }
        public Guid Key { get; init; }
        public string Title { get; init; }
        public bool IsDone { get; init; }
        public Guid OwnerKey { get; init; }
        public DateTime Timestamp { get; init; }
    }
}