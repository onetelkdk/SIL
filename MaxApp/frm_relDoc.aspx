<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="frm_relDoc.aspx.cs" Inherits="MaxApp.frm_relDoc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="server">
    <script type="text/javascript">
        function btoUpload() {
            document.getElementById('<% = FileUploadASP.ClientID %>').click();
        }
        function Upload() {
            document.getElementById('<% = btnCargar.ClientID %>').click();
        }
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
                <h5 class="data_titulo">Relacionador de Documentos</h5>
            </div>
            <div class="data">
                <div class="data_body">
                    <div class="row m0">
                        <div class="col-lg-12 p0" id="cuerpoAdjuntar">
                            <div class="botones_nev mb15">
                                <%--<button type="button" class="btn btn-nuevo ic_documento_adjunto" id="ic_adjuntardoc" onclick="traerFormAdjuntar()">Adjuntar documentos</button>--%>
                                <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-guardar" Text="Guardar" Visible="false" OnClick="btnGuardar_Click" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-cerrar" Text="Cancelar" Visible="false" OnClick="btnCancel_Click" />
                                <asp:Button ID="btnCargar" runat="server" CssClass="hidden" Text="prueba" OnClick="CargarArchivo" />
                            </div>
                            <%--  --%>
                            <fieldset id="FBuscar" runat="server">
                                <legend>Buscar</legend>
                                <div class="row">
                                    <div class="col-lg-2 col-md-3 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label>Documento</label>
                                            <asp:DropDownList ID="ddlDocumento" runat="server" CssClass="select-240">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
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
                                                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn-limpiar" OnClick="btnLimpiar_Click" />
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
                            <br>
                            <div class="panel-body shadow" id="gridresultados" runat="server">
                                <asp:GridView ID="GridViewDocumentos" runat="server" CssClass="table table-striped table-bordered table-hover datatable"
                                    ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
                                    AutoGenerateColumns="false"
                                    DataKeyNames=" Numero
                                                      ,Fecha
                                                      ,Descripcion">

                                    <Columns>
                                        <asp:TemplateField HeaderText="#" HeaderStyle-Width="12%">
                                            <ItemTemplate>
                                                <asp:Literal ID="Numero" runat="server" Text='<%# Eval("Numero")  %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Descripción" HeaderStyle-Width="78%">
                                            <ItemTemplate>
                                                <asp:Literal ID="Descripcion" runat="server" Text='<%# Eval("Descripcion")  %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fecha" HeaderStyle-Width="12%">
                                            <ItemTemplate>
                                                <asp:Literal ID="Fecha" runat="server" Text='<%# Eval("Fecha")  %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="ImageAdjuntar" class="btn btn-nuevo ic_documento_adjunto" Text="Adjuntar" runat="server" OnClick="Adjuntar" CommandArgument='<%# Eval("Numero")%>' Height="25px" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="ImageVisualizar" class="btn btn-visualizar" Text="Ver" runat="server" OnClick="VisualizarRegistro" CommandArgument='<%# Eval("ComCfmId")%>' Height="25px" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <!-- Formulario para adjuntar /////////////////////////////////-->

                        <div id="formularioAdjuntar" tabindex="-1" runat="server" visible="false">
                            <div class="data_body shadow formularioAdjuntar" style="background-color: #f7f7f7;" runat="server" id="gridAdjunta">

                                <!-- CUERPO ///////////////////////////////////////////// -->
                                <div class="modal-body">
                                    <div class="nav warning-color-reldoc" role="alert" align="center">
                                        <div id="tituloIni">
                                            
                                            <h1 class="reldoch1"><asp:Label ID="lblDescripcion" runat="server" Text="Label" CssClass="blue"></asp:Label></h1>
                                        </div>
                                    </div>
                                    <%--  --%>
                                    <div class="tab-pane fade in active" id="datos_ctrl">
                                        <div class="row">
                                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label>Titulo del documento</label>
                                                    <asp:TextBox ID="txtTDocumento" placeholder="Titulo del documento" runat="server" CssClass="text-240"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label>Clase de Documento</label>
                                                    <asp:DropDownList ID="ddlCDocumento" runat="server" CssClass="select-240">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label>Tipo de Documento</label>
                                                    <asp:DropDownList ID="ddlTDocumento" runat="server" CssClass="select-240">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <br></br>
                                                    <div class="col col-2col fr">
                                                        <asp:FileUpload ID="FileUploadASP" ClientIDMode="Static" runat="server" CssClass="hidden" onchange="Upload()" />
                                                        <button id="btnSubir" type="button" class="btn-huella" style="margin-bottom: -10px;" onclick="btoUpload()">Subir documento</button>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                    <%--  --%>

                                    <%--  --%>

                                    <fieldset>
                                        <div class="col-lg-12 col-sx-12 col-sm-12 p0">
                                            <div class="table-responsive" id="dvHistorico">
                                                <asp:GridView ID="GridViewDocumentosCargados" runat="server" CssClass="table table-striped table-bordered table-hover datatable"
                                                    ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
                                                    AutoGenerateColumns="false"
                                                    DataKeyNames=" AdmDocSecuencia
                                                      ,AdmDocTitulo,AdmDocRuta,AdmDocExtension"
                                                      >

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Secuencia" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="AdmDocSecuencia" runat="server" Text='<%# Eval("AdmDocSecuencia")  %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Titulo" HeaderStyle-Width="78%">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="AdmDocTitulo" runat="server" Text='<%# Eval("AdmDocTitulo")  %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <%--<asp:TemplateField HeaderText="Fecha" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="AdmDocFechaReg" runat="server" Text='<%# Eval("AdmDocFechaReg")  %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:Button ID="ImageVer" class="estado btn-warning ic_view24" Text="Ver documento" runat="server" OnClick="VisualizarDocumento" CommandArgument='<%# Eval("AdmDocTitulo")%>' Height="25px" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnBorrar" runat="server" Text="Borrar" CssClass="btn btn-borrar b1s" OnClick="BorrarRegistroViewState" CommandArgument='<%# Eval("AdmDocTitulo")%>' Height="25px" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                            </div>

                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                        </div>
                        <!-- Final formulario para adjuntar ////////////////////////////////-->
                    </div>
                </div>

            </div>

        </div>

    </div>
    <!-- End Data Panel /////////// -->
    <% Response.WriteFile("footer.aspx"); %>
</asp:Content>
