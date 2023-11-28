using Logic.Common;
using Microsoft.EntityFrameworkCore;

namespace Logic.Atms;

public class AtmRepository : Repository<Atm>
{
    public AtmRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IReadOnlyList<AtmDto> GetAtmList()
    {
        return Context.Atms.AsNoTracking()
            .Select(a =>
                new AtmDto(a.Id, a.MoneyInside.Amount))
            .ToList();
    }
}