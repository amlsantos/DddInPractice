using System.Threading.Tasks;
using DevExpress.Mvvm;
using Logic.Atms;
using Logic.SharedKernel;
using UI.Common;

namespace UI.Atms;

public class AtmViewModel : ViewModel
{
    private readonly Atm _atm;
    private readonly IPaymentGateway _gateway;
    private readonly AtmRepository _repository;

    private string _message;

    public AtmViewModel(Atm atm, AtmRepository repository, IPaymentGateway gateway)
    {
        _atm = atm;
        _repository = repository;
        _gateway = gateway;

        TakeMoneyCommand = new AsyncCommand<decimal>(async amount => { await TakeMoney(amount); });
    }

    public override string Caption => "ATM";

    public Money MoneyInside => _atm.MoneyInside;
    public string MoneyCharged => _atm.MoneyCharged.ToString("C2");

    public string Message
    {
        get => _message;
        private set
        {
            _message = value;
            Notify();
        }
    }

    public AsyncCommand<decimal> TakeMoneyCommand { get; }

    private async Task TakeMoney(decimal amount)
    {
        var error = _atm.CanTakeMoney(amount);
        if (!string.IsNullOrEmpty(error))
        {
            NotifyClient(error);
            return;
        }

        var amountWithCommission = _atm.CalculateAmountWithCommission(amount);
        _gateway.ChargePayment(amountWithCommission);
        _atm.TakeMoney(amount);

        await _repository.SaveChangesAsync();

        NotifyClient("You have taken " + amount.ToString("C2"));
    }

    private void NotifyClient(string message)
    {
        Message = message;
        Notify(nameof(MoneyInside));
        Notify(nameof(MoneyCharged));
    }
}