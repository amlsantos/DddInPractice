using FluentAssertions;
using Logic.Domain;
using Logic.Persistence;
using Logic.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IntegrationTests;

public class SnackRepositorySpecs
{
    private const string ConnectionString = "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=DddInPractice";
    private readonly SnackRepository _repository;

    public SnackRepositorySpecs()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(ConnectionString)
            .Options;
        var context = new ApplicationDbContext(options);
        _repository = new SnackRepository(context);
    }

    public static TheoryData<Snack> SnacksTypes => new() { Snack.Chocolate, Snack.Gum, Snack.Soda };
    
    [Theory]
    [MemberData(nameof(SnacksTypes))]
    public void Reference_data_is_correct(Snack snack)
    {
        // arrange
        var id = snack.Id;
        
        // act
        var existingSnack = _repository.GetById(id);
        
        // assert
        existingSnack?.Should().NotBeNull();
        existingSnack?.Id.Should().Be(snack.Id);
        existingSnack?.Name.Should().Be(snack.Name);
    }
}