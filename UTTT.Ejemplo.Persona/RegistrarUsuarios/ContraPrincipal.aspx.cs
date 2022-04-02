using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona.RegistrarUsuarios
{
    public partial class ContraPrincipal : System.Web.UI.Page
    {
        DataContext dcGuardar = new DcGeneralDataContext();
        string url;
        private UTTT.Ejemplo.Linq.Data.Entity.Usuario baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!IsValid)
            {
                return;
            }
            try
            {
                CtrlPassword email = new CtrlPassword();
                DataContext db = new DcGeneralDataContext();

                var persona = db.GetTable<Linq.Data.Entity.Persona>().FirstOrDefault(p => p.strEmail == this.txtCorreoElectronico.Text.Trim());
                if (persona == null)
                {
                    this.lblMensaje.Visible = true;
                    this.lblMensaje.Text = "El correo ingresado no existe";
                    return;
                }
                var usuario = db.GetTable<Usuario>().FirstOrDefault(u => u.idPersona == persona.id);
                if (usuario == null)
                {
                    this.lblMensaje.Visible = true;
                    this.lblMensaje.Text = "El correo ingresado no tiene un usuario.";
                    return;
                }
                if (usuario.idCatValorUsuario == 1)
                {
                    this.lblMensaje.Visible = true;
                    this.lblMensaje.Text = "El usuario asignado a este correo no está activo";
                    return;
                }
                var token = this.GetSHA256(Guid.NewGuid().ToString());
                usuario.strTokenRecover2 = token;
                db.SubmitChanges();
                if (recoveryPasswordEmail(persona.strEmail, usuario.strNombreUsuario, token))
                {
                    this.lblMensaje.Visible = true;
                    this.lblMensaje.Text = "Revise su correo para la recuperacion de la contraseña";

                }
                else
                {
                    this.lblMensaje.Visible = true;
                    this.lblMensaje.Text = "Error al enviar el correo";
                    return;
                }



            }
            catch (Exception ex)
            {
                Response.Redirect("~/MensajeError.aspx", false);
            }
        }


        public string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

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

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Login.aspx", false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error");
                this.showMessageException(_e.Message);
            }
        }
    }
}