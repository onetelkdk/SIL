<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="frm_cieleg.aspx.cs" Inherits="MaxApp.frm_cieleg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="server">
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
				<h5 class="data_titulo">Cierre de Legislaturas</h5>
			</div>
			<div class="data">
				<div class="data_body shadow m0">
					<div class="col-lg-12 col-md-12 col-sx-12 col-sm-12">

						<!-- Botones Acciones /////////// -->
						<div class="btn-top-panel">
							<div class="btn-nev" style="margin-bottom: 7px;">
								<asp:Button ID="btnPreCerrar" runat="server" CssClass="btn btn-lock" Text="Pre-Cerrar Legislatura" OnClick="btn_Click" />
								<asp:Button ID="btnCerrar" runat="server" CssClass="btn btn-lock" Text="Cerrar Legislatura" OnClick="btn_Click" />
								<asp:Button ID="btnImprimir" runat="server" CssClass="btn btn-print" Text="Imprimir" OnClick="btn_Click" />
								<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-cerrar" Text="Salir" OnClick="btn_Click" />
							</div>
						<div style="clear: both"></div>
						</div>
					<!-- Final Botones Acciones /////////// -->



						<div class="tab-pane fade in active" id="datos_control">
								<fieldset class="mb15">
									<legend>Fecha de Cierre</legend>
									<fieldset class="mr10 p0" style="padding-top: 3px !important;">
									<div style="text-align: center">
<%--										<h1 class="blue m0">0012-2451-PLO</h1>--%>
										<h1><asp:Label ID="labelLegislatura" CssClass="blue m0" runat="server" Text="0012-2451-PLO" /></h1>
									</div>
									</fieldset>
									<div class="col-lg-3 col-md-4 col-sm-12">
										<div class="form-group">
											<label>Fecha inicio</label>
											<asp:TextBox ID="txtFechaDesde" runat="server" class="text-default" ReadOnly="true" />
										</div>
									</div>
									<div class="col-lg-3 col-md-4 col-sm-12">
										<div class="form-group">
											<label>Fecha final</label>
											<asp:TextBox ID="txtFechaHasta" runat="server" class="text-default" ReadOnly="true" />
										</div>
									</div>
									<div class="col-lg-3 col-md-4 col-sm-12">
										<div class="form-group">
											<label class="white">.</label>
											<asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-buscar" Text="Buscar" OnClick="btn_Click" />
										</div>
									</div>
								</fieldset>
							</div>
								<fieldset style="margin-bottom: 13px;">
									<legend>Iniciativas que perimieron</legend>
								<div class="table-responsive">
									
									<asp:GridView ID="gv_Iniciativas" runat="server" CssClass="table table-striped table-bordered table-hover datatable"
											ShowHeaderWhenEmpty="true" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles"
											AutoGenerateColumns="false" DataKeyNames="AdmEstCodigo,IniIniCodigoSis,IniIniSecuencia,IniIniNumero,IniIniDescripcion,IniIniFechaConteoLeg,IniIniNumLegislaturaVigente,IniCacCodigo,IniCacDescripcion">
											<Columns>
												<asp:TemplateField HeaderText="Iniciativa #                 ">
													<ItemTemplate>
														<asp:Literal ID="fIniciativa" runat="server" Text='<%# Eval("IniIniNumero")  %>' />
													</ItemTemplate>
												</asp:TemplateField>

												<asp:TemplateField HeaderText="Descripción">
													<ItemTemplate>
														<asp:Literal ID="fDescripción" runat="server" Text='<%# Eval("IniIniDescripcion")  %>' />
													</ItemTemplate>
												</asp:TemplateField>
											</Columns>
										</asp:GridView>

								</div>
								</fieldset>
							</div>
			</div>
		</div>
		<!-- End Data Panel /////////// -->
		<!-- Footer /////// -->
		<footer class="Mfooter">
			<p class="footer">Copyright © 2010 Senado de la República Dominicana | Derechos Reservados</p>
		</footer>
		<!-- End Footer /////// -->
	</div>

	<input id="hfId" type="hidden" value="0" runat="server" />
</asp:Content>
