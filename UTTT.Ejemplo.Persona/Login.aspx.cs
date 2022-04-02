using System;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                DataContext db = new DcGeneralDataContext();
                String mensaje = String.Empty;
                if (!this.validacion(ref mensaje))
                {
                    this.lblErrores.Text = mensaje;
                    this.lblErrores.Visible = true;
                    return;
                }
                if (!this.validacion(ref mensaje))
                {
                    this.lblErrores.Text = mensaje;
                    this.lblErrores.Visible = true;
                    return;
                }
                if (!this.HtmlInyeccion(ref mensaje))
                {
                    this.lblErrores.Text = mensaje;
                    this.lblErrores.Visible = true;
                    return;
                }
                var dbUser = db.GetTable<Usuario>().FirstOrDefault(u => u.strNombreUsuario == this.txtEmail.Text.Trim());
                if (dbUser == null)
                {
                    this.lblErrores.Text = "usuario o contraseña incorrectos";
                    this.lblErrores.Visible = true;
                    return;
                }
                if (dbUser.idCatValorUsuario == 1)
                {
                    this.lblErrores.Text = "Usuario Bloqueado o inactivo";
                    this.lblErrores.Visible = true;
                    return;
                }
                var passDec = this.DesEncriptar(dbUser.strPassword);
                if (!passDec.Equals(this.txtContraseña.Text.Trim()))
                {
                    this.lblErrores.Text = "usuario o contraseña incorrectos";
                    this.lblErrores.Visible = true;
                    return;
                }
                Session["UsernameSession"] = dbUser.strNombreUsuario;
                this.Response.Redirect("~/Bibliotecta.aspx", false);

            }
            catch (Exception ex)
            {
                var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                string strHost = smtpSection.Network.Host;
                int port = smtpSection.Network.Port;
                string strUserName = smtpSection.Network.UserName;
                string strFromPass = smtpSection.Network.Password;
                SmtpClient smtp = new SmtpClient(strHost, port);
                MailMessage msg = new MailMessage(); string body = "<h1>Error" + ex.Message + "</h1>";
                msg.From = new MailAddress(smtpSection.From, "Errores");
                msg.To.Add(new MailAddress("pruebastareasiris@gmail.com"));
                msg.Subject = "Correo";
                msg.IsBodyHtml = true;
                msg.Body = body;
                smtp.Credentials = new NetworkCredential(strUserName, strFromPass);
                smtp.EnableSsl = true;
                smtp.Send(msg);

                Response.Redirect("~/ErrorPage.aspx", false);
            }
        }
        public bool validacion(ref String mensaje)
        {
            if (txtEmail.Text.Trim().Length == 0)
            {
                mensaje = "Nombre de usuario no puede estar vacio";
                return false;
            }
            if (txtEmail.Text.Trim().Length > 15)
            {
                mensaje = "15 caracteres como máximo.";
                return false;
            }
            if (txtContraseña.Text.Trim().Length == 0)
            {
                mensaje = "Password necesaria";
                return false;
            }
            if (this.txtContraseña.Text.Trim().Length > 15)
            {
                mensaje = "15 caracteres como máximo.";
                return false;
            }
            return true;




        }

        public bool Inyeccion(ref String _mensaje)
        {
            CtrlSeguridad valida = new CtrlSeguridad();
            String _mensajeFuncion = String.Empty;
            if (valida.sqlInjectionValida(this.txtEmail.Text.Trim(), ref _mensajeFuncion, "Nombre de usuario", ref this.txtEmail))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (valida.sqlInjectionValida(this.txtContraseña.Text.Trim(), ref _mensajeFuncion, "Contraseña", ref this.txtContraseña))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            return true;
        }

        public bool HtmlInyeccion(ref String _mensaje)
        {
            CtrlSeguridad valida = new CtrlSeguridad();
            String mensajeFuncion = String.Empty;
            if (valida.htmlInjectionValida(this.txtEmail.Text.Trim(), ref mensajeFuncion, "Nombre de usuario", ref this.txtEmail))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInjectionValida(this.txtContraseña.Text.Trim(), ref mensajeFuncion, "Contraseña", ref this.txtContraseña))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            return true;
        }

        public string Encriptar(string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }
        public string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("~/RegistrarUsuarios/ContraPrincipal.aspx", false);
        }
    }
}