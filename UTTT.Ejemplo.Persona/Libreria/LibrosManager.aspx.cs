using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;
namespace UTTT.Ejemplo.Persona.Libreria
{
    public partial class LibrosManager : System.Web.UI.Page
    {
        private Libros baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private SessionManager session = new SessionManager();
        private int idLibros = 0;
        private int tipoAccion = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.lblUser.Text = Session["UsernameSession"] as string;
            try
            {
                this.Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];
                this.idLibros = this.session.Parametros["idLibros"] != null ?
                    int.Parse(this.session.Parametros["idLibros"].ToString()) : 0;
                if (this.idLibros == 0)
                {
                    this.baseEntity = new Libros();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Libros>().First(u => u.id == idLibros);
                    this.tipoAccion = 2;
                }

                if (!IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    List<Linq.Data.Entity.Persona> personas = this.dcGlobal.GetTable<Linq.Data.Entity.Persona>().ToList();
                    List<Linq.Data.Entity.Categoria> categoria = this.dcGlobal.GetTable<Linq.Data.Entity.Categoria>().ToList();
                    List<Linq.Data.Entity.Pertenencia> lista = dcGlobal.GetTable<Linq.Data.Entity.Pertenencia>().ToList();

                    this.ddlPersona.DataValueField = "id";
                    this.ddlPersona.DataTextField = "strNombre";

                    this.ddlPertenencia.DataValueField = "id";
                    this.ddlPertenencia.DataTextField = "strPertenencia";

                    this.ddlCategoria.DataValueField = "id";
                    this.ddlCategoria.DataTextField = "strCategoria";

                    if (this.idLibros == 0)
                    {
                        this.lblAccion.Text = "Agregar";
                        this.ddlPersona.DataSource = personas;
                        this.ddlPersona.DataBind();
                        this.ddlPertenencia.Visible = true;
                        this.ddlCategoria.Visible = true;

                        Pertenencia catTemp = new Pertenencia();
                        catTemp.id = -1;
                        catTemp.strPertenencia = "Seleccionar";
                        lista.Insert(0, catTemp);
                        this.ddlPertenencia.DataSource = lista;
                        this.ddlPertenencia.DataBind();

                        Categoria cateTemp = new Categoria();
                        cateTemp.id = -1;
                        cateTemp.strCategoria = "Seleccionar";
                        categoria.Insert(0, cateTemp);
                        this.ddlCategoria.DataSource = categoria;
                        this.ddlCategoria.DataBind();
                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        this.Label3.Visible = false;
                        this.ddlPersona.Visible = false;
                        this.Label1.Visible = false;
                        this.ddlCategoria.Visible = false;
                        this.Label2.Visible = false;
                        this.ddlPertenencia.Visible = false;

                        List<Categoria> estadosCate = dcGlobal.GetTable<Categoria>().ToList();
                        this.ddlCategoria.DataTextField = "strCategoria";
                        this.ddlCategoria.DataValueField = "id";
                        this.ddlCategoria.DataSource = estadosCate;
                        this.ddlCategoria.DataBind();
                        this.setItem(ref this.ddlCategoria, baseEntity.Categoria.strCategoria);

                        List<Pertenencia> estadosPer = dcGlobal.GetTable<Pertenencia>().ToList();
                        this.ddlPertenencia.DataTextField = "strPertenencia";
                        this.ddlPertenencia.DataValueField = "id";
                        this.ddlPertenencia.DataSource = estadosPer;
                        this.ddlPertenencia.DataBind();
                        this.setItem(ref this.ddlCategoria, baseEntity.Pertenencia.strPertenencia);


                        this.txtNombre.Text = baseEntity.strNombre;
                        this.txtPublicacion.Text = baseEntity.strPublicaciones;
                        this.txtPaginas.Text = baseEntity.strPaginas;


                    }
                }

            }
            catch (Exception ex)
            {
                this.Response.Redirect("~/Libreria/LibrosPrincipal.aspx", false);
            }
      }
        private bool validacion(Libros _libros, ref String mensaje)
        {
            if (_libros.strNombre.Length == 0)
            {
                mensaje = "Nombre de libro no puede estar vacio";
                return false;
            }
            if (_libros.strNombre.Length > 15)
            {
                mensaje = "Nombre de libro solo acepta 15 caracteres";
                return false;
            }
            if (_libros.strNombre.Length < 3)
            {
                mensaje = "Nombre de libro es muy corto";
                return false;
            }
            return true;
        }

        public bool sqlInjectionValida(ref String _mensaje)
        {
            CtrlSeguridad valida = new CtrlSeguridad();
            String _mensajeFuncion = String.Empty;
            if (valida.sqlInjectionValida(this.txtNombre.Text.Trim(), ref _mensajeFuncion, "Nombre de libro", ref this.txtNombre))
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
            if (valida.htmlInjectionValida(this.txtNombre.Text.Trim(), ref mensajeFuncion, "Nombre de libro", ref this.txtNombre))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            return true;
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
                this.Response.Redirect("~/Libreria/LibrosPrincipal.aspx", false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtNombre.Text.Trim() == String.Empty && this.txtPaginas.Text.Trim() == String.Empty && this.txtPublicacion.Text.Trim() == String.Empty &&
                    int.Parse(this.ddlPersona.Text).Equals(-1) && int.Parse(this.ddlCategoria.Text).Equals(-1) && int.Parse(this.ddlPersona.Text).Equals(-1))
                {
                    this.Response.Redirect("~/PersonaPrincipal.aspx", true);
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

                DataContext dcGuardar = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Libros libros = new Linq.Data.Entity.Libros();

                if (this.idLibros == 0)
                {

                    libros.strNombre = this.txtNombre.Text.Trim();
                    libros.strPaginas = this.txtPaginas.Text.Trim();
                    libros.strPublicaciones = this.txtPublicacion.Text.Trim();
                    libros.idPersona = int.Parse(this.ddlPersona.SelectedValue);
                    libros.idPertenencia = int.Parse(this.ddlPertenencia.SelectedValue);
                    libros.idCategoria = int.Parse(this.ddlCategoria.SelectedValue);

                    String mensaje = String.Empty;
                    if (!this.validacion(libros, ref mensaje))
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
                    dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Libros>().InsertOnSubmit(libros);
                    dcGuardar.SubmitChanges();
                    this.Response.Redirect("~/Libreria/LibrosPrincipal.aspx", false);
                }
                if (this.idLibros > 0)
                {
                    libros = dcGuardar.GetTable<Libros>().First(u => u.id == this.idLibros);
                    libros.strNombre = this.txtNombre.Text.Trim();
                    libros.strPaginas = this.txtPaginas.Text.Trim();
                    libros.strPublicaciones = this.txtPublicacion.Text.Trim();
                    


                    String mensaje = String.Empty;
                    if (!this.validacion(libros, ref mensaje))
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
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se edito correctamente.");
                    this.Response.Redirect("~/Libreria/LibrosPrincipal.aspx", false);
                    // editar
                }
            }
            catch (Exception _e)
            {
                Response.Redirect("~/ErrorPage.aspx", false);
            }
        }
    }
}