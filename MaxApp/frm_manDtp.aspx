<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="frm_manDtp.aspx.cs" Inherits="MaxApp.frm_manDtp" %>

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
                <h5 class="data_titulo">Definir Tipos Documentos</h5>
            </div>
            <div class="data">
                <div class="data_body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <!-- Botones Acciones /////////// -->
                            <div class="btn-top-panel">
                                <div class="btn-nev">
                                    <asp:Button ID="btnNuevo" runat="server" CssClass="btn btn-nuevo ic_mas" Text="Nuevo" OnClick="btnNuevo_Click"   />
                                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" Text="Guardar" Visible="false"  OnClientClick="return confirm('Desea continuar con la operación?');" OnClick="btnGuardar_Click"  />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-brown" Text="Cancelar" Visible="false" OnClick="btnCancel_Click"    />
                                </div>
                                <div style="clear: both"></div>
                            </div>
                            <!-- Final Botones Acciones /////////// -->
                            <div class="panel panel-default" id="panelLista" runat="server">
                                <div class="panel-heading">

                                    <asp:GridView ID="gv_TipoDocumentos" runat="server" CssClass="table table-striped table-bordered table-hover datatable"
                                        ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
                                        AutoGenerateColumns="false" DataKeyNames="AdmDtpCodigo,AdmDtpDescripcion,AdmDclCodigo,AdmStsCodigo">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Código">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fCodigo" runat="server" Text='<%# Eval("AdmDtpCodigo")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Clase Documentos">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fCla" runat="server" Text='<%# Eval("AdmDclDescripcion")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Tipo Documentos">
                                                <ItemTemplate>
                                                    <asp:Literal ID="fTip" runat="server" Text='<%# Eval("AdmDtpDescripcion")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="ImageEdit" CssClass="btn btn-editar ic_lapiz" Text="Editar" runat="server" OnClick="EditarRegistro"  CommandArgument='<%# Eval("AdmDtpCodigo")%>' Height="25px" />
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
                    <div class="panel-body" runat="server" visible="false" id="PanelMantenimientos">
                        <div class="row m0">
                            <!-- CONTENIDO Formularios /////////////-->
                            <div class="content">
                                <div class="col col-4col">
                                    <div class="form-group">
                                        <label>Clase Documentos</label>
                                        <asp:DropDownList ID="cboClaseDocumentos" runat="server" CssClass="select-240"></asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>Descripción</label>
                                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="text-240" />
                                    </div>
                                     
                                   <div class="form-group">
                                         <label>Activo</label>
                                         <asp:DropDownList ID="cboEstado" runat="server" class="select-240">
                                            <asp:ListItem Value="1">Si</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>
                            <!-- END CONTENIDO Formularios /////////////-->
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
