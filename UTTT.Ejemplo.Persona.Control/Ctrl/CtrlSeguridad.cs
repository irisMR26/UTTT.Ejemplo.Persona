using System;
using System.Text.RegularExpressions;

namespace UTTT.Ejemplo.Persona.Control.Ctrl
{
    public class CtrlSeguridad
    {
        private readonly Regex sqlInjectionRegex = new Regex(@"('(''|[^'])*')|(\b(ALTER|alter|Alter|CREATE|create|Create|DELETE|delete|Delete|DROP|drop|Drop|EXEC(UTE){0,1}|exec(ute){0,1}|Exec(ute){0,1}|INSERT( +INTO){0,1}|insert( +into){0,1}|Insert( +into){0,1}|MERGE|merge|Merge|SELECT|Select|select|UPDATE|update|Update|UNION( +ALL){0,1}|union( +all){0,1}|Union( +all){0,1})\b)");
        private readonly Regex htmlInjectionRegex = new Regex(@"<.*?>|&.*?;");
        public bool sqlInjectionValida(String _informacion, ref String _mensaje, String _etiquetaReferente, ref System.Web.UI.WebControls.TextBox _control)
        {
            bool isMatch = this.sqlInjectionRegex.IsMatch(_informacion);
            if (isMatch)
            {
                _mensaje = "Caracteres no permitidos en " + _etiquetaReferente.Replace(":", "");
                _control.Focus();
            }
            return isMatch;
        }

        public bool htmlInjectionValida(String _informacion, ref String _mensaje, String _etiquetaReferente, ref System.Web.UI.WebControls.TextBox _control)
        {
            bool isMatch = this.htmlInjectionRegex.IsMatch(_informacion);
            if (isMatch)
            {
                _mensaje = "Caracteres no permitidos en " + _etiquetaReferente.Replace(":", "");
                _control.Focus();
            }
            return isMatch;
        }
    }
}
