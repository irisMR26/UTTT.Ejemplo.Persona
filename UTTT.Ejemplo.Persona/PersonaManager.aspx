<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonaManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.PersonaManager" debug=false%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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

         function curpValida(curp) {
             var re = /^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$/,
                 validado = curp.match(re);

             if (!validado)  //Coincide con el formato general?
                 return false;

             //Validar que coincida el dígito verificador
             function digitoVerificador(curp17) {
                 //Fuente https://consultas.curp.gob.mx/CurpSP/
                 var diccionario = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ",
                     lngSuma = 0.0,
                     lngDigito = 0.0;
                 for (var i = 0; i < 17; i++)
                     lngSuma = lngSuma + diccionario.indexOf(curp17.charAt(i)) * (18 - i);
                 lngDigito = 10 - lngSuma % 10;
                 if (lngDigito == 10) return 0;
                 return lngDigito;
             }

             if (validado[2] != digitoVerificador(validado[1]))
                 return false;

             return true; //Validado
         }


         //Handler para el evento cuando cambia el input
         //Lleva la CURP a mayúsculas para validarlo
         function validarInput(input) {
             var curp = input.value.toUpperCase(),
                 resultado = document.getElementById("resultado"),
                 valido = "No válido";

             if (curpValida(curp)) { // ⬅️ Acá se comprueba
                 valido = "Válido";
                 resultado.classList.add("ok");
             } else {
                 resultado.classList.remove("ok");
             }

             resultado.innerText = "CURP: " + curp + "\nFormato: " + valido;
         }

     </script>
