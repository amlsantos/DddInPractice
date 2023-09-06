#region

using Logic.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Logic.SnackMachines;

public class SnackMachineRepository : Repository<SnackMachine>
{
    public SnackMachineRepository(IMediator mediator, ApplicationDbContext context) : base(mediator, context)
    {
    }

    public override SnackMachine? GetById(long id)
    {
        var snacks = Context.SnackMachines
            .Include(x => x.Slots)
            .ThenInclude(x => x.SnackPile)
            .ThenInclude(x => x.Snack)
            .ToList();

        return snacks.Single(x => x.Id == id);
    }
}