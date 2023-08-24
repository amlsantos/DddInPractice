using FluentAssertions;
using Logic;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Xunit;

namespace UnitTests;

public class TemporaryTests
{
    private readonly ApplicationDbContext _dbContext;
    
    public TemporaryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer("Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=DddInPractice")
            .Options;
        _dbContext = new ApplicationDbContext(options);
    }
    
    [Fact]
    public async Task ExistingSnack_ShouldBeReturnedFromDatabase()
    {
        // arrange
        // act
        var existingSnackMachine = await _dbContext.SnackMachines.FirstOrDefaultAsync();

        // assert
        existingSnackMachine.Should().NotBeNull();
    }
    
    [Fact]
    public async Task NewSnack_ShowBePersisted()
    {
        // arrange
        var snackMachine = new SnackMachine();
        snackMachine.InsertMoney(Money.Quarter);
        snackMachine.InsertMoney(Money.TenCent);
        snackMachine.InsertMoney(Money.TwentyDollar);
        
        snackMachine.BuySnack();
        
        // act
        _dbContext.SnackMachines.Add(snackMachine);
        var result = await _dbContext.SaveChangesAsync();

        // assert
        result.Should().Be(1);
    }
}