<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="frm_regasi.aspx.cs" Inherits="MaxApp.frm_regasi" %>

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
                    <asp:Label ID="lblNombrePagina" runat="server" Text="N/A"></asp:Label></h5>
            </div>
            <div class="data">
                <div class="data_body">

                    <!--  DIV Formularios /////////////-->
                    <div class="panel-body shadow" runat="server" visible="true" id="PanelMantenimientos">
                        <div class="row m0">
                            <!-- CONTENIDO Formularios /////////////-->
                            <div class="col-lg-12 col-md-12 col-sx-12 col-sm-12 p0">
                                <div id="tabContenedor">
                                    <ul id="myTab" class="nav nav-tabs">
                                        <li class="active">
                                            <a href="#datos_de_control" data-toggle="tab">Datos de control</a>
                                        </li>
                                        <li>
                                            <a href="#miembrosComision" data-toggle="tab">Miembros</a>
                                        </li>
                                        <li>
                                            <a href="#invitados_comision" data-toggle="tab">Invitados</a>
                                        </li>
                                    </ul>
                                    <div id="myTabContent" class="tab-content p0">
                                        <div class="tab-pane fade in active" id="datos_de_control">
                                            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label for="fecha_recepcion">Actividad</label>
                                                    <asp:DropDownList ID="ddlActividades" runat="server" AutoPostBack="true" class="select-240" OnSelectedIndexChanged="ddlActividades_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label for="">Hora de Inicio</label>
                                                    <asp:TextBox ID="txtHoraInicio" runat="server" class="time time-240" />
                                                </div>

                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label for="fecha_recepcion">Fecha de Reunión</label>
                                                    <asp:TextBox ID="txtFechaReunion" runat="server" class="date fecha-240" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="">Hora final</label>
                                                    <asp:TextBox ID="txtHoraFinal" runat="server" class="time time-240" />
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <label for="materia">Comisión</label>
                                                    <asp:DropDownList ID="ddlComision" runat="server" class="select-240" />
                                                </div>

                                            </div>
                                            <div class="row m0">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>Asunto</label>
                                                        <textarea class="area100p-2row" id="txtAsunto" runat="server" placeholder="Escriba aquí el asunto"></textarea>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade pt10 mlr15" id="miembrosComision" runat="server">
                                            <div class="row m0">
                                                <fieldset class="mb15">
                                                    <legend>Listado de Legisladores</legend>
                                                    <div class="table-responsive">

                                                        <asp:GridView ID="gv_Legisladores" runat="server" CssClass="table table-striped table-bordered table-hover datatable"
                                                            ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
                                                            AutoGenerateColumns="false">

                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-Width="2%">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkRow" runat="server" onclick="CheckOne(this)" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Código" HeaderStyle-Width="10%" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Literal ID="fCodigo" runat="server" Text='<%# Eval("AdmLegCodigo")  %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Nombre" HeaderStyle-Width="40%">
                                                                    <ItemTemplate>
                                                                        <asp:Literal ID="fNombre" runat="server" Text='<%# Eval("ComMbrNombre")  %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Provincia" HeaderStyle-Width="35%">
                                                                    <ItemTemplate>
                                                                        <asp:Literal ID="fProvincia" runat="server" Text='<%# Eval("AdmPrvDescripcion")  %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Partido" HeaderStyle-Width="20%">
                                                                    <ItemTemplate>
                                                                        <asp:Literal ID="fPartido" runat="server" Text='<%# Eval("AdmPdoDescripcion")  %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>

                                                    </div>

                                                </fieldset>
                                            </div>
                                            <fieldset class="mb15">
                                                <legend>Formulario de Asistencia Senadores</legend>
                                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12">
                                                    <div class="form-group">
                                                        <label>Hora de Llegada</label>
                                                        <asp:TextBox ID="txtHoraLlegadaAsistenciaMiembro" runat="server" class="time time-180" />
                                                    </div>
                                                    <div class="form-group">
                                                        <label>Hora de Salida</label>
                                                        <asp:TextBox ID="txtHoraSalidaAsistenciaMiembro" runat="server" class="time time-180" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                                    <fieldset style="padding-bottom: 16px;">
                                                        <legend>Inasistencia</legend>
                                                        <fieldset>
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <asp:CheckBox ID="chkExcusaMiembro" runat="server" />
                                                                    <label class="lb-inline blue" style="position: relative; top: -3px;">Excusa</label>
                                                                    <asp:FileUpload ID="FileUploadASP" ClientIDMode="Static" runat="server" CssClass="hidden" />
                                                                    <button id="btnAdjuntar" type="button" class="adjuntar-default" onclick="btoUpload()" style="margin-left: 33px;">Adjuntar</button>
                                                                </div>
                                                            </div>
                                                        </fieldset>
                                                        <asp:DropDownList ID="ddlExcusa" runat="server" class="select-240" />
                                                    </fieldset>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                    <div class="form-group">
                                                        <label>Observaciones</label>
                                                        <textarea id="txtObservaciones" runat="server" class="area100p-2row" placeholder="Escriba cualquier observación aquí"></textarea>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-10 col-sm-12 mt15">
                                                        <fieldset>
                                                            <div class="btn-group btn-group-justified" role="group" aria-label="...">
                                                                <div class="btn-group" role="group">
                                                                    <asp:Button ID="btnAgregarMiembro" runat="server" Text="Agregar" class="btn btn-agregar b1s" OnClick="btnClickMbr" OnClientClick="return confirm('Desea continuar con la operación?');" />
                                                                </div>
                                                                <div class="btn-group" role="group">
                                                                    <asp:Button ID="btnEditarMiembro" runat="server" Text="Editar" class="btn btn-editar2 b1s" OnClick="btnClickMbr" OnClientClick="return confirm('Desea continuar con la operación?');" />
                                                                </div>
                                                                <div class="btn-group" role="group">
                                                                    <asp:Button ID="btnBorrarMiembro" runat="server" Text="Borrar" class="btn btn-borrar b1s" OnClick="btnClickMbr" OnClientClick="return confirm('Desea continuar con la operación?');" />
                                                                </div>
                                                            </div>
                                                        </fieldset>
                                                    </div>
                                                </div>
                                            </fieldset>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <fieldset class="mb15">
                                                        <legend>Senadores con asistencia o excusa</legend>

                                                        <asp:GridView ID="gv_LegisladoresAsistencia" runat="server" CssClass="table table-striped table-bordered table-hover datatable"
                                                            ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
                                                            AutoGenerateColumns="false">

                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-Width="2%">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkRowMbr" runat="server" onclick="CheckOne(this)" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Código" HeaderStyle-Width="10%" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Literal ID="fCodigoMbr" runat="server" Text='<%# Eval("AdmLegCodigo")  %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Nombre" HeaderStyle-Width="30%">
                                                                    <ItemTemplate>
                                                                        <asp:Literal ID="fNombreMbr" runat="server" Text='<%# Eval("ComAmbrNombre")  %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Provincia" HeaderStyle-Width="20%">
                                                                    <ItemTemplate>
                                                                        <asp:Literal ID="fProvinciaMbr" runat="server" Text='<%# Eval("AdmPrvDescripcion")  %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="H/llegada" HeaderStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <asp:Literal ID="fHllegada" runat="server" Text='<%# String.IsNullOrEmpty(Eval("ComAmbrHoraLLegada").ToString())?"":DateTime.Parse(Eval("ComAmbrHoraLLegada").ToString()).Hour>12?String.Format("{0}:{1} p.m.",DateTime.Parse(Eval("ComAmbrHoraLLegada").ToString()).Hour%12,DateTime.Parse(Eval("ComAmbrHoraLLegada").ToString()).Minute):String.Format("{0}:{1} a.m.",DateTime.Parse(Eval("ComAmbrHoraLLegada").ToString()).Hour,DateTime.Parse(Eval("ComAmbrHoraLLegada").ToString()).Minute) %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="H/salida" HeaderStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <asp:Literal ID="fHsalida" runat="server" Text='<%# String.IsNullOrEmpty(Eval("ComAmbrHoraSalida").ToString())?"":DateTime.Parse(Eval("ComAmbrHoraSalida").ToString()).Hour>12?String.Format("{0}:{1} p.m.",DateTime.Parse(Eval("ComAmbrHoraSalida").ToString()).Hour%12,DateTime.Parse(Eval("ComAmbrHoraSalida").ToString()).Minute):String.Format("{0}:{1} a.m.",DateTime.Parse(Eval("ComAmbrHoraSalida").ToString()).Hour,DateTime.Parse(Eval("ComAmbrHoraSalida").ToString()).Minute)  %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Asistencia" HeaderStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                        <asp:Literal ID="fAsistencia" runat="server" Text='<%# Eval("ComAmbrPresente")  %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>



                                                            </Columns>
                                                        </asp:GridView>

                                                    </fieldset>
                                                </div>

                                            </div>
                                            <div class="row m0">
                                            </div>
                                        </div>
                                        <div class="tab-pane fade mlr15 pt10" id="invitados_comision">
                                            <div class="row m0">
                                                <fieldset class="mb15">
                                                    <legend>Listado de Legisladores</legend>

                                                    <asp:GridView ID="gv_Invitados" runat="server" CssClass="table table-striped table-bordered table-hover datatable"
                                                        ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
                                                        AutoGenerateColumns="false">

                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-Width="2%">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkRowInv" runat="server" onclick="CheckOne(this)" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Secuencia" HeaderStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:Literal ID="fInvSecuencia" runat="server" Text='<%# Eval("ComInvSecuencia")  %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Nombre" HeaderStyle-Width="85%">
                                                                <ItemTemplate>
                                                                    <asp:Literal ID="fInvNombre" runat="server" Text='<%# Eval("ComInvNombre")  %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>

                                                </fieldset>
                                            </div>
                                            <fieldset class="mb15">
                                                <legend>Formulario de Asistencia Invitados</legend>
                                                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                                                    <div class="form-group">
                                                        <label>Hora de Llegada</label>
                                                        <input type="text" class="time time-180">
                                                    </div>

                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                                                    <div class="form-group">
                                                        <label>Hora de Salida</label>
                                                        <input type="text" class="time time-180">
                                                    </div>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                    <fieldset style="text-align: center; padding: 20px;">
                                                        <legend>Asistencia</legend>
                                                        <div class="col">
                                                            <asp:CheckBox ID="chkAsistio" runat="server" onchange="CheckUnCheck(this)" />
                                                            <label class="lb-inline blue">Asistió</label>
                                                        </div>
                                                        <div class="col">
                                                            <asp:CheckBox ID="chkNoAsistio" runat="server" onchange="CheckUnCheck(this)" />
                                                            <label class="lb-inline red">No Asistió</label>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-lg-6 col-md-10 col-sm-12 mt15">
                                                            <fieldset class="mb15">
                                                                <div class="btn-group btn-group-justified" role="group" aria-label="...">
                                                                    <div class="btn-group" role="group">
                                                                        <asp:Button ID="btnAgreagarInvitados" runat="server" Text="Agregar" class="btn btn-agregar b1s" OnClick="btnClickInv" />
                                                                    </div>
                                                                    <div class="btn-group" role="group">
                                                                        <asp:Button ID="btnEditarInvitados" runat="server" Text="Editar" class="btn btn-editar2 b1s" OnClick="btnClickInv" />
                                                                    </div>
                                                                    <div class="btn-group" role="group">
                                                                        <asp:Button ID="btnBorrarInvitados" runat="server" Text="Borrar" class="btn btn-borrar b1s" OnClick="btnClickInv" />
                                                                    </div>
                                                                </div>
                                                            </fieldset>
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <fieldset class="mb15">
                                                        <legend>Invitados que asistieron</legend>

                                                        <asp:GridView ID="gv_InvitadosAsistencia" runat="server" CssClass="table table-striped table-bordered table-hover datatable"
                                                            ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
                                                            AutoGenerateColumns="false">

                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-Width="2%">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkRowInvA" runat="server" onclick="CheckOne(this)" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Código" HeaderStyle-Width="10%" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Literal ID="fCodigoInnA" runat="server" Text='<%# Eval("ComAinLinea")  %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Nombre" HeaderStyle-Width="45%">
                                                                    <ItemTemplate>
                                                                        <asp:Literal ID="fNombreInvA" runat="server" Text='<%# Eval("ComAinNombre")  %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="H/llegada" HeaderStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <asp:Literal ID="fHllegadaInvA" runat="server" Text='<%#  Eval("ComAinHoraLLegada")  %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="H/salida" HeaderStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <asp:Literal ID="fHsalidaInvA" runat="server" Text='<%# Eval("ComAinHoraSalida")  %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Asistencia" HeaderStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                        <asp:Literal ID="fAsistenciaInvA" runat="server" Text='<%# Eval("ComAinAsistio")  %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>



                                                            </Columns>
                                                        </asp:GridView>

                                                    </fieldset>
                                                </div>

                                            </div>
                                            <div class="row m0">
                                            </div>
                                        </div>
                                    </div>
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

    <script type="text/javascript">
        function CheckOne(obj) {
            var grid = obj.parentNode.parentNode.parentNode;
            var inputs = grid.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    if (obj.checked && inputs[i] != obj && inputs[i].checked) {
                        inputs[i].checked = false;
                    }
                }
            }
        }

        function btoUpload() {
            document.getElementById('<% = FileUploadASP.ClientID %>').click();
        }
    </script>


    <input id="hfIdAsistencia" type="hidden" value="0" runat="server" />
    <input id="hfComAmbrCodigo" type="hidden" value="0" runat="server" />
</asp:Content>
