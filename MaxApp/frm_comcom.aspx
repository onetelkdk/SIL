<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="frm_comcom.aspx.cs" Inherits="MaxApp.frm_comcom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="server">
    <script type="text/javascript">
        function ActivarComision()
        {
            if (document.getElementById('<% = idComision.ClientID %>').value == 0) {
                if (confirm("Desea Activar la Comisión?")) {
                    document.getElementById('<% = Activada.ClientID %>').value = 1;
                }
                else {
                    document.getElementById('<% = Activada.ClientID %>').value = 0;
                }
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
                <h5 class="data_titulo">Conformación de Comisiones</h5>
            </div>
            <div class="data">
                <div class="data_body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <!-- Botones Acciones /////////// -->
                            <div class="btn-top-panel">
                                <div class="btn-nev" style="margin-bottom: 7px;">
                                    <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btn btn-nuevo ic_mas" OnClick="btnNuevo_Click" />
                                    <asp:Button ID="btnEditar2" runat="server" Text="Editar" class="btn btn-editar ic_lapiz" Visible="False" />
                                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-guardar" Text="Guardar" OnClick="btnGuardar_Click" OnClientClick="ActivarComision();" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-cerrar" Text="Cancelar" Visible="False" OnClick="btnCancel_Click" />
                                </div>
                                <div style="clear: both"></div>
                            </div>
                            <!-- Final Botones Acciones /////////// -->
                            <div class="panel panel-default" runat="server" id="panelLista">
                                <div class="panel-heading">
                                    <!-- Table Panel/////////// -->
                                    <asp:GridView ID="GridViewConfirmacionComision" runat="server" CssClass="table table-striped table-bordered table-hover datatable"
                                        ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
                                        AutoGenerateColumns="false"
                                        DataKeyNames=" ComCfmId
                                                      ,ComComCodigo
                                                      ,ComComNombre
                                                      ,ExpExpNumero
                                                      ,ComTipCodigo
                                                      ,ComTipDescripcion
                                                      ,AdmPcoCodigo
                                                      ,AdmpcoDescripcion
                                                      ,ComCfmFecha
                                                      ,ComCfmCoordinador
                                                      ,ComCfmSecretaria
                                                      ,ComCfmDisuelta
                                                      ,ComCfmAtribucion
                                                      ,ComCfmUserdata
                                                      ,AdmEstCodigo
                                                      ,ComCfmUsuario
                                                      ,ComCfmFechaReg">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" HeaderStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:Literal ID="ComCfmId" runat="server" Text='<%# Eval("ComCfmId")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Descripción" HeaderStyle-Width="78%">
                                                <ItemTemplate>
                                                    <asp:Literal ID="ComComNombre" runat="server" Text='<%# Eval("ComComNombre")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="ImageDelete" class="btn btn-editar ic_lapiz" Text="Editar" runat="server" OnClick="EditarRegistro" CommandArgument='<%# Eval("ComCfmId")%>' Height="25px" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="ImageVisualizar" class="btn btn-visualizar" Text="Ver" runat="server" OnClick="VisualizarRegistro" CommandArgument='<%# Eval("ComCfmId")%>' Height="25px" />
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
                                <div class="row m0">
                                    <div class="form-group">
                                        <fieldset style="text-align: center; width: 100%;">
                                            <legend class="m0 p0">Período Constitucional</legend>
                                            <%--<h1 class="blue m0 fwb">2010-2016</h1>--%>
                                            <asp:Label ID="lblPConstitucional" runat="server" Text="Label" CssClass="blue m0 fwb"></asp:Label>
                                        </fieldset>
                                    </div>
                                </div>
                                <ul id="myTab" class="nav nav-tabs">
                                    <li class="active">
                                        <a href="#datos_ctrl" data-toggle="tab" data-original-title="" title="">Datos de Control</a>
                                    </li>
                                    <li>
                                        <a href="#miembros" data-toggle="tab" data-original-title="" title="">Miembros / Integrantes</a>
                                    </li>
                                </ul>
                                <div id="myTabContent" class="tab-content">
                                    <div class="tab-pane fade in active" id="datos_ctrl">
                                        <div class="row">
                                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label>Comisión</label>
                                                    <asp:DropDownList ID="ddlComision" runat="server" CssClass="select-240">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>Coordinador técnico</label>
                                                    <asp:DropDownList ID="ddlCTecnico" runat="server" CssClass="select-240">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label>Tipo</label>
                                                    <asp:DropDownList ID="ddlTipo" runat="server" CssClass="select-240">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>Secretaria</label>
                                                    <asp:DropDownList ID="ddlSecretaria" runat="server" CssClass="select-240">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label>Estado</label>
                                                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="select-240">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>Fecha</label>
                                                    <asp:TextBox ID="dtFecha" runat="server" CssClass="date fecha-240"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row m0 plr15">
                                                <div class="col-md-12 p0">
                                                    <div class="form-group">
                                                       <label>Atribuciones</label>
                                                       <asp:TextBox ID="txtAtribuciones" placeholder="Atribuciones" runat="server" CssClass="area100p-2row" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>        
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="miembros"> 
                                        <div class="row miembrosotracamara">
                                                <div class="col-md-12">
                                                    <%--<label>Miembros de otra cámara</label>--%>
                                                    <asp:Label ID="lblMiembros" runat="server" Text="Miembros de otra cámara"></asp:Label>
                                                    <asp:TextBox ID="txtMCamra" runat="server" placeholder="Agregue separando por ; (punto y coma)."  CssClass="tags"></asp:TextBox>
                                                    <%--<textarea id="txtMCamra" runat="server" placeholder="Agregue separando por ; (punto y coma)." class="tags" cols="20" rows="4"></textarea>--%>
                                                </div>
                                            </div> 
                                        <fieldset class="mb15">
                                            <div class="row">
                                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label>Miembros</label>
                                                     <asp:DropDownList ID="ddlMiembros" runat="server" CssClass="select-240">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label>Función</label>
                                                     <asp:DropDownList ID="ddlFuncion" runat="server" CssClass="select-240">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-2 col-md-3 col-sm-6 col-xs-12">
                                                <label style="color:#F9FBFC">.</label>
                                                <div class="btn-group btn-group-justified" role="group" aria-label="...">
                                                  <div class="btn-group" role="group">
                                                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-agregar b1s"  OnClick="AgregarRegistroViewState"/>
                                                  </div>
                                                </div>
                                            </div>
                                        </div> 
                                        </fieldset>
                                        <div class="col-md-12 p0">
                                            <fieldset class="mb15">
                                                <legend>Listado de Legisladores</legend>
                                                <div class="table-responsive">
                                                   <asp:GridView ID="GridViewLegisladores" runat="server" CssClass="table table-striped table-bordered table-hover datatable"
                                        ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
                                        AutoGenerateColumns="false"
                                        DataKeyNames=" ComCfmId
                                                      ,ComMbrSecuencia
                                                      ,ComMbrNombre
                                                      ,AdmFunDescripcion">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" HeaderStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:Literal ID="ComMbrSecuencia" runat="server" Text='<%# Eval("ComMbrSecuencia")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Legislador" HeaderStyle-Width="78%">
                                                <ItemTemplate>
                                                    <asp:Literal ID="ComMbrNombre" runat="server" Text='<%# Eval("ComMbrNombre")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Funcion" HeaderStyle-Width="78%">
                                                <ItemTemplate>
                                                    <asp:Literal ID="AdmFunDescripcion" runat="server" Text='<%# Eval("AdmFunDescripcion")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnBorrar" runat="server" Text="Borrar" CssClass="btn btn-borrar b1s"  OnClick="BorrarRegistroViewState" CommandArgument='<%# Eval("ComMbrNombre")%>' Height="25px" />
                                                    <%--<asp:Button ID="ImageDelete" class="btn btn-editar ic_lapiz" Text="Editar" runat="server" OnClick="EditarRegistro" CommandArgument='<%# Eval("ComCfmId")%>' Height="25px" />--%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                                </div>
                                            </fieldset> 
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
    <input id="Activada" type="hidden" value="0" runat="server" />
    <input id="idComision" type="hidden" value="0" runat="server" />
</asp:Content>
