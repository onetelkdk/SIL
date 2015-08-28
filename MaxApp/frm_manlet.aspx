<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="frm_manlet.aspx.cs" Inherits="MaxApp.frm_manlet" %>

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
                <%--Aqui es que van la opciones de Menu--%>
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
                <h5 class="data_titulo">Definir Legislaturas</h5>
            </div>
            <div class="data">
                <div class="data_body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <!-- Botones Acciones /////////// -->
                            <div class="btn-top-panel">
                                <div class="btn-nev" style="margin-bottom: 7px;">
                                    <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btn btn-nuevo ic_mas" OnClick="btnNuevo_Click" />
                                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-guardar" Text="Guardar" Visible="false" OnClick="btnGuardar_Click" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-cerrar" Text="Cancelar" Visible="false" OnClick="btnCancel_Click" />
                                </div>
                                <div style="clear: both"></div>
                            </div>
                            <%--  --%>
                            <fieldset id="FBuscar" runat="server">
                                <legend>Buscar</legend>
                                <div class="row">
                                    <div class="col-lg-2 col-md-3 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Legislatura</label>
                                            <asp:TextBox ID="txtBLegislatura" runat="server" CssClass="text-240"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="white">.</label>
                                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-buscar" OnClick="btnBuscar_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="white">.</label>
                                                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn-limpiar" OnClick="btnLimpiar_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <%--<div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="white">.</label>
                                                       <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-buscar" OnClick="btnBuscar_Click" />
                                                       <asp:Button ID="BtnLimpiar" runat="server" Text="Limpiar" CssClass="btn-limpiar" OnClick="BtnLimpiar_Click" />
                                                </div>
                                            </div>--%>
                                </div>
                            </fieldset>
                            <br>
                            <!-- Final Botones Acciones /////////// -->
                            <div class="panel panel-default" runat="server" id="panelLista">
                                <div class="panel-heading">
                                    <!-- Table Panel/////////// -->
                                    <asp:GridView ID="GridViewLegislatura" runat="server" CssClass="table table-striped table-bordered table-hover datatable"
                                        ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
                                        AutoGenerateColumns="false"
                                        DataKeyNames=" AdmLetCodigo
                                                      ,AdmLetAno
                                                      ,AdmLetDescripcion
                                                      ,AdmLetFechaDesde
                                                      ,AdmLetFechaHasta
                                                      ,AdmPcoCodigo
                                                      ,AdmLtpCodigo
                                                      ,AdmLetUserdata
                                                      ,AdmEstCodigo">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" HeaderStyle-Width="18%">
                                                <ItemTemplate>
                                                    <asp:Literal ID="AdmLetCodigo" runat="server" Text='<%# Eval("AdmLetCodigo")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Legislatura" HeaderStyle-Width="72%">
                                                <ItemTemplate>
                                                    <asp:Literal ID="AdmLetDescripcion" runat="server" Text='<%# Eval("AdmLetDescripcion")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="ImageDelete" class="btn btn-editar ic_lapiz" Text="Editar" runat="server" OnClick="EditarRegistro" CommandArgument='<%# Eval("AdmLetCodigo")%>' Height="25px" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="ImageVisualizar" class="btn btn-visualizar" Text="Ver" runat="server" OnClick="VisualizarRegistro" CommandArgument='<%# Eval("AdmLetCodigo")%>' Height="25px" />
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
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Legislatura</label>
                                    <asp:TextBox ID="txtLegislatura" runat="server" CssClass="text-240"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Tipo</label>
                                    <asp:DropDownList ID="ddlTipo" runat="server" CssClass="select-240">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Periodo</label>
                                    <asp:DropDownList ID="ddlPco" runat="server" CssClass="select-240">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Desde</label>
                                    <asp:TextBox ID="txtDesde" runat="server" CssClass="text-240 date fecha-240" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Hasta</label>
                                    <asp:TextBox ID="txtHasta" runat="server" CssClass="text-240 date fecha-240" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="AdmLetAno">Año de la Legislatura</label>
                                    <asp:TextBox ID="txtAño" runat="server" CssClass="text-240"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Estado</label>
                                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="select-240" Enabled="False">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <!-- END CONTENIDO Formularios /////////////-->
                        </div>
                        <div class="row m0">
                        </div>
                    </div>
                    <!-- End Div Fromularios ////////////////-->
                </div>
            </div>
        </div>
        <!-- End Data Panel /////////// -->
        <!-- Footer /////// -->
        <% Response.WriteFile("footer.aspx"); %>>
        <!-- End Footer /////// -->
    </div>
</asp:Content>
