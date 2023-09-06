#region

using Logic.Common;
using MediatR;

#endregion

namespace Logic.SnackMachines;

public class SnackRepository : Repository<Snack>
{
    public SnackRepository(IMediator mediator, ApplicationDbContext context) : base(mediator, context)
    {
    }
}