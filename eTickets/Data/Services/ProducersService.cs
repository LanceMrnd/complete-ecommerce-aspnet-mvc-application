using eTickets.Models.Data.Base;

namespace eTickets.Models.Data.Services
{
    public class ProducersService: EntityBaseRepository<Producer>, IProducersService
    {
        public ProducersService(AppDbContext context) : base(context)
        {

        }
    }
}
