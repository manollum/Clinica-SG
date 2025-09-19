<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModificacionDisponibilidad.aspx.cs" Inherits="Vistas.Administrador.SubMenu_GestionDisponibilidad.ModificacionDisponibilidad" %>

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
        .auto-style8 {
            width: 23px;
            height: 23px;
        }
        .auto-style19 {
            width: 496px;
            height: 23px;
        }
        .auto-style7 {
            width: 23px;
        }
        .auto-style18 {
            width: 496px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
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
                        <asp:HyperLink ID="hlAltaDisponibilidad" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionMedicos/SubMenu-GestionDisponibilidad/AltaDisponibilidad.aspx">Alta Disponibilidad</asp:HyperLink>
                    </td>
                    <td>
                        <asp:HyperLink ID="hlBajaDisponibilidad" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionMedicos/SubMenu-GestionDisponibilidad/BajaDisponibilidad.aspx">Baja Disponibilidad</asp:HyperLink>
                    </td>
                    <td>
                        <asp:HyperLink ID="hlModificacionDisponibilidad" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionMedicos/SubMenu-GestionDisponibilidad/ModificacionDisponibilidad.aspx">Modificación Disponibilidad</asp:HyperLink>
                    </td>
                    <td>
                        <asp:HyperLink ID="hlListarDisponibilidad" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionMedicos/SubMenu-GestionDisponibilidad/ListarDisponibilidad.aspx">Listar Disponibilidad</asp:HyperLink>
                    </td>
                </tr>
            </table>
            <br />
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                    <td style="font-size: medium">Usuario:
                        <asp:Label ID="lblUsuarioAdministrador" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lblTituloAltaPaciente" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Modificación de Disponibilidad Médica:"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        <table class="auto-style1">
            <tr>
                <td class="auto-style8"></td>
                <td class="auto-style19"></td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style18">
                    <asp:GridView ID="gvModificacionDisponibilidad" runat="server" AllowPaging="True" AutoGenerateColumns="False" AutoGenerateEditButton="True" CellPadding="4" ForeColor="#333333" GridLines="Vertical" OnPageIndexChanging="gvModificacionDisponibilidad_PageIndexChanging" OnRowCancelingEdit="gvModificacionDisponibilidad_RowCancelingEdit" OnRowEditing="gvModificacionDisponibilidad_RowEditing" OnRowUpdating="gvModificacionDisponibilidad_RowUpdating" PageSize="5" OnRowDataBound="gvModificacionDisponibilidad_RowDataBound">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Médico">
                                <EditItemTemplate>
                                    <asp:Label ID="lbl_eit_Nombre" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Nombre" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Día">
                                <EditItemTemplate>
                                    <asp:Label ID="lbl_eit_Dia" runat="server" Text='<%# Bind("Dia") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Dia" runat="server" Text='<%# Bind("Dia") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Inicio Turno">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddl_eit_HoraInicio" runat="server" OnSelectedIndexChanged="ddl_eit_HoraInicio_SelectedIndexChanged" AutoPostBack="True" Width="110px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvInicioHorario" runat="server" ControlToValidate="ddl_eit_HoraInicio" ErrorMessage="No se puede dejar en blanco" ValidationGroup="Modificacion">*</asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Inicio" runat="server" Text='<%# Bind("Inicio") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fin Del Turno">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddl_eit_HoraFin" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_eit_HoraFin_SelectedIndexChanged" Width="110px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvHorarioFin" runat="server" ControlToValidate="ddl_eit_HoraFin" ErrorMessage="No se puede dejar el campo vacio." ValidationGroup="Modificacion">*</asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Fin" runat="server" Text='<%# Bind("Fin") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Legajo" ShowHeader="False" Visible="False">
                                <EditItemTemplate>
                                    <asp:Label ID="lbl_eit_Legajo" runat="server" Text='<%# Bind("Legajo") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Legajo" runat="server" Text='<%# Bind("Legajo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Estado" Visible="False">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="cb_eit_Estado" runat="server" Checked='<%# Bind("Estado") %>' />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cb_it_Estado" runat="server" Checked='<%# Bind("Estado") %>' />
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
                </td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style18">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8"></td>
                <td class="auto-style19">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style8">&nbsp;</td>
                <td class="auto-style19">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">&nbsp;</td>
                <td class="auto-style19">
                        <asp:HyperLink ID="hlMenuGestionDisponibilidad" runat="server" NavigateUrl="~/Administrador/SubMenu-GestionMedicos/SubMenu-GestionDisponibilidad/MenuDisponibilidad.aspx">Regresar a Menú de Gestion de Disponibilidad...</asp:HyperLink>
                    </td>
            </tr>
        </table>
        </div>
        <asp:ValidationSummary ID="vsModificacionDisponibilidad" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Modificacion" />
    </form>
</body>
</html>
