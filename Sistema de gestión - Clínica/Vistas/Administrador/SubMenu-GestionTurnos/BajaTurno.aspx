<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BajaTurno.aspx.cs" Inherits="Vistas.BajaTurno" %>

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
        .auto-style9 {
            width: 1100px;
        }
        .auto-style10 {
            width: 1617px;
        }
        .auto-style11 {
            width: 1617px;
            height: 23px;
        }
        .auto-style12 {
            width: 100%;
        }
        .auto-style13 {
            width: 172px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="auto-style9">
                <tr>
                    <td class="auto-style10">&nbsp;
                        <asp:Panel ID="Panel1" runat="server">
                            &nbsp;<table class="auto-style12">
                                <tr>
                                    <td class="auto-style13">
                                        <asp:HyperLink ID="hlAltaTurno" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionTurnos/AltaTurno.aspx">Alta Turno</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hlBajaTurno" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionTurnos/BajaTurno.aspx">Baja Turno</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hlModificacionTurno" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionTurnos/ModificacionTurno.aspx">Modificación Turno</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hpListarTurnos" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionTurnos/ListarTurnos.aspx">Listar Turnos</asp:HyperLink>
                                    </td>
                                    <td>Usuario:
                                        <asp:Label ID="lblUsuarioAdministrador" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10">&nbsp;&nbsp;
                        </td>
                </tr>
                <tr>
                    <td class="auto-style10">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblBajaTurno" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Cancelación de Turno"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style10">
                        Código de turno:&nbsp; <asp:TextBox ID="txtCodigoTurno" runat="server" Width="108px" TextMode="Number" ValidationGroup="BajaTurno"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvTxtCodigoTurno" runat="server" ControlToValidate="txtCodigoTurno" Display="Dynamic" ErrorMessage="Debe ingresar un código de turno." ValidationGroup="BajaTurno">*</asp:RequiredFieldValidator>
&nbsp;<asp:Button ID="btnCancelarTurno" runat="server" Text="Cancelar" Width="83px" OnClick="btnCancelarTurno_Click" ValidationGroup="BajaTurno" />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style11">
                        <asp:Label ID="lblResultadoBaja" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style11">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="BajaTurno" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style11">
                        <asp:HyperLink ID="hlGestionTurnos" runat="server" NavigateUrl="~/Administrador/SubMenu-GestionTurnos/MenuGestionTurnos.aspx">Regresar a Menú de Gestión de Turnos...</asp:HyperLink>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
