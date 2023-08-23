using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Xunit;

namespace UnitTests;

public class TemporaryTests
{
    [Fact]
    public async Task Test()
    {
        // arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer("Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=DddInPractice")
            .Options;
        var context = new ApplicationDbContext(options);
        
        // act
        var snackMachine = await context.SnackMachines.FirstOrDefaultAsync();
        
        // assert
        snackMachine.Should().NotBeNull();
    }
}