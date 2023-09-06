using FluentAssertions;
using Logic.Common;
using Logic.Management;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IntegrationTests;

public class HeadOfficeRepositorySpecs
{
    private const string ConnectionString = "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=DddInPractice";
    private readonly HeadOfficeRepository _repository;

    public HeadOfficeRepositorySpecs()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(ConnectionString)
            .Options;
        var context = new ApplicationDbContext(options);
        _repository = new HeadOfficeRepository(null, context);
    }

    [Fact]
    public void ExistingHeadOffice_ShouldBeReturnedFromDatabase()
    {
        // arrange
        const int headOfficeId = 1;

        // act
        var existingHeadOffice = _repository.GetById(headOfficeId);

        // assert
        existingHeadOffice.Should().NotBeNull();
        existingHeadOffice?.Id.Should().Be(headOfficeId);
    }
}