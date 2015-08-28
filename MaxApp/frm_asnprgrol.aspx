<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="frm_asnprgrol.aspx.cs" Inherits="MaxApp.frm_asnprgrol" %>

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
                <h5 class="data_titulo">Asignación de Programas a Rol</h5>
            </div>
            <div class="data">
                <div class="data_body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <!-- Botones Acciones /////////// -->
                            <div class="btn-top-panel">
                                <div class="btn-nev" style="margin-bottom: 7px;">
                                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-guardar" Text="Guardar" OnClientClick="return confirm('Desea continuar con la operación?');" OnClick="btnGuardar_Click" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-cerrar" Text="Cancelar" OnClick="btnCancel_Click" />
                                </div>
                            </div>
                            <!-- Final Botones Acciones /////////// -->
                        </div>
                    </div>
                    <!--  DIV Formularios /////////////-->
                    <div class="panel-body">
                        <div class="row m0">
                            <!-- CONTENIDO Formularios /////////////-->
                            <div class="content">
                                <fieldset>
                                    <legend>Seleccione un Rol</legend>
                                    <div class="col col-4col">
                                        <div class="form-group">
                                            <asp:DropDownList ID="cboRoles" CssClass="select-240" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboRoles_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="row mt15">
                                            <div style="padding-left: 15px;">
                                                <div class="alert alert-warning alert-dismissible" role="alert">
                                                    <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                                    Seleccione el Rol al cual desea agregar los programas.
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <fieldset class="mt15">
                                    <legend>Seleccione el Modulo y el Menú</legend>
                                    <div class="col col-4col">
                                        <div class="form-group">
                                            <label>Módulo</label>
                                            <asp:DropDownList ID="cboModulos" AutoPostBack="true" CssClass="select-240" runat="server" OnSelectedIndexChanged="cboModulos_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col col-4col">
                                        <div class="form-group">
                                            <label>Menú</label>
                                            <asp:DropDownList ID="cboOpciones" CssClass="select-240" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboOpciones_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div style="padding: 0 15px;">
                                            <div class="alert alert-warning alert-dismissible" role="alert">
                                                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                                Seleccione el Módulo luego el Menú del cual quiere cargar los programas que desea agregar y/o eliminar al rol.
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <!-- END CONTENIDO Formularios /////////////-->
                        </div>
                        <div class="row m0">
                            <fieldset>
                                <div class="col-md-5">
                                    <h2 class="blue" style="font-weight: bold">OPCIONES DISPONIBLES</h2>
                                    <asp:ListBox ID="ListDisponibles" CssClass="selectrol" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                </div>
                                <div class="col-md-2" align="center" style="margin-top: 150px;">
                                    <a href="#">
                                        <asp:ImageButton ID="imgAgregar" src="images/arrow-right.png" runat="server" CssClass="arrows" OnClick="imgAgregar_Click" />
                                        <%-- <img src="images/arrow-right.png" class="arrows">--%>
                                    </a>
                                    <a href="#">
                                        <asp:ImageButton ID="imgBorrar" src="images/arrow-left.png" runat="server" CssClass="arrows" OnClick="imgBorrar_Click" />
                                        <%-- <img src="images/arrow-left.png" class="arrows">--%>
                                    </a>
                                </div>
                                <div class="col-md-5">
                                    <h2 style="font-weight: bold; color: rgb(64, 127, 68)">OPCIONES ASIGNADAS</h2>
                                    <asp:ListBox ID="ListAsignadas" CssClass="selectrol" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                </div>
                                <div style="clear: both"></div>
                                <div class="row mt15">
                                    <div style="padding: 10px 30px 0px 30px;">
                                        <div class="alert alert-warning alert-dismissible" role="alert">
                                            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                            <strong>Información.!</strong> Mantenga pulsada la tecla control (Ctrl) para seleccionar múltiples opciones. Utilice las flechas para agregar o eliminar opciones al rol.
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
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
</asp:Content>
