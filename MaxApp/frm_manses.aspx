<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="frm_manses.aspx.cs" Inherits="MaxApp.frm_manses" %>

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
                <h5 class="data_titulo">Definir Sesiones</h5>
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
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-cerrar" Text="Cancelar" Visible="false" OnClick="btnCancel_Click"/>
                                </div>
                                <div style="clear: both"></div>
                            </div>
                            <!-- Final Botones Acciones /////////// -->
                            <div class="panel panel-default" runat="server" id="panelLista">
                                <div class="panel-heading">
                                    <!-- Table Panel/////////// -->
                                   <asp:GridView ID="GridViewSesiones" runat="server" CssClass="table table-striped table-bordered table-hover datatable"
                                        ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
                                        AutoGenerateColumns="false"
                                        DataKeyNames="   IniSesCodigoSis
                                                        ,IniSesSecuencia
                                                        ,IniSesNumero
                                                        ,IniSesTipo
                                                        ,IniSesFecha
                                                        ,AdmLetCodigo
                                                        ,AdmSalCodigo
                                                        ,AdmPcoCodigo
                                                        ,AdmEstCodigo
                                                        ">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" HeaderStyle-Width="18%">
                                                <ItemTemplate>
                                                    <asp:Literal ID="IniSesCodigoSis" runat="server" Text='<%# Eval("IniSesCodigoSis")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Número de Sesión" HeaderStyle-Width="72%">
                                                <ItemTemplate>
                                                    <asp:Literal ID="IniSesNumero" runat="server" Text='<%# Eval("IniSesNumero")  %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="ImageDelete" class="btn btn-editar ic_lapiz" Text="Editar" runat="server" OnClick="EditarRegistro" CommandArgument='<%# Eval("IniSesCodigoSis")%>' Height="25px" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="ImageVisualizar" class="btn btn-visualizar" Text="Ver" runat="server" OnClick="VisualizarRegistro" CommandArgument='<%# Eval("IniSesCodigoSis")%>' Height="25px" />
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
                            <div class="col-md-12">
                                <fieldset class="mt15" style="text-align: center">
                                    <asp:Label ID="lblSesion" runat="server"  CssClass="blue" Text="Label"></asp:Label>
                                </fieldset>
                            </div>
                            <ul id="myTab" class="nav nav-tabs">
                                <li class="active">
                                    <a href="#datos_sesion" data-toggle="tab" data-original-title="" title="">De la Sesión</a>
                                </li>
                                <li class="">
                                    <a href="#dtcrtlses" data-toggle="tab" data-original-title="" title="">Datos de Control</a>
                                </li>
                                <li class="">
                                    <a href="#funcionarios" data-toggle="tab" data-original-title="" title="">Funcionarios</a>
                                </li>
                                <li class="">
                                    <a href="#asistencia" data-toggle="tab" data-original-title="" title="">Asistencia</a>
                                </li>
                            </ul>
                            <div id="myTabContent" class="tab-content">
                                <div class="tab-pane fade active in" id="datos_sesion">
                                    <div class="body-data-tabs">
                                        <div class="col-lg-3 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Número de sesión</label>
                                                <asp:TextBox ID="txtNSesion" runat="server" CssClass="text-240" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Legislatura</label>
                                                <asp:DropDownList ID="ddlLegislatura" runat="server" CssClass="select-240" Enabled="False">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Tipo</label>
                                                <asp:DropDownList ID="ddlTipo" runat="server" CssClass="select-240">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Lugar de sesión</label>
                                                <asp:DropDownList ID="ddlLSesion" runat="server" CssClass="select-240">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Estado</label>
                                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="select-240">
                                                    <asp:ListItem Value="1">Activa</asp:ListItem>
                                                    <asp:ListItem Value="0">Inactiva</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label>Fecha</label>
                                                <asp:TextBox ID="txtFecha" runat="server" CssClass="date fecha-240" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row"></div>
                                </div>
                                <div class="tab-pane fade" id="dtcrtlses">
                                    <div class="col-lg-3 col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label>Hora de inicio</label>
                                            <input type="text" class="time time-240">
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label>Hora de cierre</label>
                                            <input type="text" class="time time-240">
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label>Taquígrafa responsable</label>
                                            <select class="select-240">
                                                <option></option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label>Técnico en trámite</label>
                                            <select class="select-240">
                                                <option></option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label>Presidente</label>
                                            <select class="select-240">
                                                <option></option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label>Secretario 1</label>
                                            <select class="select-240">
                                                <option></option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label>Secretario 2</label>
                                            <select class="select-240">
                                                <option></option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-sm-6">
                                        <div class="form-group">
                                            <label>Acta aprobada</label>
                                            <div class="col-md-4 p0">
                                                <select class="select-240">
                                                    <option></option>
                                                </select>
                                            </div>
                                            <div class="col-md-8 pr0">
                                                <input type="text" class="date fecha-165">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row"></div>
                                </div>
                                <div class="tab-pane fade" id="funcionarios">
                                    <div class="form-group proponetes">
                                        <label>Otros funcionarios</label>
                                        <input type="text" class="tags" placeholder="Agregue separando por ; punto y coma">
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="asistencia">
                                    <div class="form-group proponetes">
                                        <label>Asistencia</label>
                                        <input type="text" class="tags" placeholder="Agregue separando por ; punto y coma">
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
</asp:Content>
