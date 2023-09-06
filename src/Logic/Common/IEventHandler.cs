using MediatR;

namespace Logic.Common;

public interface IEventHandler<in T> : INotificationHandler<T> where T : IDomainEvent
{
}