<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="frm_ManMsj.aspx.cs" Inherits="MaxApp.frm_ManMsj" %>

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
                <h5 class="data_titulo">Definir Mensajes del Sistema</h5>
            </div>
            <div class="data">
                <div class="data_body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <!-- Botones Acciones /////////// -->
                            <div class="btn-top-panel">
                                <div class="btn-nev">
                                    <asp:Button ID="btnNuevo" runat="server" CssClass="btn btn-nuevo ic_mas" Text="Nuevo" OnClick="btnNuevo_Click" />
                                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" Text="Guardar" Visible="false"  OnClientClick="return confirm('Desea continuar con la operación?');" OnClick="btnGuardar_Click" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-brown" Text="Cancelar" Visible="false" OnClick="btnCancel_Click" />
                                </div>
                                <div style="clear: both"></div>
                            </div>
                            <!-- Final Botones Acciones /////////// -->
                            <div class="panel panel-default" runat="server" id="panelLista">
                                <div class="panel-heading">
                                    <!-- Table Panel/////////// -->
                                    <asp:GridView ID="gv_Mensajes" runat="server" CssClass="table table-striped table-bordered table-hover datatable" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
                                        AutoGenerateColumns="false" DataKeyNames="Id,Codigo,Programa,Descripcion">
                                        <Columns>

                                             <asp:TemplateField HeaderText="#" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fID" runat="server" Text='<%# Eval("Id")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Código" HeaderStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fCodigo" runat="server" Text='<%# Eval("Codigo")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Programa" HeaderStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fPrograma" runat="server" Text='<%# Eval("Programa") +" // " + Eval("DescPrograma")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Mensaje" HeaderStyle-Width="42%">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fDescripcion" runat="server" Text='<%# Eval("Descripcion")%>' />
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
                                    <!-- End Table Panel/////////// -->
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--  DIV Formularios /////////////-->
                    <div class="panel-body" runat="server"  id="PanelMantenimientos" visible="false">
                        <div class="row m0">
                            <!-- CONTENIDO Formularios /////////////-->
                            <div class="content">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Código</label>
                                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="text-240" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Nombre Programa</label>
                                       <%-- <asp:TextBox ID="txtPrograma" runat="server" CssClass="text-240" />--%>
                                          <asp:DropDownList ID="cboProgramas" CssClass="select-240" runat="server" />
                                    </div>
                                </div>
                                <div class="row m0">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                        <label>Mensaje</label>
                                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="text-240" TextMode="MultiLine" Height="50px" />
                                    </div>
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
        <% Response.WriteFile("footer.aspx"); %>
        <!-- End Footer /////// -->
    </div>

    <input id="hfId" type="hidden" value="0" runat="server" />
</asp:Content>
