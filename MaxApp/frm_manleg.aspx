<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="frm_manleg.aspx.cs" Inherits="MaxApp.frm_manleg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="server">

    <script type="text/javascript">
        function btoUpload()
        {
            document.getElementById('<% = FileUploadASP.ClientID %>').click();
        }

        function ShowImagePreview(input) {
            if (input.files && input.files[0])
            {
                var reader = new FileReader();
                reader.onload = function (e)
                {
                    $('#<% = ImagenPerfil.ClientID %>').prop('src', e.target.result);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
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
                <h5 class="data_titulo">Definir Legisladores</h5>
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

                                    <asp:GridView ID="gv_Legisladores" runat="server" CssClass="table table-striped table-bordered table-hover datatable"
                                        ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
                                        AutoGenerateColumns="false" DataKeyNames="AdmLegCodigo,AdmLegTipoId,AdmLegCedula,FullName,AdmlegNombres,AdmlegApellido1,AdmlegApellido2,AdmlegSexo,AdmFunCodigo,AdmProProvincia,AdmlegFoto,AdmLegHuella,AdmPdoCodigo,AdmPcoCodigo,AdmLegProfesion,AdmLegFechaNac,AdmLegDireccion,AdmPrvCodigo,AdmMunCodigo,AdmSecCodigo,AdmLegCelular,AdmLegTelefonoSenado,AdmLegTelefonoProvincial,AdmLegCorreo,AdmLegApartadoPostal,AdmLegFax,AdmLegTwitter,AdmLegSitioWeb,AdmLegLinkedlin,AdmLegAreasInteres,AdmLegPrioridad,ExpExpNumero,AdmEstCodigo,AdmLegUsuario,AdmLegFechaReg">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Código">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fCodigo" runat="server" Text='<%# Eval("AdmLegCodigo")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Tipo identificación">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fTipoId" runat="server" Text='<%# Eval("AdmParString")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Nombres">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fNombres" runat="server" Text='<%# Eval("AdmlegNombres")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Apellido 1">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fApellido1" runat="server" Text='<%# Eval("AdmlegApellido1")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Apellido 2">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fApellido2" runat="server" Text='<%# Eval("AdmlegApellido2")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEdit" CssClass="btn btn-editar ic_lapiz" Text="Editar" runat="server" CommandArgument='<%# Eval("AdmLegCodigo")%>' Height="25px" OnClick="btn_Click" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnVisualizar" class="btn btn-visualizar" Text="Ver" runat="server" OnClick="btn_Click" CommandArgument='<%# Eval("AdmLegCodigo")%>' Height="25px" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

                                    <!-- Table Panel/////////// -->
                                    <table class="table table-striped table-bordered table-hover datatable">
                                    </table>
                                </div>
                            </div>


                        </div>
                    </div>
                    <!--  DIV Formularios /////////////-->
                    <div class="panel-body shadow" runat="server" visible="true" id="PanelMantenimientos">
                        <div class="row m0">


                            <!-- CONTENIDO Formularios /////////////-->
                            <div class="col-lg-12 col-md-12 col-sx-12 col-sm-12 p0" runat="server" id="formLegisladores">
                                <ul id="myTab" class="nav nav-tabs">
                                    <li class="active"><a href="#datos_personales" data-toggle="tab" data-original-title=""
                                        title="">Datos Personales</a>
                                    </li>
                                    <li>
                                        <a href="#contacto" data-toggle="tab" data-original-title="" title="">Contacto</a>
                                    </li>
                                    <li>
                                        <a href="#comisiones" id="tabComisiones" runat="server" data-toggle="tab" data-original-title="" title="">Comisiones</a>
                                    </li>
                                    <li>
                                        <a href="#excusas" id="tabExcusas" runat="server" data-toggle="tab" data-original-title="" title="">Excusas</a>
                                    </li>
                                    <li>
                                        <a href="#iniciativas" id="tabIniciativas" runat="server" data-toggle="tab" data-original-title="" title="">Iniciativas</a>
                                    </li>
                                </ul>
                                <div id="myTabContent" class="tab-content">
                                    <div class="tab-pane fade in active" id="datos_personales">
                                        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label for="AdmLegTipoId">Tipo de Identificacion</label>
                                                <asp:DropDownList ID="DropDownAdmLegTipoId" runat="server" class="select-default" TabIndex="1001"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label for="nombre">Nombres</label>
                                                <asp:TextBox ID="txtAdmlegNombres" runat="server" CssClass="text-default" TabIndex="1003" />
                                            </div>
                                            <div class="form-group">
                                                <label for="apellido">Segundo Apellido</label>
                                                <asp:TextBox ID="txtAdmlegApellido1" runat="server" CssClass="text-default" TabIndex="1005" />
                                            </div>
                                            <div class="form-group">
                                                <label for="AdmlegFechaNac">Fecha de Nacimiento</label>
                                                <asp:TextBox ID="txtAdmlegFechaNac" runat="server" ReadOnly="true" CssClass="text-default date fecha-240 mask-fecha" TabIndex="1007" />
                                            </div>
                                            <div class="form-group">
                                                <label for="apellido2">Posición (Bloque Partidario)</label>
                                                <asp:DropDownList ID="DropDownAdmFunCodigo" runat="server" class="select-default" TabIndex="1009"></asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label for="AdmLegCedula">Número de Identificación</label>
                                                <asp:TextBox ID="txtAdmLegCedula" runat="server" CssClass="text-default mask-ced" TabIndex="1002"/>

                                            </div>
                                            <div class="form-group">
                                                <label for="apellido2">Primer Apellido</label>
                                                <asp:TextBox ID="txtAdmlegApellido2" runat="server" CssClass="text-default" TabIndex="1004" />
                                            </div>
                                            <div class="form-group">
                                                <label for="AdmlegSexo">Sexo</label>
                                                <asp:DropDownList ID="DropDownAdmLegSexo" runat="server" class="select-default" TabIndex="1006"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label for="AdmPdoCodigo">Partido Político</label>
                                                <asp:DropDownList ID="DropDownAdmPdoCodigo" runat="server" class="select-default" TabIndex="1008"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label for="AdmlegPeriodoElecto">Periodo Electo</label>
                                                <asp:DropDownList ID="DropDownAdmLegPeriodoElecto" runat="server" class="select-default" TabIndex="1010"></asp:DropDownList>
                                            </div>
                                            <fieldset>
                                                <legend>Huella</legend>
                                                <div class="form-group" style="text-align: center;">
                                                    <div class="img-lgs" style="height:205px;">
                                                        <img alt='' src="images/img_huella.png" style="margin: 15% 0;"/>
                                                    </div>
                                                    <button type="button" class="btn-huella" style="margin-bottom: -10px;" tabindex="1012">Tomar huella</button>
                                                </div>
                                            </fieldset>
                                        </div>
                                        <div id="contPerfil" class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                            <div class="form-group" id="dvEstado">
                                                <label for="AdmEstCodigo">Estado</label>
                                                <asp:DropDownList ID="DropDownAdmEstCodigo" runat="server" class="select-default" TabIndex="1011"></asp:DropDownList>
                                            </div>
                                            <fieldset>
                                                <legend>Foto de perfil</legend>
                                                <div class="form-group" style="text-align: center;">
                                                    <div class="img-fto">
                                                        <div style="height:205px;">
                                                            <asp:Image ID="ImagenPerfil" ImageUrl="images/silueta.png" runat="server" />
                                                        </div>
                                                        <%--<img alt='' src="images/silueta.png" />--%>
                                                    </div>
                                                    <asp:FileUpload ID="FileUploadASP" ClientIDMode="Static" onchange="ShowImagePreview(this)" runat="server" CssClass="hidden" accept="image/*" />
                                                    <button id="btnPerfil" type="button" class="btn-fotoperfil" tabindex="1013" onclick="btoUpload()">Subir foto de perfil</button>
                                                    <%--<asp:Button ID="btnPerfil" runat="server" CssClass="btn-fotoperfil" Text="Subir foto de perfil" OnClick="btn_Click" />--%>
                                                    <%--<input type="file" id="flPerfil" name="flPerfil[]" accept="image/*" class="hide" />--%>
                                                </div>
                                            </fieldset>
                                        </div>
                                        <div class="row">
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="contacto">
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label for="territorio ">Dirección</label>
                                                <asp:TextBox ID="txtAdmLegDireccion" runat="server" CssClass="text-240 ic_home" TabIndex="1014"/>
                                            </div>
                                            <div class="form-group">
                                                <label for="sector">Sector</label>
                                                <asp:DropDownList ID="DropDownAdmSecCodigo" runat="server" class="select-default" TabIndex="1017"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label for="AdmlegFax">Fax</label>
                                                <asp:TextBox ID="txtAdmLegFax" runat="server" CssClass="text-240 ic_fax mask-tel" TabIndex="1020" />
                                            </div>
                                            <div class="form-group">
                                                <label for="AdmlegApartadoPostal">Apartado Postal</label>
                                                <asp:TextBox ID="txtAdmLegApartadoPostal" runat="server" CssClass="text-240" TabIndex="1022" />
                                            </div>
                                            <div class="form-group">
                                                <label for="sitioweb">Linkedin</label>
                                                <asp:TextBox ID="txtAdmLegLinkedin" runat="server" CssClass="text-240 ic_linkedin" TabIndex="1025" />
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label for="territorio ">Provincia</label>
                                                <asp:DropDownList ID="DropDownListAdmPrvCodigo" runat="server" class="select-default" TabIndex="1015"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label for="AdmLegCelular ">Teléfono celular</label>
                                                <asp:TextBox ID="txtAdmLegCelular" runat="server" CssClass="text-240 ic_iphone mask-tel" TabIndex="1018" />
                                            </div>
                                            <div class="form-group">
                                                <label for="AdmlegCorreo">Correo Electrónico</label>
                                                <asp:TextBox ID="txtAdmLegCorreo" runat="server" CssClass="text-240 ic_email" TabIndex="1021" />
                                            </div>
                                            <div class="form-group">
                                                <label>Twitter</label>
                                               <%-- <input type="text" class="text-240 ic_twitter" placeholder="@su_usuario">--%>
                                                <asp:TextBox ID="txtAdmLegTwitter" runat="server" CssClass="text-240 ic_twitter" placeholder="@su_usuario" TabIndex="1023" />
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label>Municipio</label>
                                                <asp:DropDownList ID="DropDownAdmMunCodigo" runat="server" class="select-default" TabIndex="1016"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label>Teléfono Senado</label>
                                            <asp:TextBox ID="txtAdmLegTelefonoSenado" runat="server" CssClass="text-240 ic_tel1 mask-tel" TabIndex="1019" />
                                            </div>
                                            
                                            <div class="form-group">
                                                <label>Teléfono Provincial</label>
                                                <asp:TextBox ID="txtAdmLegTelefonoProvincial" runat="server" CssClass="text-240 ic_tel1 mask-tel" TabIndex="1021" />
                                            </div>
                                            <div class="form-group">
                                                <label>Sitio Web</label>
                                                <asp:TextBox ID="txtAdmLegSitioWeb" runat="server" CssClass="text-240 ic_iexplorer" placeholder="www.susitioweb.com" TabIndex="1024" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="comisiones">
                                        
                                        <div class="panel panel-default" runat="server" id="panelComisiones">
                                            <div class="panel-heading">

                                                <div class="table-responsive">

                                                    <asp:GridView ID="gv_Comisiones" runat="server" CssClass="table table-striped table-bordered table-hover dataTable"
                                                    ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
                                                    AutoGenerateColumns="false" DataKeyNames="ComCfmId,ComComCodigo,ExpExpNumero,ComTipCodigo,AdmPcoCodigo,ComCfmFecha,ComCfmCoordinador,ComCfmSecretaria,ComCfmDisuelta,ComCfmAtribucion,ComCfmUserdata,AdmEstCodigo,ComCfmUsuario,ComCfmFechaReg,ComComNombre,ComComUserdata,ComComUsuario,ComComFechaReg,ComMbrSecuencia,ComMbrNombre,AdmFunCodigo,ComMbrInterno,ComMbrUsuario,ComMbrFechaReg,AdmFunDescripcion,AdmCatCodigo,AdmFunUserdata,AdmStsCodigo,AdmFunUsuario,AdmFunFechaReg">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Código">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="fCodigo" runat="server" Text='<%# Eval("ComCfmId")  %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Descripción">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="fDescripcion" runat="server" Text='<%# Eval("ComComNombre")  %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Función">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="fFuncion" runat="server" Text='<%# Eval("AdmFunDescripcion")  %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="excusas">
                                        
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <div class="table-responsive">
                                                    
                                                    <asp:GridView ID="gv_Excusas" runat="server" CssClass="table table-striped table-bordered table-hover dataTable"
                                                    ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
                                                    AutoGenerateColumns="false" DataKeyNames="ComAsiNumReporte,AdmLegCedula,ComAleHoraLLegada,ComAleHoraSalida,ComAlePresente,ComAleExcusa,AdmExcCodigo,ComAleDefinicionExc,ComAleUsuario,ComAleFechaReg,AdmExcDescripcion,AdmExcUsuario,AdmExcFechaReg">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Fecha de actividad">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="fFechaActividad" runat="server" Text='<%# Eval("ComAleFechaReg")  %>' /> <!--POR DEFINIR: Según Itinerario (Semana 3)-->
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Tipo de excusa">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="fTipoExcusa" runat="server" Text='<%# Eval("AdmExcDescripcion")  %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Justificación">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="fJustifacion" runat="server" Text='<%# Eval("ComAleDefinicionExc")  %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Notificar a">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="fNotificarA" runat="server" Text='<%# Eval("ComAleNotificar")  %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="iniciativas">
                                        
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <div class="table-responsive">
                                                    
                                                    <asp:GridView ID="gv_Iniciativas" runat="server" CssClass="table table-striped table-bordered table-hover dataTable"
                                                    ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
                                                    AutoGenerateColumns="false" DataKeyNames="IniIniCodigoSis,IniIniSecuencia,IniIniNumero,AdmpcoCodigo,IniTipCodigo,IniStpCodigo,IniIniDescripcion,IniIniFecha,AdmLetCodigoInicio,AdmAnoCodigo,ComCfmId,ExpExpNumero,IniIniObservaciones,IniIniMateria,AdmCamCodigo,IniIniVecesDev,IniIniConteoLeg,IniIniPoderOrigen,IniIniNumOficioOrig,IniIniProponentes,IniIniMbrComisionesEsp,IniIniPriorizada,IniIniInformeAses,IniIniInformeDtrl,IniIniInformeOpa,IniIniInformeOtros,IniIniNumExpDiputados,IniIniInformeElaborado,IniIniInformeComisiones,IniIniCorreccionEst,IniIniAprobPresidida,IniIniSecretarios,IniIniDigitadoPor,IniIniEnviadoComPor,IniIniRevisadoPor,IniIniOficioEnvComis,IniIniRecibidoTrans,IniIniDespachada,IniIniDespachadopor,IniIniDespachadaHacia,IniIniNumOficioDesp,IniIniPromulgada,IniIniNumProm,IniIniNumArchivo,IniIniNumLegislaturaVigente,IniCacCodigo,IniCacDescripcion,IniIniNotasDespacho,IniIniDespachado,IniIniTranscritoPor,IniIniRevisadoTrans,IniIniDespachadoTrans,IniIniCreadoPor,IniIniCorreccionTec,IniIniCorregidoTrans,IniIniAnalisisLeg,IniIniAnalizadoPor,AdmDepCodigo,AdmEstCodigo,IniIniUsuario,IniIniFechaReg,AdmParClase,AdmParCodigo,AdmParSeq,AdmParDescripcion,AdmCiaCodigo,AdmSucCodigo,AdmParNumerico,AdmParDouble,AdmParString,AdmParBoolean,AdmParFecha,AdmStsCodigo,AdmParUserdata,AdmParUsuario,AdmParFechaReg">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Nro. de iniciativa">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="fNroIniciativa" runat="server" Text='<%# Eval("IniIniNumero")  %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Fecha">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="fFechaIniciativa" runat="server" Text='<%# Eval("IniIniFecha")  %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Descripción">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="fDescripcion" runat="server" Text='<%# Eval("IniIniDescripcion")  %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Estado">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="fEstadoIniciativa" runat="server" Text='<%# Eval("IniCacDescripcion")  %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>

                                                </div>
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

    <input id="hfId" type="hidden" value="0" runat="server" />
</asp:Content>