</head>
<body>
    <form id="form1" runat="server" class="aling-content-center">
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="navbar bg-dark navbar-dark">
<br />

    
    <div class="container text-white">
            <h1>Persona</h1>
        </div>

        <div class="container text-white">
            <h4>
                <asp:Label ID="lblAccion" runat="server" Text="Accion" Font-Bold="True"></asp:Label>
            </h4>
        </div>
        </div>

        <br />
        <div class="container">
            <div class="row">
                <div class="col">
                         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate> 
                    Sexo:<br /> <asp:DropDownList ID="ddlSexo" runat="server"
                Height="40px" Width="253px" class="btn btn-outline-warning text-black">
            </asp:DropDownList>
         <asp:RangeValidator ID="RangeValidator2" runat="server"  ValidationGroup="svGuardar" ViewStateMode="Disabled" ControlToValidate="ddlSexo" ErrorMessage="Sexo requerido" MaximumValue="2" MinimumValue="1" ></asp:RangeValidator>
                &nbsp;<asp:RangeValidator ID="RangeValidator3" runat="server"   ValidationGroup="svGuardar" ControlToValidate="ddlSexo" ErrorMessage="Vacio" MaximumValue="2" MinimumValue="1" Type="Integer"></asp:RangeValidator>    
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;
         </div>
                      </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlSexo" EventName="SelectedIndexChanged" />
                </Triggers>
                </asp:UpdatePanel>
         </div>

                 <div class="col">
                         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate> 
                    Estado Civil:<br /> <asp:DropDownList ID="ddlEstadoCivil" runat="server"
                Height="40px" Width="253px" class="btn btn-outline-warning text-black">
            </asp:DropDownList>
         <asp:RangeValidator ID="RangeValidator4" runat="server"  ValidationGroup="svGuardar" ViewStateMode="Disabled" ControlToValidate="ddlEstadoCivil" ErrorMessage="Requerido" MaximumValue="4" MinimumValue="1" ></asp:RangeValidator>
                &nbsp;<asp:RangeValidator ID="RangeValidator5" runat="server"   ValidationGroup="svGuardar" ControlToValidate="ddlEstadoCivil" ErrorMessage="Vacio" MaximumValue="4" MinimumValue="1" Type="Integer"></asp:RangeValidator>    
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;
         </div>
                      </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlEstadoCivil" EventName="SelectedIndexChanged" />
                </Triggers>
                </asp:UpdatePanel>
        

            
        

        <div class="col"> 
        
                Clave Unica:<br />
                <asp:TextBox ID="txtClaveUnica" runat="server" 
                Width="253px" Height="25px" ViewStateMode="Disabled" onkeypress="return validaNumeros(event);" Class="btn btn-outline-dark"   ></asp:TextBox> <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="svGuardar" ControlToValidate="txtClaveUnica" ErrorMessage="Clave Requerida"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtClaveUnica" ErrorMessage="Solo de 100 a 999" MaximumValue="999" MinimumValue="100" Type="Integer"></asp:RangeValidator>
          </div>
        
        

        <div class="col">
        
           Nombre:<br />
            <asp:TextBox 
                ID="txtNombre" runat="server" Width="253px" Height="25px" Class="btn btn-outline-dark" ViewStateMode="Disabled" requiered pattern ="^([A-Za-zÑñÁáÉéÍíÓóÚú]+['\-]{0,1}[A-Za-zÑñÁáÉéÍíÓóÚú]+)(\s+([A-Za-zÑñÁáÉéÍíÓóÚú]+['\-]{0,1}[A-Za-zÑñÁáÉéÍíÓóÚú]+))*$"></asp:TextBox>
              
            <asp:RequiredFieldValidator ID="rvfNombre" runat="server" ValidationGroup="svGuardar" ControlToValidate="txtNombre" ErrorMessage="Nombre obligatorio" ></asp:RequiredFieldValidator>
         </div>

       
        <div class="col"> 
                A Paterno:<br />
                <asp:TextBox 
                ID="txtAPaterno" runat="server" Width="253px" Height="25px" ViewStateMode="Disabled" Class="btn btn-outline-dark" requiered pattern ="^([A-Za-zÑñÁáÉéÍíÓóÚú]+['\-]{0,1}[A-Za-zÑñÁáÉéÍíÓóÚú]+)(\s+([A-Za-zÑñÁáÉéÍíÓóÚú]+['\-]{0,1}[A-Za-zÑñÁáÉéÍíÓóÚú]+))*$"></asp:TextBox>
             
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="svGuardar" ControlToValidate="txtAPaterno" ErrorMessage="A Paterno obligatorio" ></asp:RequiredFieldValidator>
         </div>

        <div class="col">
                A Materno:<br />
                <asp:TextBox 
                ID="txtAMaterno" runat="server" Width="253px" Height="25px" ViewStateMode="Disabled" Class="btn btn-outline-dark" requiered pattern ="^([A-Za-zÑñÁáÉéÍíÓóÚú]+['\-]{0,1}[A-Za-zÑñÁáÉéÍíÓóÚú]+)(\s+([A-Za-zÑñÁáÉéÍíÓóÚú]+['\-]{0,1}[A-Za-zÑñÁáÉéÍíÓóÚú]+))*$"></asp:TextBox>

            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="svGuardar" ControlToValidate="txtAMaterno" ErrorMessage="A Materno obligatorio" ></asp:RequiredFieldValidator>  
          </div>
  
        <div class="col">
           CURP:<br />
            <asp:TextBox ID="txtCURP" runat="server" Width="253px"  Class="btn btn-outline-dark" Height="25px" ViewStateMode="Disabled" oninput="validarInput(this)"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ValidationGroup="svGuardar" ControlToValidate="txtCURP" ErrorMessage="CURP obligatorio" ></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revCURP" runat="server" ControlToValidate="txtCURP" ErrorMessage="La CURP es incorrecta" ValidationExpression="^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$"></asp:RegularExpressionValidator>
    </div>
          <div class="col">
           Email:<br />
            <asp:TextBox ID="txtCorreo" runat="server" Width="253px"  Class="btn btn-outline-dark" Height="25px" ViewStateMode="Disabled" pattern="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:TextBox>
                 <asp:RegularExpressionValidator ID="RevCorreo" runat="server" ControlToValidate="txtCorreo" ErrorMessage="Correo Invalido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
    </div>
     
        <div class="col">
                Fecha nacimiento:<br />
        <asp:TextBox ID="txtFechaNacimiento" runat="server" Height="25px" Width="253px" Class="btn btn-outline-danger text-black"></asp:TextBox>
        <asp:ImageButton ID="imgPopup" ImageUrl="Images/delrecord_16x16.png" ImageAlign="Bottom" runat ="server" CausesValidation="False" Height="15px" Width="15px" />
        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtFechaNacimiento" Format="MM/dd/yyyy" />
        </div>


           <div class=""> 
      <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
    </div>
    <br />
    <div class="col btn-group">
        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" class="btn btn-success btn-sm" 
            onclick="btnAceptar_Click" ViewStateMode="Disabled"  />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-danger btn-sm" 
            onclick="btnCancelar_Click" ViewStateMode="Disabled" CausesValidation="false" /> 
             
        </div>

                </div>


        </div>
    
        </form>

</body>
</html>