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
    public partial class NumeroSeguroPrincipal : System.Web.UI.Page
    {
        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Persona baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
#pragma warning disable CS0414 // El campo 'NumeroSeguroPrincipal.tipoAccion' está asignado pero su valor nunca se usa
        private int tipoAccion = 0;
#pragma warning restore CS0414 // El campo 'NumeroSeguroPrincipal.tipoAccion' está asignado pero su valor nunca se usa
        private int idSeguro = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
#pragma warning disable CS0168 // La variable '_e' se ha declarado pero nunca se usa
            try
            {
                Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];
                this.idPersona = this.session.Parametros["idPersona"] != null ?
                    int.Parse(this.session.Parametros["idPersona"].ToString()) : 0;

                this.idSeguro = this.session.Parametros["idSeguro"] != null ?
                    int.Parse(this.session.Parametros["idSeguro"].ToString()) : 0;

                if (!this.IsPostBack)
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Persona>().First(c => c.id == this.idPersona);
                    this.txtPersona.Text = this.baseEntity.strNombre + " " + this.baseEntity.strAPaterno + "  " + this.baseEntity.strAMaterno;
                    if (this.baseEntity.NumeroSeguro.Count() == 0)
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

        protected void LinqDataSourceNumeroSeguro_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
#pragma warning disable CS0168 // La variable '_e' se ha declarado pero nunca se usa
            try
            {
                DataContext dcConsulta = new DcGeneralDataContext();
                Expression<Func<UTTT.Ejemplo.Linq.Data.Entity.NumeroSeguro, bool>>
                    predicate = c => c.idPersona == this.idPersona;
                predicate.Compile();
                List<UTTT.Ejemplo.Linq.Data.Entity.NumeroSeguro> listaPersona =
                    dcConsulta.GetTable<UTTT.Ejemplo.Linq.Data.Entity.NumeroSeguro>().Where(predicate).ToList();
                e.Result = listaPersona;
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/PersonaPrincipal.aspx", false);
            }
#pragma warning restore CS0168 // La variable '_e' se ha declarado pero nunca se usa
        }

        protected void dgvNumeroSeguro_RowCommand(object sender, GridViewCommandEventArgs e)
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
                this.session.Pantalla = "~/Extras/NumeroSeguroManager.aspx";
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idPersona", this.idPersona.ToString());
                parametrosRagion.Add("idSeguro", "0");
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


        private void editar(int _seguro)
        {
            try
            {
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idPersona", this.idPersona.ToString());
                parametrosRagion.Add("idSeguro", _seguro.ToString());
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.session.Pantalla = String.Empty;
                this.session.Pantalla = "~/Extras/NumeroSeguroManager.aspx";
                this.Response.Redirect(this.session.Pantalla, false);

            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        private void eliminar(int _idSeguro)
        {
            try
            {
                DataContext dcDelete = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.NumeroSeguro seguro = dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.NumeroSeguro>().First(
                    c => c.id == _idSeguro);
                dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.NumeroSeguro>().DeleteOnSubmit(seguro);
                dcDelete.SubmitChanges();
                this.showMessage("El registro se elimino correctamente.");
                this.LinqDataSourceNumeroSeguro.RaiseViewChanged();
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

        protected void dgvNumeroSeguro_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}