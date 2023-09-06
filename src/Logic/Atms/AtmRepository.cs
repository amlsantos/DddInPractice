using Logic.Common;

namespace Logic.Atms;

public class AtmRepository : Repository<Atm>
{
    public AtmRepository(ApplicationDbContext context) : base(context)
    {
    }
}