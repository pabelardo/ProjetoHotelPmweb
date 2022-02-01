using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevIO.App.ViewModels
{
    public class HotelViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter {1} caracteres")]
        public string CNPJ { get; set; }

        [DisplayName("Endereço")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 5)]
        public string Endereco { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(500, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 5)]
        public string Descricao { get; set; }

        [DisplayName("Foto do Hotel")]
        public IFormFile? FotosUpload { get; set; }

        public string? Fotos { get; set; }

        public IEnumerable<QuartoViewModel> Quartos { get; set; }

        public HotelViewModel()
        {
            Quartos ??= new List<QuartoViewModel>();
        }
    }
}
