<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="frm_orddia.aspx.cs" Inherits="MaxApp.frm_orddia" %>

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
                <h5 class="data_titulo">Creacion Orden del Día</h5>
            </div>
            <div class="data">
                <div class="data_body">
                    <div class="col-lg-12 col-md-12 col-sx-12 col-sm-12 shadow">
                        <div class="tab-pane fade in active" id="datos_control">
                            <fieldset class="mt15 mb15">
                                <fieldset class="mr10 p0">
                                    <div style="text-align: center">
                                        <h1 class="blue m0">0012-2451-PLO</h1>
                                    </div>
                                </fieldset>
                                <div class="col-lg-3 col-md-6 col-sm-12">
                                    <div class="form-group">
                                        <label>Tipo Orden Día</label>
                                        <select class="select-default">
                                            <option disabled selected style="display: none">[ Seleccione una opción ]</option>
                                            <option>Ordinaria</option>
                                            <option>Extra-Ordinaria</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-6 col-sm-12">
                                    <div class="form-group">
                                        <label>Sesión</label>
                                        <select class="select-default">
                                            <option disabled selected style="display: none">[ Seleccione una opción ]</option>
                                            <option>Ordinaria</option>
                                            <option>Extra-Ordinaria</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-6 col-sm-12">
                                    <div class="form-group">
                                        <label>Día de la Sesión</label>
                                        <input type="text" class="text-default" readonly disabled>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-6 col-sm-12">
                                    <div class="form-group">
                                        <label>Período</label>
                                        <input type="text" class="text-default" readonly disabled>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-6 col-sm-12">
                                    <div class="form-group">
                                        <label>Legislatura</label>
                                        <input type="text" class="text-default">
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <fieldset style="margin-bottom: 13px;">
                            <legend>Listado de Iniciativas</legend>
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover datatable">
                                    <thead>
                                        <tr class="table_heading">
                                            <th>
                                                <input type="checkbox"></th>
                                            <th>Iniciativa #</th>
                                            <th>Descripción</th>
                                            <th>Estado</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <input type="checkbox"></td>
                                            <td>1452</td>
                                            <td>PLD</td>
                                            <td>2:30pm</td>
                                            <td><i class="fa fa-check-circle" style="color: green; font-size: 20px;"></i></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="checkbox"></td>
                                            <td>2015454</td>
                                            <td>PRD</td>
                                            <td>2:30pm</td>
                                            <td><i class="fa fa-check-circle" style="color: green; font-size: 20px;"></i></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="checkbox"></td>
                                            <td>369784</td>
                                            <td>PRSC</td>
                                            <td>2:30pm</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="checkbox"></td>
                                            <td>Kerlin Garcia</td>
                                            <td>PRM</td>
                                            <td>2:30pm</td>
                                            <td><i class="fa fa-check-circle" style="color: green; font-size: 20px;"></i></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="checkbox"></td>
                                            <td>Hector Luis Lara</td>
                                            <td>BIS</td>
                                            <td>2:30pm</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="checkbox"></td>
                                            <td>Kerlin Garcia</td>
                                            <td>MIUCA</td>
                                            <td>2:30pm</td>
                                            <td><i class="fa fa-check-circle" style="color: green; font-size: 20px;"></i></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="checkbox"></td>
                                            <td>Hector Luis Lara</td>
                                            <td>MODA</td>
                                            <td>2:30pm</td>
                                            <td><i class="fa fa-check-circle" style="color: green; font-size: 20px;"></i></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
        </div>
        <!-- End Data Panel /////////// -->
        <!-- Footer /////// -->
        <% Response.WriteFile("footer.aspx"); %>>
        <!-- End Footer /////// -->
    </div>
</asp:Content>
