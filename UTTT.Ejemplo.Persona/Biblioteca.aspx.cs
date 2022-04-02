using System;
using System.Data.Linq;
using System.Linq;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;

namespace UTTT.Ejemplo.Persona
{
    public partial class Biblioteca : System.Web.UI.Page
    {
        private UTTT.Ejemplo.Linq.Data.Entity.Usuario baseEntity;
        private SessionManager session = new SessionManager();
        public static string error;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsernameSession"] == null)
            {
                this.Response.Redirect("~/Login.aspx", false);
            }
            if (Session["UsernameSession"] != null)
            {

            }
            this.lblUsuario.Text = Session["UsernameSession"] as string;

        }

        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            Session["UsernameSession"] = null;
            this.baseEntity = new Linq.Data.Entity.Usuario();
            this.Response.Redirect("~/Login.aspx", false);
        }
    }
}