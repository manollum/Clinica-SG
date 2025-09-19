<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NuevoUsuario.aspx.cs" Inherits="Vistas.NuevoUsuario" UnobtrusiveValidationMode="None" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>Nuevo Usuario Médico</title>

<style type="text/css">
    body {
        background: linear-gradient(135deg, #f5f7fa, #c3cfe2);
        font-family: 'Segoe UI', Arial, sans-serif;
        color: #333;
        margin: 0;
        padding: 0;
    }

    .form-container {
        background: rgba(255, 255, 255, 0.95);
        border-radius: 10px;
        padding: 30px;
        max-width: 500px;
        margin: 50px auto;
        box-shadow: 0 6px 15px rgba(0, 0, 0, 0.1);
    }

    h1 {
        text-align: center;
        font-size: 24px;
        margin-bottom: 20px;
    }

    .auto-style1 {
        width: 100%;
    }

    .input-field {
        width: 100%;
        padding: 8px;
        margin: 6px 0 12px 0;
        border: 1px solid #ccc;
        border-radius: 4px;
        font-size: 14px;
    }

    .btn-submit {
        width: 100%;
        padding: 10px;
        background-color: #4CAF50;
        color: white;
        border: none;
        font-size: 16px;
        cursor: pointer;
        border-radius: 4px;
    }

    .btn-submit:hover {
        background-color: #45a049;
    }

    .link-back {
        display: block;
        margin-top: 10px;
        text-align: center;
        text-decoration: none;
        color: #333;
    }

    .error {
        color: red;
        font-size: 13px;
    }

    label {
        font-size: 14px;
    }
</style>
</head>

<body>
    <form id="form1" runat="server">
        <div class="form-container">
            <h1>Nuevo Usuario Médico</h1>

            <asp:HyperLink ID="hlVolver" runat="server" NavigateUrl="~/Login.aspx" CssClass="link-back">Volver al Login</asp:HyperLink>

            <table class="auto-style1">
                <tr>
                    <td>
                        <label>Nombre de Usuario:</label><br />
                        <asp:TextBox ID="txtNuevoUsuario" runat="server" CssClass="input-field" MaxLength="16" ValidationGroup="NuevoUsuario"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvUsuarioNuevo" runat="server" ControlToValidate="txtNuevoUsuario" ForeColor="Red" CssClass="error" ErrorMessage="Ingrese nombre de Usuario" ValidationGroup="NuevoUsuario" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="revNuevoUsuario" runat="server" ControlToValidate="txtNuevoUsuario" ValidationExpression="^\S+$" ForeColor="Red" CssClass="error" ErrorMessage="No ingrese espacios en blanco" ValidationGroup="NuevoUsuario" Display="Dynamic" />
                    </td>
                </tr>

                <tr>
                    <td>
                        <label>Contraseña:</label><br />
                        <asp:TextBox ID="txtNuevaContraseña" runat="server" CssClass="input-field" MaxLength="30" TextMode="Password" ValidationGroup="NuevoUsuario"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvContraseña" runat="server" ControlToValidate="txtNuevaContraseña" ForeColor="Red" CssClass="error" ErrorMessage="Ingrese una contraseña" ValidationGroup="NuevoUsuario" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="revContraseña" runat="server" ControlToValidate="txtNuevaContraseña" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d])[^\s]+$" ForeColor="Red" CssClass="error" ErrorMessage="Debe tener mayúscula, minúscula, número, carácter especial y sin espacios" ValidationGroup="NuevoUsuario" Display="Dynamic" />
                    </td>
                </tr>

                <tr>
                    <td>
                        <label>Reingrese la Contraseña:</label><br />
                        <asp:TextBox ID="txtNuevaContraseña2" runat="server" CssClass="input-field" MaxLength="30" TextMode="Password" ValidationGroup="NuevoUsuario"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvContraseña2" runat="server" ControlToValidate="txtNuevaContraseña2" ForeColor="Red" CssClass="error" ErrorMessage="Debe ingresar la contraseña nuevamente" ValidationGroup="NuevoUsuario" Display="Dynamic" />
                        <asp:CompareValidator ID="cvContraseñas" runat="server" ControlToCompare="txtNuevaContraseña" ControlToValidate="txtNuevaContraseña2" ForeColor="Red" CssClass="error" ErrorMessage="Las contraseñas deben coincidir" ValidationGroup="NuevoUsuario" Display="Dynamic" />
                    </td>
                </tr>

                <tr>
                    <td>
                        <label>Legajo Médico:</label><br />
                        <asp:TextBox ID="txtLegajoMedico" runat="server" CssClass="input-field" TextMode="Number" ValidationGroup="NuevoUsuario"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvLegajo" runat="server" ControlToValidate="txtLegajoMedico" ForeColor="Red" CssClass="error" ErrorMessage="Ingrese un legajo" ValidationGroup="NuevoUsuario" Display="Dynamic" />
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Button ID="btnAgregarUsuarioMedico" runat="server" Text="Agregar Usuario Médico" CssClass="btn-submit" ValidationGroup="NuevoUsuario" OnClick="btnAgregarUsuarioMedico_Click" />
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="lblContraseña" runat="server" ForeColor="Red" CssClass="error"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
