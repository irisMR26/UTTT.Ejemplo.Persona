<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LibrosManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.Libreria.LibrosManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
 <meta charset="utf-8">
 <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
   <script  src= "https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
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

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <asp:ScriptManager ID="ScriptManager1" 
                               runat="server" />

           <div class="navbar bg-dark navbar-dark">
    <br />
 
    <div class="container text-white">
            <h1>Administracion de libros</h1>
        </div>
   </div>
<br />
       
        <div class="container">
            <h4>
               <asp:Label ID="lblAccion" runat="server" Text="Accion" Font-Bold="True"></asp:Label>
            </h4>
        </div>

        <br />

            <div class="container">

    <div style="font-family: Arial; font-size: medium; font-weight: bold">
   <asp:Label ID="lblPersona" runat="server" Text="Cuidado con los datos a ingresar solo podras ingrsar una vez y no podras editar" Font-Bold="True"></asp:Label>
    </div>
              
 <br />
         
             <div class="row">
             <div class="col">
             <asp:Label ID="Label3" runat="server" Text="Persona:" Font-Bold="True"></asp:Label>
                    
                     <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                         <ContentTemplate>
                             <asp:DropDownList ID="ddlPersona" runat="server"
                                 Height="40px" Width="253px"
                                 >
                             </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPersona" EnableClientScript="False" ErrorMessage="Elija una ubicacion" ValidationGroup="svGuardar"></asp:RequiredFieldValidator>
                         </ContentTemplate>
                     </asp:UpdatePanel>
                 </div>


             <asp:Label ID="Label1" runat="server" Text="Ubicacion:" Font-Bold="True"></asp:Label>
                    
                     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                         <ContentTemplate>
                             <asp:DropDownList ID="ddlPertenencia" runat="server"
                                 Height="40px" Width="253px"
                                 >
                             </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlPertenencia" EnableClientScript="False" ErrorMessage="Elija una ubicacion" ValidationGroup="svGuardar"></asp:RequiredFieldValidator>
                         </ContentTemplate>
                     </asp:UpdatePanel>
                

                <asp:Label ID="Label2" runat="server" Text="Categoria:" Font-Bold="True"></asp:Label>
                    
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                         <ContentTemplate>
                             <asp:DropDownList ID="ddlCategoria" runat="server"
                                 Height="40px" Width="253px"
                                 >
                             </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="svGuardar" ControlToValidate="ddlCategoria" EnableClientScript="False" ErrorMessage="Elija una ubicacion"></asp:RequiredFieldValidator>
                         </ContentTemplate>
                     </asp:UpdatePanel>
                 <div class ="col">
                <asp:Label ID="Label5" runat="server" Text="Nombre del libro:" Font-Bold="True"></asp:Label>
                     <br />
                <asp:TextBox ID="txtNombre" runat="server" Width="249px" ViewStateMode="Disabled" requiered pattern="^(?=[A-Za-z]+[0-9]|[0-9]+[A-Za-z])[A-Za-z0-9]{8,12}$"></asp:TextBox>
                 <br />
                <asp:Label ID="Label6" runat="server" Text=" Paginas:" Font-Bold="True"></asp:Label>
                     <br />
                <asp:TextBox runat="server" ID="txtPaginas" Width="249px" ViewStateMode="Disabled" onkeypress="return validaNumeros(event);" ></asp:TextBox>
                 <br />
                <asp:Label ID="Label7" runat="server" Text="Publicacion" Font-Bold="True"></asp:Label>
                     <br />
                <asp:TextBox runat="server" ID="txtPublicacion" Width="249px" ViewStateMode="Disabled" requiered pattern="^(?=[A-Za-z]+[0-9]|[0-9]+[A-Za-z])[A-Za-z0-9]{8,12}$"></asp:TextBox>
            </div>
                 <div class="form-group">
                 <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                     <br />
                     <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" 
            onclick="btnAceptar_Click" ViewStateMode="Disabled" OnClientClick="return valid();" class="btn btn-primary"/>
        &nbsp;&nbsp;&nbsp;
                     <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
            onclick="btnCancelar_Click" ViewStateMode="Disabled" class="btn btn-warning" CausesValidation="false"/>
                     <br />
                     <br />
                     <asp:Label ID="lblUser" runat="server"></asp:Label>
                     <div class="container body-content">
                     </div>

                 </div>
        </div>
        </div>
    </form>
</body>
</html>
