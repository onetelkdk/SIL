<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="frm_diadbt.aspx.cs" Inherits="MaxApp.frm_diadbt" %>

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
                <h5 class="data_titulo">Definir Tipo de Programas</h5>
            </div>
            <div class="data">
                <div class="data_body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <!-- Botones Acciones /////////// -->
                            <div class="btn-top-panel">
                                <div class="botones_nev">
                                    <button type="button" class="btn btn-nuevo ic_mas" data-toggle="modal">Nuevo</button>
                                    <button type="button" class="btn btn-editar ic_lapiz">Editar</button>
                                    <button type="button" class="btn btn-visualizar ic_monitor">Visualizar</button>
                                </div>
                                <div style="clear: both"></div>
                            </div>
                            <!-- Final Botones Acciones /////////// -->
                            <div class="panel panel-default" runat="server" id="panelLista">
                                <div class="panel-heading">
                                    <!-- Table Panel/////////// -->
                                    <table class="table table-striped table-bordered table-hover datatable">
                                        <thead>
                                            <tr class="table_heading">
                                                <th></th>
                                                <th>Sesión #</th>
                                                <th>Fecha</th>
                                                <th>Hora</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <input type="checkbox"></td>
                                                <td>10245</td>
                                                <td>01/05/2014</td>
                                                <td>2:30pm</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="checkbox"></td>
                                                <td>101778</td>
                                                <td>01/05/2014</td>
                                                <td>2:30pm</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="checkbox"></td>
                                                <td>10245</td>
                                                <td>02/05/2015</td>
                                                <td>2:30pm</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="checkbox"></td>
                                                <td>101778</td>
                                                <td>01/05/2014</td>
                                                <td>2:30pm</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="checkbox"></td>
                                                <td>10245</td>
                                                <td>01/05/2014</td>
                                                <td>2:30pm</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="checkbox"></td>
                                                <td>101778</td>
                                                <td>02/05/2015</td>
                                                <td>2:30pm</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="checkbox"></td>
                                                <td>10245</td>
                                                <td>02/05/2015</td>
                                                <td>2:30pm</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--  DIV Formularios /////////////-->
                    <div class="panel-body shadow" runat="server" visible="true" id="PanelMantenimientos">
                        <div class="row m0">
                            <!-- CONTENIDO Formularios /////////////-->
                            <fieldset style="padding-bottom: 8px; margin-bottom: 10px;">
                                <div class="row m0">
                                    <div class="form-group">
                                        <fieldset style="text-align: center; width: 100%;">
                                            <legend class="m0 p0">Iniciativa</legend>
                                            <h1 class="blue m0 fwb">SLO-214525656</h1>
                                        </fieldset>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label>No. Sesión</label>
                                        <input type="text" class="text-default">
                                    </div>
                                </div>   
                                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label>Leído por</label>
                                        <select class="select-default">
                                            <option selected disabled style="display: none">[ Seleccione una Opción ]</option>
                                            <option>Juan Manuel Diaz</option>
                                        </select>
                                    </div>
                                </div>                                                             
                                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label>Votos a favor</label>
                                        <input type="text" class="text-default">
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label>Votos en contra</label>
                                        <input type="text" class="text-default">
                                    </div>
                                </div>
                                <div class="row m0">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Opiniones</label>
                                            <textarea class="area100p-1row" placeholder="Escriba las opiniones aquí"></textarea>
                                        </div>
                                        <div class="form-group">
                                            <label>Modificaciones</label>
                                            <textarea style="margin-top: 0" class="area100p-1row" placeholder="Escriba las modificaciones aquí"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12 mb15">
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
                            <fieldset style="margin-bottom: 13px;">
                                <legend>Listado de Iniciativas</legend>
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered table-hover datatable5">
                                        <thead>
                                            <tr class="table_heading">
                                                <th></th>
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

                            <fieldset style="margin-bottom: 13px;">
                                <legend>Detalle Diario de Debates</legend>
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered table-hover datatable5">
                                        <thead>
                                            <tr class="table_heading">
                                                <th></th>
                                                <th>Sesión #</th>
                                                <th>Iniciativa #</th>
                                                <th>Leído por</th>
                                                <th>Votos a Favor</th>
                                                <th>Votos en Contra</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <input type="checkbox"></td>
                                                <td>12547</td>
                                                <td>SOL-554765</td>
                                                <td>Hector Lara</td>
                                                <td>14</td>
                                                <td>222</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </fieldset>
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
