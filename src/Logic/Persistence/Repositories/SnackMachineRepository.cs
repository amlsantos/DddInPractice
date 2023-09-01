using Logic.Domain;
using Microsoft.EntityFrameworkCore;

namespace Logic.Persistence.Repositories;

public class SnackMachineRepository : Repository<SnackMachine>
{
    public SnackMachineRepository(ApplicationDbContext context) : base(context)
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

    public void Create(SnackMachine snackMachine) => Context.SnackMachines.Add(snackMachine);

    public void Delete(SnackMachine snackMachine) => Context.SnackMachines.Remove(snackMachine);
}