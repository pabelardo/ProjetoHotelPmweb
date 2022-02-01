using System.Collections;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces;

public interface IQuartoRepository : IRepository<Quarto>
{
    Task<IEnumerable<Quarto>> ObterQuartosPorHotel(Guid hotelId); //vou obter quais são os quartos daquele hotel
    Task<IEnumerable<Quarto>> ObterQuartosHotels(); //vou obter uma lista de quartos com a informação daquele hotel
    Task<Quarto> ObterQuartoHotel(Guid id); //Retornar um quarto e o hotel a quem ele pertence
}