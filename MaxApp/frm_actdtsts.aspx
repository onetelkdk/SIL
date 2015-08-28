<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="frm_actdtsts.aspx.cs" Inherits="MaxApp.frm_actdtsts" %>

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
                <h5 class="data_titulo">Actualizar Datos y Estados</h5>
            </div>
            <div class="data">
                <div class="data_body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <!-- Botones Acciones /////////// -->
                            <div class="btn-top-panel">
                                <div class="botones_nev">
                                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="btn btn-nuevo ic_actualizar" OnClick="btnActualizar_Click" />
                                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-guardar" OnClick="btnGuardar_Click" Visible="false" OnClientClick="return confirm('Desea continuar con la operación?');" />
                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-cerrar" OnClick="btnCancelar_Click" Visible="false" />
                                    <div style="float: right">
                                        <asp:DropDownList ID="cboOpcion" runat="server" class="select-240">
                                            <asp:ListItem Value="0">[ Seleccione una Opción ]</asp:ListItem>
                                            <asp:ListItem Value="1">Campos</asp:ListItem>
                                            <asp:ListItem Value="2">Estados</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div style="clear: both"></div>
                            </div>
                            <!-- Final Botones Acciones /////////// -->
                            <div class="panel panel-default" runat="server" id="panelLista">
                                <div class="panel-heading">
                                    <!-- Table Panel/////////// -->
                                    <fieldset>
                                        <legend>Filtrar Por</legend>
                                        <div class="row">
                                            <div class="col-lg-2 col-md-6 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label>Fecha desde</label>
                                                    <asp:TextBox ID="txtFechaDesde" runat="server" class="date fecha-240" />
                                                </div>
                                            </div>
                                            <div class="col-lg-2 col-md-6 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label>Fecha hasta</label>
                                                    <asp:TextBox ID="txtFechaHasta" runat="server" class="date fecha-240" />
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label>Estado</label>
                                                    <asp:DropDownList ID="cboEstados" runat="server" class="select-240"></asp:DropDownList>
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
                                    <asp:GridView ID="gv_Iniciativas" runat="server" CssClass="table table-striped table-bordered table-hover datatable"
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
                                            <asp:TemplateField HeaderStyle-Width="2%">
                                                <HeaderTemplate>
                                                    <asp:CheckBox
                                                        ID="chkSelectAll"
                                                        runat="server"
                                                        AutoPostBack="true"
                                                        OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkRow" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Codigo Iniciativa" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="fID" runat="server" Text='<%# Eval("IniIniCodigoSis")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Codigo Estado" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="fCodigoEstado" runat="server" Text='<%# Eval("AdmEstCodigo")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="#" HeaderStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:Label ID="fIniIniNumero" runat="server" Text='<%# Eval("IniIniNumero")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Estado" HeaderStyle-Width="16%">
                                                <ItemTemplate>
                                                    <asp:Label ID="fDescEstado" runat="server" Text='<%# Eval("AdmEstDescripcion")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Codigo Condicion Actual" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="fCondActual" runat="server" Text='<%# Eval("IniCacCodigo")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Descripción" HeaderStyle-Width="70%">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fIniIniDescripcion" runat="server" Text='<%# Eval("IniIniDescripcion")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--  DIV Formularios /////////////-->
                    <div class="panel-body shadow" runat="server" visible="false" id="PanelMantenimientos">
                        <div class="row m0">
                            <!-- CONTENIDO Formularios /////////////-->
                            <div class="col-lg-12 col-md-12 col-sx-12 col-sm-12 p0">
                                <div class="ActualizarEstados" id="PanelEstados" runat="server" visible="false">

                                    <fieldset class="mb15">
                                        <div class="col-lg-4 col-md-4 col-sm-12">
                                            <div class="form-group">
                                                <label for="IniIniEnviadoComPor">Estado Actual</label>
                                                <asp:TextBox ID="txtEstadoActual" runat="server" CssClass="text-240" ReadOnly="true" />
                                            </div>
                                            <div class="form-group">
                                                <label>Inicia</label>
                                                <asp:TextBox ID="txtFechaInicia" runat="server" class="date fecha-240" />
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-12">
                                            <div class="form-group">
                                                <label>Estados</label>
                                                <asp:DropDownList ID="cboEstadoSiguiente" runat="server" CssClass="select-240"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label>Vence</label>
                                                <asp:TextBox ID="txtFechaVence" runat="server" class="date fecha-240" />
                                            </div>
                                            <div class="form-group">
                                                <label>Preaviso</label>
                                                <asp:DropDownList ID="cboPreaviso" runat="server" CssClass="select-240">
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-12">
                                            <div class="form-group">
                                                <label>Lista Iniciativas</label>
                                                <asp:ListBox ID="ListIniciativas" runat="server" CssClass="multipleSts" />
                                            </div>
                                        </div>

                                        <div class="row m0">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Notas</label>
                                                    <textarea class="area100p-2row" placeholder="Escriba algunas notas aquí" runat="server" id="txtNotaEstado"></textarea>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>

                                </div>
                            </div>
                            <!-- END CONTENIDO Formularios /////////////-->
                        </div>

                        <div class="ActualizasCampo" runat="server" id="PanelCampos">

                            <div class="archivoCorrespondencia" runat="server" id="Panel20">
                                <fieldset class="mb15">
                                    <legend>Archivo y Correspondencia</legend>
                                    <div class="row">
                                        <div class="col-lg-4 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Despachada hacia</label>
                                                <asp:DropDownList ID="cboDespachadaHacia" runat="server" class="select-240" />
                                            </div>
                                        </div>
                                        <div class="col-lg-2 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Núm. oficio despacho</label>
                                                <asp:TextBox ID="txtNroOficioDespacho" runat="server" class="text-240" />
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-12 col-sm-6">
                                            <label>Promulgada</label>
                                            <div class="col-lg-8 col-md-6 col-sm-3 pl0">
                                                <div class="form-group">
                                                    <asp:DropDownList ID="cboPromulgada" runat="server" class="select-240">
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-6 col-sm-6 pr0">
                                                <div class="form-group">
                                                  <asp:TextBox ID="txtFechaPromulgada" runat="server" class="date fecha-240" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <label>Notas de despacho</label>
                                            <textarea id="txtNotaDespacho" runat="server" class="area100p-2row mb15" placeholder="Escriba las notas de despacho" />
                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                            <div class="comisiones" runat="server" id="Panel07">
                                <fieldset class="mb15">
                                    <legend>Comisiones</legend>
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                            <label>Informe comisiones</label>
                                            <div class="col-md-3 col-sm-3 pl0 pr0">
                                                <div class="form-group">
                                                    <asp:DropDownList ID="cboInformeComisiones" runat="server" class="select-240">
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="form-group">
                                                  <asp:TextBox ID="txtFechaInfoComisiones" runat="server" class="date fecha-240" />
                                                </div>
                                            </div>
                                            <div class="col-md-3 col-sm-3 pl0">
                                                <div class="form-group">
                                                    <button class="btn btn-visualizar ic_documento">Adjuntar</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Informe elaborado por</label>
                                                <asp:DropDownList ID="cboInformeElaboradoPor" runat="server" class="select-240" />
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Corrección de estilo</label>
                                                <asp:DropDownList ID="cboCorreccionEstilo" runat="server" class="select-240" />
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Corrección de técnica</label>
                                                <asp:DropDownList ID="cboCorrecionTecnica" runat="server" class="select-240" />
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                            <div class="secGralLegislativa" runat="server" id="Panel60">
                                <fieldset class="mb15">
                                    <legend>Secretaría General Legislativa</legend>
                                    <div class="row">
                                        <div class="col-lg-3 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Condición actual</label>
                                                <asp:DropDownList ID="cboCodicionActual" runat="server" CssClass="select-240"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Veces Devuelto De la Cámara Diputados</label>
                                                <asp:TextBox ID="txtVecesDev" runat="server" class="text-240" onKeyPress="return mtSoloNumeros(event)" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Archivado con el número</label>
                                                <asp:TextBox ID="txtArchivoConNro" runat="server" class="text-240" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-sm-6 col-xs-12">
                                            <label>Iniciativa Priorizada?</label>
                                            <div class="col-lg-4 col-md-4 col-sm-6 pl0 pr0">
                                                <div class="form-group">
                                                    <asp:DropDownList ID="cboIniciativaPriorizada" runat="server" class="select-240">
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
                                        <div class="col-lg-3 col-md-6 col-sm-6 col-xs-12">
                                            <label>Perimida?</label>
                                            <div class="col-lg-4 col-md-4 col-sm-6 pl0 pr0">
                                                <div class="form-group">
                                                    <asp:DropDownList ID="cboPerimida" runat="server" class="select-240">
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
                                        <div class="col-lg-3 col-md-6 col-sm-6 col-xs-12">
                                            <label>Conteo legislaturas iniciado</label>
                                            <div class="col-lg-4 col-md-4 col-sm-6 pl0 pr0">
                                                <div class="form-group">
                                                     <asp:DropDownList ID="cboConteoLegislaturaInicio" runat="server" class="select-240">
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
                                        <div class="col-lg-3 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Número de Legislatura Vigente</label>
                                                <asp:TextBox ID="txtNroLegislaturaVigente" runat="server" class="text-240" onKeyPress="return mtSoloNumeros(event)" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Número de Expediente Cámara Diputados</label>
                                                <asp:TextBox ID="txtNroExpCamDiputados" runat="server" class="text-240" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Enviado a comisiones por</label>
                                                <asp:DropDownList ID="cboEnviadoComisionPor" runat="server" class="select-240" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Análisis Realizado Por</label>
                                                <asp:DropDownList ID="cboAnalisisRealizadoPor" runat="server" class="select-240" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Revisado por</label>
                                                <asp:DropDownList ID="cboRevisadoPor" runat="server" class="select-240" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Despachado por</label>
                                                <asp:DropDownList ID="cboDespachadoPor" runat="server" class="select-240" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Número de promulgación</label>
                                                <asp:TextBox ID="txtNroPromulgacion" runat="server" class="text-240" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Aprobación Presidida Por</label>
                                                <asp:TextBox ID="txtAprobacionPresididaPor" runat="server" class="text-240" MaxLength="150" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Oficio envio a comisiones elaborado por</label>
                                                <asp:DropDownList ID="cboOficioEnviadoComisionPor" runat="server" class="select-240" />
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="proponetes">
                                                <label>Miembros comisión especial</label>
                                                <input type="text" id="txtMiembroComisionEspecial" runat="server" class="tags" placeholder="Agregue separando por ; (punto y coma).">

                                                <label>Secretarios en aprobación:</label>
                                                <input type="text" id="txtSecretariosAprobacion" runat="server" class="tags" placeholder="Agregue separando por ; (punto y coma).">
                                            </div>

                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                            <div class="DTRL" runat="server" id="Panel09">
                                <fieldset class="mb15">
                                    <legend>Informes</legend>
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                            <label>Informe asesores</label>
                                            <div class="col-md-3 col-sm-3 pl0 pr0">
                                                <div class="form-group">
                                                    <asp:DropDownList ID="cboInformeAsesores" runat="server" class="select-240">
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtFechaInfoAccesores" runat="server"  class="date fecha-240" />
                                                </div>
                                            </div>
                                            <div class="col-md-3 col-sm-3 pl0">
                                                <div class="form-group">
                                                    <button class="btn btn-visualizar ic_documento">Adjuntar</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                            <label>Informe OPA</label>
                                            <div class="col-md-3 col-sm-3 pl0 pr0">
                                                <div class="form-group">
                                                    <asp:DropDownList ID="cboInformeOPA" runat="server" class="select-240">
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="form-group">
                                                   <asp:TextBox ID="txtFechaInfoOPA" runat="server"  class="date fecha-240" />
                                                </div>
                                            </div>
                                            <div class="col-md-3 col-sm-3 pl0">
                                                <div class="form-group">
                                                    <button class="btn btn-visualizar ic_documento">Adjuntar</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                            <label>Informe DTRL</label>
                                            <div class="col-md-3 col-sm-3 pl0 pr0">
                                                <div class="form-group">
                                                    <asp:DropDownList ID="cboInformeDTRL" runat="server" class="select-240">
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="form-group">
                                                      <asp:TextBox ID="txtFechaInfoDTRL" runat="server"  class="date fecha-240" />
                                                </div>
                                            </div>
                                            <div class="col-md-3 col-sm-3 pl0">
                                                <div class="form-group">
                                                    <button class="btn btn-visualizar ic_documento">Adjuntar</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                            <label>Otros informes</label>
                                            <div class="col-md-3 col-sm-3 pl0 pr0">
                                                <div class="form-group">
                                                    <asp:DropDownList ID="cboOtrosInformes" runat="server" class="select-240">
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="form-group">
                                                   <asp:TextBox ID="txtFechaOtrosInformes" runat="server"  class="date fecha-240" />
                                                </div>
                                            </div>
                                            <div class="col-md-3 col-sm-3 pl0">
                                                <div class="form-group">
                                                    <button class="btn btn-visualizar ic_documento">Adjuntar</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                            <div class="transcripcion" runat="server" id="Panel63">
                                <fieldset class="mb15">
                                    <legend>Transcripciones</legend>
                                    <div class="row">
                                        <div class="col-lg-3 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Recibido en Transcripción Por</label>
                                                <asp:DropDownList ID="cboRecibidoTranscripcionPor" runat="server" class="select-240" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Transcrito por</label>
                                                <asp:DropDownList ID="cboTranscritoPor" runat="server" class="select-240" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Corregido en Transcipción por</label>
                                                <asp:DropDownList ID="cboCorregidoTranPor" runat="server" class="select-240" />
                                            </div>
                                        </div>
                                       <%-- <div class="col-lg-3 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Validadas las correcciones en Transcripción por</label>
                                                <asp:DropDownList ID="cboValidadasCorreccionPor" runat="server" class="select-240" />
                                            </div>
                                        </div>--%>
                                    </div>
                                </fieldset>
                            </div>

                            <div class="auditoria" runat="server" id="Panel23">
                                <fieldset class="mb15">
                                    <legend>Auditoría</legend>
                                    <div class="row">
                                        <%--<div class="col-lg-4 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Validado el despacho en Transcripción por</label>
                                                <asp:DropDownList ID="cboValidadoDespachoTranPor" runat="server" class="select-240" />
                                            </div>
                                        </div>--%>
                                        <div class="col-lg-4 col-md-6 col-sm-6">
                                            <label>Despachada</label>
                                            <div class="col-lg-8 col-md-6 col-sm-3 pl0 pr0">
                                                <div class="form-group">
                                                    <asp:DropDownList ID="cboDespachada" runat="server" class="select-240">
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-6 col-sm-6 pr0">
                                                <div class="form-group">
                                                   <asp:TextBox ID="txtFechaDespachada" runat="server" class="date fecha-240" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Despachado en Transcripción por</label>
                                                <asp:DropDownList ID="cboDespachadoTranPor" runat="server" class="select-240" />
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                    <!-- End Div Fromularios ////////////////-->
                </div>
            </div>
        </div>
        <!-- End Data Panel /////////// -->
        <% Response.WriteFile("footer.aspx"); %>
    </div>

    <input id="hfIdEstado" type="hidden" value="0" runat="server" />
    <input id="hfCondicionActual" type="hidden" value="0" runat="server" />

</asp:Content>
