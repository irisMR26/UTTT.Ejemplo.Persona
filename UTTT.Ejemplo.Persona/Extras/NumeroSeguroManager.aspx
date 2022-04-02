<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NumeroSeguroManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.Extras.NumeroSeguroManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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

        function nssValido(nss) {
            const re = /^(\d{2})(\d{2})(\d{2})\d{5}$/,
                validado = nss.match(re);

            if (!validado)  // 11 dígitos y subdelegación válida?
                return false;

            const subDeleg = parseInt(validado[1], 10),
                anno = new Date().getFullYear() % 100;
            var annoAlta = parseInt(validado[2], 10),
                annoNac = parseInt(validado[3], 10);

            //Comparar años (excepto que no tenga año de nacimiento)
            if (subDeleg != 97) {
                if (annoAlta <= anno) annoAlta += 100;
                if (annoNac <= anno) annoNac += 100;
                if (annoNac > annoAlta)
                    return false; // Err: se dio de alta antes de nacer!
            }

            return luhn(nss);
        }

        // Algoritmo de Luhn
        //  https://es.wikipedia.org/wiki/Algoritmo_de_Luhn
        function luhn(nss) {
            var suma = 0,
                par = false,
                digito;

            for (var i = nss.length - 1; i >= 0; i--) {
                var digito = parseInt(nss.charAt(i), 10);
                if (par)
                    if ((digito *= 2) > 9)
                        digito -= 9;

                par = !par;
                suma += digito;
            }
            return (suma % 10) == 0;
        }


        //Handler para el evento cuando cambia el input
        //Elimina cualquier caracter no numérico y comprueba validez
        function validarInput(input) {
            var nss = input.value.replace(/\D+/g, ""),
                resultado = document.getElementById("resultado"),
                valido;

            if (nssValido(nss)) { // ⬅️ Acá se comprueba
                valido = "Válido";
                resultado.classList.add("ok");
            } else {
                valido = "No válido";
                resultado.classList.remove("ok");
            }

            resultado.innerText = "NSS: " + nss + "\nFormato: " + valido;
        }

    </script>



</head>
<body>
    <form id="form1" runat="server">
          <div class="navbar bg-dark navbar-dark">
<br />

    
    <div class="container text-white">
            <h1>Numero de seguridad social y enfermedades</h1>
        </div>

        <div class="container text-white">
            <h4>
                  <asp:Label ID="lblAccion" runat="server" Text="Accion" Font-Bold="True"></asp:Label>
            </h4>
        </div>
        </div>
        <br />
        <div class="container">

       <div style="font-family: Arial; font-size: medium; font-weight: bold">
       Ingresa los datos requeridos</div>
            <br />

        <div class="col">
            NSS:<br />
            <asp:TextBox 
                ID="txtnss" runat="server" Width="249px" ViewStateMode="Disabled" oninput="validarInput(this)"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtnss" ValidationGroup="svGuardar" runat="server" ErrorMessage="Campo Vacio"></asp:RequiredFieldValidator>
            <pre id="resultado"></pre>
        </div>

        <div class="col"> 
         Numero de Asegurados:<br />
            <asp:TextBox 
                ID="txtAsegurados" runat="server" Width="249px" ViewStateMode="Disabled" onkeypress="return validaNumeros(event);"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Campo Vacio" ControlToValidate="txtAsegurados" ValidationGroup="svGuardar"></asp:RequiredFieldValidator>
        
            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtAsegurados" ErrorMessage="De 0 a 10" MaximumValue="10" MinimumValue="0" Type="Integer"></asp:RangeValidator>
        
        </div>

        <div class="col">        
            Descripcion de los asegurados:
            <br />
            <asp:TextBox ID="txtDescripcion" runat="server" Width="248px" 
                ViewStateMode="Disabled" requiered pattern ="^([A-Za-zÑñÁáÉéÍíÓóÚú]+['\-]{0,1}[A-Za-zÑñÁáÉéÍíÓóÚú]+)(\s+([A-Za-zÑñÁáÉéÍíÓóÚú]+['\-]{0,1}[A-Za-zÑñÁáÉéÍíÓóÚú]+))*$"></asp:TextBox>
        
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Campo vacio" ControlToValidate="txtDescripcion" ValidationGroup="svGuardar"></asp:RequiredFieldValidator>
        
        </div>

        <div class="col">
            Uso de lentes:
            <br />
            <asp:TextBox ID="txtLentes" runat="server" Width="248px" 
                ViewStateMode="Disabled" requiered pattern ="^([A-Za-zÑñÁáÉéÍíÓóÚú]+['\-]{0,1}[A-Za-zÑñÁáÉéÍíÓóÚú]+)(\s+([A-Za-zÑñÁáÉéÍíÓóÚú]+['\-]{0,1}[A-Za-zÑñÁáÉéÍíÓóÚú]+))*$"></asp:TextBox>
        
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Campo vacio" ControlToValidate="txtLentes" ValidationGroup="svGuardar"></asp:RequiredFieldValidator>
        
        </div>
         <div class="col">
            Tipo de sangre:
             <br />
            <asp:TextBox ID="txtSangre" runat="server" Width="248px" 
                ViewStateMode="Disabled" requiered pattern="^([AaBbOo]|[Aa][Bb])[\+-]"></asp:TextBox>
        
             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Campo vacio" ControlToValidate="txtSangre" ValidationGroup="svGuardar"></asp:RequiredFieldValidator>
        
        </div>

         <div class="col">    
            Peso : 
             <br />
            <asp:TextBox ID="txtPeso" runat="server" Width="248px" 
                ViewStateMode="Disabled" onkeypress="return validaNumeros(event);"></asp:TextBox>
        
             <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Campo Vacio" ControlToValidate="txtPeso" ValidationGroup="svGuardar" MaximumValue="200" MinimumValue="35" Type="Integer"></asp:RangeValidator>
        
        </div>

         <div class="col">        
           Sufrio COVID19
             <br />
            <asp:TextBox ID="txtCovid" runat="server" Width="248px" 
                ViewStateMode="Disabled" requiered pattern ="^([A-Za-zÑñÁáÉéÍíÓóÚú]+['\-]{0,1}[A-Za-zÑñÁáÉéÍíÓóÚú]+)(\s+([A-Za-zÑñÁáÉéÍíÓóÚú]+['\-]{0,1}[A-Za-zÑñÁáÉéÍíÓóÚú]+))*$"></asp:TextBox>
        
             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Campo Vacio" ControlToValidate="txtCovid" ValidationGroup="svGuardar"></asp:RegularExpressionValidator>
        
        </div>
    <div> 
    
        <br />
    
    </div>
    <div>
        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" 
            onclick="btnAceptar_Click" ViewStateMode="Disabled" class="btn btn-small btn-primary" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
            onclick="btnCancelar_Click" ViewStateMode="Disabled" class="btn btn-small btn-danger" CausesValidation="false"/>
      
        </div>
            <br />
    </div>
    </form>
</body>
</html>
