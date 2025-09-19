<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListarTurnos.aspx.cs" Inherits="Vistas.Administrador.SubMenu_GestionTurnos.ListarTurnos" %>

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
        }
        .auto-style2 {
            width: 663px;
        }
        .auto-style4 {
            width: 1658px;
            height: 23px;
        }
        .auto-style6 {
            width: 663px;
            height: 30px;
        }
        .auto-style8 {
            width: 663px;
            height: 35px;
        }
        .auto-style9 {
            width: 1658px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style4">&nbsp;<asp:Panel ID="Panel1" runat="server">
                        <table class="auto-style1">
                            <tr>
                                <td>
                                    <asp:HyperLink ID="hlAltaTurno" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionTurnos/AltaTurno.aspx">Alta Turno</asp:HyperLink>
                                </td>
                                <td>
                                    <asp:HyperLink ID="hlBajaTurno" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionTurnos/BajaTurno.aspx">Baja Turno</asp:HyperLink>
                                </td>
                                <td>
                                    <asp:HyperLink ID="hlModificacionTurno" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionTurnos/ModificacionTurno.aspx">Modificación Turno</asp:HyperLink>
                                </td>
                                <td>
                                    <asp:HyperLink ID="hlListarTurnos" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionTurnos/ListarTurnos.aspx">Listar Turnos</asp:HyperLink>
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
                    <td class="auto-style4"></td>
                </tr>
                <tr>
                    <td class="auto-style9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Listar Turnos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style9">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style9">Código de turno: <asp:TextBox ID="txtListarTurno" runat="server" Width="135px" TextMode="Number"></asp:TextBox>
&nbsp;<asp:RequiredFieldValidator ID="rfvCodigoTurno" runat="server" ControlToValidate="txtListarTurno" ErrorMessage="Ingrese un numero valido" ValidationGroup="FiltroTurno">*</asp:RequiredFieldValidator>
&nbsp;<asp:Button ID="btnFiltarTurno" runat="server" Text="Buscar" OnClick="btnFiltarTurno_Click" ValidationGroup="FiltroTurno" Width="101px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnLimpiarFiltrosAvanzados" runat="server" Height="27px" Text="Limpiar Filtros" Width="129px" OnClick="btnLimpiarFiltrosAvanzados_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style9">
                        <asp:GridView ID="gvTablaTurnos" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="Vertical" OnPageIndexChanging="gvTablaTurnos_PageIndexChanging" AutoGenerateColumns="False">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="ID Consulta">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_it_IDConsulta" runat="server" Text='<%# Bind("[ID Consulta]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Médico">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_it_Medico" runat="server" Text='<%# Bind("Medico") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Paciente">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_it_Paciente" runat="server" Text='<%# Bind("Paciente") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Turno">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_it_Turno" runat="server" Text='<%# Bind("Turno") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pendiente">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_it_Pendiente" runat="server" Checked='<%# Eval("Pendiente") != DBNull.Value && Convert.ToBoolean(Eval("Pendiente")) %>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Asistencia">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_it_Asistencia" runat="server" Checked='<%# Eval("Asistencia") != DBNull.Value && Convert.ToBoolean(Eval("Asistencia")) %>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripción">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_it_Descripcion" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Estado" Visible="False">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_it_Estado" runat="server" Checked='<%# Eval("Estado") != DBNull.Value && Convert.ToBoolean(Eval("Estado")) %>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                        <br />
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnMostrarFiltrosAvanzado0" runat="server" Height="29px" OnClick="btnAplicarFiltroAvanzado0_Click" Text="Aplicar Filtros Avanzado" Width="234px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Panel ID="panelListarTurnos" runat="server" Visible="False">
                            <table class="auto-style1">
                                <tr>
                                    <td class="auto-style6">Fecha y hora: <asp:TextBox ID="txtFiltroFecha" runat="server" Width="105px" TextMode="DateTime"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revFecha" runat="server" ControlToValidate="txtFiltroFecha" ErrorMessage="Ingrese DD/MM/AAAA HH/MM" ValidationExpression="^([1-9]|0[1-9]|[12][0-9]|3[01])/([1-9]|0[1-9]|1[0-2])/\d{4} (([0-9]|1[0-9]|2[0-3])|0[0-9]):[0-5][0-9]$" ValidationGroup="FiltroAvanzado">*</asp:RegularExpressionValidator>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; DNI:
                                        <asp:DropDownList ID="ddlDniPaciente" runat="server" Height="20px" Width="120px">
                                            <asp:ListItem Value="0" Selected="True">-- Seleccione --</asp:ListItem>
                                            <asp:ListItem Value="1">Medico</asp:ListItem>
                                            <asp:ListItem Value="2">Paciente</asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;<asp:TextBox ID="txtFiltroDni" runat="server" Width="105px" TextMode="Number"></asp:TextBox>
                                        &nbsp;&nbsp;<asp:RegularExpressionValidator ID="revDNI" runat="server" ControlToValidate="txtFiltroDni" ErrorMessage="Ingrese solo caracteres numéricos" ValidationExpression="^\d+$" ValidationGroup="FiltroAvanzado">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style8">
                                        <asp:Label ID="lblResultadoFiltroAvanzado" runat="server"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAplicarFiltroAvanzado" runat="server" Height="27px" Text="Aplicar" Width="129px" OnClick="btnAplicarFiltroAvanzado_Click" ValidationGroup="FiltroAvanzado" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style9">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style9">
                        <asp:HyperLink ID="hlGestionTurnos" runat="server" NavigateUrl="~/Administrador/SubMenu-GestionTurnos/MenuGestionTurnos.aspx">Regresar a Menú de Gestión de Turnos...</asp:HyperLink>
                    </td>
                </tr>
            </table>
        </div>
        <asp:ValidationSummary ID="vsFiltrarPorCodigo" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="FiltroTurno" />
        <asp:ValidationSummary ID="vsFiltroAvanzado" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="FiltroAvanzado" />
    </form>
</body>
</html>
