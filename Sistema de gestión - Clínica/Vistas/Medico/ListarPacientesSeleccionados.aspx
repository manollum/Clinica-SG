<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListarPacientesSeleccionados.aspx.cs" Inherits="Vistas.Medico.ListarPacientesSeleccionados" %>

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
        .auto-style6 {
            width: 100%;
        }
        .auto-style7 {
            height: 23px;
        }
        .auto-style8 {
            width: 21px;
        }
        .auto-style9 {
            width: 21px;
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
        <table class="auto-style6">
            <tr>
                <td class="auto-style8">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">&nbsp;</td>
                <td>
                    <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Size="25pt" Text="Listado de pacientes"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style9">&nbsp;</td>
                <td class="auto-style7">
                    <asp:GridView ID="gvPacientes" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="Legajo" HeaderText="Legajo" />
                            <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="Dni" HeaderText="DNI" />
                            <asp:BoundField DataField="Sexo" HeaderText="Sexo" />
                            <asp:BoundField DataField="Nacionalidad" HeaderText="Nacionalidad" />
                            <asp:BoundField DataField="FechaNacimientoFormateada" HeaderText="Fecha de Nacimiento" />
                            <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                            <asp:BoundField DataField="Provincia" HeaderText="Provincia" />
                            <asp:BoundField DataField="CorreoElectronico" HeaderText="Correo " />
                            <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </td>
                <td class="auto-style7"></td>
                <td class="auto-style7"></td>
                <td class="auto-style7"></td>
                <td class="auto-style7"></td>
                <td class="auto-style7"></td>
            </tr>
            <tr>
                <td class="auto-style9"></td>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style7"></td>
                <td class="auto-style7"></td>
                <td class="auto-style7"></td>
                <td class="auto-style7"></td>
                <td class="auto-style7"></td>
            </tr>
            <tr>
                <td class="auto-style9"></td>
                <td class="auto-style7">
                    <asp:Label ID="lblSinPacientes" runat="server" Text="No hay pacientes seleccionados."></asp:Label>
                </td>
                <td class="auto-style7"></td>
                <td class="auto-style7"></td>
                <td class="auto-style7"></td>
                <td class="auto-style7"></td>
                <td class="auto-style7"></td>
            </tr>
            <tr>
                <td class="auto-style8">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">&nbsp;</td>
                <td>
                    <asp:Button ID="btnEliminarTodo" runat="server" Text="Eliminar todo" OnClick="btnEliminarTodo_Click" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
