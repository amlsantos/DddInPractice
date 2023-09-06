using FluentAssertions;
using Logic.Atms;
using Logic.Common;
using Logic.Management;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using static Logic.SharedKernel.Money;

namespace IntegrationTests;

public class AtmRepositorySpecs
{
    private const string ConnectionString = "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=DddInPractice";
    private readonly ApplicationDbContext _dbContext;
    private readonly AtmRepository _repository;

    public AtmRepositorySpecs()
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        var provider = serviceCollection.BuildServiceProvider();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(ConnectionString)
            .Options;

        _dbContext = new ApplicationDbContext(options, null);
        _repository = new AtmRepository(_dbContext);
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<HeadOfficeRepository>();
        services.AddScoped<PublishDomainEventsInterceptor>();
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(ConnectionString));

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
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
    public async Task NewAtm_ShowBePersisted()
    {
        // arrange
        var atm = new Atm();
        atm.LoadMoney(Dollar);

        // act
        _repository.Add(atm);
        var savedEntities = await _repository.SaveChangesAsync();

        atm.TakeMoney(Dollar.Amount);
        await _repository.SaveChangesAsync();

        // assert
        atm.Id.Should().NotBe(0);
        savedEntities.Should().Be(1);

        await Clear(atm);
    }

    private async Task Clear(Atm atm)
    {
        _repository.Remove(atm);
        await _repository.SaveChangesAsync();
    }
}