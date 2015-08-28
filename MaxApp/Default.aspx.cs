using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class Default : System.Web.UI.Page
    {
        MaxBll.clsUsuarios objUsuarios;

        public void mtvClearMessage()
        {
            Panel fPanel = (Panel)this.FindControl("PanelMensajes");
            Label fLabel = (Label)this.FindControl("lblStatus");

            String fCss = String.Empty;
            fPanel.CssClass = fCss;
            fPanel.Visible = false;
            fLabel.Text = String.Empty;
            fPanel.Dispose();
        }

        public void mtvAddMessage(String Description, MessageType Type)
        {
            UpdatePanel fUpdate = (UpdatePanel)this.FindControl("UpMensaje");
            Panel fPanel = (Panel)this.FindControl("PanelMensajes");
            Label fLabel = (Label)this.FindControl("lblStatus");
            Image fImagen = (Image)this.FindControl("imgAlert");
            String fCss = "";

            switch (Type)
            {
                case MessageType.error:
                    fCss = "alert alert-danger fade in msgLabel";
                    fImagen.ImageUrl = "/images/ic_warning.png";
                    break;
                case MessageType.information:
                    fCss = "alert alert-info fade in msgLabel";
                    fImagen.ImageUrl = "";
                    break;
                case MessageType.success:
                    fCss = "alert alert-success fade in msgLabel";
                    fImagen.ImageUrl = "";
                    break;
                case MessageType.warning:
                    fCss = "alert alert-warning fade in msgLabel";

                    break;
            }

            fPanel.CssClass = fCss;
            fPanel.Visible = true;
            fLabel.Text = Description;
            fUpdate.Update();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            mtvClearMessage();

            try
            {
                if (!Page.IsPostBack)
                {
                    Session["AdmusrCodigo"] =null;
                    Session["AdmRolCodigo"] = null;
                    Session["AdmDepCodigo"] = null;
                    Session["AdmUsrNombre"] = null;
                    Session["AdmmodCodigo"] = null;
                    Session["AdmmodNombre"] = null;
                }
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                objUsuarios = new MaxBll.clsUsuarios();
                DataTable dt = objUsuarios.mtGetUsuario(AdmusrCodigo.Value);
                objUsuarios.mtDispose();

                //Existe el Usuario
                if (dt.Rows.Count > 0)
                {

                    Boolean fUsuarioExiste = MaxBll.clsUtils.ValidateMD5HashData(AdmusrPassword.Value.Trim(), dt.Rows[0]["AdmusrPassword"].ToString());

                    //Validar si coincide la clave
                    if (fUsuarioExiste)//dt.Rows[0]["AdmusrPassword"].ToString().ToLower() == AdmusrPassword.Value.ToLower())
                    {
                        Session["AdmusrCodigo"] = AdmusrCodigo.Value;
                        Session["AdmRolCodigo"] = dt.Rows[0]["AdmRolCodigo"].ToString();
                        Session["AdmDepCodigo"] = dt.Rows[0]["AdmDepCodigo"].ToString();
                        Session["AdmUsrNombre"] = dt.Rows[0]["AdmUsrNombre"].ToString();
                        Response.Redirect("Modulos.aspx");
                    }
                    else
                    {
                        mtvAddMessage("Usuario o contraseña invalida", MessageType.warning);
                        AdmusrCodigo.Focus();
                    }

                }
                else
                {
                    mtvAddMessage("El Usuario especificado no existe", MessageType.warning);
                    AdmusrCodigo.Focus();
                }
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }
    }
}