using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using Logic.SharedKernel;
using Logic.SnackMachines;
using UI.Common;

namespace UI.SnackMachines;

public class SnackMachineViewModel : ViewModel
{
    private readonly SnackMachineRepository _repository;
    private readonly SnackMachine _snackMachine;

    private string _message = "";

    public SnackMachineViewModel(SnackMachine snackMachine, SnackMachineRepository repository)
    {
        _snackMachine = snackMachine;
        _repository = repository;

        InsertCentCommand = new DelegateCommand(() => InsertMoney(Money.Cent));
        InsertTenCentCommand = new DelegateCommand(() => InsertMoney(Money.TenCent));
        InsertQuarterCommand = new DelegateCommand(() => InsertMoney(Money.Quarter));
        InsertDollarCommand = new DelegateCommand(() => InsertMoney(Money.Dollar));
        InsertFiveDollarCommand = new DelegateCommand(() => InsertMoney(Money.FiveDollar));
        InsertTwentyDollarCommand = new DelegateCommand(() => InsertMoney(Money.TwentyDollar));
        ReturnMoneyCommand = new DelegateCommand(ReturnMoney);
        BuySnackCommand = new AsyncCommand<string>(BuySnack);
    }

    public override string Caption => "Snack Machine";
    public string MoneyInTransaction => _snackMachine.MoneyInTransaction.ToString(CultureInfo.InvariantCulture);
    public Money MoneyInside => _snackMachine.MoneyInside;

    public IReadOnlyList<SnackPileViewModel> Piles => GetPiles();

    public string Message
    {
        get => _message;
        private set
        {
            _message = value;
            Notify();
        }
    }

    public DelegateCommand InsertCentCommand { get; private set; }
    public DelegateCommand InsertTenCentCommand { get; private set; }
    public DelegateCommand InsertQuarterCommand { get; private set; }
    public DelegateCommand InsertDollarCommand { get; private set; }
    public DelegateCommand InsertFiveDollarCommand { get; private set; }
    public DelegateCommand InsertTwentyDollarCommand { get; private set; }
    public DelegateCommand ReturnMoneyCommand { get; private set; }
    public AsyncCommand<string> BuySnackCommand { get; private set; }

    private List<SnackPileViewModel> GetPiles()
    {
        return _snackMachine.GetAllSnackPiles()
            .Select(x => new SnackPileViewModel(x))
            .ToList();
    }

    private void InsertMoney(Money coinOrNote)
    {
        _snackMachine.InsertMoney(coinOrNote);
        NotifyClient("You have inserted: " + coinOrNote);
    }

    private void ReturnMoney()
    {
        _snackMachine.ReturnMoney();
        NotifyClient("Money was returned");
    }

    private async Task BuySnack(string positionString)
    {
        var position = int.Parse(positionString);
        var error = _snackMachine.CanBuySnack(position);
        if (error != string.Empty)
        {
            NotifyClient(error);
            return;
        }

        _snackMachine.BuySnack(position);
        await _repository.SaveChangesAsync();

        NotifyClient("You have bought a snack");
    }

    private void NotifyClient(string message)
    {
        Message = message;
        Notify(nameof(MoneyInTransaction));
        Notify(nameof(MoneyInside));
        Notify(nameof(Piles));
    }
}