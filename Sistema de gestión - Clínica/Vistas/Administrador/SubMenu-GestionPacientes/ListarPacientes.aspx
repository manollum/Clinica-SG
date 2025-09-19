<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListarPacientes.aspx.cs" Inherits="Vistas.Administrador.SubMenu_GestionPacientes.ListarPacientes" %>

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
        .auto-style2 {
            width: 853px;
        }
        .auto-style3 {
            width: 100%;
            height: 100%;
        }
        .auto-style5 {
            width: 100%;
        }
        .auto-style6 {
            width: 98px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <div>
            <table class="auto-style1">
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:HyperLink ID="hlAltaPaciente" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionPacientes/AltaPaciente.aspx">Alta Paciente</asp:HyperLink>
                    </td>
                    <td>
                        <asp:HyperLink ID="hlBajaPaciente" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionPacientes/BajaPaciente.aspx">Baja Paciente</asp:HyperLink>
                    </td>
                    <td>
                        <asp:HyperLink ID="hlModificacionPaciente" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionPacientes/ModificacionPaciente.aspx">Modificación Paciente</asp:HyperLink>
                    </td>
                    <td>
                        <asp:HyperLink ID="hlListarPacientes" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionPacientes/ModificacionPaciente.aspx">Listar Pacientes</asp:HyperLink>
                    </td>
                </tr>
            </table>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td style="font-size: medium">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td style="font-size: medium">Usuario:
                        <asp:Label ID="lblUsuarioAdministrador" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lblTituloListarPaciente" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Listar Pacientes:"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <br />
            Búsqueda por DNI del paciente: <asp:TextBox ID="txtFiltroDNIPaciente" runat="server" Width="162px" TextMode="Number"></asp:TextBox>
                            &nbsp;&nbsp;<asp:Button ID="btnFiltrarPacienteDNI" runat="server" Text="Filtrar" OnClick="btnFiltrarPacienteDNI_Click" ValidationGroup="GrupoListarPacientes" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnMostrarTodosPacientes" runat="server" Text="Mostrar Todos" OnClick="btnMostrarTodosPacientes_Click" />
                            &nbsp;&nbsp;
                                <asp:Label ID="lblDNInoEncontrado" runat="server"></asp:Label>
                            <br />
            <table class="auto-style3">
                <tr>
                    <td class="auto-style6">
                        &nbsp;</td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvTxtFiltroDNI" runat="server" ControlToValidate="txtFiltroDNIPaciente" Display="Dynamic" ErrorMessage="Debe ingresar un valor antes de filtrar." ValidationGroup="GrupoListarPacientes">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revTxtFiltro_Dni" runat="server" ControlToValidate="txtFiltroDNIPaciente" Display="Dynamic" ErrorMessage="Solo se permite el ingreso de números en el DNI." ValidationExpression="^\d+$" ValidationGroup="GrupoListarPacientes">*</asp:RegularExpressionValidator>
                        </td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        <asp:GridView ID="gvProvincias" runat="server" AutoGenerateColumns="False" GridLines="None">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnProvincia" runat="server" CommandArgument='<%# Bind("CodProvincia_PR") %>' CommandName="FiltroProvincias" OnCommand="btnProvincia_Command1" Text='<%# Bind("Descripcion_PR") %>' />
                                        <br />
                                        <br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td style="vertical-align: top;">
                        <asp:Panel ID="Panel1" runat="server">
                            <table class="auto-style5">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:GridView ID="gvListadoPacientes" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="Vertical" OnPageIndexChanging="gvListadoPacientes_PageIndexChanging" PageSize="10">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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
                                        <asp:ValidationSummary ID="vsFiltroDNI" runat="server" ValidationGroup="GrupoListarPacientes" />
                                        <br />
                                        <br />
                                        <asp:Button ID="btnMenuFiltrosAvanzados" runat="server" Height="29px" OnClick="btnMenuFiltrosAvanzados_Click" Text="Aplicar Filtros Avanzados" Width="234px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlFiltrosAvanzados" runat="server" Visible="False">
                                            <table class="auto-style1">
                                                <tr>
                                                    <td>
                                                        <br />
                                                        <asp:Label ID="lblDniMedico" runat="server" Text="DNI: "></asp:Label>
                                                        &nbsp;<asp:DropDownList ID="ddlOperatorsDni" runat="server" Height="20px" Width="105px">
                                                            <asp:ListItem Value="1">Igual a:</asp:ListItem>
                                                            <asp:ListItem Value="2">Mayor a:</asp:ListItem>
                                                            <asp:ListItem Value="3">Menor a:</asp:ListItem>
                                                        </asp:DropDownList>
                                                        &nbsp;<asp:TextBox ID="txtDniPaciente" runat="server" Width="140px" TextMode="Number"></asp:TextBox>
                                                        &nbsp;<asp:RegularExpressionValidator ID="revTxtFiltroDni" runat="server" ControlToValidate="txtDniPaciente" ErrorMessage="Solo se permite el ingreso de números naturales en el DNI." ValidationExpression="^\d+$" ValidationGroup="GrupoFiltrarAvanzado">*</asp:RegularExpressionValidator>
                                                        &nbsp;&nbsp;
                                                        <asp:Label ID="lblNombrePaciente" runat="server" Text="Nombre:"></asp:Label>
                                                        &nbsp;<asp:DropDownList ID="ddlOperatorsNombre" runat="server" Height="20px" style="margin-bottom: 0px">
                                                            <asp:ListItem Value="1">Contiene:</asp:ListItem>
                                                            <asp:ListItem Value="2">Empieza con:</asp:ListItem>
                                                            <asp:ListItem Value="3">Termina con:</asp:ListItem>
                                                        </asp:DropDownList>
                                                        &nbsp;<asp:TextBox ID="txtNombrePaciente" runat="server" Width="145px"></asp:TextBox>
                                                        &nbsp;<asp:RegularExpressionValidator ID="revTxtFiltroNombre" runat="server" ControlToValidate="txtNombrePaciente" ErrorMessage="Solo se permite el ingreso de letras y espacios." ValidationExpression="^[A-ZÁÉÍÓÚÑa-záéíóúñ\s]+$" ValidationGroup="GrupoFiltrarAvanzado">*</asp:RegularExpressionValidator>
                                                        &nbsp;&nbsp;
                                                        <asp:Label ID="lblCorreoMedico" runat="server" Text="Teléfono:"></asp:Label>
                                                        &nbsp;<asp:DropDownList ID="ddlOperatorsTelefono" runat="server" Height="20px" style="margin-bottom: 0px">
                                                            <asp:ListItem Value="1">Contiene:</asp:ListItem>
                                                            <asp:ListItem Value="2">Empieza con:</asp:ListItem>
                                                            <asp:ListItem Value="3">Termina con:</asp:ListItem>
                                                        </asp:DropDownList>
                                                        &nbsp;<asp:TextBox ID="txtTelefonoPaciente" runat="server" Width="145px" TextMode="Number"></asp:TextBox>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAplicarFiltrosAvanzados" runat="server" Height="27px" OnClick="btnAplicarFiltrosAvanzados_Click" Text="Aplicar" Width="103px" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="btnLimpiarFiltrosPacientes" runat="server" Height="27px" OnClick="btnLimpiarFiltrosPacientes_Click" Text="Limpiar Filtros" Width="129px" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <br />
                                                        &nbsp;<asp:Label ID="lblFiltrosAvanzadosVacios" runat="server"></asp:Label>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:ValidationSummary ID="vsFiltrosAvanzados" runat="server" ValidationGroup="GrupoFiltrarAvanzado" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HyperLink ID="hlMenuGestionPacientes" runat="server" NavigateUrl="~/Administrador/SubMenu-GestionPacientes/MenuGestionPacientes.aspx">Regresar a Menú de Gestión de Pacientes...</asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        </td>
                </tr>
                </table>
            <br />
        </div>
        </div>
    </form>
</body>
</html>
