using System.ComponentModel.DataAnnotations;

namespace Chatbot.Presentation.ViewModels
{
    public class CreateClientViewModel
    {
        [Required(ErrorMessage = "A matrícula é obrigatória!")]
        public string ClientId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "É obrigatório os 4 primeiros dígitos do CPF.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Digite apenas os 4 primeiros dígitos do CPF.")]
        public string CpfPrefix { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O Status é obrigatório!")]
        public string Status { get; set; }
        public int ProfileId { get; set; }
    }
}
