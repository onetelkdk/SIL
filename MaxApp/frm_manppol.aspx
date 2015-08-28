<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="frm_manppol.aspx.cs" Inherits="MaxApp.frm_manppol" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="server">
    <script type="text/javascript">
        function btoUpload() {
            document.getElementById('<% = FileUploadASP.ClientID %>').click();
        }

        function ShowImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<% = ImagenLogo.ClientID %>').prop('src', e.target.result);
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
                <h5 class="data_titulo">Definir Partidos Politicos</h5>
            </div>
            <div class="data">
                <div class="data_body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <!-- Botones Acciones /////////// -->
                            <div class="btn-top-panel">
                                <div class="btn-nev" style="margin-bottom: 7px;">
                                    <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btn btn-nuevo ic_mas" OnClick="btnNuevo_Click" />
                                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-guardar" Text="Guardar" OnClick="btnGuardar_Click" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-cerrar" Text="Cancelar" Visible="False" OnClick="btnCancel_Click" />
                                </div>
                                <div style="clear: both"></div>
                            </div>
                            <!-- Final Botones Acciones /////////// -->
                            <div class="panel panel-default" runat="server" id="panelLista">
                                <div class="panel-heading">
                                    <!-- Table Panel/////////// -->
                                    <asp:GridView ID="GridViewPartidos" runat="server" CssClass="table table-striped table-bordered table-hover datatable"
                                        ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
                                        AutoGenerateColumns="false"
                                        DataKeyNames="AdmPdoCodigo,AdmPdoSiglas,AdmPdoDescripcion,AdmStsCodigo,AdmPdoLogo">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" HeaderStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:Literal ID="AdmPdoCodigo" runat="server" Text='<%# Eval("AdmPdoCodigo")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Siglas" HeaderStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Literal ID="AdmPdoSiglas" runat="server" Text='<%# Eval("AdmPdoSiglas")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Descripción" HeaderStyle-Width="78%">
                                                <ItemTemplate>
                                                    <asp:Literal ID="AdmPdoDescripcion" runat="server" Text='<%# Eval("AdmPdoDescripcion")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="ImageDelete" class="btn btn-editar ic_lapiz" Text="Editar" runat="server" OnClick="EditarRegistro" CommandArgument='<%# Eval("AdmPdoCodigo")%>' Height="25px" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="ImageVisualizar" class="btn btn-visualizar" Text="Ver" runat="server" OnClick="VisualizarRegistro" CommandArgument='<%# Eval("AdmPdoCodigo")%>' Height="25px" />
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
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Siglas</label>
                                    <asp:TextBox ID="txtSiglas" runat="server" CssClass="text-240"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Descripción</label>
                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="text-240"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Estado</label>
                                    <asp:DropDownList ID="ddlEstado" runat="server" class="select-240">
                                        <asp:ListItem Value="True">Activo</asp:ListItem>
                                        <asp:ListItem Value="False">Inactivo</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <fieldset>
                                    <legend>Logo</legend>
                                    <div class="form-group" style="text-align: center;">
                                        <div class="img-lgs">
                                            <asp:Image ID="ImagenLogo" ImageUrl="images/logotipo-inst.png" Style="margin: 15% 0;" runat="server" />
                                        </div>
                                        <asp:FileUpload ID="FileUploadASP" ClientIDMode="Static" onchange="ShowImagePreview(this)" runat="server" CssClass="hidden" accept="image/*" />
                                        <button id="btnSubir" type="button" class="btn-huella" style="margin-bottom: -10px;" onclick="btoUpload()">Subir imagen</button>
                                        <%--<asp:Button ID="btnSubir" runat="server" CssClass="btn-huella" style="margin-bottom: -10px;" Text="Subir imagen" OnClick="btnSubir_Click" />--%>
                                    </div>
                                </fieldset>
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
        <%-- <footer class="Mfooter">
            <p class="footer">Copyright © 2010 Senado de la República Dominicana | Derechos Reservados</p>
        </footer>--%>
        <!-- End Footer /////// -->
    </div>
    <% Response.WriteFile("footer.aspx"); %>
</asp:Content>
