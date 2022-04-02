using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona.RegistrarUsuarios
{
    public partial class RegistroPrincipal : System.Web.UI.Page
    {
        private SessionManager session = new SessionManager();
        public static string error;

        protected void Page_Load(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.FirstChanceException += (enviar, ee) =>
            {
                System.Text.StringBuilder msg = new System.Text.StringBuilder();
                msg.AppendLine(ee.Exception.GetType().FullName);
                msg.AppendLine(ee.Exception.Message);
                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
                msg.AppendLine(st.ToString());
                msg.AppendLine();
                PersonaPrincipal.error = msg.ToString();
            };

            this.lblUsuario.Text = Session["UsernameSession"] as string;
            try
            {
                Response.Buffer = true;
                DataContext dcTemp = new DcGeneralDataContext();
                if (!this.IsPostBack)
                {
                    List<CatValorUsuario> estadoUsuarios = dcTemp.GetTable<CatValorUsuario>().ToList();
                    CatValorUsuario estadoTemp = new CatValorUsuario();
                    estadoTemp.id = -1;
                    estadoTemp.strValor = "Todos";
                    estadoUsuarios.Insert(0, estadoTemp);
                    this.ddlEstado.DataTextField = "strValor";
                    this.ddlEstado.DataValueField = "id";
                    this.ddlEstado.DataSource = estadoUsuarios;
                    this.ddlEstado.DataBind();
                }
            }
            catch (Exception ex)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                this.DataSourceUsuario.RaiseViewChanged();
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al buscar");
            }

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                this.session.Pantalla = "~/RegistrarUsuarios/RegistroManager.aspx";
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idUsuario", "0");
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.Response.Redirect(this.session.Pantalla, false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al agregar");
            }
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void DataSourceUsuario_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            if (Session["UsernameSession"] == null)
            {
                Response.Redirect("~/Login.aspx", false);
                return;
            }
            try
            {
                DataContext dcConsulta = new DcGeneralDataContext();
                bool nombreBool = false;
                bool estadoBool = false;
                if (!this.txtNombre.Text.Equals(String.Empty))
                {
                    nombreBool = true;
                }
                if (this.ddlEstado.Text != "-1")
                {
                    estadoBool = true;
                }

                Expression<Func<UTTT.Ejemplo.Linq.Data.Entity.Usuario, bool>>
                    predicate =
                    (c =>
                    ((estadoBool) ? c.idCatValorUsuario == int.Parse(this.ddlEstado.Text) : true) &&
                    ((nombreBool) ? (((nombreBool) ? c.strNombreUsuario.Contains(this.txtNombre.Text.Trim()) : false)) : true)
                    );

                predicate.Compile();

                List<UTTT.Ejemplo.Linq.Data.Entity.Usuario> usersList
                    = dcConsulta.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuario>().Where(predicate).ToList();
                e.Result = usersList;
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        protected void dgvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idUsuario = int.Parse(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Editar":
                        this.editar(idUsuario);
                        break;
                    case "Eliminar":
                        this.eliminar(idUsuario);
                        break;

                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al seleccionar");
            }
        }
        private void editar(int _idUsuario)
        {
            try
            {
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idUsuario", _idUsuario.ToString());
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.session.Pantalla = String.Empty;
                this.session.Pantalla = "~/RegistrarUsuarios/RegistroManager.aspx";
                this.Response.Redirect(this.session.Pantalla, false);

            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        private void eliminar(int _idUsuario)
        {
            try
            {
                DataContext dcDelete = new DcGeneralDataContext();
                Usuario users = dcDelete.GetTable<Usuario>().First(
                    c => c.id == _idUsuario);
                dcDelete.GetTable<Usuario>().DeleteOnSubmit(users);
                dcDelete.SubmitChanges();
                this.showMessage("El registro se agrego correctamente.");
                this.DataSourceUsuario.RaiseViewChanged();
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        protected void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }
        protected void btnTrick_Click(object sender, EventArgs e)
        {
            this.DataSourceUsuario.RaiseViewChanged();
        }

        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Response.Redirect("~/Hotel.aspx", false);
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
    }
}