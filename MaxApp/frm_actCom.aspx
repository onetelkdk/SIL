<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="frm_actCom.aspx.cs" Inherits="MaxApp.frm_actCom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="server">

        <script type="text/javascript">

            function controlChange(control)
            {
                var d = new Date(stringToDate(control.value, "dd/MM/yyyy", "/"));
                var weekday = new Array(7);
                weekday[0] = "Domingo";
                weekday[1] = "Lunes";
                weekday[2] = "Martes";
                weekday[3] = "Miercoles";
                weekday[4] = "Jueves";
                weekday[5] = "Viernes";
                weekday[6] = "Sábado";

                var n = weekday[d.getDay()];
                document.getElementById('<% = txtDiaSemana.ClientID %>').value = n;
            }

            function stringToDate(_date, _format, _delimiter)
            {btn
                var formatLowerCase = _format.toLowerCase();
                var formatItems = formatLowerCase.split(_delimiter);
                var dateItems = _date.split(_delimiter);
                var monthIndex = formatItems.indexOf("mm");
                var dayIndex = formatItems.indexOf("dd");
                var yearIndex = formatItems.indexOf("yyyy");
                var month = parseInt(dateItems[monthIndex]);
                month -= 1;
                var formatedDate = new Date(dateItems[yearIndex], month, dateItems[dayIndex]);
                return formatedDate;
            }

            function ShowTab(tab)
            {
                document.getElementById(tab).click();
            }

