<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Vistas.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
<style>
    body {
        background: linear-gradient(135deg, #f5f7fa, #c3cfe2); /* Fondo con degradado suave */
        font-family: 'Segoe UI', Arial, sans-serif; /* Tipografía moderna, clara */
        color: #333; /* Gris oscuro legible */
    }

    .container {
        width: 340px; /* Ancho cómodo */
        margin: 60px auto; /* Centrado */
        background: rgba(255, 255, 255, 0.9); /* Blanco semitransparente para suavidad */
        padding: 25px; /* Espaciado interno */
        border-radius: 12px; /* Bordes redondeados */
        box-shadow: 0 8px 20px rgba(0,0,0,0.1); /* Sombra más definida */
        backdrop-filter: blur(3px); /* Efecto de vidrio esmerilado */
    }

    h1 {
        text-align: center; /* Centrado */
        font-size: 22px; /* Tamaño claro */
        margin-bottom: 18px; /* Espaciado abajo */
        color: #2b5876; /* Azul grisáceo elegante */
    }

    label {
        font-weight: 600; /* Negrita moderada */
        color: #4a4a4a; /* Gris medio */
    }

    .input-field {
        width: 90%; /* Ocupa todo el ancho */
        padding: 10px;; /* Espaciado interno */
        margin: 8px 0 16px 0; /* Margen vertical */
        border: 1px solid #b0c4de; /* Azul gris suave */
        border-radius: 6px; /* Bordes redondeados */
        background: #f9fbfd; /* Fondo azul muy claro */
        color: #333; /* Texto gris oscuro */
        transition: border-color 0.3s, box-shadow 0.3s; /* Transiciones suaves */
    }

    .input-field:focus {
        outline: none; /* Sin contorno */
        border-color: #6ca0dc; /* Azul claro */
        box-shadow: 0 0 5px rgba(108, 160, 220, 0.5); /* Sombra azul suave */
    }

    .btn {
        width: 100%; /* Todo el ancho */
        padding: 12px; /* Espaciado */
        background: linear-gradient(135deg, #81e6d9, #38b2ac); /* Degradado verde agua a turquesa */
        border: none; /* Sin bordes */
        color: white; /* Texto blanco */
        font-weight: bold; /* Negrita */
        border-radius: 6px; /* Bordes redondeados */
        cursor: pointer; /* Manito */
        font-size: 16px; /* Tamaño visible */
        transition: background 0.3s, transform 0.2s; /* Transición suave */
    }

    .btn:hover {
        background: linear-gradient(135deg, #38b2ac, #319795); /* Oscurece al pasar */
        transform: scale(1.02); /* Aumenta levemente */
    }

    .link {
        display: block; /* Línea completa */
        text-align: center; /* Centrado */
        margin-top: 15px; /* Espaciado arriba */
        color: #38b2ac; /* Turquesa */
        text-decoration: none; /* Sin subrayado */
        font-weight: 500; /* Un poco más marcado */
        transition: color 0.3s; /* Transición suave */
    }

    .link:hover {
        color: #2c7a7b; /* Turquesa más oscuro */
        text-decoration: underline; /* Subrayado al pasar */
    }
</style>

</head>
<body>

    <form id="form1" runat="server">
        <div class="container">
            <h1>Sistema de Gestión Clínica Médica</h1> <!-- Título centrado -->

            <table style="width:100%;">
                <tr>
                    <td>Usuario:</td>
                    <td>
                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="input-field"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Contraseña:</td>
                    <td>
                        <asp:TextBox ID="txtContraseña" runat="server" TextMode="Password" CssClass="input-field"></asp:TextBox>
                        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td> <!-- Vacío para mantener estructura y alinear -->
                    <td>
                        <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn" OnClick="btnIngresar_Click" />
                    </td>
                </tr>
                <tr>
                    <td></td> <!-- Vacío para mantener estructura y alinear -->
                    <td>
                        <asp:HyperLink ID="hlNuevoUsuario" runat="server" NavigateUrl="~/NuevoUsuario.aspx" CssClass="link">
                            Crear Nuevo Usuario
                        </asp:HyperLink>
                    </td>
                </tr>
            </table>
        </div>
    </form>

</body>
</html>
