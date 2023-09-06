using FluentAssertions;
using Logic.Atms;
using Xunit;
using static Logic.SharedKernel.Money;

namespace UnitTests;

public class AtmSpecs
{
    [Fact]
    public void Take_money_exchanges_money_with_commission()
    {
        // arrange
        var atm = new Atm();
        atm.LoadMoney(Dollar);

        // act
        atm.TakeMoney(1m);

        // assert
        atm.MoneyInside.Amount.Should().Be(0m);
        atm.MoneyCharged.Should().Be(1.01m);
    }

    [Fact]
    public void Commission_is_at_least_one_cent()
    {
        // arrange
        var atm = new Atm();
        atm.LoadMoney(Cent);

        // act
        atm.TakeMoney(0.01m);

        // assert
        atm.MoneyCharged.Should().Be(0.02m);
    }

    [Fact]
    public void Commission_is_rounded_up_to_next_cent()
    {
        // arrange
        var atm = new Atm();
        atm.LoadMoney(Dollar + TenCent);

        // act
        atm.TakeMoney(1.1m);

        // assert
        atm.MoneyCharged.Should().Be(1.12m);
    }

    [Fact]
    public void Take_money_raises_event()
    {
        // arrange
        var atm = new Atm();
        atm.LoadMoney(Dollar);
        atm.TakeMoney(Dollar.Amount);

        // act
        // assert
        var balanceChangedEvent = atm.DomainEvents.First() as BalanceChangedEvent;
        balanceChangedEvent.Should().NotBeNull();
        balanceChangedEvent?.Amount.Should().Be(1.01m);
    }
}