using FluentAssertions;
using Logic;
using Logic.Domain;
using Xunit;
using static Logic.Domain.Money;

namespace UnitTests;

public class SnackMachineSpecs
{
    [Fact]
    public void Return_money_empties_money_in_transaction()
    {
        // arrange
        var snackMachine = new SnackMachine();
        snackMachine.InsertMoney(Dollar);

        // act
        snackMachine.ReturnMoney();

        // assert
        snackMachine.MoneyInTransaction.Amount.Should().Be(0m);
    }

    [Fact]
    public void Insert_money_goes_to_money_in_transaction()
    {
        // arrange
        var snackMachine = new SnackMachine();

        // act
        snackMachine.InsertMoney(Cent);
        snackMachine.InsertMoney(Dollar);

        // assert
        snackMachine.MoneyInTransaction.Amount.Should().Be(1.01m);
    }

    [Fact]
    public void Cannot_insert_more_than_one_coin_or_note_at_the_time()
    {
        // arrange
        var snackMachine = new SnackMachine();
        var twoCent = Cent + Cent;

        // act
        var action = () => snackMachine.InsertMoney(twoCent);

        // assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Money_in_transaction_goes_to_money_inside_after_purchase()
    {
        // arrange
        var snackMachine = new SnackMachine();
        snackMachine.InsertMoney(Dollar);
        snackMachine.InsertMoney(Dollar);

        // act
        snackMachine.BuySnack();

        // assert
        snackMachine.MoneyInside.Amount.Should().Be(2);
        snackMachine.MoneyInTransaction.Amount.Should().Be(0);
    }
}