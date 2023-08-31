using Logic.Domain;

namespace Logic.Persistence.Repositories;

public class SnackMachineRepository : Repository<SnackMachine>
{
    public SnackMachineRepository(ApplicationDbContext context) : base(context) { }

    public void Create(SnackMachine snackMachine) => Context.SnackMachines.Add(snackMachine);

    public void Delete(SnackMachine snackMachine) => Context.SnackMachines.Remove(snackMachine);
}