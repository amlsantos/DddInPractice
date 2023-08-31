using Logic.Domain;

namespace Logic.Persistence.Repositories;

public class SnackRepository : Repository<Snack>
{
    public SnackRepository(ApplicationDbContext context) : base(context) { }
}