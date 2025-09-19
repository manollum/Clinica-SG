<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModificacionMedico.aspx.cs" Inherits="Vistas.Administrador.SubMenu_GestionMedicos.ModificacionMedico" %>

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
            width: 719px;
            height: 23px;
        }
        .auto-style17 {
            height: 23px;
            width: 212px;
        }
        .auto-style7 {
            width: 23px;
        }
        .auto-style18 {
            width: 719px;
        }
        .auto-style15 {
            width: 212px;
        }
        .auto-style20 {
            width: 23px;
            height: 31px;
        }
        .auto-style21 {
            width: 719px;
            height: 31px;
        }
        .auto-style22 {
            width: 212px;
            height: 31px;
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
                        <asp:HyperLink ID="hlAltaMedico" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionMedicos/AltaMedico.aspx">Alta Médico</asp:HyperLink>
                    </td>
                    <td>
                        <asp:HyperLink ID="hlBajaMedico" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionMedicos/BajaMedico.aspx">Baja Médico</asp:HyperLink>
                    </td>
                    <td>
                        <asp:HyperLink ID="hlModificacionMedico" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionMedicos/ModificacionMedico.aspx">Modificación Médico</asp:HyperLink>
                    </td>
                    <td>
                        <asp:HyperLink ID="hlListarMedicos" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionMedicos/ListarMedicos.aspx">Listar Médicos</asp:HyperLink>
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
                        <asp:Label ID="lblTituloModificacionMedico" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Modificación de Registros de Médicos:"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        <table class="auto-style1">
            <tr>
                <td class="auto-style8">&nbsp;</td>
                <td class="auto-style19" style="border: thin dotted #FFFFFF; font-size: x-small; font-style: normal; font-weight: normal; font-family: 'Cascadia Code Light'; text-decoration: blink; cursor: wait; background-color: #FFFFFF; color: #006699; z-index: auto;">Tener en cuenta: Los turnos ya registrados a una disponibilidad modificada deberán de ser realizados de todos modos, aun estando fuera de los nuevos horarios registrados.</td>
                <td class="auto-style17"></td>
                <td class="auto-style17"></td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style18">
                    <asp:GridView ID="gvModificacionMedicos" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="Vertical" OnPageIndexChanging="gvModificacionMedicos_PageIndexChanging" OnRowCancelingEdit="gvModificacionMedicos_RowCancelingEdit" OnRowEditing="gvModificacionMedicos_RowEditing" OnRowUpdating="gvModificacionMedicos_RowUpdating" PageSize="5" OnRowDataBound="gvModificacionMedicos_RowDataBound" AutoGenerateColumns="False"  DataKeyNames="Legajo">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                             <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEditar" runat="server" CausesValidation="False" 
                                        CommandName="Edit" Text="Editar"></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="btnActualizar" runat="server" CausesValidation="True" 
                                        CommandName="Update" Text="Actualizar" 
                                        ValidationGroup="Modificacion" OnClick="btnActualizar_Click"></asp:LinkButton>
                                    <asp:LinkButton ID="btnCancelar" runat="server" CausesValidation="False" 
                                        CommandName="Cancel" Text="Cancelar"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Apellido">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_et_Apellido" runat="server" Text='<%# Bind("Apellido") %>' ValidationGroup="Modificacion"></asp:TextBox>
                                    &nbsp;<asp:RequiredFieldValidator ID="rfvApellido" runat="server" ControlToValidate="txt_et_Apellido" ErrorMessage="Debe ingresar un apellido." ValidationGroup="Modificacion">*</asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Apellido" runat="server" Text='<%# Bind("Apellido") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_et_Nombre" runat="server" Text='<%# Bind("Nombre") %>' ValidationGroup="Modificacion"></asp:TextBox>
                                    &nbsp;<asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txt_et_Nombre" ErrorMessage="Debe ingresar un nombre." ValidationGroup="Modificacion">*</asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Nombre" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Legajo">
                                <EditItemTemplate>
                                    <asp:Label ID="lbl_et_Legajo" runat="server" Text='<%# Bind("Legajo") %>'></asp:Label>
                                    <br />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Legajo" runat="server" Text='<%# Bind("Legajo") %>'></asp:Label>
                                    <br />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DNI">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_et_DNI" runat="server" ValidationGroup="Modificacion" Text='<%# Eval("DNI") %>' TextMode="Number" MaxLength="8"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDNI" runat="server" ControlToValidate="txt_et_DNI" ErrorMessage="Debe ingresar un dni valido." ValidationGroup="Modificacion">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revDNI" runat="server" ControlToValidate="txt_et_DNI" ErrorMessage="El DNI ingresado debe componerse de 8 números enteros positivos. " ValidationExpression="^\d{8}$" ValidationGroup="Modificacion">*</asp:RegularExpressionValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_DNI" runat="server" Text='<%# Bind("DNI") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sexo">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbl_et_Sexo" runat="server" Font-Size="Small" ValidationGroup="Modificacion">
                                        <asp:ListItem Value="F">Femenino</asp:ListItem>
                                        <asp:ListItem Value="M">Masculino</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="rfvSexo" runat="server" ControlToValidate="rbl_et_Sexo" ErrorMessage="Debe indicar el sexo del medico." ValidationGroup="Modificacion">*</asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Sexo" runat="server" Text='<%# Bind("Sexo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha de Nacimiento">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_et_FechaNacimiento" runat="server" Text='<%# Eval("Fecha de Nacimiento") %>' TextMode="Date" ValidationGroup="Modificacion"></asp:TextBox>
                                    &nbsp;<asp:RequiredFieldValidator ID="rfvFeechaNacimiento" runat="server" ControlToValidate="txt_et_FechaNacimiento" ErrorMessage="Ingrese una fecha." ValidationGroup="Modificacion">*</asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="rvFecha" runat="server" ControlToValidate="txt_et_FechaNacimiento" ErrorMessage="Debe ingresarse una fecha como minimo superior a 1900." MaximumValue="2500-12-31" MinimumValue="1900-01-01" ValidationGroup="Modificacion">*</asp:RangeValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_FechaNacimiento" runat="server" Text='<%# Eval("Fecha de Nacimiento") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nacionalidad">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_et_Nacionalidad" runat="server" Text='<%# Bind("Nacionalidad") %>' ValidationGroup="Modificacion"></asp:TextBox>
                                    &nbsp;<asp:RequiredFieldValidator ID="rfvNacionaldiad" runat="server" ControlToValidate="txt_et_Nacionalidad" ErrorMessage="Debe ingresar una nacionalidad." ValidationGroup="Modificacion">*</asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Nacionalidad" runat="server" Text='<%# Bind("Nacionalidad") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Provincia">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddl_et_Provincias" runat="server" ValidationGroup="Modificacion" OnSelectedIndexChanged="ddl_et_Provincias_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                    &nbsp;<asp:RequiredFieldValidator ID="rfvProvincia" runat="server" ControlToValidate="ddl_et_Provincias" ErrorMessage="Debe seleccionar una provincia." InitialValue="0" ValidationGroup="Modificacion">*</asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Provincia" runat="server" Text='<%# Bind("Provincia") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Localidad">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_et_Localidad" runat="server" Text='<%# Bind("Localidad") %>' ValidationGroup="Modificacion"></asp:TextBox>
                                    &nbsp;<asp:RequiredFieldValidator ID="rfvLocalidad" runat="server" ControlToValidate="txt_et_Localidad" ErrorMessage="Debe ingresar una localidad." ValidationGroup="Modificacion">*</asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Localidad" runat="server" Text='<%# Bind("Localidad") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dirección">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_et_Direccion" runat="server" Text='<%# Bind("Direccion") %>' ValidationGroup="Modificacion"></asp:TextBox>
                                    &nbsp;<asp:RegularExpressionValidator ID="revDireccion" runat="server" ControlToValidate="txt_et_Direccion" ErrorMessage="Debe ingresar calle y numero (no se permite ningun tipo de caracter especial) ." ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+ \d+$" ValidationGroup="Modificacion">*</asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="rfvDireccion" runat="server" ControlToValidate="txt_et_Direccion" ErrorMessage="Ingrese la direccion." ValidationGroup="Modificacion">*</asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Direccion" runat="server" Text='<%# Bind("Direccion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Teléfono">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_et_Telefono" runat="server" Text='<%# Bind("Telefono") %>' ValidationGroup="Modificacion" TextMode="Number"></asp:TextBox>
                                    &nbsp;<asp:RequiredFieldValidator ID="rfvTelefono" runat="server" ControlToValidate="txt_et_Telefono" ErrorMessage="Debe ingresar un numero de telefono." ValidationGroup="Modificacion">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revTelefono" runat="server" ControlToValidate="txt_et_Telefono" ErrorMessage="Un telefono solo permite numeros  positivos y enteros (entre 10 y 16 dígitos)." ValidationExpression="^\d{10,16}$" ValidationGroup="Modificacion">*</asp:RegularExpressionValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Telefono" runat="server" Text='<%# Bind("Telefono") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Correo">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_et_Correo" runat="server" style="margin-bottom: 0px" Text='<%# Bind("Correo") %>' ValidationGroup="Modificacion" TextMode="Email"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revCorreo" runat="server" ControlToValidate="txt_et_Correo" ErrorMessage="Debe ingresar un correo electronico valido." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Modificacion">*</asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="rfvCorreoElectronico" runat="server" ControlToValidate="txt_et_Correo" ErrorMessage="Debe ingresar el correo electronico." ValidationGroup="Modificacion">*</asp:RequiredFieldValidator>
                                    <br /> 
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Correo" runat="server" Text='<%# Bind("Correo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Especialidad">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddl_et_Especialidades" runat="server" ValidationGroup="Modificacion" AutoPostBack="True" OnSelectedIndexChanged="ddl_et_Especialidades_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvEspecialidad" runat="server" ControlToValidate="ddl_et_Especialidades" ErrorMessage="Debe seleccionar una especialdiad." InitialValue="0" ValidationGroup="Modificacion">*</asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                    <ItemTemplate>
                                    <asp:Label ID="lbl_it_Especialidad" runat="server" Text='<%# Bind("Especialidad") %>'></asp:Label>
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
                <td class="auto-style15"></td>
                <td class="auto-style15">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style20"></td>
                <td class="auto-style21">
                    </td>
                <td class="auto-style22"></td>
                <td class="auto-style22"></td>
            </tr>
            <tr>
                <td class="auto-style8"></td>
                <td class="auto-style19">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <br />
            <asp:ValidationSummary ID="vsModificacionMedico" runat="server" ValidationGroup="Modificacion" />
                </td>
                <td class="auto-style17">
                    &nbsp;</td>
                <td class="auto-style17"></td>
            </tr>
            <tr>
                <td class="auto-style8">&nbsp;</td>
                <td class="auto-style19">
                    &nbsp;</td>
                <td class="auto-style17">
                    &nbsp;</td>
                <td class="auto-style17">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">&nbsp;</td>
                <td class="auto-style19">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Administrador/SubMenu-GestionMedicos/MenuGestionMedicos.aspx">Regresar a Menú de Gestión de Médicos...</asp:HyperLink>
                </td>
                <td class="auto-style17">
                    &nbsp;</td>
                <td class="auto-style17">&nbsp;</td>
            </tr>
            </table>
            <br />
        </div>
    </form>
</body>
</html>
