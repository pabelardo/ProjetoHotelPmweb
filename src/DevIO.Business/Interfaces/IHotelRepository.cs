using DevIO.Business.Models;

namespace DevIO.Business.Interfaces;

public interface IHotelRepository : IRepository<Hotel>
{
    Task<Hotel> ObterHotelQuartos(Guid id); //Vou obter o Hotel e os quartos dele
}