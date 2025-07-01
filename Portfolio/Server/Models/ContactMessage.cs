using System.ComponentModel.DataAnnotations;

namespace Portfolio.Server.Models
{
    /// <summary>
    /// Représente un message envoyé depuis le formulaire de contact.
    /// </summary>
    public class ContactMessage
    {
        [Required(ErrorMessage = "Le nom est requis.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "L'email est requis.")]
        [EmailAddress(ErrorMessage = "L'adresse email n'est pas valide.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le message est requis.")]
        public string Message { get; set; }
    }
}
