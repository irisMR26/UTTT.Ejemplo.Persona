using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UTTT.Ejemplo.Persona
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            enviar(PersonaPrincipal.error);
        }

        public void enviar(String error)
        {
            MailMessage correo = new MailMessage();
            correo.From = new MailAddress("pruebastareasiris@gmail.com", "CorreoPrueba", System.Text.Encoding.UTF8);//Correo de salida
            correo.To.Add("19300851@uttt.edu.mx"); //Correo destino?
            correo.Subject = "Correo de prueba"; //Asunto
            correo.Body = error; //Mensaje del correo
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Host = "smtp.gmail.com"; //Host del servidor de correo
            smtp.Port = 587; //Puerto de salida
            smtp.Credentials = new System.Net.NetworkCredential("pruebastareasiris@gmail.com", "Iris159!");//Cuenta de correo
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;//True si el servidor de correo permite ssl
            smtp.Send(correo);
        }

    }
}