<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CambioDeTurno.aspx.cs" Inherits="Vistas.Medico.CambioDeTurno" %>

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
            width: 1300px;
        }
        .auto-style4 {
            width: 259px;
            height: 23px;
        }
        .auto-style5 {
            width: 260px;
            height: 23px;
        }
        .auto-style7 {
            width: 23px;
        }
        .auto-style8 {
            width: 23px;
            height: 23px;
        }
        .auto-style15 {
            width: 212px;
        }
        .auto-style17 {
            height: 23px;
            width: 212px;
        }
        .auto-style18 {
            width: 496px;
        }
        .auto-style19 {
            width: 496px;
            height: 23px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style4">
                        <asp:HyperLink ID="hpCerrarSesion" runat="server" NavigateUrl="~/Login.aspx">Cerrar sesión</asp:HyperLink>
                    </td>
                    <td class="auto-style4"></td>
                    <td class="auto-style5">
                        <asp:HyperLink ID="hpMenuMedico" runat="server" NavigateUrl="~/Medico/MenuMedico.aspx">Volver al menú</asp:HyperLink>
                    </td>
                    <td class="auto-style5"></td>
                    <td class="auto-style5">Usuario:<asp:Label ID="lblUsuario" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <table class="auto-style1">
            <tr>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style18">&nbsp;</td>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style15">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style18">
                    <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Size="25pt" Text="Modificación de turno"></asp:Label>
                </td>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style15">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8"></td>
                <td class="auto-style19">
                    <br />
                    <asp:GridView ID="gvActualizacionTurnos" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="gvActualizacionTurnos_PageIndexChanging" OnRowCancelingEdit="gvActualizacionTurnos_RowCancelingEdit" OnRowDataBound="gvActualizacionTurnos_RowDataBound" OnRowEditing="gvActualizacionTurnos_RowEditing" OnRowUpdating="gvActualizacionTurnos_RowUpdating" PageSize="5">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="btnActualizar" runat="server" CausesValidation="True" CommandName="Update" Text="Actualizar" ValidationGroup="Modificacion"></asp:LinkButton>
                                    <asp:LinkButton ID="btnCancelar" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar"></asp:LinkButton>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtn_it_Editar" runat="server" CausesValidation="False" CommandName="Edit" Enabled="False" Text="Editar"></asp:LinkButton>
                                    &nbsp;<br />
                                    <asp:LinkButton ID="btnSeleccionar" runat="server" CausesValidation="False" CommandName="Select" Text="Seleccionar" CommandArgument='<%# Eval("ID Consulta") %>' OnCommand="btnSeleccionar_Command" ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ID Turno">
                                <EditItemTemplate>
                                    <asp:Label ID="lbl_et_IDTurno" runat="server" Text='<%# Eval("ID Consulta") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_IDTurno" runat="server" Text='<%# Eval("ID Consulta") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Paciente">
                                <EditItemTemplate>
                                    <asp:Label ID="lbl_et_Paciente" runat="server" Text='<%# Eval("Paciente") %>'></asp:Label>
                                    <br />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Paciente" runat="server" Text='<%# Eval("Paciente") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha">
                                <EditItemTemplate>
                                    <asp:Label ID="lbl_et_Fecha" runat="server" Text='<%# Eval("Turno") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Fecha" runat="server" Text='<%# Eval("Turno") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Asistencia">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbl_et_Asistencia" runat="server">
                                        <asp:ListItem Value="0">Ausente</asp:ListItem>
                                        <asp:ListItem Value="1">Presente</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <br />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Asistencia" runat="server" Text='<%# Eval("Asistencia") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Observaciones Medicas">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_et_Descripcion" runat="server" Text='<%# Eval("Descripcion") %>' TextMode="MultiLine" Width="430px" MaxLength="300"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Descripcion" runat="server" Text='<%# Eval("Descripcion") %>'></asp:Label>
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
                <td class="auto-style17">&nbsp;</td>
                <td class="auto-style17"></td>
                <td class="auto-style17"></td>
                <td class="auto-style17"></td>
                <td class="auto-style17"></td>
            </tr>
            </table>
    </form>
</body>
</html>
