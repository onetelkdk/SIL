<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="frm_manmod.aspx.cs" Inherits="MaxApp.frm_manmod" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="server">
    <div class="main_container">
        <div class="main_menu">
           <!--  Accordion Menu ////////// -->
            <div class="menuleft" style="">
                <div class="nombre_top_menu">
                    <a href="Menu.aspx">
                        <img src="images/atras.png" style="margin-top: 17px; float: left" title="REGRESAR AL MENU ANTERIOR">
                        <h2> <asp:Label ID="lblModulo" runat="server" Text="N/A"></asp:Label></h2>
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
                <h5 class="data_titulo">Definir Modulos del Sistema</h5>
            </div>
            <div class="data">
                <div class="data_body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        	<!-- Botones Acciones /////////// -->
                            <div class="btn-top-panel">
                                <div class="btn-nev">
                                	<asp:Button ID="btnNuevo" runat="server" CssClass="btn btn-nuevo ic_mas" Text="Nuevo" OnClick="btnNuevo_Click"  />
                                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-guardar" Text="Guardar" Visible="false"  OnClientClick="return confirm('Desea continuar con la operación?');" OnClick="btnGuardar_Click" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-cerrar" Text="Cancelar" Visible="false" OnClick="btnCancel_Click"  />
                                </div>
                                 <div style="clear: both"></div>
                            </div>
                            <!-- Final Botones Acciones /////////// -->
                            <div class="panel panel-default" runat="server" id="panelLista">
                                <div class="panel-heading">
                                    <!-- Table Panel/////////// -->
                                    <asp:GridView ID="gv_Modulos" runat="server" CssClass="table table-striped table-bordered table-hover datatable" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
                                        AutoGenerateColumns="false" DataKeyNames="Id,AdmmodCodigo,AdmmodNombre,AdmmodOrden,AdmmodIcono,AdmModColorFondo,AdmModTamano,AdmEstCodigo">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Código">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fCodigo" runat="server" Text='<%# Eval("AdmmodCodigo")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Nombre">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fNombre" runat="server" Text='<%# Eval("AdmmodNombre")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Orden">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fOrden" runat="server" Text='<%# Eval("AdmmodOrden")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEdit" CssClass="btn btn-editar ic_lapiz" Text="Editar" runat="server" OnClick="EditarRegistro" CommandArgument='<%# Eval("Id")%>' Height="25px" />
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
                            <div class="content">
                                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label>Código Módulo</label>
                                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="text-240" />
                                    </div>
                                    <div class="form-group">
                                        <label>Nombre</label>
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="text-240"/>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label>Icono</label>
                                        <asp:TextBox ID="txtIcono" runat="server" CssClass="text-240" />
                                    </div>
                                    <div class="form-group">
                                        <label>Color Fondo</label>
                                        <asp:TextBox ID="txtColorFondo" runat="server" CssClass="text-240" />
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label>Orden</label>
                                        <asp:TextBox ID="txtOrden" runat="server" CssClass="text-115" />
                                    </div>
                                    <div class="form-group">
                                        <label>Tamaño</label>
                                        <asp:TextBox ID="txtTamano" runat="server" CssClass="text-240" />
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label>Estado</label>
                                        <select class="select-240">
                                            <option>Opciones</option>
                                        </select>
                                        <asp:CheckBox ID="chkEstado" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <!-- END CONTENIDO Formularios /////////////-->
                        </div>
                    </div> 
                    <!-- End Div Fromularios ////////////////-->
                </div>
            </div>
        </div>
        <!-- End Data Panel /////////// -->
        <!-- Footer /////// -->
        <% Response.WriteFile("footer.aspx"); %>>
        <!-- End Footer /////// -->
    </div>

    <input id="hfId" type="hidden" value="0" runat="server" />

</asp:Content>
