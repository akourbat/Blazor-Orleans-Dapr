using System;
using System.Threading.Tasks;
using BlazorServer.Models;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;

namespace BlazorServer
{
    public class TodoGrain : Grain, ITodoGrain
    {
        private readonly ILogger<TodoGrain> _logger;
        private readonly IPersistentState<State> _state;

        private string GrainType => nameof(TodoGrain);
        private Guid GrainKey => this.GetPrimaryKey();

        public TodoGrain(ILogger<TodoGrain> logger, [PersistentState("State", "Redis")] IPersistentState<State> state)
        {
            this._logger = logger;
            this._state = state;
        }

        public Task<TodoItem> GetAsync() => Task.FromResult(_state.State.Item);

        public async Task SetAsync(TodoItem item)
        {
            // Ensure the key is consistent
            if (item.Key != GrainKey)
            {
                throw new InvalidOperationException();
            }

            // Save the item
            _state.State.Item = item;
            await _state.WriteStateAsync();

            // Register the item with its owner list
            await GrainFactory.GetGrain<ITodoManagerGrain>(item.OwnerKey)
                .RegisterAsync(item.Key);

            // For sample debugging
            _logger.LogInformation(
                "{@GrainType} {@GrainKey} now contains {@Todo}",
                GrainType, GrainKey, item);

            this.AsReference<ITodoGrain>().Notify(item).Ignore();
            // Notify listeners - best effort only
            //GetStreamProvider("SMS").GetStream<object>(item.OwnerKey, nameof(ITodoGrain))
            //    .OnNextAsync(new TodoNotification(item.Key, item))
            //    .Ignore();
        }

        public Task Notify(TodoItem item)
        {
            GetStreamProvider("SMS").GetStream<TodoEvent>(item.OwnerKey, nameof(ITodoGrain))
                .OnNextAsync(new TodoNotification(item.Key, item));
                //.Ignore();
            return Task.CompletedTask;
        }

        public async Task ClearAsync()
        {
            // Fast path for already cleared state
            if (_state.State.Item == null) return;

            // Hold on to the keys
            var itemKey = _state.State.Item.Key;
            var ownerKey = _state.State.Item.OwnerKey;

            // Unregister from the registry
            await GrainFactory.GetGrain<ITodoManagerGrain>(ownerKey)
                .UnregisterAsync(itemKey);

            // Clear the state
            await _state.ClearStateAsync();

            // For sample debugging
            _logger.LogInformation(
                "{@GrainType} {@GrainKey} is now cleared",
                GrainType, GrainKey);

            // Notify listeners - best effort only
            GetStreamProvider("SMS").GetStream<TodoEvent>(ownerKey, nameof(ITodoGrain))
                .OnNextAsync(new TodoNotification(itemKey, null))
                .Ignore();

            // No need to stay alive anymore
            DeactivateOnIdle();
        }

        public class State
        {
            public TodoItem Item { get; set; }
        }
    }
}