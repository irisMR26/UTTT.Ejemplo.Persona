using System;
using System.Data.Linq;
using System.Linq;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona.Extras
{
    public partial class NumeroSeguroManager : System.Web.UI.Page
    {
        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.NumeroSeguro baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private int tipoAccion = 0;
        private int idSeguro = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];
                this.idPersona = this.session.Parametros["idPersona"] != null ?
                    int.Parse(this.session.Parametros["idPersona"].ToString()) : 0;

                this.idSeguro = this.session.Parametros["idSeguro"] != null ?
                    int.Parse(this.session.Parametros["idSeguro"].ToString()) : 0;

                if (this.idSeguro == 0)
                {
                    this.baseEntity = new Linq.Data.Entity.NumeroSeguro();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.NumeroSeguro>().First(c => c.id == this.idSeguro);
                    this.tipoAccion = 2;
                }

                if (!this.IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    if (this.idSeguro == 0)
                    {
                        this.lblAccion.Text = "Agregar";
                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        this.txtnss.Text = this.baseEntity.strNumeroSeguro;
                        this.txtAsegurados.Text = this.baseEntity.strNumAsegurados;
                        this.txtDescripcion.Text = this.baseEntity.strDescripcionAsegurados;
                        this.txtLentes.Text = this.baseEntity.strLentes;
                        this.txtSangre.Text = this.baseEntity.strTipoSangre;
                        this.txtPeso.Text = this.baseEntity.strPeso;
                        this.txtCovid.Text = this.baseEntity.strCovid;
                    }
                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/Extras/NumeroSeguroPrincipal.aspx", false);
            }
#pragma warning restore CS0168 // La variable '_e' se ha declarado pero nunca se usa
        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtnss.Text.Trim() == String.Empty && this.txtAsegurados.Text.Trim() == String.Empty && this.txtDescripcion.Text.Trim() == String.Empty &&
                  this.txtLentes.Text.Trim() == String.Empty && this.txtPeso.Text.Trim() == String.Empty && this.txtPeso.Text.Trim() == String.Empty && this.txtCovid.Text.Trim() == String.Empty)
                {
                    this.Response.Redirect("~/Extras/NumeroSeguroPrincipal.aspx", true);
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
                UTTT.Ejemplo.Linq.Data.Entity.NumeroSeguro seguro = new Linq.Data.Entity.NumeroSeguro();
                if (this.idSeguro == 0)
                {
                    seguro.idPersona = this.idPersona;
                    seguro.strNumeroSeguro = this.txtnss.Text.Trim();
                    seguro.strNumAsegurados = this.txtAsegurados.Text.Trim();
                    seguro.strDescripcionAsegurados = this.txtDescripcion.Text.Trim();
                    seguro.strLentes = this.txtLentes.Text.Trim();
                    seguro.strTipoSangre = this.txtSangre.Text.Trim();
                    seguro.strPeso = this.txtPeso.Text.Trim();
                    seguro.strCovid = this.txtCovid.Text.Trim();

                    dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.NumeroSeguro>().InsertOnSubmit(seguro);
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se agrego correctamente.");
                    this.Response.Redirect("~/Extras/NumeroSeguroPrincipal.aspx");
                }
                if (this.idSeguro > 0)
                {
                    seguro = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.NumeroSeguro>().First(c => c.id == this.idSeguro);
                    seguro.strNumeroSeguro = this.txtnss.Text.Trim();
                    seguro.strNumAsegurados = this.txtAsegurados.Text.Trim();
                    seguro.strDescripcionAsegurados = this.txtDescripcion.Text.Trim();
                    seguro.strLentes = this.txtLentes.Text.Trim();
                    seguro.strTipoSangre = this.txtSangre.Text.Trim();
                    seguro.strPeso = this.txtPeso.Text.Trim();
                    seguro.strCovid = this.txtCovid.Text.Trim();
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se edito correctamente.");
                    this.Server.Transfer("~/Extras/NumeroSeguroPrincipal.aspx");

                }
            }
            catch (Exception _e)
            {
                this.showMessageException(_e.Message);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
#pragma warning disable CS0168 // La variable '_e' se ha declarado pero nunca se usa
            try
            {
                this.Response.Redirect("~/Extras/NumeroSeguroPrincipal.aspx");
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
#pragma warning restore CS0168 // La variable '_e' se ha declarado pero nunca se usa
        }
    }
}