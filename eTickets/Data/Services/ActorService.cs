using eTickets.Models.Data.Base;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Models.Data.Services
{
    public class ActorService : EntityBaseRepository<Actor>, IActorService
    {
        public ActorService(AppDbContext context) : base(context) { }

    }
}
