using Logic.Common;
using Logic.SharedKernel;

namespace Logic.Management;

public class HeadOffice : AggregateRoot
{
    public decimal Balance { get; set; }
    public Money Cash { get; set; } = Money.None;

    public void ChangeBalance(decimal amount)
    {
        Balance += amount;
    }
}