<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="frm_manini.aspx.cs" Inherits="MaxApp.frm_manini" %>

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
                <h5 class="data_titulo">Registro de Iniciativas </h5>
            </div>
            <div class="data">
                <div class="data_body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <!-- Botones Acciones /////////// -->
                            <div class="btn-top-panel">
                                <div class="btn-nev">
                                    <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btn btn-nuevo ic_mas" OnClick="btnNuevo_Click" />
                                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-guardar" OnClick="btnGuardar_Click" OnClientClick="return confirm('Desea continuar con la operación?');" />
                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-cerrar" OnClick="btnCancelar_Click" Visible="False" />
                                </div>
                                <div style="clear: both"></div>
                            </div>
                            <!-- Final Botones Acciones /////////// -->
                            <div class="panel panel-default" id="panelLista" runat="server">
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
                                                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-buscar" OnClick="btnBuscar_Click" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="white">.</label>
                                                        <asp:Button ID="BtnLimpiar" runat="server" Text="Limpiar" CssClass="btn-limpiar" OnClick="BtnLimpiar_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                    <asp:GridView ID="GridViewIniciativas" runat="server" CssClass="table table-striped table-bordered table-hover datatable"
                                        ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
                                        AutoGenerateColumns="false"
                                        DataKeyNames="IniIniCodigoSis,IniIniSecuencia,IniIniNumero,AdmpcoCodigo,IniTipCodigo,IniStpCodigo,IniIniDescripcion,IniIniFecha,
                                                      IniIniReintroducida,IniIniFechaReintroducida,AdmLetCodigoInicio,AdmAnoCodigo,ComCfmId,ExpExpNumero,IniIniObservaciones,
                                                      IniIniMateria,AdmCamCodigo,IniIniVecesDev,IniIniConteoLeg,IniIniFechaConteoLeg,IniIniPoderOrigen,IniIniNumOficioOrig,
                                                      IniIniProponentes,IniIniMbrComisionesEsp,IniIniPriorizada,IniIniFechaPriorizada,IniIniInformeAses,IniIniFechaInformeAses,
                                                      IniIniInformeDtrl,IniIniFechaInformeDtrl,IniIniInformeOpa,IniIniFechaInformeOpa,IniIniInformeOtros,IniIniFechaInformeOtros,
                                                      IniIniNumExpDiputados,IniIniInformeElaborado,IniIniInformeComisiones,IniIniFechaInformeComisiones,IniIniCorreccionEst,
                                                      IniIniAprobPresidida,IniIniSecretarios,IniIniDigitadoPor,IniIniEnviadoComPor,IniIniRevisadoPor,IniIniOficioEnvComis,
                                                      IniIniRecibidoTrans,IniIniDespachada,IniIniFechaDespachada,IniIniDespachadopor,IniIniDespachadaHacia,IniIniNumOficioDesp,
                                                      IniIniPromulgada,IniIniFechaPromulgada,IniIniNumProm,IniIniNumArchivo,IniIniNumLegislaturaVigente,IniCacCodigo,IniIniNotasDespacho,
                                                      IniIniDespachado,IniIniTranscritoPor,IniIniRevisadoTrans,IniIniDespachadoTrans,IniIniCreadoPor,IniIniCorreccionTec,IniIniCorregidoTrans,
                                                      IniIniAnalisisLeg,IniIniFechAnalisisLeg,IniIniAnalizadoPor,IniIniPerimida,IniIniFechaPerimida,AdmEstCodigo,IniIniUsuario,IniIniFechaReg">

                                        <Columns>
                                            <asp:TemplateField HeaderText="Iniciativa #" HeaderStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fIniIniNumero" runat="server" Text='<%# Eval("IniIniNumero")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Descripción" HeaderStyle-Width="78%">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fIniIniDescripcion" runat="server" Text='<%# Eval("IniIniDescripcion")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEditar" class="btn btn-editar ic_lapiz" Text="Editar" runat="server" OnClick="EditarRegistro" CommandArgument='<%# Eval("IniIniCodigoSis")%>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnVisualizar" class="btn btn-visualizar ic_documento" Text="Ver" runat="server" OnClick="VerRegistro" CommandArgument='<%# Eval("IniIniCodigoSis")%>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <!-- End Table Panel/////////// -->
                                </div>
                            </div>


                        </div>
                    </div>

                    <!--  DIV Formularios /////////////-->

                    <div class="panel-body shadow" id="PanelMantenimientos" runat="server">
                        <div class="row m0">
                            <!--  TABS Formularios /////////////-->
                            <ul id="myTab" class="nav nav-tabs">
                                <li class="active">
                                    <a href="#inciativa" data-toggle="tab" data-original-title="" title="">De la iniciativa</a>
                                </li>
                                <li>
                                    <a href="#dtcontrol" data-toggle="tab" data-original-title="" title="">Datos de control</a>
                                </li>
                                <li>
                                    <a href="#rev-auditoria" data-toggle="tab" data-original-title="" title="">Control, Revisión y Auditoría</a>
                                </li>
                                <li>
                                    <a href="#flujoenv" data-toggle="tab" data-original-title="" title="">Flujo, envío y destino</a>
                                </li>
                                <li>
                                    <a href="#estados" data-toggle="tab" data-original-title="" title="">Estados</a>
                                </li>
                                <li>
                                    <a href="#docanexos" data-toggle="tab" data-original-title="" title="">Documentos Anexos</a>
                                </li>
                            </ul>
                            <!-- END TABS Formularios /////////////-->
                            <!-- TABS CONTENIDO Formularios /////////////-->
                            <div id="myTabContent" class="tab-content p0">
                                <div class="tab-pane fade in active pt10" id="inciativa">
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>No. iniciativa</label>
                                            <asp:TextBox ID="txtNoIniciativa" runat="server" class="text-240 disabled" />
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Materia</label>
                                            <asp:DropDownList ID="ddlMateria" runat="server" class="select-240"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <label>Iniciativa priorizada?</label>
                                        <div class="col-lg-4 col-md-4 col-sm-6 pl0 pr0">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlIniciativaPriorizada" runat="server" class="select-240">
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-8 col-md-8 col-sm-6 pr0">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtFechaIniciativaPriorizada" runat="server" class="date fecha-240" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Tipo iniciativa</label>
                                            <asp:DropDownList ID="ddlTipoIniciativa" runat="server" class="select-240"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Legislatura de inicio</label>
                                            <asp:DropDownList ID="ddlLegislaturaInicio" runat="server" class="select-240"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Período constitucional</label>
                                            <asp:DropDownList ID="ddlPeriodoConstitucional" runat="server" class="select-240"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Sub-tipo iniciativa</label>
                                            <asp:DropDownList ID="ddlSubTipoIniciativa" runat="server" class="select-240"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Fecha recibido por el senado</label>
                                            <asp:TextBox ID="txtINiIniFecha" runat="server" class="date fecha-240"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row proponetes" style="margin: 0 15px;">
                                        <div class="col-md-12 p0">
                                            <label>Descripción del proyecto</label>
                                            <textarea id="txtDescripcionIniciativa" class="area100p-2row" placeholder="Escriba la descripción del proyecto" runat="server"></textarea>
                                            <label>Proponentes</label>
                                            <input id="txtProponentes" type="text" class="tags" placeholder="Agregue proponetes separando por ; (punto y coma)." runat="server">
                                            <label>Anotaciones especiales</label>
                                            <textarea id="txtObservaciones" class="area100p-1row mb15" placeholder="Anotaciones especiales / Observaciones" runat="server"></textarea>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade in pt10" id="dtcontrol">
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Comisión</label>
                                            <asp:ListBox ID="listComision" runat="server" SelectionMode="Multiple" CssClass="multipleSts"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Año legislativo</label>
                                            <asp:TextBox ID="txtAnioLegislativo" runat="server" class="text-240" onKeyPress="return mtSoloNumeros(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <label>Conteo legislaturas iniciado</label>
                                        <div class="col-lg-4 col-md-4 col-sm-6 pl0 pr0">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlConteoLegislaturaInicio" runat="server" class="select-240">
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-8 col-md-8 col-sm-6 pr0">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtFechaConteoLegInicio" runat="server" class="date fecha-240" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Cámara inicial</label>
                                            <asp:DropDownList ID="ddlCamaraInicial" runat="server" class="select-240"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Veces devuelta cámara diputados</label>
                                            <asp:TextBox ID="txtIniIniVecesDev" runat="server" class="text-240" onKeyPress="return mtSoloNumeros(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <label>Perimida?</label>
                                        <div class="col-lg-4 col-md-4 col-sm-6 pl0 pr0">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlPerimida" runat="server" class="select-240">
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-8 col-md-8 col-sm-6 pr0">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtFechaPerimida" runat="server" class="date fecha-240" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Creado por</label>
                                            <asp:DropDownList ID="ddlCreadoPor" runat="server" class="select-240"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Poder de origen</label>
                                            <asp:DropDownList ID="ddlPoderDeOrigen" runat="server" class="select-240"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Digitado por</label>
                                            <asp:DropDownList ID="ddlDigitadoPor" runat="server" class="select-240"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Número oficio original</label>
                                            <asp:TextBox ID="txtIniIniNumOficioOrig" runat="server" class="text-240" />
                                        </div>
                                    </div>
                                    <div class="row proponetes" style="margin: 0 15px;">
                                        <div class="col-md-12 p0">
                                            <label>Miembros comisión especial</label>
                                            <input id="txtMiembroComisionEspecial" runat="server" type="text" class="tags" placeholder="Agregue separando por ; (punto y coma) ].">
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade in pt10" id="rev-auditoria">
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <label>Análisis legislativo</label>
                                        <div class="col-lg-4 col-md-4 col-sm-6 pl0 pr0">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlAnalisisLegislativo" runat="server" class="select-240">
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-8 col-md-8 col-sm-6 pr0">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtFechaAnalisisLeg" runat="server" class="date fecha-240" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Análisis realizado por </label>
                                            <asp:DropDownList ID="ddlAnalisisRealizadoPor" runat="server" class="select-240" />
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Aprobación presidida por</label>
                                            <asp:TextBox ID="txtAprobacionPresidida" runat="server" class="text-240" MaxLength="150" />
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <label>Promulgada?</label>
                                        <div class="col-lg-4 col-md-4 col-sm-6 pl0 pr0">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlPromulgada" runat="server" class="select-240">
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-8 col-md-8 col-sm-6 pr0">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtFechaPromulgada" runat="server" class="date fecha-240" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Número Promulgación</label>
                                            <asp:TextBox ID="txtNroPromulgacion" runat="server" class="text-240" />
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label># Exp. Cámara diputados</label>
                                            <asp:TextBox ID="txtNroExpCamaraDiputados" runat="server" CssClass="text-default" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <label>Informe comisiones</label>
                                        <div class="col-lg-4 col-md-4 col-sm-6 pl0 pr0">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlInformeComisiones" runat="server" class="select-240" Enabled="false">
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-8 col-md-8 col-sm-6 pr0">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtFechaInfoComisiones" runat="server" class="date fecha-240" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Archivado con el Núm</label>
                                            <asp:TextBox ID="txtArchivadoConNro" runat="server" class="text-240" />
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Corregido transcripción por</label>
                                            <asp:DropDownList ID="ddlCorregidoTranscripcionPor" runat="server" class="select-240" Enabled="false" />
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Corrección de estilo</label>
                                            <asp:DropDownList ID="ddlCorreccionEstilo" runat="server" class="select-240" Enabled="false" />
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Informe elaborado por</label>
                                            <asp:DropDownList ID="ddlInformeElaboradoPor" runat="server" class="select-240" Enabled="false" />
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Corrección de técnica</label>
                                            <asp:DropDownList ID="ddlCorrecionTecnica" runat="server" class="select-240" Enabled="false" />
                                        </div>
                                    </div>
                                    <div class="col-lg-12 col-md-12">
                                        <fieldset class="mb15">
                                            <legend>Informes</legend>
                                            <div class="row">
                                                <div class="col-lg-3 col-md-6 col-sm-6 col-xs-12">
                                                    <label>Informe asesores</label>
                                                    <div class="col-md-3 col-sm-3 pl0 pr0">
                                                        <div class="form-group">
                                                            <asp:DropDownList ID="ddlInformeAsesores" runat="server" class="select-240" Enabled="false">
                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-sm-6">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtFechaInfoAccesores" runat="server" class="text-240" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3 col-sm-3 pl0">
                                                        <div class="form-group">
                                                            <button class="btn btn-visualizar ic_documento">Ver</button>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-6 col-sm-6 col-xs-12">
                                                    <label>Informe OPA</label>
                                                    <div class="col-md-3 col-sm-3 pl0 pr0">
                                                        <div class="form-group">
                                                            <asp:DropDownList ID="ddlInformeOPA" runat="server" class="select-240" Enabled="false">
                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-sm-6">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtFechaInfoOPA" runat="server" class="text-240" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3 col-sm-3 pl0">
                                                        <div class="form-group">
                                                            <button class="btn btn-visualizar ic_documento">Ver</button>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-6 col-sm-6 col-xs-12">
                                                    <label>Informe DTRL</label>
                                                    <div class="col-md-3 col-sm-3 pl0 pr0">
                                                        <div class="form-group">
                                                            <asp:DropDownList ID="ddlInformeDTRL" runat="server" class="select-240" Enabled="false">
                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-sm-6">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtFechaInfoDTRL" runat="server" class="text-240" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3 col-sm-3 pl0">
                                                        <div class="form-group">
                                                            <button class="btn btn-visualizar ic_documento">Ver</button>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-6 col-sm-6 col-xs-12">
                                                    <label>Otros informes</label>
                                                    <div class="col-md-3 col-sm-3 pl0 pr0">
                                                        <div class="form-group">
                                                            <asp:DropDownList ID="ddlOtrosInformes" runat="server" class="select-240" Enabled="false">
                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-sm-6">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtFechaOtrosInformes" runat="server" class="text-240" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3 col-sm-3 pl0">
                                                        <div class="form-group">
                                                            <button class="btn btn-visualizar ic_documento">Ver</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="row proponetes m0">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>Secretarios en aprobación</label>
                                                <input id="txtSecretariosAprobacion" type="text" class="tags" placeholder="Agregue Secretarios separando por ; (punto y coma)." runat="server">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade in pt10" id="flujoenv">
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Enviado a comisiones por</label>
                                            <asp:DropDownList ID="ddlEnviadoComisionPor" runat="server" class="select-240" Enabled="false" />
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <label>Despachada</label>
                                        <div class="col-lg-4 col-md-4 col-sm-6 pl0 pr0">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlDespachada" runat="server" class="select-240" Enabled="false">
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-8 col-md-8 col-sm-6 pr0">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtFechaDespachada" runat="server" class="text-240" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Despachado por</label>
                                            <asp:DropDownList ID="ddlDespachadoPor" runat="server" class="select-240" Enabled="false" />
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Revisado por</label>
                                            <asp:DropDownList ID="ddlRevisadoPor" runat="server" class="select-240" Enabled="false" />
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Despachada hacia</label>
                                            <asp:DropDownList ID="ddlDespachadaHacia" runat="server" class="select-240" Enabled="false" />
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Revisado en transcripción por</label>
                                            <asp:DropDownList ID="ddlRevisadoTranscripcionPor" runat="server" class="select-240" Enabled="false" />
                                        </div>
                                    </div>

                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Oficio envío a comisiones por</label>
                                            <asp:DropDownList ID="ddlOficioEnviadoComisionPor" runat="server" class="select-240" Enabled="false" />
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Número oficio despachado</label>
                                            <asp:TextBox ID="txtNroOficioDespacho" runat="server" class="text-240" Enabled="false" />
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Despachado a transcripcion por</label>
                                            <asp:DropDownList ID="ddlDespachadoTranscripcionPor" runat="server" class="select-240" Enabled="false" />
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Recibido en transcripción por</label>
                                            <asp:DropDownList ID="ddlRecibidoTranscripcionPor" runat="server" class="select-240" Enabled="false" />
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Número legislatura vigente</label>
                                            <asp:TextBox ID="txtNroLegislaturaVigente" runat="server" class="text-240" Enabled="false" />
                                        </div>
                                    </div>
                                    <div class="row m0">
                                        <div class="col-md-12">
                                            <label>Notas de despacho</label>
                                            <textarea id="txtNotaDespacho" runat="server" class="area100p-2row mb15" placeholder="Escriba las notas de despacho" />
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade in pt10" id="estados">
                                    <div class="row">
                                        <div class="col-md-12" style="padding: 0 30px;">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Condición actual</label>
                                                    <asp:DropDownList ID="ddlCondicionActual" runat="server" class="select-240" />
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <!-- Table Panel/////////// -->
                                                    <div class="table-responsive">

                                                        <asp:GridView ID="gv_Estados" runat="server" CssClass="table table-striped table-bordered table-hover datatable"
                                                            ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
                                                            AutoGenerateColumns="false">

                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Número" HeaderStyle-Width="8%">
                                                                    <ItemTemplate>
                                                                        <asp:Literal ID="fNumero" runat="server" Text='<%# Eval("IniIniNumero")  %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Fecha" HeaderStyle-Width="14%">
                                                                    <ItemTemplate>
                                                                        <asp:Literal ID="fFecha" runat="server" Text='<%# Eval("IniHisFechaReg")  %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Estado" HeaderStyle-Width="68%">
                                                                    <ItemTemplate>
                                                                        <asp:Literal ID="fEstado" runat="server" Text='<%# Eval("AdmEstDescripcion")  %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>

                                                    </div>
                                                    <!-- End Table Panel/////////// -->
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade in pt10" id="docanexos">
                                    <div class="row">
                                        <div class="col-md-12" style="padding: 0 30px;">
                                            <div class="panel-body mb15">
                                                <!-- Table Panel/////////// -->
                                                <div class="table-responsive">

                                                    <asp:GridView ID="gv_Documentos" runat="server" CssClass="table table-striped table-bordered table-hover datatable"
                                                        ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
                                                        AutoGenerateColumns="false">

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Número" HeaderStyle-Width="12%">
                                                                <ItemTemplate>
                                                                    <asp:Literal ID="fNumero" runat="server" Text='<%# Eval("AdmDocSecuencia")  %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Estado" HeaderStyle-Width="78%">
                                                                <ItemTemplate>
                                                                    <asp:Literal ID="fEstado" runat="server" Text='<%# Eval("AdmDocTitulo")  %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">

                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnVisualizar" class="btn btn-visualizar ic_documento" Text="Ver" runat="server" Height="25px" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>

                                                </div>
                                                <!-- End Table Panel/////////// -->
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- END TABS CONTENIDO Formularios /////////////-->
                        </div>
                    </div>

                    <div class="row">
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
