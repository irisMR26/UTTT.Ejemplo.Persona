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
namespace UTTT.Ejemplo.Persona.Libreria
{
    public partial class LibrosPrincipal : System.Web.UI.Page
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
                    List<Pertenencia> ubicacion = dcTemp.GetTable<Pertenencia>().ToList();
                    Pertenencia estadoTemp = new Pertenencia();
                    estadoTemp.id = -1;
                    estadoTemp.strPertenencia = "Todos";
                    ubicacion.Insert(0, estadoTemp);
                    this.ddlPertenencia.DataTextField = "strPertenencia";
                    this.ddlPertenencia.DataValueField = "id";
                    this.ddlPertenencia.DataSource = ubicacion;
                    this.ddlPertenencia.DataBind();
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
                this.DataSourceLibros.RaiseViewChanged();
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
                this.session.Pantalla = "~/Libreria/LibrosManager.aspx";
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idLibro", "0");
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


        protected void DataSourceLibros_Selecting(object sender, LinqDataSourceSelectEventArgs e)
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
                if (this.ddlPertenencia.Text != "-1")
                {
                    estadoBool = true;
                }

                Expression<Func<UTTT.Ejemplo.Linq.Data.Entity.Libros, bool>>
                    predicate =
                    (c =>
                    ((estadoBool) ? c.idPertenencia == int.Parse(this.ddlPertenencia.Text) : true) &&
                    ((nombreBool) ? (((nombreBool) ? c.strNombre.Contains(this.txtNombre.Text.Trim()) : false)) : true)
                    );

                predicate.Compile();

                List<UTTT.Ejemplo.Linq.Data.Entity.Libros> LibrosList
                    = dcConsulta.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Libros>().Where(predicate).ToList();
                e.Result = LibrosList;
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        protected void dgvLibros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idLibros = int.Parse(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Editar":
                        this.editar(idLibros);
                        break;
                    case "Eliminar":
                        this.eliminar(idLibros);
                        break;

                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al seleccionar");
            }
        }
        private void editar(int _idLibro)
        {
            try
            {
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idLibro", _idLibro.ToString());
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.session.Pantalla = String.Empty;
                this.session.Pantalla = "~/Libreria/LibrosManager.aspx";
                this.Response.Redirect(this.session.Pantalla, false);

            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        private void eliminar(int _idLibros)
        {
            try
            {
                DataContext dcDelete = new DcGeneralDataContext();
                Libros libros = dcDelete.GetTable<Libros>().First(
                    c => c.id == _idLibros);
                dcDelete.GetTable<Libros>().DeleteOnSubmit(libros);
                dcDelete.SubmitChanges();
                this.showMessage("El registro se agrego correctamente.");
                this.DataSourceLibros.RaiseViewChanged();
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
            this.DataSourceLibros.RaiseViewChanged();
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
