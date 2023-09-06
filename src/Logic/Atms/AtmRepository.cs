using Logic.Common;
using MediatR;

namespace Logic.Atms;

public class AtmRepository : Repository<Atm>
{
    public AtmRepository(IMediator mediator, ApplicationDbContext context) : base(mediator, context)
    {
    }
}