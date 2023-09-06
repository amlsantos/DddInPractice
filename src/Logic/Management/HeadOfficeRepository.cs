using Logic.Common;

namespace Logic.Management;

public class HeadOfficeRepository : Repository<HeadOffice>
{
    public HeadOfficeRepository(ApplicationDbContext context) : base(context)
    {
    }
}