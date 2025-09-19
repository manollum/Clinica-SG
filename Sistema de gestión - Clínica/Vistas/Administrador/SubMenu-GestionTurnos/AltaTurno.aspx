<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AltaTurno.aspx.cs" Inherits="Vistas.Administrador.SubMenu_GestionTurnos.AltaTurno" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
                body {
        background: linear-gradient(135deg, #e0f7fa, #b3e5fc); /* degradado azul claro */
        font-family: 'Segoe UI', Arial, sans-serif; /* tipografía moderna */
        color: #333; /* texto gris oscuro */
        margin: 0;
        padding: 0;
             }

        .auto-style1 {
            width: 100%;
            height: 100%;
            margin-bottom: 0px;
        }
        .auto-style6 {
            width: 1770px;
            height: 21px;
        }
        .auto-style3 {
            width: 1770px;
        }
        .auto-style5 {
            width: 1770px;
            height: 65px;
        }
        .auto-style10 {
            height: 26px;
            width: 1017px;
        }
        .auto-style12 {
            width: 883px;
        }
        .auto-style13 {
            width: 1017px;
        }
        .auto-style14 {
            width: 1770px;
            height: 27px;
        }
        .auto-style15 {
            width: 1770px;
            height: 17px;
        }
        .auto-style17 {
            width: 1100px;
            height: 100%;
            margin-bottom: 0px;
        }
        .auto-style19 {
            width: 56%;
            height: 100%;
            margin-bottom: 0px;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
        <div>
            <table class="auto-style17">
                <tr>
                    <td class="auto-style10">
                        &nbsp;
                        <asp:HyperLink ID="hlAltaTurno" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionTurnos/AltaTurno.aspx">Alta Turno</asp:HyperLink>
                    </td>
                    <td class="auto-style10">
                        <asp:HyperLink ID="hlBajaTurno" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionTurnos/BajaTurno.aspx">Baja Turno</asp:HyperLink>
                    </td>
                    <td class="auto-style10">
                        <asp:HyperLink ID="hlModificacionTurno" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionTurnos/ModificacionTurno.aspx">Modificación Turno</asp:HyperLink>
                    </td>
                    <td class="auto-style10">
                        <asp:HyperLink ID="hlListarTurno" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionTurnos/ListarTurnos.aspx">Listar Turno</asp:HyperLink>
                    </td>
                    <td class="auto-style10">
                        Usuario: <asp:Label ID="lblUsuarioAdministrador" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style13">
                        &nbsp;</td>
                    <td class="auto-style13">
                        &nbsp;</td>
                    <td class="auto-style13">
                        &nbsp;</td>
                    <td class="auto-style13">
                        &nbsp;</td>
                    <td class="auto-style13">
                        &nbsp;</td>
                </tr>
            </table>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblTituloAltaTurno" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Alta de un Turno:"></asp:Label>
            <br />
            <table class="auto-style19">
                <tr>
                    <td class="auto-style12">
            <table class="auto-style1">
                <tr>
                    <td class="auto-style6" style="font-size: medium; text-decoration: underline overline"></td>
                </tr>
                <tr>
                    <td class="auto-style14" style="font-size: small; text-decoration: blink">&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; Especialidad:&nbsp; &nbsp;
                        <asp:DropDownList ID="ddlEspecialidad" runat="server" AutoPostBack="True" Height="20px" OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged" Width="202px">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Médico:&nbsp;
                        <asp:DropDownList ID="ddlMedico" runat="server" AutoPostBack="True" Height="20px" Width="202px" OnSelectedIndexChanged="ddlMedico_SelectedIndexChanged">
                            
                        </asp:DropDownList>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3" style="font-size: small; text-decoration: blink">&nbsp; &nbsp; &nbsp;&nbsp; DNI Paciente:&nbsp;&nbsp;
                        <asp:TextBox ID="txtDniPaciente" runat="server" Font-Size="Small" Height="17px" Width="120px" TextMode="Number"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3" style="font-size: small; text-decoration: blink">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Semana:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; <asp:DropDownList ID="ddlSemana" runat="server" Height="20px" Width="100px" OnSelectedIndexChanged="ddlSemana_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Dia: <asp:DropDownList ID="ddlDia" runat="server" Height="20px" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="ddlDia_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;Horario:&nbsp; <asp:DropDownList ID="ddlHorario" runat="server" Height="20px" Width="100px" OnSelectedIndexChanged="ddlHorario_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblError" runat="server" Visible="False" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style15" style="font-size: small; text-decoration: blink">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style15" style="font-size: small; text-decoration: blink">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style6" style="font-size: small; text-decoration: blink">
                                    <asp:Label ID="lblInfoTurnos" runat="server" Font-Bold="True" Font-Size="12pt"></asp:Label>
                                </td>
                </tr>
                <tr>
                    <td class="auto-style5" style="font-size: small; text-decoration: blink">
                        <asp:Button ID="btnAgregarT" runat="server" Text="Agregar Turno" OnClick="btnAgregarT_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3" style="text-decoration: underline overline; font-size: medium">
                        <asp:Label ID="lblResultadoAltaT" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3" style="text-decoration: blink; font-size: small">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style9" style="text-decoration: blink; font-size: small">
                        <asp:HyperLink ID="hlGestionTurnos" runat="server" Font-Size="12pt" NavigateUrl="~/Administrador/SubMenu-GestionTurnos/MenuGestionTurnos.aspx">Regresar a Menú de Gestión de Turnos...</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3" style="font-size: small; text-decoration: blink">
                        <asp:Label ID="lblMensajeErrorLegajoT" runat="server"></asp:Label>
                        <asp:Label ID="lblMensajeErrorDniT" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3" style="font-size: small; text-decoration: blink">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        &nbsp;</td>
                </tr>
                </table>
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </form>
</body>
</html>
