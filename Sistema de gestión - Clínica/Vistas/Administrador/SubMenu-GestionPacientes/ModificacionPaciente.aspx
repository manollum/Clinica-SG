<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModificacionPaciente.aspx.cs" Inherits="Vistas.Administrador.SubMenu_GestionPacientes.ModificacionPaciente" %>

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
        .auto-style18 {
            width: 1460px;
        }
        .auto-style20 {
            width: 23px;
        }
        .auto-style21 {
            width: 23px;
            height: 23px;
        }
        .auto-style22 {
            width: 1460px;
            height: 23px;
        }
        .auto-style23 {
            width: 23px;
            height: 20px;
        }
        .auto-style24 {
            width: 1460px;
            height: 20px;
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
                        <asp:HyperLink ID="hlAltaPaciente" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionPacientes/AltaPaciente.aspx">Alta Paciente</asp:HyperLink>
                    </td>
                    <td>
                        <asp:HyperLink ID="hlBajaPaciente" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionPacientes/BajaPaciente.aspx">Baja Paciente</asp:HyperLink>
                    </td>
                    <td>
                        <asp:HyperLink ID="hlModificacionPaciente" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionPacientes/ModificacionPaciente.aspx">Modificación Paciente</asp:HyperLink>
                    </td>
                    <td>
                        <asp:HyperLink ID="hlListarPacientes" runat="server" Font-Size="Large" NavigateUrl="~/Administrador/SubMenu-GestionPacientes/ListarPacientes.aspx">Listar Pacientes</asp:HyperLink>
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
                        <asp:Label ID="lblTituloAltaPaciente" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Modificación de Registros de Pacientes:"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        <table class="auto-style1">
            <tr>
                <td class="auto-style21"></td>
                <td class="auto-style22"></td>
            </tr>
            <tr>
                <td class="auto-style20">&nbsp;</td>
                <td class="auto-style18">
                    <asp:GridView ID="gvModificacionPacientes" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Legajo" ForeColor="#333333" GridLines="Vertical" OnPageIndexChanging="gvModificacionPacientes_PageIndexChanging" OnRowCancelingEdit="gvModificacionPacientes_RowCancelingEdit" OnRowDataBound="gvModificacionPacientes_RowDataBound" OnRowEditing="gvModificacionPacientes_RowEditing" OnRowUpdating="gvModificacionPacientes_RowUpdating" PageSize="5">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField ShowHeader="False">
                                <EditItemTemplate>
                                    <asp:LinkButton ID="btnActualizar" runat="server" CommandName="Update" ValidationGroup="GrupoModificarPaciente">Actualizar</asp:LinkButton>
                                    <asp:LinkButton ID="btnCancelar" runat="server" CausesValidation="False" CommandName="Cancel">Cancelar</asp:LinkButton>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEditar" runat="server" CausesValidation="False" CommandName="Edit">Editar</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Apellido">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_et_Apellido" runat="server" Text='<%# Bind("Apellido") %>' MaxLength="50" ValidationGroup="GrupoModificarPaciente"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTxtApellido" runat="server" ControlToValidate="txt_et_Apellido" Display="Dynamic" ErrorMessage="No puede dejar el campo Apellido vacío" ValidationGroup="GrupoModificarPaciente">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revTxtApellido" runat="server" ControlToValidate="txt_et_Apellido" Display="Dynamic" ErrorMessage="En un apellido solo se permiten letras y espacios, con una longitud entre 2 y 50 caracteres (no puede empezar ni terminar con espacios)." ValidationExpression="^(?=.{2,50}$)[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ]+(?: [a-zA-ZáéíóúÁÉÍÓÚñÑüÜ]+)*$" ValidationGroup="GrupoModificarPaciente">*</asp:RegularExpressionValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Apellido" runat="server" Text='<%# Bind("Apellido") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_et_Nombre" runat="server" Text='<%# Bind("Nombre") %>' MaxLength="50" ValidationGroup="GrupoModificarPaciente"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTxtNombre" runat="server" ControlToValidate="txt_et_Nombre" Display="Dynamic" ErrorMessage="No puede dejar el campo Nombre vacío" ValidationGroup="GrupoModificarPaciente">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revTxtNombre" runat="server" ControlToValidate="txt_et_Nombre" Display="Dynamic" ErrorMessage="En un nombre solo se permiten letras y espacios, con una longitud entre 2 y 50 caracteres (no puede empezar ni terminar con espacios)." ValidationExpression="^(?=.{2,50}$)[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ]+(?: [a-zA-ZáéíóúÁÉÍÓÚñÑüÜ]+)*$" ValidationGroup="GrupoModificarPaciente">*</asp:RegularExpressionValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Nombre" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Legajo">
                                <EditItemTemplate>
                                    <asp:Label ID="lbl_et_Legajo" runat="server" Text='<%# Bind("Legajo") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Legajo" runat="server" Text='<%# Bind("Legajo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DNI">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_et_DNI" runat="server" Text='<%# Bind("DNI") %>' ValidationGroup="GrupoModificarPaciente" TextMode="Number" MaxLength="8"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTxtDNI" runat="server" ControlToValidate="txt_et_DNI" Display="Dynamic" ErrorMessage="No puede dejar el campo DNI vacío." ValidationGroup="GrupoModificarPaciente">*</asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="rvTxtDNI" runat="server" ControlToValidate="txt_et_DNI" Display="Dynamic" ErrorMessage="El DNI ingresado debe componerse de 8 caracteres numéricos. " MaximumValue="99999999" MinimumValue="10000000" ValidationGroup="GrupoModificarPaciente">*</asp:RangeValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_DNI" runat="server" Text='<%# Bind("DNI") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sexo">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbl_et_Sexo" runat="server" Font-Size="Small" ValidationGroup="GrupoModificarPaciente">
                                        <asp:ListItem Value="F">Femenino</asp:ListItem>
                                        <asp:ListItem Value="M">Masculino</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="rfvRblSexo" runat="server" ControlToValidate="rbl_et_Sexo" Display="Dynamic" ErrorMessage="No puede dejar el campo Sexo vacío." ValidationGroup="GrupoModificarPaciente">*</asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Sexo" runat="server" Text='<%# Bind("Sexo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha de Nacimiento">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_et_FechaNacimiento" runat="server" Text='<%# Eval("Fecha de Nacimiento") %>' TextMode="Date" ValidationGroup="GrupoModificarPaciente"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTxtFechaNacimiento" runat="server" ControlToValidate="txt_et_FechaNacimiento" Display="Dynamic" ErrorMessage="No puede dejar el campo Fecha de Nacimiento vacío." ValidationGroup="GrupoModificarPaciente">*</asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_FechaNacimiento" runat="server" Text='<%# Eval("Fecha de Nacimiento") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nacionalidad">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_et_Nacionalidad" runat="server" Text='<%# Bind("Nacionalidad") %>' MaxLength="50" ValidationGroup="GrupoModificarPaciente"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTxtNacionalidad" runat="server" ControlToValidate="txt_et_Nacionalidad" Display="Dynamic" ErrorMessage="No puede dejar el campo Nacionalidad vacío" ValidationGroup="GrupoModificarPaciente">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revTxtNacionalidad" runat="server" ControlToValidate="txt_et_Nacionalidad" Display="Dynamic" ErrorMessage="En una Nacionalidad solo se permiten letras y espacios, con una longitud entre 2 y 50 caracteres (no puede empezar ni terminar con espacios)." ValidationExpression="^(?=.{2,50}$)[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ]+(?: [a-zA-ZáéíóúÁÉÍÓÚñÑüÜ]+)*$" ValidationGroup="GrupoModificarPaciente">*</asp:RegularExpressionValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Nacionalidad" runat="server" Text='<%# Bind("Nacionalidad") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Provincia">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddl_et_Provincias" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_et_Provincias_SelectedIndexChanged" ValidationGroup="GrupoModificarPaciente">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDdlProvincia" runat="server" ControlToValidate="ddl_et_Provincias" Display="Dynamic" ErrorMessage="No puede dejar el campo Provincia vacío" InitialValue="0" ValidationGroup="GrupoModificarPaciente">*</asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Provincia" runat="server" Text='<%# Bind("Provincia") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Localidad">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_et_Localidad" runat="server" Text='<%# Bind("Localidad") %>' MaxLength="50" ValidationGroup="GrupoModificarPaciente"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTxtLocalidad" runat="server" ControlToValidate="txt_et_Localidad" Display="Dynamic" ErrorMessage="No puede dejar el campo Localidad vacío" ValidationGroup="GrupoModificarPaciente">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revTxtLocalidad" runat="server" ControlToValidate="txt_et_Localidad" Display="Dynamic" ErrorMessage="Solo puede ingresar letras y espacios en el campo Localidad" ValidationExpression="^[A-ZÁÉÍÓÚÜÑa-záéíóúüñ¨\s]+$" ValidationGroup="GrupoModificarPaciente">*</asp:RegularExpressionValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Localidad" runat="server" Text='<%# Bind("Localidad") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dirección">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_et_Direccion" runat="server" Text='<%# Bind("Direccion") %>' MaxLength="100" ValidationGroup="GrupoModificarPaciente"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTxtDireccion" runat="server" ControlToValidate="txt_et_Direccion" Display="Dynamic" ErrorMessage="No puede dejar el campo Dirección vacío" ValidationGroup="GrupoModificarPaciente">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revTxtDireccion" runat="server" ControlToValidate="txt_et_Direccion" Display="Dynamic" ErrorMessage="Debe ingresar calle y número." ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+ \d+$" ValidationGroup="GrupoModificarPaciente">*</asp:RegularExpressionValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Direccion" runat="server" Text='<%# Bind("Direccion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Teléfono">
                                <EditItemTemplate>
                                    <br />
                                    <asp:TextBox ID="txt_et_Telefono" runat="server" Text='<%# Bind("Telefono") %>' TextMode="Number" MaxLength="10" ValidationGroup="GrupoModificarPaciente"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTxtTelefono" runat="server" ControlToValidate="txt_et_Telefono" Display="Dynamic" ErrorMessage="No puede dejar el campo Télefono vacío" ValidationGroup="GrupoModificarPaciente">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revTelefono" runat="server" ControlToValidate="txt_et_Telefono" ErrorMessage="Un telefono solo permite numeros positivos y enteros (de 10 a 16 dígitos)." ValidationExpression="^\d{10,16}$" ValidationGroup="GrupoModificarPaciente">*</asp:RegularExpressionValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Telefono" runat="server" Text='<%# Bind("Telefono") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Correo">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_et_Correo" runat="server" Text='<%# Bind("Correo") %>' TextMode="Email" MaxLength="100" ValidationGroup="GrupoModificarPaciente"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTxtCorreo" runat="server" ControlToValidate="txt_et_Correo" Display="Dynamic" ErrorMessage="No puede dejar el campo Correo vacío" ValidationGroup="GrupoModificarPaciente">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revTxtCorreo" runat="server" ControlToValidate="txt_et_Correo" Display="Dynamic" ErrorMessage="No ingresó un formato de Correo válido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="GrupoModificarPaciente">*</asp:RegularExpressionValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_it_Correo" runat="server" Text='<%# Bind("Correo") %>'></asp:Label>
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
                <td class="auto-style21"></td>
                <td class="auto-style22">
                </td>
            </tr>
            <tr>
                <td class="auto-style21"></td>
                <td class="auto-style22">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="GrupoModificarPaciente" />
                </td>
            </tr>
            <tr>
                <td class="auto-style21">&nbsp;</td>
                <td class="auto-style22">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style23"></td>
                <td class="auto-style24">
                        <asp:HyperLink ID="hlMenuGestionPacientes" runat="server" NavigateUrl="~/Administrador/SubMenu-GestionPacientes/MenuGestionPacientes.aspx">Regresar a Menú de Gestión de Pacientes...</asp:HyperLink>
                    </td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>
