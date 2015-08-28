<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="frm_ComStsOrdia.aspx.cs" Inherits="MaxApp.frm_ComStsOrdia" %>

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
                                    <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btn btn-nuevo ic_mas" />
                                    <asp:Button ID="btnEditar2" runat="server" Text="Editar" class="btn btn-editar ic_lapiz" Visible="False" />
                                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-guardar" Text="Guardar" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-cerrar" Text="Cancelar" Visible="False" />
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
                            <div class="col-lg-3 col-md-5 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label>Estados Priorizados</label>
                                    <select multiple class="multipleStsOrdia">
                                        <option>Aprobada en primera</option>
                                        <option>Aprobada en primera con modificación</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-lg-1 col-md-2" align="center" style="margin-top: 140px;">
                                <a href="#" data-original-title="" title="">
                                    <input type="image" class="arrows" src="images/arrow-right.png">
                                </a>
                                <a href="#" data-original-title="" title="">
                                    <input type="image" class="arrows" src="images/arrow-left.png">
                                </a>
                            </div>
                            <div class="col-lg-4 col-md-5 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label>Estados de Orden del Día</label>
                                    <select multiple class="multipleStsOrdia">
                                        <option>Aprobada en primera</option>
                                        <option>Aprobada en primera con modificación</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-lg-1 col-md-12" align="center" style="margin-top: 140px;">
                                <a href="#" data-original-title="" title="">
                                    <input type="image" class="arrows" src="images/arrow-right.png">
                                </a>
                                <a href="#" data-original-title="" title="">
                                    <input type="image" class="arrows" src="images/arrow-left.png">
                                </a>
                            </div>
                            <div class="col-lg-3 col-md-12 col-sm-12 col-xs-12">
                                <div class="form-group">
                                    <label>Pendientes Orden del Día Anterior</label>
                                    <select multiple class="multipleStsOrdia">
                                        <option>Aprobada en primera</option>
                                        <option>Aprobada en primera con modificación</option>
                                    </select>
                                </div>
                            </div>
                            <!-- END CONTENIDO Formularios /////////////-->
                        </div>
                        <div class="row">
                            <div style="padding: 10px 30px 0px 30px;">
                                <div class="alert alert-warning alert-dismissible" role="alert">
                                    <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">×</span></button>
                                    Mantenga pulsada la tecla control (Ctrl) para seleccionar múltiples opciones. Utilice las flechas para moverlas de un lado a otro.
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
