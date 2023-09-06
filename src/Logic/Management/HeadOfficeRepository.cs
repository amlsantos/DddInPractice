using Logic.Common;
using MediatR;

namespace Logic.Management;

public class HeadOfficeRepository : Repository<HeadOffice>
{
    public HeadOfficeRepository(IMediator mediator, ApplicationDbContext context) : base(context)
    {
    }
}