<%--            function AddRow() {
                var table = document.getElementById('<% = gv_Miembros.ClientID %>');

                var bt = document.createElement('button');
                bt.type = "button";
                bt.className += "btn btn-borrar";
                bt.textContent = "Quitar";
                bt.onclick = DeleteRow;

                var newRow = table.insertRow(table.rows.length);
                var cell1 = newRow.insertCell(0);
                var cell2 = newRow.insertCell(1);
                var cell3 = newRow.insertCell(2);
                var cell4 = newRow.insertCell(3);
                var textCell1 = document.createTextNode(document.getElementById("<% = DropDownMiembros.ClientID %>").value);
                var textCell2 = document.createTextNode(document.getElementById("<% = DropDownMiembros.ClientID %>").options[document.getElementById("<% = DropDownMiembros.ClientID%>").selectedIndex].text);
                var textCell3 = document.createTextNode(document.getElementById("<% = DropDownFuncion.ClientID %>").options[document.getElementById("<% = DropDownFuncion.ClientID%>").selectedIndex].text);
                cell1.appendChild(textCell1);
                cell2.appendChild(textCell2);
                cell3.appendChild(textCell3);
                cell4.appendChild(bt);
            }

            function DeleteRow(rowSelected)
            {
                var i = rowSelected.parentNode.parentNode.rowIndex;
                document.getElementById('<% = gv_Miembros.ClientID %>').deleteRow(i);
            }--%>

    </script>


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
                <h5 class="data_titulo">Actividades de Comisiones</h5>
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
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-cerrar" Text="Cancelar" OnClick="btn_Click" />
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
                                                    <label>Fecha desde</label>
                                                    <asp:TextBox ID="txtFechaDesde" runat="server" class="date fecha-240" />
                                                </div>
                                            </div>
                                            <div class="col-lg-2 col-md-3 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label>Fecha hasta</label>
                                                     <asp:TextBox ID="txtFechaHasta" runat="server" class="date fecha-240" />
                                                </div>
                                            </div>
                                              <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="white">.</label>
                                                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-buscar" OnClick="btn_Click" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="white">.</label>
                                                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn-limpiar" OnClick="btn_Click" />
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

                                    <asp:GridView ID="gv_ActividadComisiones" runat="server" CssClass="table table-striped table-bordered table-hover datatable"
                                        ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
                                        AutoGenerateColumns="false" DataKeyNames="ComActCodigoSis,ComActSecuencia,ComActNumero,ComActTipo,ComCfmId,ComActFecha,AdmLetCodigo,IniIniCodigoSis,AdmPcoCodigo,ComActTipoReunion,ComActDescripcion,ComActResultados,ComActInvitados,AdmSalCodigo,ComActHoraConvoca,ComActHoraInicio,ComActHoraCierre,ComActFuncionarioCom,AdmEstCodigo,ComActUsuario,ComActFechaReg">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Código     ">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fCodigo" runat="server" Text='<%# Eval("ComActNumero")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Descripción">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fDescripcion" runat="server" Text='<%# Eval("ComActDescripcion")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Fecha">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fFecha" runat="server" Text='<%# Eval("ComActFecha")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Hora convocatoria">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fHoraConvocatoria" runat="server" Text='<%# Eval("ComActHoraConvoca")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Hora inicio">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fHoraInicio" runat="server" Text='<%# Eval("ComActHoraInicio")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Hora cierre">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fHoraCierre" runat="server" Text='<%# Eval("ComActHoraCierre")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEdit" CssClass="btn btn-editar ic_lapiz" Text="Editar" runat="server" CommandArgument='<%# Eval("ComActNumero")%>' Height="25px" OnClick="btn_Click" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnVisualizar" class="btn btn-visualizar" Text="Ver" runat="server" OnClick="btn_Click" CommandArgument='<%# Eval("ComActNumero")%>' Height="25px" />
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
                            <div class="col-lg-12 col-md-12 col-sx-12 col-sm-12 p0">
                                <ul id="myTab" class="nav nav-tabs">
                                    <li class="active">
                                        <a href="#de_la_comision" id="tabComision" runat="server" data-toggle="tab" data-original-title="" title="">De la comisión</a>
                                    </li>
                                    <li>
                                        <a href="#datos_de_control" id="tabDatosControl" runat="server" data-toggle="tab" data-original-title="" title="">Datos de control</a>
                                    </li>
                                    <li>
                                        <a href="#miembrosComision" id="tabMiembrosComision" runat="server" data-toggle="tab" data-original-title="" title="">Miembros</a>
                                    </li>
                                </ul>
                                <div id="myTabContent" class="tab-content p0">
                                    <div class="tab-pane fade in active pt10" id="de_la_comision">
                                        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label for="codigo">No. Actividad</label>
                                                <asp:TextBox ID="txtNroActividad" runat="server" CssClass="text-default disabled" TabIndex="1001" />
                                            </div>
                                            <div class="form-group">
                                                <label for="digitado">Tipo de Reunión</label>
                                                <asp:DropDownList ID="DropDownTipoReunion" runat="server" class="select-default" TabIndex="1006"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label for="materia">Comisión</label>
                                                <asp:DropDownList ID="DropDownComision" runat="server" class="select-default" TabIndex="1002"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label for="">Tipo de Actividad</label>
                                                <asp:DropDownList ID="DropDownListTipoActividad" runat="server" class="select-default" TabIndex="1007"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label for="fecha_recepcion">Fecha de Actividad</label>
                                                <asp:TextBox ID="txtFechaActividad" runat="server" CssClass="text-default fecha-default date" onchange="controlChange(this)" TabIndex="1004" />
                                            </div>
                                            <div class="form-group">
                                                <label for="">Dia de la semana</label>
                                                <asp:TextBox ID="txtDiaSemana" runat="server" CssClass="text-default" ReadOnly="true" ClientIDMode="Static" TabIndex="1008" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label>Estado</label>
                                                <asp:DropDownList ID="DropDownEstado" runat="server" class="select-default" TabIndex="1005"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row m0">
                                        	<div class="col-md-12">
                                                <label>Iniciativas</label>
                                                <asp:ListBox ID="listBoxNroIniciativa" runat="server" CssClass="selectM-2row" SelectionMode="Multiple" TabIndex="1009"></asp:ListBox>
                                        	</div>
                                        </div>
                                        <div class="row m0">
                                            <div class="col-lg-6  col-md-12 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <label for="descripcion">Descripción de la actividad</label>
                                                    <asp:TextBox ID="txtDescripcionActividad" runat="server" class="area100p-2row" placeholder="Escriba la descripción aquí" TextMode="MultiLine" TabIndex="1010"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6  col-md-12 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <label for="descripcion">Resultados de la Actividad</label>
                                                    <asp:TextBox ID="txtResultadosActividad" runat="server" class="area100p-2row" placeholder="Describa los resultados de la actividad aquí" TextMode="MultiLine"  TabIndex="1011"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row m0">
                                            <div class="col-md-12">
                                                <div class="form-group tag-actCom">
                                                    <label>Invitados</label>
                                                    <%--<asp:TextBox ID="txtInvitados" runat="server" class="text-default tags" placeholder="Agregue los invitados separando por ; (punto y coma)." TextMode="MultiLine" TabIndex="1014"></asp:TextBox>--%>
                                                    <asp:TextBox ID="txtInvitados" runat="server" CssClass="tags" placeholder="Agregue los invitados separando por ; (punto y coma)." TabIndex="1012"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="datos_de_control">
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label for="">Legislatura</label>
                                                <asp:DropDownList ID="DropDownLegislatura" runat="server" class="select-default" TabIndex="1013"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label for="">Hora de Inicio</label>
                                                <asp:TextBox ID="txtHoraInicio" runat="server" CssClass="time time-240" onkeypress="return false" TabIndex="1016"/>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label for="">Lugar de Actividad</label>
                                                <asp:DropDownList ID="DropDownLugarActividad" runat="server" class="select-default" TabIndex="1014"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label for="">Hora de Cierre</label>
                                                <asp:TextBox ID="txtHoraCierre" runat="server" CssClass="time time-240" onkeypress="return false" TabIndex="1016"/>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label for="">Hora de Convocatoria</label>
                                                <asp:TextBox ID="txtHoraConvocatoria" runat="server" CssClass="time time-240" onkeypress="return false" TabIndex="1015"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="miembrosComision">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="row m0">
                                                    <div class="row m0">
                                                        <div class="col-md-8">
                                                            <div class="form-group">
                                                                <label>Funcionarios de comisiones encargado</label>
                                                                <asp:DropDownList ID="DropDownFuncionarios" runat="server" class="select-240" TabIndex="1008"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                                        <div class="form-group">
                                                            <label>Miembros</label>
                                                            <asp:DropDownList ID="DropDownMiembros" runat="server" class="select-240" TabIndex="1009" ClientIDMode="Static"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                                        <div class="form-group">
                                                            <label>Función</label>
                                                            <asp:DropDownList ID="DropDownFuncion" runat="server" class="select-240" TabIndex="1010" ClientIDMode="Static"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-2 col-md-3 col-sm-6 col-xs-12">
                                                        <label style="color: #F9FBFC">.</label>
                                                        <div class="btn-group btn-group-justified" role="group" aria-label="...">
                                                            <div class="btn-group" role="group">
                                                                <%--<button id="btnAgregar" type="button" class="btn btn-agregar b1s" value="btnAgregar" onclick="AddRow();">Agregar</button>--%>
                                                                <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-agregar b1s" Text="Agregar" CommandArgument='<%# Eval("ComActCodigoSis")%>' OnClick="btn_Click" OnClientClick="ShowTab('#tabMiembrosComision')" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row m0">
                                            <div class="col-md-12">
                                                <div class="table-responsive">

                                                <asp:GridView ID="gv_Miembros" runat="server" CssClass="table table-striped table-bordered table-hover datatable"
                                                ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
                                                AutoGenerateColumns="false" DataKeyNames="ComActCodigoSis,ComActNumero,AdmLegCodigo,FullName,AdmFunCodigo,AdmFunDescripcion" onchange="ShowTab('#tabMiembrosComision');">

                                                <Columns>

                                                    <asp:TemplateField HeaderText="Nombre">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="fNombre" runat="server" Text='<%# Eval("FullName")  %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Posición">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="fPosición" runat="server" Text='<%# Eval("AdmFunDescripcion")  %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <%--<button id="btnQuitar" type="button" class="btn btn-borrar" value="btnQuitar" onclick="DeleteRow(this);">Quitar</button>--%>
                                                        <asp:Button ID="btnQuitar" class="btn btn-borrar" Text="Quitar" runat="server" OnClick="btn_Click" CommandArgument='<%# Eval("AdmLegCodigo")%>' Height="25px" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:TemplateField>

                                                </Columns>
                                                </asp:GridView>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
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

    <input id="hfId" type="hidden" value="0" runat="server" />
</asp:Content>
