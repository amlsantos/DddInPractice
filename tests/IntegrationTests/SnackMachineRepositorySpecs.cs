using FluentAssertions;
using Logic.Domain;
using Logic.Persistence;
using Logic.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IntegrationTests;

public class SnackMachineRepositorySpecs
{
    private const string ConnectionString = "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=DddInPractice";
    private readonly SnackMachineRepository _repository;

    public SnackMachineRepositorySpecs()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(ConnectionString)
            .Options;
        var context = new ApplicationDbContext(options);
        _repository = new SnackMachineRepository(context);
    }

    [Fact]
    public void ExistingSnackMachine_ShouldBeReturnedFromDatabase()
    {
        // arrange
        // act
        var existingSnackMachine = _repository.GetById(1);

        // assert
        existingSnackMachine.Should().NotBeNull();
    }

    [Fact]
    public void NewSnackMachine_ShowBePersisted()
    {
        // arrange
        var snackMachine = new SnackMachine();
        snackMachine.InsertMoney(Money.Dollar);
        snackMachine.InsertMoney(Money.Dollar);
        snackMachine.LoadSnacks(position: 1, new SnackPile(Snack.Chocolate, 1, 2m));
        
        snackMachine.BuySnack(slotPosition: 1);

        // act
        _repository.Create(snackMachine);
        var result = _repository.Save();

        // assert
        result.Should().Be(1);
        Clear(snackMachine);
    }

    private void Clear(SnackMachine snackMachine)
    {
        _repository.Delete(snackMachine);
        _repository.Save();
    }
}