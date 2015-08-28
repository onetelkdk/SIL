using MaxBll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_mancom : PageBase
    {
       clsComision Comision;

        protected void Page_Load(object sender, EventArgs e)
        {
            mtvClearMessage();
            if (!Page.IsPostBack)
            {
                try
                {
                    if (mtValidPage("frm_mancom.aspx", lblModulo))
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
            Comision = new clsComision();

            mtBindGridView(GridViewComisiones, Comision.mtGetComision2());
            if (GridViewComisiones.Rows.Count > 0)
            {
                GridViewComisiones.UseAccessibleHeader = true;
                GridViewComisiones.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            Comision.mtDispose();
        }

        protected void VisualizarRegistro(object sender, EventArgs e)
        {
            EditarRegistro(sender, e);
            btnGuardar.Visible = false;
            txtTitulo.Enabled = false;
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


                foreach (GridViewRow row in GridViewComisiones.Rows)
                {
                    if (fId == GridViewComisiones.DataKeys[row.RowIndex].Values["ComComCodigo"].ToString())
                    {
                        txtTitulo.Text = GridViewComisiones.DataKeys[row.RowIndex].Values["ComComNombre"].ToString();
                        string codigo = String.IsNullOrEmpty(GridViewComisiones.DataKeys[row.RowIndex].Values["AdmStsCodigo"].ToString()) ? "1" : GridViewComisiones.DataKeys[row.RowIndex].Values["AdmStsCodigo"].ToString();
                        ddlEstado.SelectedValue = codigo.Trim();
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
                btnEditar.Visible = false;
                btnNuevo.Visible = false;
                btnGuardar.Visible = true;
                btnCancel.Visible = true;
                txtTitulo.Text = "";
                ddlEstado.SelectedIndex = 0;
                txtTitulo.Focus();
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
                txtTitulo.Enabled = true;
                ddlEstado.Enabled = true;
                mtLoadData();
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitulo.Text.Trim()))
            {
                mtvAddMessage("Captura el titulo de la comision", MessageType.warning);
                return;
            }


            Comision = new clsComision();
            Comision.mtPutComision
                (Int32.Parse(ViewState["hfId"].ToString())//Id del Registro
                , txtTitulo.Text.Trim()
                , ddlEstado.SelectedValue
                );
            Comision.mtDispose();
            ViewState["hfId"] = "0";

            mtvAddMessage("Registro Guardado satisfactoriamente", MessageType.success);

            PanelMantenimientos.Visible = false;
            btnGuardar.Visible = false;
            btnNuevo.Visible = true;
            btnCancel.Visible = false;
            panelLista.Visible = true;

            mtLoadData();
        }
    }
}