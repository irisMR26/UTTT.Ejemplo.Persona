<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContraPrincipal.aspx.cs" Inherits="UTTT.Ejemplo.Persona.RegistrarUsuarios.ContraPrincipal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
  <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js" integrity="sha384-IQsoLXl5PILFhosVNubq5LC7Qb9DXgDA9i+tQ8Zj3iwWAwPtgFTxbJ8NT4GN1R8p" crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.min.js" integrity="sha384-cVKIPhGWiC2Al4u+LWgxfKTRIcfu0JTxR+EQDz/bgldoEyl4H0zUF0QKbrJ0EcQF" crossorigin="anonymous"></script>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
 <meta charset="utf-8">
 <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title></title>

</head>
<body>
	<form class="form" id="form1" runat="server">
    <div class="navbar bg-dark navbar-dark">
<br />
    <div class="container text-white">
            <h1>Bienvenido</h1>
        </div>

        <div class="container text-white">
            <h4>
                <asp:Label ID="lblAccion" runat="server" Text="Accion" Font-Bold="True">Recuperacion de contraseña</asp:Label>
            </h4>
        </div>
        </div>
        <br />
               <div class="container">
                   <div class="row">
                <div class="user">
                    <div class="form-wrap">
                        <!-- TABS -->
                    	<div class="col">
                            <h4 class="login-tab log-in active"><span>Escriba su correo electronico</span></h4>
                    	</div>
                        <!-- TABS CONTENT -->
                    	<div class="tabs-content">
                            <!-- TABS CONTENT LOGIN -->
                    		<div id="login-tab-content" class="active">
                    		        <div class="col">
                                    <asp:TextBox ID="txtCorreoElectronico" runat="server" class="input" placeholder="Email" Width="253px" Height="25px"> </asp:TextBox> 
                                    </div>
                                <br />
                        <asp:Button class="btn btn-sm btn-success" ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" Text="Aceptar" /> 
                                    &nbsp;&nbsp; 
                     <asp:Button class="btn btn-sm btn-danger" ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar"/>
                     <br />
                                    <br />
                      <div class="mb-2">
                       <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                 </div>
                    			
                    		</div>
                           
                    	</div>
                	</div>
                </div>
                   </div>
                   </div>
        </form>
</body>
</html>
