<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="accesodenegado.aspx.cs" Inherits="MaxApp.accesodenegado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="server">
    <div class="nopermitido">
        <img src="images/screemlock.png" alt="Acceso no permitido" width="200"/>
        <h1 style="color: #676A6C;font-size: 35px; margin: 15px 0;font-weight: 100;">Acceso No Permitido!</h1>
        <p style="line-height:22px">
             Lo sentimos, usted no tiene permisos para visualizar esta página. La página no se encuentra disponible en tu nivel de acceso. Si cres que esto es un error debes ponerte en contacto con el administrador del sistema.
        
        </p>
       
        <div style="text-align:center">
            <asp:Button ID="btnRegresar" CssClass="btn btn-fb" runat="server" Text="Regresar" OnClick="btnRegresar_Click" />
        <%--<button class="btn btn-fb"><i class="fa fa-arrow-left"></i> Regresar</button>--%>
    </div>
    </div>
    
</asp:Content>
