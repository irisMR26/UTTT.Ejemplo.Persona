using System;
using System.Data.Linq;
using System.Linq;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona.Extras
{
    public partial class DirectorioManager : System.Web.UI.Page
    {
        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Directorio baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
#pragma warning disable CS0414 // El campo 'DirectorioManager.tipoAccion' está asignado pero su valor nunca se usa
        private int tipoAccion = 0;
#pragma warning restore CS0414 // El campo 'DirectorioManager.tipoAccion' está asignado pero su valor nunca se usa
        private int idTelefono = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
#pragma warning disable CS0168 // La variable '_e' se ha declarado pero nunca se usa
            try
            {
                this.Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];
                this.idPersona = this.session.Parametros["idPersona"] != null ?
                    int.Parse(this.session.Parametros["idPersona"].ToString()) : 0;

                this.idTelefono = this.session.Parametros["idTelefono"] != null ?
                    int.Parse(this.session.Parametros["idTelefono"].ToString()) : 0;

                if (this.idTelefono == 0)
                {
                    this.baseEntity = new Linq.Data.Entity.Directorio();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Directorio>().First(c => c.id == this.idTelefono);
                    this.tipoAccion = 2;
                }

                if (!this.IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    if (this.idTelefono == 0)
                    {
                        this.lblAccion.Text = "Agregar";
                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        this.txtNumero.Text = this.baseEntity.strTelefono;
                    }
                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/Extras/DirectorioPrincipal.aspx", false);
            }
#pragma warning restore CS0168 // La variable '_e' se ha declarado pero nunca se usa
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
#pragma warning disable CS0168 // La variable '_e' se ha declarado pero nunca se usa
            try
            {


                DataContext dcGuardar = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Directorio directorio = new Linq.Data.Entity.Directorio();
                if (this.idTelefono == 0)
                {
                    directorio.idPersona = this.idPersona;
                    directorio.strTelefono = this.txtNumero.Text.Trim();

                    dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Directorio>().InsertOnSubmit(directorio);
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se agrego correctamente.");
                    this.Response.Redirect("~/Extras/DirectorioPrincipal.aspx");
                }
                if (this.idTelefono > 0)
                {
                    directorio = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Directorio>().First(c => c.id == this.idTelefono);
                    directorio.strTelefono = this.txtNumero.Text.Trim();

                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se edito correctamente.");
                    this.Server.Transfer("~/Extras/DirectorioPrincipal.aspx");

                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/Extras/DirectorioPrincipal.aspx", false);
            }
#pragma warning restore CS0168 // La variable '_e' se ha declarado pero nunca se usa
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
#pragma warning disable CS0168 // La variable '_e' se ha declarado pero nunca se usa
            try
            {
                this.Response.Redirect("~/Extras/DirectorioPrincipal.aspx");
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
#pragma warning restore CS0168 // La variable '_e' se ha declarado pero nunca se usa
        }
    }
}