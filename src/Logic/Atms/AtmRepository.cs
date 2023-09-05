using Logic.Common;

namespace Logic.Atms;

public class AtmRepository : Repository<Atm>
{
    public AtmRepository(ApplicationDbContext context) : base(context)
    {
    }

    public void Create(Atm atm)
    {
        Context.Atms.Add(atm);
    }

    public void Delete(Atm atm)
    {
        Context.Atms.Remove(atm);
    }
}