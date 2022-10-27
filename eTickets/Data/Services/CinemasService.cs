using eTickets.Models.Data.Base;

namespace eTickets.Models.Data.Services
{
    public class CinemasService: EntityBaseRepository<Cinema>, ICinemasService
    {
        public CinemasService(AppDbContext context) : base(context) { }
    }
}
