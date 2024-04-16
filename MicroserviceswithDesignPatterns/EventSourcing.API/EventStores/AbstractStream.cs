using EventSourcing.Shared.Events;
using EventStore.Client;
using System.Text.Json;
using System.Text;
using System.Linq;

namespace EventSourcing.API.EventStores
{
    public abstract class AbstractStream
    {
        protected readonly LinkedList<IEvent> Events = new LinkedList<IEvent>();

        private string _streamName { get; }

        private readonly EventStoreClient _eventStoreClient;

        protected AbstractStream(string streamName, EventStoreClient eventStoreClient)
        {
            _streamName = streamName;
            _eventStoreClient = eventStoreClient;
        }

        public async Task SaveAsync()
        {
            var eventsToAppend = Events.ToList().Select(x => new EventData(
                Uuid.NewUuid(),
                x.GetType().Name,
                Encoding.UTF8.GetBytes(JsonSerializer.Serialize(x, inputType: x.GetType())),
                Encoding.UTF8.GetBytes(x.GetType().FullName))).ToList();

            await _eventStoreClient.AppendToStreamAsync(_streamName, StreamState.Any, eventsToAppend);

            Events.Clear();
        }
    }
    }

