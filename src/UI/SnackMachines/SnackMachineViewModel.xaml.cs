#region

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Logic.SharedKernel;
using Logic.SnackMachines;
using UI.Common;

#endregion

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

        InsertCentCommand = new Command(() => InsertMoney(Money.Cent));
        InsertTenCentCommand = new Command(() => InsertMoney(Money.TenCent));
        InsertQuarterCommand = new Command(() => InsertMoney(Money.Quarter));
        InsertDollarCommand = new Command(() => InsertMoney(Money.Dollar));
        InsertFiveDollarCommand = new Command(() => InsertMoney(Money.FiveDollar));
        InsertTwentyDollarCommand = new Command(() => InsertMoney(Money.TwentyDollar));
        ReturnMoneyCommand = new Command(ReturnMoney);
        BuySnackCommand = new Command<string>(BuySnack);
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

    public Command InsertCentCommand { get; private set; }
    public Command InsertTenCentCommand { get; private set; }
    public Command InsertQuarterCommand { get; private set; }
    public Command InsertDollarCommand { get; private set; }
    public Command InsertFiveDollarCommand { get; private set; }
    public Command InsertTwentyDollarCommand { get; private set; }
    public Command ReturnMoneyCommand { get; private set; }
    public Command<string> BuySnackCommand { get; private set; }

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

    private void BuySnack(string positionString)
    {
        var position = int.Parse(positionString);
        var error = _snackMachine.CanBuySnack(position);
        if (error != string.Empty)
        {
            NotifyClient(error);
            return;
        }

        _snackMachine.BuySnack(position);
        _repository.Save();

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