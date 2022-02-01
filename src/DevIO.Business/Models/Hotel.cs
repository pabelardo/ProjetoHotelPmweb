namespace DevIO.Business.Models;

public class Hotel : Entity
{
    public string Nome { get; set; }
    public string CNPJ { get; set; }
    public string Endereco { get; set; }
    public string Descricao { get; set; }
    public string? Fotos { get; set; }
    public IEnumerable<Quarto> Quartos { get; set; }
}