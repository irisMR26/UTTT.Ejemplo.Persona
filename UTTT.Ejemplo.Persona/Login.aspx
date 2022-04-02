<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UTTT.Ejemplo.Persona.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <form id="form1" runat="server">
<div class="navbar bg-dark navbar-dark">
<br />

    
    <div class="container text-white">
            <h1>Bienvenido</h1>
        </div>

        <div class="container text-white">
            <h4>
                <asp:Label ID="lblAccion" runat="server" Text="Accion" Font-Bold="True">Ingresa los datos que se solicitan</asp:Label>
            </h4>
        </div>
        </div>

        <div class="container">

        <div class="row w-25">

            <div class="col">   
                <br />
               <h4> Usuario: </h4>
          <asp:TextBox ID="txtEmail" runat="server" Width="253px" Height="25px"></asp:TextBox>
            </div>

            <br />
            <br />

            <br />
            <br />
            <div class="col">
               <h4> Contraseña: </h4><asp:TextBox ID="txtContraseña" runat="server" Width="253px" Height="25px" type="password"></asp:TextBox>
            </div>

            <br />
            <br />
   </div>
            <div>

            </div>
            <br />
            <div class="col">
            <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesion" OnClick="btnLogin_Click"  class="btn btn-success btn-primary"/>
                </div>
            <br />
            <div>
            </div>

            <div class="row">
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" class="btn-lg">Recuperar contraseña</asp:LinkButton>
            </div>

            <br />

            <asp:Label ID="lblErrores" runat="server" Text=""></asp:Label>
           
     

  </div>

    </form>
</body>
</html>
