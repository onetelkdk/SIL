<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="frm_manins.aspx.cs" Inherits="MaxApp.frm_manins" %>

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
                <h5 class="data_titulo">Definir Instituciones</h5>
            </div>
            <div class="data">
                <div class="data_body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <!-- Botones Acciones /////////// -->
                            <div class="btn-top-panel">
                                <div class="btn-nev">
                                    <asp:Button ID="btnNuevo" runat="server" CssClass="btn btn-nuevo ic_mas" Text="Nuevo" />
                                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" Text="Guardar" Visible="false" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-brown" Text="Cancelar" Visible="false" />
                                </div>
                                <div style="clear: both"></div>
                            </div>
                            <!-- Final Botones Acciones /////////// -->
                            <div class="panel panel-default" runat="server" id="panelLista">
                                <div class="panel-heading">
                                    <!-- Table Panel/////////// -->

                                    <!-- End Table Panel/////////// -->
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--  DIV Formularios /////////////-->
                    <div class="panel-body shadow" runat="server" visible="true" id="PanelMantenimientos">
                        <div class="row m0">
                            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                                <fieldset>
                                    <legend>Logotipo</legend>
                                    <div align="center">
                                        <img src="images/logotipo-inst.png">
                                        <div style="clear: both"></div>
                                        <button type="file" class="adjuntar-default">Adjuntar</button>
                                    </div>
                                </fieldset>
                                <div class="form-group">
                                    <label>Dirección</label>
                                    <input type="text" class="text-default" />
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label>Código SIS</label>
                                    <input type="text" readonly="readonly" disabled class="text-default">
                                </div>
                                <div class="form-group">
                                    <label>Siglas</label>
                                    <input type="text" class="text-default" />
                                </div>
                                <div class="form-group">
                                    <label>E-mail</label>
                                    <input type="text" class="text-default" />
                                </div>
                                <div class="form-group">
                                    <label>Sitio Web</label>
                                    <input type="url" class="text-default" />
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label>Descripción</label>
                                    <input type="text" class="text-240">
                                </div>
                                <div class="form-group">
                                    <label>Tipo</label>
                                    <select class="select-default">
                                        <option selected=""></option>
                                        <option>Gubernamental</option>
                                    </select>
                                </div>
                                <div class="form-group">
                                    <label>Clasificador Institucional</label>
                                    <select class="select-default">
                                        <option selected=""></option>
                                        <option>C000001</option>
                                        <option>C000002</option>
                                    </select>
                                </div>
                                <div class="form-group">
                                    <label>Fax</label>
                                    <input type="text" class="text-default" />
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label>R.N.C.</label>
                                    <input class="text-default" />
                                </div>
                                <div class="form-group">
                                    <label>Teléfono</label>
                                    <input type="text" class="text-default" />
                                </div>
                                <div class="form-group">
                                    <label>Teléfono Adicional</label>
                                    <input type="text" class="text-default" />
                                </div>
                                <div class="form-group">
                                    <label>Slogan</label>
                                    <input type="text" class="text-default" />
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
        <% Response.WriteFile("footer.aspx"); %>
        <!-- End Footer /////// -->
    </div>
</asp:Content>
