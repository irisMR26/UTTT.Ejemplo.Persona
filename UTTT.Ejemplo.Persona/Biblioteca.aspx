<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Biblioteca.aspx.cs" Inherits="UTTT.Ejemplo.Persona.Biblioteca" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
  <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js" integrity="sha384-IQsoLXl5PILFhosVNubq5LC7Qb9DXgDA9i+tQ8Zj3iwWAwPtgFTxbJ8NT4GN1R8p" crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.min.js" integrity="sha384-cVKIPhGWiC2Al4u+LWgxfKTRIcfu0JTxR+EQDz/bgldoEyl4H0zUF0QKbrJ0EcQF" crossorigin="anonymous"></script>
 <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title></title>
</head>
<body class="bg-light">
     <form id="form1" runat="server">
     
     <div class="navbar bg-dark navbar-dark">
     <br />
    <div class="container text-white">
            <h1>Bienvenido : </h1>
        </div>

        <div class="container text-white">
            <h4>
                 <asp:Label ID="lblUsuario" runat="server"></asp:Label>
            </h4>
        </div>

        </div>
           
        <div>

              <div class="container">
       <div class="row mt-1">
           
       </div>
        <div class="row justify-content-md-center  mt-3">
            <div class="col-md-5">
                <div class="efecto">
                <div class="card">
                    <img src="../Images/brinda-libreria-caracter-servicio-en-planteles-uaeh-fuera-de-zona-metropolitana.jpg" class="card-img-top" alt="persona" height="250" />
                    <div class="sep">
                        <div class="wrapper">
                        </div>
                   </div>
                </div>
                    <br />
                     <p class="slogan" style="text-align: justify;"><span><h4> Recuerda seleccionar un boton a visitar para cualquier accion que deseas realizar </h4> </span>.</p>
                     <asp:LinkButton ID="LinkButton4" runat="server" href="PersonaPrincipal.aspx">Registro de Personal</asp:LinkButton>
             &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton5" runat="server" href="RegistrarUsuarios/RegistroPrincipal.aspx">Registro de Usuarios</asp:LinkButton>
             &nbsp;&nbsp;
              <asp:LinkButton ID="LinkButton1" runat="server" href="Libreria/LibrosPrincipal.aspx">Registro de Libros</asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click">Salir</asp:LinkButton>
            </div>
            </div>
        </div>
        <div class="row mt-3">
            <div class="mt-5">
           
            </div>
        </div>
    </div>
        </div>



    </form>
</body>
</html>
