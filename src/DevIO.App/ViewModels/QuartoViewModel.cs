using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevIO.App.ViewModels;

public class QuartoViewModel
{
    [Key]
    public Guid Id { get; set; }

    [DisplayName("Hotel")]
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public Guid HotelId { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Nome { get; set; }

    [DisplayName("Nº de Ocupantes")]
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int NumeroOcupantes { get; set; }

    [DisplayName("Nº de Adultos")]
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int NumeroDeAdultos { get; set; }

    [DisplayName("Nº de Crianças")]
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int NumeroDeCriancas { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public decimal Preco { get; set; }

    [DisplayName("Foto do Quarto")]
    public IFormFile? FotosUpload { get; set; }

    public string? Fotos { get; set; }

    public HotelViewModel? Hotel { get; set; }

    public IEnumerable<HotelViewModel> Hotels { get; set; }

    public QuartoViewModel()
    {
        Hotels ??= new List<HotelViewModel>();
    }
}