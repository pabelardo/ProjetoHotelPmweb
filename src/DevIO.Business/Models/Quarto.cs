namespace DevIO.Business.Models;

public class Quarto : Entity
{
    public Guid HotelId { get; set; }
    public string Nome { get; set; }
    public int NumeroOcupantes { get; set; }
    public int NumeroDeAdultos { get; set; }
    public int NumeroDeCriancas { get; set; }
    public decimal Preco { get; set; }
    public string? Fotos { get; set; }
    public Hotel Hotel { get; set; }
}