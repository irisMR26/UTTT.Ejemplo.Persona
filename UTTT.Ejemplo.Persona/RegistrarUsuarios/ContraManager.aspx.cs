using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Net;
using System.Net.Configuration;
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
    public partial class ContraManager : System.Web.UI.Page
    {
        private String recoveryToken;
        protected void Page_Load(object sender, EventArgs e)
        {
            DataContext db = new DcGeneralDataContext();
            var token = Request.QueryString["token"] as String;
            if (token == null)
            {
                Response.Redirect("~/Login.aspx", false);
                return;
            }
            this.recoveryToken = token;
            var user = db.GetTable<Usuario>().FirstOrDefault(u => u.strTokenRecover2 == token);
            if (user == null)
            {
                Response.Redirect("~/RegistrarUsuarios/ContraPrincipal.aspx", false);
                return;
            }
            else
            {
                var _persona = db.GetTable<Linq.Data.Entity.Persona>().FirstOrDefault(p => p.id == user.idPersona);
                this.txtUsuario.Text = user.strNombreUsuario;
                this.txtUsuario.Enabled = false;
                this.txtUsuario.Text = _persona.strEmail;
                this.txtUsuario.Enabled = false;

            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx", false);
        }


        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!IsValid)
            {
                return;
            }
            try
            {
                DataContext db = new DcGeneralDataContext();
                String mensaje = String.Empty;
                var usuario = db.GetTable<Usuario>().FirstOrDefault(u => u.strTokenRecover2 == this.recoveryToken);
                if (!this.validacion(ref mensaje))
                {
                    this.lblMensaje.Text = mensaje;
                    this.lblMensaje.Visible = true;
                    return;
                }
                if (!this.sqlInjectionValida(ref mensaje))
                {
                    this.lblMensaje.Text = mensaje;
                    this.lblMensaje.Visible = true;
                    return;
                }
                if (!this.htmlInjectionValida(ref mensaje))
                {
                    this.lblMensaje.Text = mensaje;
                    this.lblMensaje.Visible = true;
                    return;
                }
                usuario.strPassword = this.Encriptar(this.txtContraseña.Text.Trim());
                usuario.strTokenRecover2 = null;
                db.SubmitChanges();
                Session["UsernameSession"] = usuario.strNombreUsuario;
                Response.Redirect("~/Login.aspx", false);
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
                msg.From = new MailAddress(smtpSection.From, "Prueba");
                msg.To.Add(new MailAddress("19300851@uttt.edu.mx"));
                msg.Subject = "Correo";
                msg.IsBodyHtml = true;
                msg.Body = body;
                smtp.Credentials = new NetworkCredential(strUserName, strFromPass);
                smtp.EnableSsl = true;
                smtp.Send(msg);

                Response.Redirect("~/ErrorPage.aspx", false);
            }
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


        private bool validacion(ref String mensaje)
        {

            // Validacion de contraseña

            if (txtContraseña.Text.Trim().Length == 0)
            {
                mensaje = "La contraseña es necesaria";
                return false;
            }
            if (this.txtContraseña.Text.Trim().Length > 15)
            {
                mensaje = "La contraseña solo puede tener 15 caracteres como máximo.";
                return false;
            }
            if (this.txtContraseña.Text.Trim().Length < 5)
            {
                mensaje = "La contraseña es muy corta.";
                return false;
            }
            if (txtRContraseña.Text.Trim().Length == 0)
            {
                mensaje = "La contraseña es necesaria que se ingrese";
                return false;
            }
            if (!txtRContraseña.Text.Trim().Equals(txtContraseña.Text.Trim()))
            {
                mensaje = "Las contraseñas no coinciden.";
                return false;
            }
            return true;
        }
        public bool sqlInjectionValida(ref String _mensaje)
        {
            CtrlSeguridad valida = new CtrlSeguridad();
            String _mensajeFuncion = String.Empty;
            if (valida.sqlInjectionValida(this.txtContraseña.Text.Trim(), ref _mensajeFuncion, "Contraseña", ref this.txtContraseña))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (valida.sqlInjectionValida(this.txtRContraseña.Text.Trim(), ref _mensajeFuncion, "Repetir contraseña", ref this.txtRContraseña))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            return true;
        }

        public bool htmlInjectionValida(ref String _mensaje)
        {
            CtrlSeguridad valida = new CtrlSeguridad();
            String mensajeFuncion = String.Empty;
            if (valida.htmlInjectionValida(this.txtContraseña.Text.Trim(), ref mensajeFuncion, "Contraseña", ref this.txtContraseña))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInjectionValida(this.txtRContraseña.Text.Trim(), ref mensajeFuncion, "Repetir contraseña", ref this.txtRContraseña))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            return true;
        }

        public static string MD5(string word)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(word));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

    }
}