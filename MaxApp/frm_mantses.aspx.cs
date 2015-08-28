using MaxBll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_mantses : PageBase
    {
        clsTipoSesion TipoSesion;

        protected void Page_Load(object sender, EventArgs e)
        {
            mtvClearMessage();
            if (!Page.IsPostBack)
            {
                try
                {
                    if (mtValidPage("frm_mantses.aspx", lblModulo))
                    {

                        MenuFlotante.InnerHtml = mtGetMenu();

                        PanelMantenimientos.Visible = false;
                        btnGuardar.Visible = false;
                        panelLista.Visible = true;

                        mtLoadData();
                    }

                }
                catch (Exception ex)
                {

                    mtvAddMessage(ex.Message, MessageType.error);
                }
            }
        }

        private void mtLoadData()
        {
            TipoSesion = new clsTipoSesion();

            mtBindGridView(GridViewSesiones, TipoSesion.mtGetTipoSesion());
            if (GridViewSesiones.Rows.Count > 0)
            {
                GridViewSesiones.UseAccessibleHeader = true;
                GridViewSesiones.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            TipoSesion.mtDispose();
        }

        protected void VisualizarRegistro(object sender, EventArgs e)
        {
            EditarRegistro(sender, e);
            btnGuardar.Visible = false;
            txtDescripcion.Enabled = false;
            ddlEstado.Enabled = false;
        }

        protected void EditarRegistro(object sender, EventArgs e)
        {
            try
            {
                Button imgButton = (Button)sender;
                String fId = imgButton.CommandArgument;
                ViewState["hfId"] = fId.ToString();

                int id = int.Parse(ViewState["hfId"].ToString());

                PanelMantenimientos.Visible = true;
                panelLista.Visible = false;
                btnNuevo.Visible = false;
                btnGuardar.Visible = true;
                btnCancel.Visible = true;


                foreach (GridViewRow row in GridViewSesiones.Rows)
                {
                    if (fId == GridViewSesiones.DataKeys[row.RowIndex].Values["IniTseCodigo"].ToString())
                    {
                        txtDescripcion.Text = GridViewSesiones.DataKeys[row.RowIndex].Values["IniTseDescripcion"].ToString();
                        ddlEstado.SelectedValue = String.IsNullOrEmpty(GridViewSesiones.DataKeys[row.RowIndex].Values["AdmStsCodigo"].ToString()) ? "False" : GridViewSesiones.DataKeys[row.RowIndex].Values["AdmStsCodigo"].ToString();
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["hfId"] = "0";
                PanelMantenimientos.Visible = true;
                panelLista.Visible = false;
                btnNuevo.Visible = false;
                btnGuardar.Visible = true;
                btnCancel.Visible = true;
                txtDescripcion.Text = "";
                ddlEstado.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["hfId"] = "0";
                PanelMantenimientos.Visible = false;
                panelLista.Visible = true;
                btnNuevo.Visible = true;
                btnCancel.Visible = false;
                btnGuardar.Visible = false;

                txtDescripcion.Enabled = true;
                ddlEstado.Enabled = true;

                if (GridViewSesiones.Rows.Count > 0)
                {
                    GridViewSesiones.UseAccessibleHeader = true;
                    GridViewSesiones.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescripcion.Text.Trim()))
            {
                mtvAddMessage("frm_mantses.aspx", "mantses01", MessageType.warning);
                return;
            }

            TipoSesion = new clsTipoSesion();
            TipoSesion.mtPutTipoSesion
                (Int32.Parse(ViewState["hfId"].ToString())//Id del Registro
                , txtDescripcion.Text.Trim()
                , bool.Parse(ddlEstado.SelectedValue)
                , Session["AdmusrCodigo"].ToString());

            ViewState["hfId"] = "0";
            mtvAddMessage("frm_ManMsj.aspx", "MensajeSatisfactorio", MessageType.success);

            PanelMantenimientos.Visible = false;
            btnGuardar.Visible = false;
            btnNuevo.Visible = true;
            btnCancel.Visible = false;
            panelLista.Visible = true;

            mtLoadData();
        }
    }
}