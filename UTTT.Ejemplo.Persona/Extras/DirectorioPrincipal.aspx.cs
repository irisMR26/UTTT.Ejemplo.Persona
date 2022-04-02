using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona.Extras
{
    public partial class DirectorioPrincipal : System.Web.UI.Page
    {
        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Persona baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
#pragma warning disable CS0414 // El campo 'DirectorioPrincipal.tipoAccion' está asignado pero su valor nunca se usa
        private int tipoAccion = 0;
#pragma warning restore CS0414 // El campo 'DirectorioPrincipal.tipoAccion' está asignado pero su valor nunca se usa
        private int idDirectorio = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
#pragma warning disable CS0168 // La variable '_e' se ha declarado pero nunca se usa
            try
            {
                Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];
                this.idPersona = this.session.Parametros["idPersona"] != null ?
                    int.Parse(this.session.Parametros["idPersona"].ToString()) : 0;

                this.idDirectorio = this.session.Parametros["idDirectorio"] != null ?
                    int.Parse(this.session.Parametros["idDirectorio"].ToString()) : 0;

                if (!this.IsPostBack)
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Persona>().First(c => c.id == this.idPersona);
                    this.txtPersona.Text = this.baseEntity.strNombre + " " + this.baseEntity.strAPaterno + "  " + this.baseEntity.strAMaterno;
                    if (this.baseEntity.Directorio.Count() == 0)
                    {
                        this.lblAccion.Text = "Agregar";
                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";

                    }
                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/PersonaPrincipal.aspx", false);
            }
#pragma warning restore CS0168 // La variable '_e' se ha declarado pero nunca se usa
        }

        protected void LinqDataSourceDirectorio_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
#pragma warning disable CS0168 // La variable '_e' se ha declarado pero nunca se usa
            try
            {
                DataContext dcConsulta = new DcGeneralDataContext();
                Expression<Func<UTTT.Ejemplo.Linq.Data.Entity.Directorio, bool>>
                    predicate = c => c.idPersona == this.idPersona;
                predicate.Compile();
                List<UTTT.Ejemplo.Linq.Data.Entity.Directorio> listaPersona =
                    dcConsulta.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Directorio>().Where(predicate).ToList();
                e.Result = listaPersona;
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/PersonaPrincipal.aspx", false);
            }
#pragma warning restore CS0168 // La variable '_e' se ha declarado pero nunca se usa
        }

        protected void dgvDirectorio_RowCommand(object sender, GridViewCommandEventArgs e)
        {
#pragma warning disable CS0168 // La variable '_e' se ha declarado pero nunca se usa
            try
            {
                int idPersona = int.Parse(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Editar":
                        this.editar(idPersona);
                        break;
                    case "Eliminar":
                        this.eliminar(idPersona);
                        break;

                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al seleccionar");
            }
#pragma warning restore CS0168 // La variable '_e' se ha declarado pero nunca se usa
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
#pragma warning disable CS0168 // La variable '_e' se ha declarado pero nunca se usa
            try
            {
                this.session.Pantalla = "~/Extras/DirectorioManager.aspx";
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idPersona", this.idPersona.ToString());
                parametrosRagion.Add("idDirectorio", "0");
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.Response.Redirect(this.session.Pantalla, false);

            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al agregar");
            }
#pragma warning restore CS0168 // La variable '_e' se ha declarado pero nunca se usa
        }


        private void editar(int _directorio)
        {
            try
            {
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idPersona", this.idPersona.ToString());
                parametrosRagion.Add("idDirectorio", _directorio.ToString());
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.session.Pantalla = String.Empty;
                this.session.Pantalla = "~/Extras/DirectorioManager.aspx";
                this.Response.Redirect(this.session.Pantalla, false);

            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        private void eliminar(int _idDirectorio)
        {
            try
            {
                DataContext dcDelete = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Directorio directorio = dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Directorio>().First(
                    c => c.id == _idDirectorio);
                dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Directorio>().DeleteOnSubmit(directorio);
                dcDelete.SubmitChanges();
                this.showMessage("El registro se elimino correctamente.");
                this.LinqDataSourceDirectorio.RaiseViewChanged();
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }


        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PersonaPrincipal.aspx", false);
        }

        protected void dgvDirectorio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}