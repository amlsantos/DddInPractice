#region

using Logic.Common;

#endregion

namespace Logic.SnackMachines;

public class SnackRepository : Repository<Snack>
{
    public SnackRepository(ApplicationDbContext context) : base(context)
    {
    }
}