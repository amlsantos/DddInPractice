using FluentAssertions;
using Logic.Atms;
using Logic.Common;
using Microsoft.EntityFrameworkCore;
using Xunit;
using static Logic.SharedKernel.Money;

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
        const int existingId = 1;

        // act
        var existingAtm = _repository.GetById(existingId);

        // assert
        existingAtm.Should().NotBeNull();
        existingAtm?.Id.Should().Be(existingId);
    }

    [Fact]
    public void NewAtm_ShowBePersisted()
    {
        // arrange
        var atm = new Atm();
        atm.LoadMoney(FiveDollar * 50);

        atm.TakeMoney(FiveDollar.Amount);
        atm.TakeMoney(TwentyDollar.Amount);

        // act
        _repository.Add(atm);
        var savedEntities = _repository.Save();

        // assert
        atm.Id.Should().NotBe(0);
        savedEntities.Should().Be(1);

        Clear(atm);
    }

    private void Clear(Atm atm)
    {
        _repository.Remove(atm);
        _repository.Save();
    }
}