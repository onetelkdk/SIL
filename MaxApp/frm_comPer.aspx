<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="frm_comPer.aspx.cs" Inherits="MaxApp.frm_comPer" %>

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
                <h5 class="data_titulo">
                    <asp:Label Text="N/A" ID="lblNombrePagina" runat="server" /></h5>
            </div>
            <div class="data">
                <div class="data_body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                            <!-- Botones Acciones /////////// -->
                            <div class="btn-top-panel">
                                <div class="btn-nev" style="margin-bottom: 7px;">
                                    <asp:Button ID="btnNuevo" runat="server" CssClass="btn btn-nuevo ic_mas" Text="Nuevo" />
                                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-guardar" Text="Guardar" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-cerrar" Text="Cancelar" />
                                </div>
                                <div style="clear: both"></div>
                            </div>
                            <!-- Final Botones Acciones /////////// -->
                            <div class="panel panel-default" runat="server" id="panelLista">
                                <div class="panel-heading">
                                    <!-- Table Panel/////////// -->

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
                                    <label>Período</label>
                                    <select class="select-240">
                                        <option value="value">text</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Fecha</label>
                                    <input type="text" class="date fecha-240" />
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Estado</label>
                                    <select class="select-240">
                                        <option value="value">text</option>
                                    </select>
                                </div>
                            </div>
                            <div class="row m0">
                                <div class="col-lg-12 col-md-12">
                                    <div class="form-group">
                                        <label>Descripción</label>
                                        <textarea class="area100p-1row" placeholder="Escriba aquí la descripción"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="row m0">
                                <div class="col-md-12">
                                    <fieldset class="mb15">
                                        <legend>Agregar legislador a período</legend>
                                        <div class="col-lg-6 col-md-6">
                                            <div class="form-group">
                                                <label>Legislador</label>
                                                <select class="select-240">
                                                    <option value="value">text</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-12">
                                            <label class="white">.</label>
                                            <div class="btn-group btn-group-justified" role="group" aria-label="...">
                                                <div class="btn-group" role="group">
                                                    <button type="button" class="btn btn-agregar b1s">Agregar</button>
                                                </div>
                                                <div class="btn-group" role="group">
                                                    <button type="button" class="btn btn-editar2 b1s">Editar</button>
                                                </div>
                                                <div class="btn-group" role="group">
                                                    <button type="button" class="btn btn-borrar b1s">Borrar</button>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                    <fieldset class="mb15">
                                        <legend>Listado de Legisladores</legend>
                                        <div class="table-responsive">
                                            <table class="table table-striped table-bordered table-hover datatable">
                                                <thead>
                                                    <tr>
                                                        <th></th>
                                                        <th>No. ID</th>
                                                        <th>Nombre</th>
                                                        <th>Partido</th>
                                                        <th>Provincia</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td><input type="checkbox"></td>
                                                        <td>1452</td>
                                                        <td>Hector Lara</td>
                                                        <td>Politico</td>
                                                        <td>Peravia</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </fieldset>
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
        <% Response.WriteFile("footer.aspx"); %>
        <!-- End Footer /////// -->
    </div>
</asp:Content>
