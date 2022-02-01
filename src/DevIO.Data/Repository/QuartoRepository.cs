using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository;

public class QuartoRepository : Repository<Quarto>, IQuartoRepository
{
    public QuartoRepository(MyDbContext context) : base(context) { }

    public async Task<IEnumerable<Quarto>> ObterQuartosPorHotel(Guid hotelId)
    {
        return await Buscar(q => q.HotelId == hotelId);
    }

    public async Task<IEnumerable<Quarto>> ObterQuartosHotels()
    {
        return await Db.Quartos.AsNoTracking()
                               .Include(f => f.Hotel)
                               .OrderBy(q => q.Nome)
                               .ToListAsync();
    }

    public async Task<Quarto> ObterQuartoHotel(Guid id)
    {
        return await Db.Quartos.AsNoTracking()
                               .Include(q => q.Hotel)
                               .FirstOrDefaultAsync(q => q.Id == id);
    }
}