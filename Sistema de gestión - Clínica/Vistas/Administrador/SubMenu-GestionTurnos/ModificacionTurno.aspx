<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModificacionTurno.aspx.cs" Inherits="Vistas.Administrador.SubMenu_GestionTurnos.ModificacionTurno" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript">
        function toggleExclusive(pendienteId, asistenciaId, clicked) {
            var pendiente = document.getElementById(pendienteId);
            var asistencia = document.getElementById(asistenciaId);

            if (clicked === 'pendiente' && pendiente.checked) {
                asistencia.checked = false;
            } else if (clicked === 'asistencia' && asistencia.checked) {
                pendiente.checked = false;
            }
        }
    </script>
</head>
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

        .auto-style2 {
            width: 881px;
        }
        .auto-style7 {
            width: 881px;
            height: 31px;
        }
        .auto-style8 {
            width: 1188px;
        }
        .auto-style9 {
            width: 881px;
            height: 23px;
        }
        .auto-style10 {
            width: 134%;
            height: 23px;
            margin-bottom: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="auto-style8">
                <tr>
                    <td class="auto-style2" style="font-size: large">
                        <asp:Panel ID="Panel1" runat="server">
                            <table class="auto-style10">
                                <tr>
                                    <td>
                                        <asp:HyperLink ID="hlAltaTurno" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionTurnos/AltaTurno.aspx">Alta Turno</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hlBajaTurno" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionTurnos/BajaTurno.aspx">Baja Turno</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionTurnos/ModificacionTurno.aspx">Modificación Turno</asp:HyperLink>
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
                    <td class="auto-style2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Modificación de Turno"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:GridView ID="gvModificarTurnos" runat="server"
    AutoGenerateColumns="False"
    AutoGenerateEditButton="True"
    DataKeyNames="ID Consulta"
    OnRowEditing="gvModificarTurnos_RowEditing"
    OnRowUpdating="gvModificarTurnos_RowUpdating"
    OnRowCancelingEdit="gvModificarTurnos_RowCancelingEdit"
    CellPadding="4" ForeColor="#333333" GridLines="Vertical" AllowPaging="True" OnRowDataBound="gvModificarTurnos_RowDataBound" OnPageIndexChanging="gvModificarTurnos_PageIndexChanging">

    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

    <Columns>

        <asp:TemplateField HeaderText="ID Consulta">
            <EditItemTemplate>
                <asp:Label ID="lbl_et_IDConsulta" runat="server" Text='<%# Bind("[ID Consulta]") %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="lbl_it_IDConsulta" runat="server" Text='<%# Bind("[ID Consulta]") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>


        <asp:TemplateField HeaderText="Médico">
            <EditItemTemplate>
                <asp:Label ID="lbl_et_Medico" runat="server" Text='<%# Bind("Medico") %>'></asp:Label>
                <br />
                <asp:Label ID="lbl_et_Legajo" runat="server" Text='<%# Eval("Legajo Medico") %>' Visible="False"></asp:Label>
            </EditItemTemplate>
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
            <EditItemTemplate>
                <asp:DropDownList ID="ddl_et_FechasTurnos" runat="server">
                </asp:DropDownList>
                &nbsp;<asp:Label ID="lbl_et_Turno" runat="server" Text='<%# Eval("Turno") %>' Visible="False"></asp:Label>
                &nbsp;<br />
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="lbl_it_Turno" runat="server" Text='<%# Eval("Turno") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Pendiente">
            <ItemTemplate>
                <asp:CheckBox ID="chk_it_Pendiente" runat="server"
                    Checked='<%# Eval("Pendiente") != DBNull.Value && Convert.ToBoolean(Eval("Pendiente")) %>'
                    Enabled="false" />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:CheckBox ID="chk_eit_Pendiente" runat="server"
                    Checked='<%# Eval("Pendiente") != DBNull.Value && Convert.ToBoolean(Eval("Pendiente")) %>' Enabled="False" />
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Asistencia">
            <ItemTemplate>
                <asp:CheckBox ID="chk_it_Asistencia" runat="server"
                    Checked='<%# Eval("Asistencia") != DBNull.Value && Convert.ToBoolean(Eval("Asistencia")) %>'
                    Enabled="false" />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:CheckBox ID="chk_eit_Asistencia" runat="server"
                    Checked='<%# Eval("Asistencia") != DBNull.Value && Convert.ToBoolean(Eval("Asistencia")) %>' Enabled="False" />
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Descripción">
            <EditItemTemplate>
                <asp:TextBox ID="txt_et_Descripcion" runat="server" Text='<%# Eval("Descripcion") %>' TextMode="MultiLine" Width="430px" MaxLength="300"></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="lbl_it_Descripcion" runat="server"
                    Text='<%# Bind("Descripcion") %>'></asp:Label>
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


<tr>
    <td class="auto-style9">
        <asp:Label ID="Label2" runat="server"></asp:Label>
    </td>
</tr>

<tr><td class="auto-style2">
                        <asp:Label ID="lblModificacionMensaje" runat="server"></asp:Label>
                    </td></tr>

<tr>
    <td class="auto-style7">
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Administrador/SubMenu-GestionTurnos/MenuGestionTurnos.aspx">
            Regresar a Menú de Gestión de Turnos...
        </asp:HyperLink>
    </td>
</tr>


                    </td>
                </tr>
                <tr>
                    <td class="auto-style9">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
