<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DirectorioManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.Extras.DirectorioManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
     <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
   <script  src= "https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-family: Arial; font-size: medium; font-weight: bold">
    
        <div class="navbar bg-dark navbar-dark">
<br />

    
    <div class="container text-white">
            <h1>Contacto con el empleado</h1>
        </div>

        <div class="container text-white">
            <h4>
                    <asp:Label ID="lblAccion" runat="server" Text="Accion" Font-Bold="True"></asp:Label>
            </h4>
        </div>
        </div>
        <br />
    

       <div  class="container">

        <div class="col">
            <br />
        Ingrese un numero de telefono</div>

        <div>
        
            <br />
        
        </div>
        

        <div class="col">        
            Telefono:
            <br />
            <asp:TextBox ID="txtNumero" runat="server" Width="248px" 
                ViewStateMode="Disabled" requiered pattern="^\(?(\d{3})\)?[-]?(\d{3})[-]?(\d{4})$"></asp:TextBox>
        
        </div>
  <br />
    <div> 
        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" 
            onclick="btnAceptar_Click" ViewStateMode="Disabled" class="btn btn-primary btn-sm "/>
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
            onclick="btnCancelar_Click" ViewStateMode="Disabled" class="btn btn-danger btn-sm"/>


             </div>
           <br />
        </div>
    </form>
</body>
</html>
