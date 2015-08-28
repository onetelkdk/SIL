<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="frm_ctrlasises.aspx.cs" Inherits="MaxApp.frm_ctrlasises" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="server">
    <div class="main_container">
        <div class="main_menu">
           <!--  Accordion Menu ////////// -->
            <div class="menuleft" style="">
                <div class="nombre_top_menu">
                    <a href="Menu.aspx">
                        <img src="images/atras.png" style="margin-top: 17px; float: left" title="REGRESAR AL MENU ANTERIOR">
                        <h2><asp:Label ID="lblModulo" runat="server" Text="N/A"></asp:Label></h2>
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
                <h5 class="data_titulo">Control Asistencia a Sesiones</h5>
            </div>
            <div class="data">
                <div class="data_body">
                    <div class="col-lg-12 col-md-12 col-sx-12 col-sm-12">
                        <div class="tab-pane fade in active" id="datos_control">
                            <div class="row">
                                <fieldset class="mb15">
                                    <legend>Formulario de Asistencia Senadores</legend>
                                    <fieldset class="mb15">
                                        <div style="text-align: center">
                                        <h1 class="blue">0012-2451-PLO</h1>
                                    </div>
                                    </fieldset>
                                    <div class="row m0">
                                            <fieldset class="mb15">
                                                <legend>Listado de Legisladores</legend>
                                                <table class="table table-striped table-bordered table-hover datatable">
                                                    <thead>
                                                        <th></th>
                                                        <th>Nombre</th>
                                                        <th>Provincia</th>
                                                        <th>Partido</th>
                                                    </thead>
                                                    <tbody>
                                                        <td><input type="checkbox"></td>
                                                        <td>Hector Lara</td>
                                                        <td>Peravia</td>
                                                        <td>DJ</td>
                                                    </tbody>
                                                </table>
                                            </fieldset>
                                        </div>
                                    <div class="col-lg-6 col-md-12 col-sm-12 p0">
                                        <fieldset class="mb15">
                                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 pl0">
                                            <div class="form-group">
                                            <label>No. P-Lista</label>
                                            <input type="text" class="text-default" disabled="">
                                        </div>
                                        </div>
                                        <div class="col-lg-8 col-md-8 col-sm-6 col-xs-12" style="text-align: center">
                                            <label style="color: #fff">.</label>
                                            <h1 class="blue m0">Hector Luis Lara Zapata</h1>
                                        </div>
                                        <fieldset>
                                                    <div class="btn-group btn-group-justified" role="group" aria-label="...">
                                                  <div class="btn-group" role="group">
                                                    <button type="button" class="btn btn-agregar b1s" data-original-title="" title="">Agregar</button>
                                                  </div>
                                                  <div class="btn-group" role="group">
                                                    <button type="button" class="btn btn-editar2 b1s" data-original-title="" title="">Editar</button>
                                                  </div>
                                                  <div class="btn-group" role="group">
                                                    <button type="button" class="btn btn-borrar b1s" data-original-title="" title="">Borrar</button>
                                                  </div>
                                                </div>
                                         </fieldset>
                                        </fieldset>
                                    </div>
                                    <div class="col-lg-6">
                                        <fieldset class="mb15" style="padding-bottom: 39px">
                                            <legend>Asistencia</legend>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                     <label>Hora de inicio</label>
                                                     <input type="text" class="time time-default">
                                                 </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                            <label>Hora final</label>
                                            <input type="text" class="time time-default">
                                        </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </fieldset>
                            </div>
                            </div>
                                <fieldset style="margin-bottom: 13px;">
                                    <legend>Listado de senadores con pase de lista</legend>
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered table-hover datatable" id="dtt-datos_control">
                                        <thead>
                                           <tr class="table_heading">
                                               <th></th>
                                               <th>Senador</th>
                                               <th>Partido</th>
                                               <th>H/inicio</th>
                                               <th>H/final</th>
                                               <th>Escusa</th>
                                               <th>H/Trbj</th>
                                               <th>P-Lista</th>
                                           </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td><input type="checkbox"></td>
                                                <td>Hector Luis Lara</td>
                                                <td>PLD</td>
                                                <td>2:30pm</td>
                                                <td>6:25pm</td>
                                                <td>Ausente</td>
                                                <td>115</td>
                                                <td><i class="fa fa-check-circle" style="color: green;font-size: 20px;"></i></td>
                                            </tr>
                                            <tr>
                                                <td><input type="checkbox"></td>
                                                <td>Kerlin Garcia</td>
                                                <td>PRD</td>
                                                <td>2:30pm</td>
                                                <td>6:25pm</td>
                                                <td>Ausente</td>
                                                <td>115</td>
                                                <td><i class="fa fa-check-circle" style="color: green;font-size: 20px;"></i></td>
                                            </tr>
                                            <tr>
                                                <td><input type="checkbox"></td>
                                                <td>Hector Luis Lara</td>
                                                <td>PRSC</td>
                                                <td>2:30pm</td>
                                                <td>6:25pm</td>
                                                <td>Ausente</td>
                                                <td>115</td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td><input type="checkbox"></td>
                                                <td>Kerlin Garcia</td>
                                                <td>PRM</td>
                                                <td>2:30pm</td>
                                                <td>6:25pm</td>
                                                <td>Ausente</td>
                                                <td>115</td>
                                                <td><i class="fa fa-check-circle" style="color: green;font-size: 20px;"></i></td>
                                            </tr>
                                            <tr>
                                                <td><input type="checkbox"></td>
                                                <td>Hector Luis Lara</td>
                                                <td>BIS</td>
                                                <td>2:30pm</td>
                                                <td>6:25pm</td>
                                                <td>Ausente</td>
                                                <td>115</td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td><input type="checkbox"></td>
                                                <td>Kerlin Garcia</td>
                                                <td>MIUCA</td>
                                                <td>2:30pm</td>
                                                <td>6:25pm</td>
                                                <td>Ausente</td>
                                                <td>115</td>
                                                <td><i class="fa fa-check-circle" style="color: green;font-size: 20px;"></i></td>
                                            </tr>
                                            <tr>
                                                <td><input type="checkbox"></td>
                                                <td>Hector Luis Lara</td>
                                                <td>MODA</td>
                                                <td>2:30pm</td>
                                                <td>6:25pm</td>
                                                <td>Ausente</td>
                                                <td>115</td>
                                                <td><i class="fa fa-check-circle" style="color: green;font-size: 20px;"></i></td>
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
        <% Response.WriteFile("footer.aspx"); %>
        <!-- End Footer /////// -->
    </div>
</asp:Content>
