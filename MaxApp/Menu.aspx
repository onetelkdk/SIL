<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMax.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="MaxApp.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="server">

    <div class="main_container">
        <div class="main_menu">

            <div class="bloques">

                <div class="nombre_top_menu">
                    <a href="Modulos.aspx">
                        <img src="images/atras.png" style="margin-top: 17px; float: left" title="REGRESAR AL MENU ANTERIOR" />
                        <h2><asp:Label ID="lblModulo" runat="server" Text="N/A"></asp:Label></h2>
                    </a>
                </div>

                <div id="Bloques_Content" runat="server">

                

                </div>

            </div>

        </div>
    </div>

</asp:Content>
