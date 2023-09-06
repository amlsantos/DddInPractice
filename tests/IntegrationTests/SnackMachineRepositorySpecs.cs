#region

using FluentAssertions;
using Logic.Common;
using Logic.SharedKernel;
using Logic.SnackMachines;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace IntegrationTests;

public class SnackMachineRepositorySpecs
{
    private const string ConnectionString = "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=DddInPractice";
    private readonly SnackMachineRepository _snackMachineRepository;
    private readonly SnackRepository _snackRepository;

    public SnackMachineRepositorySpecs()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(ConnectionString)
            .Options;
        var context = new ApplicationDbContext(options);
        _snackMachineRepository = new SnackMachineRepository(null, context);
        _snackRepository = new SnackRepository(null, context);
    }

    [Fact]
    public void ExistingSnackMachine_ShouldBeReturnedFromDatabase()
    {
        // arrange
        // act
        var existingSnackMachine = _snackMachineRepository.GetById(1);

        // assert
        existingSnackMachine.Should().NotBeNull();
    }

    [Fact]
    public async Task NewSnackMachine_ShowBePersisted()
    {
        // arrange
        var snackMachine = new SnackMachine();

        snackMachine.InsertMoney(Money.Dollar);
        snackMachine.InsertMoney(Money.Dollar);

        var soda = _snackRepository.GetById(Snack.Soda.Id);
        var chocolate = _snackRepository.GetById(Snack.Chocolate.Id);
        var gum = _snackRepository.GetById(Snack.Gum.Id);

        snackMachine.LoadSnacks(1, new SnackPile(soda, 50, 1m));
        snackMachine.LoadSnacks(2, new SnackPile(chocolate, 20, 2m));
        snackMachine.LoadSnacks(3, new SnackPile(gum, 30, 1.5m));

        snackMachine.BuySnack(1);

        // act
        _snackMachineRepository.Add(snackMachine);
        var result = await _snackMachineRepository.SaveChangesAsync();

        // assert
        result.Should().BeGreaterThanOrEqualTo(1);
        Clear(snackMachine);
    }

    private void Clear(SnackMachine snackMachine)
    {
        _snackMachineRepository.Remove(snackMachine);
        _snackMachineRepository.SaveChangesAsync();
    }

    [Fact]
    public void ExistingSnackMachine_ShouldHaveValidNumberSlots()
    {
        // arrange
        // act
        var existingSnackMachine = _snackMachineRepository.GetById(1);

        // assert
        existingSnackMachine?.Slots.Count.Should().Be(SnackMachine.MaxNumberSlots);
    }
}