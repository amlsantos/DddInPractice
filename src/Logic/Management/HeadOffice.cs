using Logic.Atms;
using Logic.Common;
using Logic.SharedKernel;
using Logic.SnackMachines;

namespace Logic.Management;

public class HeadOffice : AggregateRoot
{
    public decimal Balance { get; set; }
    public Money Cash { get; set; } = Money.None;

    public void ChangeBalance(decimal amount)
    {
        Balance += amount;
    }

    public void UnloadCashFromSnackMachine(SnackMachine snackMachine)
    {
        var money = snackMachine.UnloadMoney();
        Cash += money;
    }

    public void LoadCashToAtm(Atm atm)
    {
        atm.LoadMoney(Cash);
        Cash = Money.None;
    }
}