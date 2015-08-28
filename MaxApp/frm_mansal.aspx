<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="frm_mansal.aspx.cs" Inherits="MaxApp.frm_mansal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="server">
    <div class="main_container">
        <div class="main_menu">
            <!--  Accordion Menu ////////// -->
            <div class="menuleft" style="">
                <div class="nombre_top_menu">
                    <a href="Menu.aspx">
                        <img src="images/atras.png" style="margin-top: 17px; float: left" title="REGRESAR AL MENU ANTERIOR">
                        <h2>
                            <asp:Label ID="lblModulo" runat="server" Text="N/A"></asp:Label></h2>
                    </a>
                </div>

                <div id="MenuFlotante" runat="server">
                </div>
            </div>
            <!--  End Accordion Menu ////////// -->
        </div>
        <!-- Data Panel/////////// -->
        <div class="main_data">
            <div class="data_header">
                <div class="hidemenu">
                    <a class="btn btn-primary" href="#" data-original-title=""><i class="fa fa-bars"></i></a>
                </div>
                <h5 class="data_titulo">Definir Salones</h5>
            </div>
            <div class="data">
                <div class="data_body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <!-- Botones Acciones /////////// -->
                            <div class="btn-top-panel">
                                <div class="btn-nev" style="margin-bottom: 7px;">
                                    <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btn btn-nuevo ic_mas" OnClick="btnNuevo_Click" />
                                    <asp:Button ID="btnEditar" runat="server" Text="Editar" class="btn btn-editar ic_lapiz" Visible="False" />
                                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-guardar" Text="Guardar" OnClick="btnGuardar_Click" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-cerrar" Text="Cancelar" Visible="False" OnClick="btnCancel_Click" />
                                </div>
                                <div style="clear: both"></div>
                            </div>
                            <!-- Final Botones Acciones /////////// -->
                            <div class="panel panel-default" runat="server" id="panelLista">
                                <div class="panel-heading">
                                    <!-- Table Panel/////////// -->
                                    <asp:GridView ID="GridViewSalones" runat="server" CssClass="table table-striped table-bordered table-hover datatable"
                                        ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
                                        AutoGenerateColumns="false"
                                        DataKeyNames="AdmSalCodigo,AdmSalDescripcion,AdmCatCodigo,AdmStsCodigo">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" HeaderStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:Literal ID="AdmSalCodigo" runat="server" Text='<%# Eval("AdmSalCodigo")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Nombre del Salon" HeaderStyle-Width="78%">
                                                <ItemTemplate>
                                                    <asp:Literal ID="AdmSalDescripcion" runat="server" Text='<%# Eval("AdmSalDescripcion")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="ImageDelete" class="btn btn-editar ic_lapiz" Text="Editar" runat="server" OnClick="EditarRegistro" CommandArgument='<%# Eval("AdmSalCodigo")%>' Height="25px" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="ImageVisualizar" class="btn btn-visualizar" Text="Ver" runat="server" OnClick="VisualizarRegistro" CommandArgument='<%# Eval("AdmSalCodigo")%>' Height="25px" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--  DIV Formularios /////////////-->
                    <div class="panel-body shadow" runat="server" visible="true" id="PanelMantenimientos">
                        <div class="row m0">
                            <!-- CONTENIDO Formularios /////////////-->
                            <div class="col-md-8">
                                <div class="form-group">
                                    <label>Nombre del Salon</label>
                                    <asp:TextBox ID="txtNombreSalon" runat="server" CssClass="text-240"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Tipo</label>
                                    <asp:DropDownList ID="ddlTipo" runat="server" class="select-240">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Estado</label>
                                    <asp:DropDownList ID="ddlEstado" runat="server" class="select-240">
                                        <asp:ListItem Value="True">Activo</asp:ListItem>
                                        <asp:ListItem Value="False">Inactivo</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <!-- END CONTENIDO Formularios /////////////-->
                        </div>
                    </div>
                    <!-- End Div Fromularios ////////////////-->
                </div>
            </div>
        </div>
        <!-- End Data Panel /////////// -->
        <!-- Footer /////// -->
        <% Response.WriteFile("footer.aspx"); %>
        <!-- End Footer /////// -->
    </div>
</asp:Content>
