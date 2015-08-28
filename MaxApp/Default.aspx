<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MaxApp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <link rel="stylesheet" type="text/css" href="css/mainstyles.css" />
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0" />
    <link href="css/responsive.css" rel="stylesheet" />
    <link rel="stylesheet" href="css/acordion.css" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <!-- DataTables CSS -->
    <link href="js/jquery-ui/jquery-ui.css" rel="stylesheet" />
    <link href="css/plugins/dataTables.bootstrap.css" rel="stylesheet" />
    <!-- Bootstrap CSS -->
    <link href="css/bootstrap.css" rel="stylesheet" media="screen" />
    <title></title>
</head>
<body style="background: #fff !important">
    <form id="form1" runat="server">

             <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <div class="breadcrumb">
                            <div style="text-align: center">
                                <asp:UpdatePanel ID="UpMensaje" RenderMode="Block" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="PanelMensajes" runat="server" Visible="false" CssClass="">
                                            <asp:Image ID="imgAlert" runat="server" />
                                            <a href="#" class="close" data-dismiss="alert">&times;</a>
                                            <asp:Label ID="lblStatus" runat="server" Style="position: relative; top: -3px;"></asp:Label>
                                        </asp:Panel>
                                    </ContentTemplate>

                                </asp:UpdatePanel>
                            </div>
                        </div>

        <div class="container">
            <div class="row">
                <div class="col-md-4 col-md-offset-4">
                    <div class="login-logo">
                        <img src="images/logo_sil-escudo.png" />
                    </div>
                                       

                    <div class="login-panel panel panel-default">

                        <div class="panel-heading frmbtop">
                            <h2 class="panel-title h2tlogin">Acceso al Sistema</h2>
                        </div>
                        <div class="panel-body">
                            <fieldset>
                                <div class="form-group">
                                    <input type="text" runat="server" id="AdmusrCodigo" class="form-control frmbl username-icon-login" placeholder="Usuario" />
                                </div>
                                <div class="form-group">
                                    <input type="password" runat ="server" id="AdmusrPassword" class="form-control frmbl pass-icon-login" placeholder="Contraseña" />
                                </div>
                                <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesión" CssClass="btn btn-lg btn-primary btn-block" OnClick="btnLogin_Click" />
                            </fieldset>
                        </div>
                        <div class="footerlogin">
                            <a href="#">Olvidó su contraseña?</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!-- scripts-->
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/acordion.js"></script>
    <script src="js/jquery-ui/jquery-ui.js"></script>
    <script type="text/javascript" src="js/maincod.js"></script>
    <!-- DataTables JavaScript -->
    <script type="text/javascript" src="js/plugins/dataTables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="js/plugins/dataTables/dataTables.bootstrap.js"></script>
    <!-- Custom Index -->
    <script src="js/plugins/timepicker/jquery.timepicker.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.js"></script>
    <script type="text/javascript" src="js/custom-components.js"></script>


        <!-- scripts-->
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".close").click(function () {
                $("#PanelMensjes").alert();
            });
        });
    </script>

</body>
</html>
