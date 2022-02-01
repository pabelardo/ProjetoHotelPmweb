#nullable disable
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository;

public class HotelRepository : Repository<Hotel>, IHotelRepository
{
    public HotelRepository(MyDbContext context) : base(context) { }

    public async Task<Hotel> ObterHotelQuartos(Guid id)
    {
        return await Db.Hotels.AsNoTracking()
                              .Include(h => h.Quartos)
                              .FirstOrDefaultAsync(h => h.Id == id);
    }
}