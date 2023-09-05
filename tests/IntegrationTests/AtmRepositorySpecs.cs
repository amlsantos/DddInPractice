using FluentAssertions;
using Logic.Atms;
using Logic.Common;
using Logic.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IntegrationTests;

public class AtmRepositorySpecs
{
    private const string ConnectionString = "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=DddInPractice";
    private readonly AtmRepository _repository;

    public AtmRepositorySpecs()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(ConnectionString)
            .Options;
        var context = new ApplicationDbContext(options);
        _repository = new AtmRepository(context);
    }

    [Fact]
    public void ExistingAtm_ShouldBeReturnedFromDatabase()
    {
        // arrange
        // act
        var existingAtm = _repository.GetById(1);

        // assert
        existingAtm.Should().NotBeNull();
        existingAtm?.Id.Should().Be(1);
    }

    [Fact]
    public void NewAtm_ShowBePersisted()
    {
        // arrange
        var atm = new Atm();
        atm.LoadMoney(Money.FiveDollar * 50);

        // act
        _repository.Create(atm);
        var savedEntities = _repository.Save();

        // assert
        atm.Id.Should().NotBe(0);
        savedEntities.Should().Be(1);

        Clear(atm);
    }

    private void Clear(Atm atm)
    {
        _repository.Delete(atm);
    }
}