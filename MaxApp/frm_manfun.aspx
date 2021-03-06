﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="frm_manfun.aspx.cs" Inherits="MaxApp.frm_manfun" %>
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
                <h5 class="data_titulo">Definir Funciones</h5>
            </div>
            <div class="data">
                <div class="data_body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <!-- Botones Acciones /////////// -->
                            <div class="btn-top-panel">
                                <div class="btn-nev" style="margin-bottom: 7px;">
                                    <asp:Button ID="btnNuevo" runat="server" CssClass="btn btn-nuevo ic_mas" Text="Nuevo" OnClick="btn_Click" />
                                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-guardar" Text="Guardar" OnClick="btn_Click" />
                                    <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-cerrar" Text="Cancelar" OnClick="btn_Click" />
                                </div>
                                <div style="clear: both"></div>
                            </div>
                            <!-- Final Botones Acciones /////////// -->
                            <div class="panel panel-default" runat="server" id="panelLista">
                                <div class="panel-heading">
                                    <!-- Table Panel/////////// -->


                                    <fieldset>
                                        <legend>Buscar</legend>
                                        <div class="row">
                                            <div class="col-lg-2 col-md-3 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label>Estado</label>
                                                    <asp:DropDownList ID="DropDownSearchEstado" runat="server" class="select-240" TabIndex="1001"></asp:DropDownList>
                                                </div>
                                            </div>
                                              <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="white">.</label>
                                                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-buscar" OnClick="btn_Click" />
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

                                    <asp:GridView ID="gv_Funciones" runat="server" CssClass="table table-striped table-bordered table-hover datatable-desc"
                                        ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
                                        AutoGenerateColumns="false" DataKeyNames="AdmFunCodigo,AdmFunDescripcion,AdmCatCodigo,AdmFunUserdata,AdmStsCodigo,AdmFunUsuario,AdmFunFechaReg">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Código">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fCodigo" runat="server" Text='<%# Eval("AdmFunCodigo")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Descripción">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fDescripcion" runat="server" Text='<%# Eval("AdmFunDescripcion")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Catálogo">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fCatalogo" runat="server" Text='<%# Eval("AdmCatCodigo")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Estado">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fEstado" runat="server" Text='<%# Eval("AdmStsCodigo")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEdit" CssClass="btn btn-editar ic_lapiz" Text="Editar" runat="server" CommandArgument='<%# Eval("AdmFunCodigo")%>' Height="25px" OnClick="btn_Click" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnVisualizar" class="btn btn-visualizar" Text="Ver" runat="server" OnClick="btn_Click" CommandArgument='<%# Eval("AdmFunCodigo")%>' Height="25px" />
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
                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label>Código SIS</label>                                    
                                    <asp:TextBox ID="txtCodigoSis" runat="server" CssClass="text-240" Enabled="false" />
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label>Catalogo</label>
                                    <asp:DropDownList ID="DropDownCatalogo" runat="server" class="select-240" TabIndex="1001"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label>Estado</label>
                                    <asp:DropDownList ID="DropDownEstado" runat="server" class="select-240" TabIndex="1001"></asp:DropDownList>
                                </div>
                            </div>
                            <!-- END CONTENIDO Formularios /////////////-->
                        </div>
                        <div class="row m0">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Descripción</label>
                                    <asp:TextBox ID="txtDescripcionFuncion" runat="server" class="area100p-2row" placeholder="Escriba la descripción aquí" TextMode="MultiLine" TabIndex="1012"></asp:TextBox>
                                </div>
                            </div>
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

    <input id="hfId" type="hidden" value="0" runat="server" />

</asp:Content>
