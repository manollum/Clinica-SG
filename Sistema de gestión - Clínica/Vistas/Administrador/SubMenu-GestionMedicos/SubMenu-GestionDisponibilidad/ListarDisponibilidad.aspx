<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListarDisponibilidad.aspx.cs" Inherits="Vistas.Administrador.SubMenu_GestionDisponibilidad.ListarDisponibilidad" %>

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
        .auto-style3 {
            width: 100%;
            height: 100%;
        }
        .auto-style4 {
            width: 120px;
        }
        .auto-style20 {
            width: 100%;
        }
        .auto-style21 {
            width: 124px;
        }
        .auto-style22 {
            margin-left: 0px;
        }
        .auto-style23 {
            height: 100px;
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
                        <asp:Label ID="lblTituloListadoDisponibilidad" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Listado de Disponibilidad Médica:"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        <table class="auto-style1">
            <tr>
                <td class="auto-style8"></td>
                <td class="auto-style19"></td>
            </tr>
            </table>
            <br />
            <br />
            Búsqueda por legajo de médico:
                                <asp:TextBox ID="txtFiltroLegajoMedico" runat="server" Width="162px" TextMode="Number" ValidationGroup="grupoLegajo"></asp:TextBox>
                            &nbsp;<asp:Button ID="btnFiltrarMedicoLegajo" runat="server" Text="Filtrar" OnClick="btnFiltrarMedicoLegajo_Click" ValidationGroup="grupoLegajo" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnMostrarTodos" runat="server" Text="Mostrar Todos" OnClick="btnMostrarTodos_Click" />
                            &nbsp;&nbsp;
                                <asp:RequiredFieldValidator ID="rfvFiltrarLegajo" runat="server" ControlToValidate="txtFiltroLegajoMedico" Display="Dynamic" ValidationGroup="grupoLegajo" ForeColor="Red">Debe introducir un legajo.</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revLegajo" runat="server" ControlToValidate="txtFiltroLegajoMedico" ErrorMessage="El legajo debe ser un número mayor o igual a 1." ValidationExpression="^[1-9]\d*$" ValidationGroup="grupoLegajo" ForeColor="Red"></asp:RegularExpressionValidator>
&nbsp;<br />
            <table class="auto-style3">
                <tr>
                    <td class="auto-style4">
                        <asp:GridView ID="gvEspecialidades" runat="server" AutoGenerateColumns="False" GridLines="None" Width="168px">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEspecialidad" runat="server" CommandArgument='<%# Bind("CodEspecialidad_ES") %>' Text='<%# Bind("Descripcion_ES") %>' CommandName="FiltroEspecialidad" OnCommand="btnEspecialidad_Command" Width="175px" Height="35px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td>
                        <br />
                                <asp:Label ID="lblLegajoNoEncontrado" runat="server" Visible="False" ForeColor="Red">El legajo buscado no se encuentra en los registros.</asp:Label>
                        <br />
                        <asp:Label ID="lblDisponibilidadActiva" runat="server" Text="Usted está viendo: disponibilidades para todos los días." ForeColor="Green"></asp:Label>
                        <br />
                        <br />
                        <asp:Panel ID="Panel1" runat="server">
                            <table class="auto-style20">
                                <tr>
                                    <td class="auto-style21">
                                        <asp:GridView ID="gvDias" runat="server" AutoGenerateColumns="False" GridLines="None" >
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnDia" runat="server" CommandArgument='<%# Bind("NumDia_DI") %>' CommandName="FiltroDia" OnCommand="btnDia_Command" Text='<%# Bind("Descripcion_DI") %>' Width="177px" Height="35px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                    <td>
                                        <asp:GridView ID="gvDisponibilidades" runat="server" CellPadding="4" CssClass="auto-style22" AllowPaging="True" OnPageIndexChanging="gvDisponibilidades_PageIndexChanging" ForeColor="#333333" GridLines="Vertical">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                        <asp:Label ID="lblSinRegistros" runat="server" Text="No hay disponibilidad para la especialidad/dia seleccionados." Visible="False" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                        <asp:Button ID="btnMenuFiltrosAvanzados" runat="server" Height="29px" Text="Aplicar Filtros Avanzados" Width="234px" OnClick="btnMenuFiltrosAvanzados_Click" />
                        <br />
                        <br />
                        </td>
                </tr>
                <tr>
                    <td class="auto-style4">
                        &nbsp;</td>
                    <td>
            <asp:Panel ID="pnlFiltrosAvanzados" runat="server" Visible="False">
                <table class="auto-style1">
                    <tr>
                        <td>
                            <br />
                            <asp:Label ID="lblCantidadHoras" runat="server" Text="Rango Horario: "></asp:Label>
                            &nbsp;<asp:DropDownList ID="ddlOperatorsRangoHorario" runat="server" Height="20px" Width="105px">
                                <asp:ListItem Value="a partir de">A partir de:</asp:ListItem>
                                <asp:ListItem Value="antes de">Antes de:</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<asp:DropDownList ID="ddlSeleccionarHorario" runat="server" Height="20px" Width="162px">
                                <asp:ListItem Value="0">-Seleccione un horario-</asp:ListItem>
                                <asp:ListItem>08:00</asp:ListItem>
                                <asp:ListItem>09:00</asp:ListItem>
                                <asp:ListItem>10:00</asp:ListItem>
                                <asp:ListItem>11:00</asp:ListItem>
                                <asp:ListItem>12:00</asp:ListItem>
                                <asp:ListItem>13:00</asp:ListItem>
                                <asp:ListItem>14:00</asp:ListItem>
                                <asp:ListItem>15:00</asp:ListItem>
                                <asp:ListItem>16:00</asp:ListItem>
                                <asp:ListItem>17:00</asp:ListItem>
                                <asp:ListItem>18:00</asp:ListItem>
                                <asp:ListItem>19:00</asp:ListItem>
                                <asp:ListItem>20:00</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEspecialidadMedico" runat="server" Text="Especialidad:"></asp:Label>
                            &nbsp;<asp:DropDownList ID="ddlOperatorsEspecialidad" runat="server" Height="20px" style="margin-bottom: 0px">
                                <asp:ListItem Value="contiene">Contiene:</asp:ListItem>
                                <asp:ListItem Value="empieza con">Empieza con:</asp:ListItem>
                                <asp:ListItem Value="termina con">Termina con:</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtEspecialidad" runat="server" Width="145px"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblSinFiltroAvanzado" runat="server" Text="&quot;No se aplicó ningún filtro avanzado&quot;" Visible="False" ForeColor="Red"></asp:Label>
                            </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style23">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAplicarFiltrosAvanzados" runat="server" Height="27px" Text="Aplicar" Width="103px" OnClick="btnAplicarFiltrosAvanzados_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnLimpiarFiltrosMedicos" runat="server" Height="27px" Text="Limpiar Filtros" Width="129px" OnClick="btnLimpiarFiltrosMedicos_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <br />
                            &nbsp;<asp:Label ID="lblFiltrosAvanzadosVacios" runat="server"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
                        </td>
                </tr>
                <tr>
                    <td class="auto-style4">
                        &nbsp;</td>
                    <td>
                            <asp:HyperLink ID="hlMenuGestionDisponibilidad" runat="server" NavigateUrl="~/Administrador/SubMenu-GestionMedicos/SubMenu-GestionDisponibilidad/MenuDisponibilidad.aspx">Regresar a Menú de Gestión de Disponibilidad...</asp:HyperLink>
                        </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
