using Microsoft.AspNetCore.Mvc;
using Portfolio.Server.Models;
using System.Net;
using System.Net.Mail;

namespace Portfolio.Server.Controllers
{
    // Indique que ce contrôleur est une API REST
    [ApiController]
    [Route("api/[controller]")] // La route devient : /api/contact
    public class ContactController : ControllerBase
    {
        private readonly IConfiguration _config;

        // Injection de la configuration (appsettings.json)
        public ContactController(IConfiguration config)
        {
            _config = config;
        }

        // Méthode appelée en POST : /api/contact
        [HttpPost]
        public IActionResult Send([FromBody] ContactModel message)
        {
            // Vérifie que les données reçues sont valides (via les [Required] du modèle)
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Création du client SMTP
                var smtpClient = new SmtpClient
                {
                    Host = _config["Smtp:Host"],                 // Ex : smtp.gmail.com
                    Port = int.Parse(_config["Smtp:Port"]),     // En général : 587
                    EnableSsl = true,                            // Connexion sécurisée
                    Credentials = new NetworkCredential(
                        _config["Smtp:User"],                    // Email de l’expéditeur
                        _config["Smtp:Password"]                 // Mot de passe d’application Gmail
                    )
                };

                // Création du message email à envoyer
                var mail = new MailMessage
                {
                    From = new MailAddress(_config["Smtp:User"]), // Doit être la même que dans les credentials
                    Subject = "Nouveau message depuis le portfolio",
                    Body = $"Nom : {message.Name}\nEmail : {message.Email}\n\nMessage :\n{message.Message}",
                    IsBodyHtml = false
                };

                // Ajout du destinataire (ex : machado.sacha@hotmail.com)
                mail.To.Add(_config["Smtp:To"]);

                // Envoi du mail
                smtpClient.Send(mail);

                // Succès : 200 OK
                return Ok(new { message = "Message envoyé avec succès !" });
            }
            catch (SmtpException ex)
            {
                // Erreur spécifique à SMTP : 500 + détails
                return StatusCode(500, new
                {
                    error = "Erreur SMTP : " + ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
            catch (Exception ex)
            {
                // Erreur générique : 500 + message d'erreur
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
