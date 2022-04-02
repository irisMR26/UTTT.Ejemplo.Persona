using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Net.Mail;

namespace UTTT.Ejemplo.Persona.Control.Ctrl
{
    public class CtrlPassword
    {
        public bool recoveryPasswordEmail(String email, String name, String token)
        {
            try
            {

                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                mailMessage.From = new MailAddress("pruebastareasiris@gmail.com");
                mailMessage.To.Add(new MailAddress(email));
                mailMessage.Subject = "Recuperar contraseña";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = this.enviarCorreo(name, token);
                smtpClient.Port = 587;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("pruebastareasiris@gmail.com", "Iris159!");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception _e)
            {

                return false;
            }
        }
        private String enviarCorreo(String name, String token)
        {
            String prodUrl = "http://irismartinezrodriguez.somee.com/RegistrarUsuarios/ContraManager.aspx?token=" + token;
            String body = "<h2>Hola " + name + "</h2><br>" +
                "<p>¿Quieres restablecer tu contraseña? Accede a este Link" +
                " </p>" +
                "<a href='" + prodUrl + "'>Restablecer contraseña</a>";
            return body;

        }
    }
}
