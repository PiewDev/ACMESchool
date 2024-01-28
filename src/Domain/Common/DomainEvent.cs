namespace Domain.Common;

public record DomainEvent(Guid Id) : INotification;