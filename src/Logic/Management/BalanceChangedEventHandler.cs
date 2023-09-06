using Logic.Atms;
using Logic.Common;

namespace Logic.Management;

public class BalanceChangedEventHandler : IEventHandler<BalanceChangedEvent>
{
    private readonly HeadOfficeRepository _repository;

    public BalanceChangedEventHandler(HeadOfficeRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(BalanceChangedEvent domainEvent, CancellationToken cancellationToken)
    {
        var existingHeadOffice = _repository.GetById(domainEvent.HeadOfficeId);
        existingHeadOffice?.ChangeBalance(domainEvent.Amount);

        await _repository.SaveChangesAsync();
    }
}