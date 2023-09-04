#region

using FluentAssertions;
using Logic.SnackMachines;
using Xunit;
using static Logic.SharedKernel.Money;

#endregion

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
        snackMachine.MoneyInTransaction.Should().Be(0m);
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
        snackMachine.MoneyInTransaction.Should().Be(1.01m);
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
    public void BuySnack_trades_inserted_money_for_Snack()
    {
        // arrange
        var snackMachine = new SnackMachine();
        var chocolate = Snack.Chocolate;

        const int initialQuantity = 10;
        const decimal price = 1m;
        var snackPile = new SnackPile(chocolate, initialQuantity, price);

        const int position = 1;

        snackMachine.LoadSnacks(position, snackPile);
        snackMachine.InsertMoney(Dollar);

        // act
        snackMachine.BuySnack(position);

        // assert
        snackMachine.MoneyInside.Amount.Should().Be(Dollar.Amount);
        snackMachine.MoneyInTransaction.Should().Be(0);
        snackMachine.GetSnackPile(position).Quantity.Should().Be(initialQuantity - position);
    }

    [Fact]
    public void Cannot_make_purchase_when_there_is_no_snacks()
    {
        // arrange
        var snackMachine = new SnackMachine();

        // act
        var action = () => snackMachine.BuySnack(1);

        // assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Cannot_make_purchase_if_not_enough_money_inserted()
    {
        // arrange
        var snackMachine = new SnackMachine();
        snackMachine.LoadSnacks(1, new SnackPile(Snack.Chocolate, 1, 2m));
        snackMachine.InsertMoney(Dollar);

        // act
        var action = () => snackMachine.BuySnack(1);

        // assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void SnackMachine_returns_money_with_highest_denomination_first()
    {
        // arrange
        var snackMachine = new SnackMachine();
        snackMachine.LoadMoney(Dollar);

        snackMachine.InsertMoney(Quarter);
        snackMachine.InsertMoney(Quarter);
        snackMachine.InsertMoney(Quarter);
        snackMachine.InsertMoney(Quarter);

        // act
        snackMachine.ReturnMoney();

        // assert
        snackMachine.MoneyInside.QuarterCentCount.Should().Be(4);
        snackMachine.MoneyInside.OneDollarCount.Should().Be(0);
    }

    [Fact]
    public void After_purchase_change_is_returned()
    {
        // arrange
        var snackMachine = new SnackMachine();

        snackMachine.LoadSnacks(1, new SnackPile(Snack.Chocolate, 1, 0.5m));
        snackMachine.LoadMoney(TenCent * 10);
        snackMachine.InsertMoney(Dollar);

        // act
        snackMachine.BuySnack(1);

        // assert
        snackMachine.MoneyInside.Amount.Should().Be(1.5m);
        snackMachine.MoneyInTransaction.Should().Be(0m);
    }

    [Fact]
    public void Cannot_buy_snack_if_not_enough_change()
    {
        // arrange
        var snackMachine = new SnackMachine();
        snackMachine.LoadSnacks(1, new SnackPile(Snack.Chocolate, 1, 0.5m));
        snackMachine.InsertMoney(Dollar);

        // act
        var action = () => snackMachine.BuySnack(1);

        // assert
        action.Should().Throw<InvalidOperationException>();
    }
}