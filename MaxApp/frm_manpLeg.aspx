﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="frm_manpLeg.aspx.cs" Inherits="MaxApp.frm_manpLeg" %>
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
                <h5 class="data_titulo">Definir Período Legislativo</h5>
            </div>
            <div class="data">
                <div class="data_body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <!-- Botones Acciones /////////// -->
                            <div class="btn-top-panel">
                                <div class="btn-nev" style="margin-bottom: 7px;">
                                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-guardar" Text="Guardar" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-cerrar" Text="Cancelar" />
                                </div>
                                <div style="clear: both"></div>
                            </div>
                            <!-- Final Botones Acciones /////////// -->
                            <div class="panel panel-default" runat="server" id="panelLista">
                                <div class="panel-heading">
                                    <!-- Table Panel/////////// -->
                                    <table class="table table-striped table-bordered table-hover datatable">
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--  DIV Formularios /////////////-->
                    <div class="panel-body shadow" runat="server" visible="true" id="PanelMantenimientos">
                        <div class="row m0">
                            <!-- CONTENIDO Formularios /////////////-->
                            <div class="col-lg-3 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label>Código SIS</label>
                                    <input type="text" class="text-240" disabled/>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label>Estado</label>
                                    <select class="select-240">
                                        <option>Opciones</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label>Desde</label>
                                    <input type="text" class="date fecha-240"/>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label>Hasta</label>
                                    <input type="text" class="date fecha-240"/>
                                </div>
                            </div>
                            <!-- END CONTENIDO Formularios /////////////-->
                        </div>
                        <div class="row m0">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Descripción</label>
                                    <textarea class="area100p-1row" placeholder="Escriba la descripción aquí"></textarea>
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
</asp:Content>
