<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.RegistrarUsuarios.RegistroManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
 <meta charset="utf-8">
 <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
   <script  src= "https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <title></title>
     <script type = "text/javascript" >
         function validaNumeros(evt) {
             //Valida la caja de texto con numeros
             var code = (evt.which) ? evt.which : evt.keyCode;
             if (code == 8) {
                 return true;
             } else if (code >= 48 && code <= 57) {
                 return true;
             } else {
                 return false;
             }
         }

         function validaLetras(e) {
             //validar que solo ingrese letras y signos caracteres especiales
             key = e.keyCode || e.which;
             tecla = String.fromCharCode(key).toLowerCase();
             letras = "áéíóúabcdefghijklmnopqrstuvwxyz";
             especiales = "8-37-39-46";
             tecla_especial = false;
             for (var i in especiales) {
                 if (key == especiales[i]) {
                     tecla_especial = true;
                     break;
                 }
             }
             if (letras.indexOf(tecla) == -1 && !tecla_especial) {
                 return false;
             }

         }
     </script>

</head>
<body>
    <form id="form1" runat="server">
             <asp:ScriptManager ID="ScriptManager1" 
                               runat="server" />
 <div class="navbar bg-dark navbar-dark">
<br />
    <div class="container text-white">
            <h1>Registro de usuarios</h1>
        </div>

        <div class="container text-white">
            <h4>
                <asp:Label ID="lblAccion" runat="server" Text="Accion" Font-Bold="True"></asp:Label>
            </h4>
        </div>
        </div>

        <br />
              
 <br />
        
         <div class="container">
             <h4>Ingresa los datos solicitantes</h4>
             <br />
              <div class="row">                 
                 <div class="col">      
                         <asp:Label ID="Label3" runat="server" Text="Persona:" Font-Bold="True"></asp:Label>
                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                         <ContentTemplate>
                             <asp:DropDownList ID="ddlPersona" runat="server"
                                 Height="40px" Width="253px">
                             </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="svGuardar" ControlToValidate="ddlPersona" EnableClientScript="False" ErrorMessage="Elija una persona" InitialValue="-1"></asp:RequiredFieldValidator>
                         </ContentTemplate>
                     </asp:UpdatePanel>
                         <asp:Label ID="Label5" runat="server" Text="Nombre Usuario:" Font-Bold="True"></asp:Label>
                         <br />
                         <asp:TextBox ID="txtNombre" runat="server" Width="249px" ViewStateMode="Disabled" MaxLength="15" requiered pattern="^(?=[A-Za-z]+[0-9]|[0-9]+[A-Za-z])[A-Za-z0-9]{8,12}$"></asp:TextBox>
                         
                         <br />
                         <asp:Label ID="Label6" runat="server" Text=" Contraseña:" Font-Bold="True"></asp:Label>
                         <br />
                         <asp:TextBox runat="server" ID="txtPassword" Width="249px" ViewStateMode="Disabled" MaxLength="15"></asp:TextBox>

                         <br />
                         <asp:Label ID="Label7" runat="server" Text="Repetir Contraseña:" Font-Bold="True"></asp:Label>
                         <br />
                         <asp:TextBox runat="server" ID="txtPassword2" Width="249px" ViewStateMode="Disabled" MaxLength="15"></asp:TextBox>

                         <br />
                         <asp:Label ID="lblEstadoEditar" runat="server" Visible="false" Font-Bold="True">Estado Usuario</asp:Label>
                         <br />
                         <asp:DropDownList ID="ddlEstadoUsuario" runat="server"  Height="40px" Width="253px">
                         </asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlEstadoUsuario" validationGroup="svGuardar" ViewStateMode="Disabled" ErrorMessage="Elija Un Estado De Usuario" MaximumValue="2" MinimumValue="1"></asp:RequiredFieldValidator>
                         <br />
                         <asp:Label ID="lblCalendar2" runat="server" Text="Fecha de Registro:" Font-Bold="True"></asp:Label>
                 &nbsp;
                         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                             <ContentTemplate>
                                 <asp:TextBox ID="txtFechaNac2" placeholder="15/04/2021" runat="server"  MaxLength="10" class="form-control" Height="35px" Width="248px" pattern="(((19|20)([2468][048]|[13579][26]|0[48])|2000)[/-]02[/-]29|((19|20)[0-9]{2}[/-](0[4678]|1[02])[/-](0[1-9]|[12][0-9]|30)|(19|20)[0-9]{2}[/-](0[1359]|11)[/-](0[1-9]|[12][0-9]|3[01])|(19|20)[0-9]{2}[/-]02[/-](0[1-9]|1[0-9]|2[0-8])))"></asp:TextBox>
                                 <asp:ImageButton ID="imbtnCalendar" runat="server" Height="16px" ImageUrl="~/Images/delrecord_16x16.png" Width="16px"/>
                                 <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy/MM/dd" PopupButtonID="imbtnCalendar" PopupPosition="BottomRight" TargetControlID="txtFechaNac2">
                                 </ajaxToolkit:CalendarExtender>
                                 <asp:Label ID="lblCalendario" runat="server" ForeColor="Red" Text="." Visible="False"></asp:Label>
                                 <asp:RequiredFieldValidator ID="rfvFechaNac" runat="server" ControlToValidate="txtFechaNac2" ErrorMessage="Seleccione la fecha"></asp:RequiredFieldValidator>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtFechaNac2" ErrorMessage="Fecha invalida" ValidationExpression="(((19|20)([2468][048]|[13579][26]|0[48])|2000)[/-]02[/-]29|((19|20)[0-9]{2}[/-](0[4678]|1[02])[/-](0[1-9]|[12][0-9]|30)|(19|20)[0-9]{2}[/-](0[1359]|11)[/-](0[1-9]|[12][0-9]|3[01])|(19|20)[0-9]{2}[/-]02[/-](0[1-9]|1[0-9]|2[0-8])))" ForeColor="#000099"></asp:RegularExpressionValidator>
                             </ContentTemplate>
                         </asp:UpdatePanel>
                         <br />
                         <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" 
            onclick="btnAceptar_Click" ViewStateMode="Disabled" OnClientClick="return valid();" class="btn btn-primary"/>
        &nbsp;&nbsp;&nbsp;
                         <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
            onclick="btnCancelar_Click" ViewStateMode="Disabled" class="btn btn-warning" CausesValidation="false"/>
                         <br />
                         <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                         <br />
                         <asp:Label ID="lblUser" runat="server"></asp:Label>
                         <div class="container body-content">
                         </div>
                 </div>
     
    </form>
</body>
</html>
