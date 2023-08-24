using Logic;
using Logic.Domain;
using Logic.Persistence;
using UI.Common;

namespace UI.ViewModels;

public class SnackMachineViewModel : ViewModel
{
    private readonly SnackMachine _snackMachine;
    private readonly ApplicationDbContext _context;

    public override string Caption => "Snack Machine";
    public string MoneyInTransaction => _snackMachine.MoneyInTransaction.ToString();
    public Money MoneyInside => (_snackMachine.MoneyInside + _snackMachine.MoneyInTransaction);

    private string _message = "";
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
    public Command BuySnackCommand { get; private set; }

    public SnackMachineViewModel(SnackMachine snackMachine, ApplicationDbContext context)
    {
        _snackMachine = snackMachine;
        _context = context;

        InsertCentCommand = new Command(() => InsertMoney(Money.Cent));
        InsertTenCentCommand = new Command(() => InsertMoney(Money.TenCent));
        InsertQuarterCommand = new Command(() => InsertMoney(Money.Quarter));
        InsertDollarCommand = new Command(() => InsertMoney(Money.Dollar));
        InsertFiveDollarCommand = new Command(() => InsertMoney(Money.FiveDollar));
        InsertTwentyDollarCommand = new Command(() => InsertMoney(Money.TwentyDollar));
        ReturnMoneyCommand = new Command(() => ReturnMoney());
        BuySnackCommand = new Command(() => BuySnack());
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

    private void BuySnack()
    {
        _snackMachine.BuySnack();
        _context.SaveChanges();

        NotifyClient("You have bought a snack");
    }

    private void NotifyClient(string message)
    {
        Message = message;
        Notify(nameof(MoneyInTransaction));
        Notify(nameof(MoneyInside));
    }
}