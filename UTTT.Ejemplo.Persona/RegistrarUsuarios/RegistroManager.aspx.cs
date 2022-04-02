using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona.RegistrarUsuarios
{
    public partial class RegistroManager : System.Web.UI.Page
    {
        private Usuario baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private SessionManager session = new SessionManager();
        private int idUsuarios = 0;
        private int tipoAccion = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblUser.Text = Session["UsernameSession"] as string;
            try
            {
                this.Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];
                this.idUsuarios = this.session.Parametros["idUsuario"] != null ?
                    int.Parse(this.session.Parametros["idUsuario"].ToString()) : 0;
                if (this.idUsuarios == 0)
                {
                    this.baseEntity = new Usuario();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Usuario>().First(u => u.id == idUsuarios);
                    this.tipoAccion = 2;
                }
                this.txtPassword.Attributes.Add("type", "password");
                this.txtPassword2.Attributes.Add("type", "password");

                if (!IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    List<Linq.Data.Entity.Persona> personas = this.dcGlobal.GetTable<Linq.Data.Entity.Persona>().ToList();
                    List<Linq.Data.Entity.CatValorUsuario> lista = dcGlobal.GetTable<Linq.Data.Entity.CatValorUsuario>().ToList();
                    this.ddlEstadoUsuario.DataTextField = "strValor";
                    this.ddlEstadoUsuario.DataValueField = "id";
                    this.ddlPersona.DataValueField = "id";
                    this.ddlPersona.DataTextField = "strNombre";
                    if (this.idUsuarios == 0)
                    {
                        this.lblAccion.Text = "Agregar";
                        this.ddlPersona.DataSource = personas;
                        this.ddlPersona.DataBind();
                        this.ddlEstadoUsuario.Visible = true;
                        this.lblEstadoEditar.Visible = true;
                        CalendarExtender1.SelectedDate = DateTime.Now;

                        CatValorUsuario catTemp = new CatValorUsuario();
                        catTemp.id = -1;
                        catTemp.strValor = "Seleccionar";
                        lista.Insert(0, catTemp);
                        this.ddlEstadoUsuario.DataSource = lista;
                        this.ddlEstadoUsuario.DataBind();
                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        this.lblEstadoEditar.Visible = true;
                        this.ddlEstadoUsuario.Visible = true;
                        this.Label3.Visible = false;
                        this.ddlPersona.Visible = false;
                        this.UpdatePanel2.Visible = false;

                        this.txtFechaNac2.Visible = false;
                        this.imbtnCalendar.Visible = false;
                        this.lblCalendar2.Visible = false;

                        List<CatValorUsuario> estadosUsuario = dcGlobal.GetTable<CatValorUsuario>().ToList();
                        this.ddlEstadoUsuario.DataTextField = "strValor";
                        this.ddlEstadoUsuario.DataValueField = "id";
                        this.ddlEstadoUsuario.DataSource = estadosUsuario;
                        this.ddlEstadoUsuario.DataBind();
                        this.setItem(ref this.ddlEstadoUsuario, baseEntity.CatValorUsuario.strValor);


                        this.txtNombre.Text = baseEntity.strNombreUsuario;
                        this.txtPassword.Text = this.DesEncriptar(baseEntity.strPassword);
                        this.txtPassword2.Text = this.DesEncriptar(baseEntity.strPassword);

                    }
                }

            }
            catch (Exception ex)
            {
                this.Response.Redirect("~/RegistrarUsuarios/RegistroPrincipal.aspx", false);
            }
        }
        private bool validacion(Usuario _usuario, ref String mensaje)
        {
            DateTime dateValue;
            if (_usuario.strNombreUsuario.Length == 0)
            {
                mensaje = "Nombre de usuario no puede estar vacio";
                return false;
            }
            if (_usuario.strNombreUsuario.Length > 15)
            {
                mensaje = "Nombre de usuario solo acepta 15 caracteres";
                return false;
            }
            if (_usuario.strNombreUsuario.Length < 3)
            {
                mensaje = "Nombre de usuario es muy corto";
                return false;
            }
            if (_usuario.strPassword.Length == 0)
            {
                mensaje = "La contraseña es necesaria";
                return false;
            }
            if (this.txtPassword.Text.Trim().Length > 15)
            {
                mensaje = "La contraseña solo es de 15 caracteres como máximo.";
                return false;
            }
            if (this.txtPassword.Text.Trim().Length < 5)
            {
                mensaje = "La contraseña es de 5 caracteres como mínimo.";
                return false;
            }

            if (!this.txtPassword.Text.Trim().Equals(this.txtPassword2.Text.Trim()))
            {
                mensaje = "Las contraseñas no son iguales";
                return false;
            }

            if (this.txtFechaNac2.Text.Trim().Length == 0 && this.idUsuarios == 0)
            {
                mensaje = "La fecha es requerida";
                return false;
            }

            return true;
        }

        public bool sqlInjectionValida(ref String _mensaje)
        {
            CtrlSeguridad valida = new CtrlSeguridad();
            String _mensajeFuncion = String.Empty;
            if (valida.sqlInjectionValida(this.txtNombre.Text.Trim(), ref _mensajeFuncion, "Nombre de usuario", ref this.txtNombre))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (valida.sqlInjectionValida(this.txtPassword.Text.Trim(), ref _mensajeFuncion, "Contraseña", ref this.txtPassword))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (valida.sqlInjectionValida(this.txtPassword2.Text.Trim(), ref _mensajeFuncion, "Repetir Contraseña", ref this.txtPassword2))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (this.idUsuarios == 0)
            {
                return true;
            }
            if (valida.sqlInjectionValida(this.txtFechaNac2.Text.Trim(), ref _mensajeFuncion, "Fecha de ingreso", ref this.txtFechaNac2))
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
            if (valida.htmlInjectionValida(this.txtNombre.Text.Trim(), ref mensajeFuncion, "Nombre de usuario", ref this.txtNombre))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInjectionValida(this.txtPassword.Text.Trim(), ref mensajeFuncion, "Contraseña", ref this.txtPassword))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInjectionValida(this.txtPassword2.Text.Trim(), ref mensajeFuncion, "Repetir Contraseña", ref this.txtPassword2))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (this.idUsuarios == 0)
            {
                return true;
            }
            if (valida.htmlInjectionValida(this.txtFechaNac2.Text.Trim(), ref mensajeFuncion, "Fecha de ingreso", ref this.txtFechaNac2))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            return true;
        }

        public bool sqlQueryValidation(Usuario usuario, ref String mensaje)
        {
            var user = dcGlobal.GetTable<Usuario>().FirstOrDefault(u => u.strNombreUsuario == usuario.strNombreUsuario);
            if (user != null)
            {
                mensaje = "Ya existe un usuario con ese nombre";
                return false;
            }
            var userInPerson = dcGlobal.GetTable<Usuario>().FirstOrDefault(u => u.idPersona == usuario.idPersona);
            if (userInPerson != null)
            {
                mensaje = "La persona  ya está asociada con un usuario";
                return false;
            }
            return true;
        }
        public bool sqlQueryValidationEditar(Usuario usuario, ref String mensaje)
        {
            var userCount = dcGlobal.GetTable<Usuario>().Where(u => u.strNombreUsuario == usuario.strNombreUsuario && u.id != usuario.id).Count();
            if (userCount > 0)
            {
                mensaje = "Ya existe un usuario con ese nombre";
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

        public void setItem(ref DropDownList _control, String _value)
        {
            foreach (ListItem item in _control.Items)
            {
                if (item.Value == _value)
                {
                    item.Selected = true;
                    break;
                }
            }
            _control.Items.FindByText(_value).Selected = true;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Response.Redirect("~/RegistrarUsuarios/RegistroPrincipal.aspx", false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
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
        public static string Base64Encode(string word)
        {
            byte[] byt = System.Text.Encoding.UTF8.GetBytes(word);
            return Convert.ToBase64String(byt);
        }
        public static string Base64Decode(string word)
        {
            byte[] b = Convert.FromBase64String(word);
            return System.Text.Encoding.UTF8.GetString(b);
        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {

            try
            {

                if (this.txtNombre.Text.Trim() == String.Empty && this.txtPassword.Text.Trim() == String.Empty && this.txtPassword2.Text.Trim() == String.Empty &&
                  this.txtFechaNac2.Text.Trim() == String.Empty && int.Parse(this.ddlEstadoUsuario.Text).Equals(-1))
                {
                    this.Response.Redirect("~/RegistrarUsuarios/RegistroPrincipal.aspx", false);
                }
                else
                {
                    btnAceptar.ValidationGroup = "svGuardar";
                    Page.Validate("svGuardar");
                }
                if (!Page.IsValid)
                {
                    return;
                }

                string date = Request.Form[this.txtFechaNac2.UniqueID];
                DateTime fechaNacimiento = Convert.ToDateTime(date);
                DataContext dcGuardar = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Usuario usuario = new Linq.Data.Entity.Usuario();

                if (this.idUsuarios == 0)
                {
                    usuario.idPersona = int.Parse(this.ddlPersona.SelectedValue);
                    usuario.strNombreUsuario = this.txtNombre.Text.Trim();
                    usuario.strPassword = this.Encriptar(this.txtPassword.Text.Trim());
                    usuario.idCatValorUsuario = int.Parse(this.ddlEstadoUsuario.SelectedValue);

                    String mensaje = String.Empty;
                    if (!this.validacion(usuario, ref mensaje))
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
                    if (!this.sqlQueryValidation(usuario, ref mensaje))
                    {
                        this.lblMensaje.Text = mensaje;
                        this.lblMensaje.Visible = true;
                        return;
                    }
                    dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuario>().InsertOnSubmit(usuario);
                    dcGuardar.SubmitChanges();
                    this.Response.Redirect("~/RegistrarUsuarios/RegistroPrincipal.aspx", false);
                }
                if (this.idUsuarios > 0)
                {
                    usuario = dcGuardar.GetTable<Usuario>().First(u => u.id == this.idUsuarios);
                    usuario.strNombreUsuario = this.txtNombre.Text.Trim();
                    usuario.strPassword = this.Encriptar(this.txtPassword.Text.Trim());
                    usuario.idCatValorUsuario = int.Parse(this.ddlEstadoUsuario.SelectedValue);


                    String mensaje = String.Empty;
                    if (!this.validacion(usuario, ref mensaje))
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
                    if (!this.sqlQueryValidationEditar(usuario, ref mensaje))
                    {
                        this.lblMensaje.Text = mensaje;
                        this.lblMensaje.Visible = true;
                        return;
                    }
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se edito correctamente.");
                    this.Response.Redirect("~/RegistrarUsuarios/RegistroPrincipal.aspx", false);
                    // editar
                }
            }
            catch (Exception _e)
            {

                this.showMessageException(_e.Message);
                MailMessage correo = new MailMessage();
                correo.From = new MailAddress("tareaspruebascarlo@gmail.com", "CorreoPrueba", System.Text.Encoding.UTF8);//Correo de salida
                correo.To.Add("19300559@uttt.edu.mx"); //Correo destino?
                correo.Subject = "Correo de prueba"; //Asunto
                correo.Body = _e.Message.ToString(); //Mensaje del correo
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;
                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp.gmail.com"; //Host del servidor de correo
                smtp.Port = 587; //Puerto de salida
                smtp.Credentials = new System.Net.NetworkCredential("tareaspruebascarlo@gmail.com", "Iris159!");//Cuenta de correo
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.EnableSsl = true;//True si el servidor de correo permite ssl
                smtp.Send(correo);
            }
        }
    }
}