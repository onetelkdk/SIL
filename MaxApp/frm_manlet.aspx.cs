using MaxBll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_manlet : PageBase
    {
        clsLegislaturaInicio LegislaturaInicio;
        clsLegislaturaRegistro LegislaturaRegistro;
        clsComision Comision;

        protected void Page_Load(object sender, EventArgs e)
        {
            mtvClearMessage();
            if (!Page.IsPostBack)
            {
                try
                {
                    if (mtValidPage("frm_manlet.aspx", lblModulo))
                    {

                        MenuFlotante.InnerHtml = mtGetMenu();

                        PanelMantenimientos.Visible = false;
                        btnGuardar.Visible = false;
                        panelLista.Visible = false;
                        mtLoadCombos();
                        //mtLoadData();

                    }

                }
                catch (Exception ex)
                {

                    mtvAddMessage(ex.Message, MessageType.error);
                }
            }
        }

        private void mtLoadCombos()
        {
            LegislaturaInicio = new clsLegislaturaInicio();
            LegislaturaRegistro = new clsLegislaturaRegistro();
            Comision = new clsComision();

            String opcion = "[Seleccione una Opción]";

            mtBindDropDownList(ddlTipo, LegislaturaInicio.mtGetTipoLegislatura(), "AdmLtpDescripcion", "AdmLtpCodigo", opcion);
            mtBindDropDownList(ddlPco, LegislaturaRegistro.mAdmPcoGetDatos(), "AdmpcoDescripcion", "AdmpcoCodigo", opcion);
            mtBindDropDownList(ddlEstado, Comision.mtGetdAdmEst(), "AdmEstDescripcion", "AdmEstCodigo", opcion);

            LegislaturaInicio.mtDispose();
            LegislaturaRegistro.mAdmParDispose();
            Comision.mtDispose();
        }

        protected void VisualizarRegistro(object sender, EventArgs e)
        {
            EditarRegistro(sender, e);
            btnGuardar.Visible = false;
            txtLegislatura.Enabled = false;
            txtAño.Enabled = false;
            txtDesde.Enabled = false;
            txtHasta.Enabled = false;
            ddlTipo.Enabled = false;
            ddlPco.Enabled = false;
            //ddlEstado.Enabled = false;
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
                FBuscar.Visible = false;
                btnNuevo.Visible = false;
                btnGuardar.Visible = true;
                btnCancel.Visible = true;

                txtLegislatura.Enabled = true;
                txtAño.Enabled = true;
                txtDesde.Enabled = true;
                txtHasta.Enabled = true;
                ddlTipo.Enabled = true;
                ddlPco.Enabled = true;
                ddlEstado.Enabled = false;


                foreach (GridViewRow row in GridViewLegislatura.Rows)
                {
                    if (fId == GridViewLegislatura.DataKeys[row.RowIndex].Values["AdmLetCodigo"].ToString())
                    {
                        txtLegislatura.Text = GridViewLegislatura.DataKeys[row.RowIndex].Values["AdmLetDescripcion"].ToString();
                        txtAño.Text = GridViewLegislatura.DataKeys[row.RowIndex].Values["AdmLetAno"].ToString();
                        txtDesde.Text = GridViewLegislatura.DataKeys[row.RowIndex].Values["AdmLetFechaDesde"].ToString();
                        txtHasta.Text = GridViewLegislatura.DataKeys[row.RowIndex].Values["AdmLetFechaHasta"].ToString();
                        ddlTipo.SelectedValue = String.IsNullOrEmpty(GridViewLegislatura.DataKeys[row.RowIndex].Values["AdmLtpCodigo"].ToString()) ? "0" : GridViewLegislatura.DataKeys[row.RowIndex].Values["AdmLtpCodigo"].ToString();
                        ddlPco.SelectedValue = String.IsNullOrEmpty(GridViewLegislatura.DataKeys[row.RowIndex].Values["AdmPcoCodigo"].ToString()) ? "0" : GridViewLegislatura.DataKeys[row.RowIndex].Values["AdmPcoCodigo"].ToString();
                        ddlEstado.SelectedValue = String.IsNullOrEmpty(GridViewLegislatura.DataKeys[row.RowIndex].Values["AdmEstCodigo"].ToString()) ? "178" : GridViewLegislatura.DataKeys[row.RowIndex].Values["AdmEstCodigo"].ToString();
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)                                         
        {
            try
            {
                if(string.IsNullOrEmpty(txtBLegislatura.Text.Trim()))
                {
                    mtvAddMessage("frm_manlet.aspx", "manlet01", MessageType.warning);
                    return;
                }

                LegislaturaInicio = new clsLegislaturaInicio();

                mtBindGridView(GridViewLegislatura, LegislaturaInicio.mtGetLegislatura(txtBLegislatura.Text.Trim()));
                if (GridViewLegislatura.Rows.Count > 0)
                {
                    GridViewLegislatura.UseAccessibleHeader = true;
                    GridViewLegislatura.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                LegislaturaInicio.mtDispose();

                panelLista.Visible = true;
                btnCancel.Visible = true;
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBLegislatura.Text = "";
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["hfId"] = "0";
                FBuscar.Visible = false;
                PanelMantenimientos.Visible = true;
                panelLista.Visible = false;
                btnNuevo.Visible = false;
                btnGuardar.Visible = true;
                btnCancel.Visible = true;
                txtLegislatura.Enabled = true;
                txtAño.Enabled = true;
                txtLegislatura.Text = "";
                txtAño.Text = "";
                txtDesde.Text = "";
                txtHasta.Text = "";
                ddlTipo.SelectedIndex = 0;
                ddlPco.SelectedIndex = 0;
                ddlEstado.SelectedValue = "176";
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                LegislaturaInicio = new clsLegislaturaInicio();
                DateTime mainFechaDesde = Convert.ToDateTime(Request.Form[txtDesde.UniqueID]);
                DateTime mainFechaHasta = Convert.ToDateTime(Request.Form[txtHasta.UniqueID]);
                
                if (string.IsNullOrEmpty(txtLegislatura.Text.Trim()))
                {
                    mtvAddMessage("frm_manlet.aspx", "manlet02", MessageType.warning);
                    return;
                }

                if (ddlPco.SelectedIndex == 0)
                {
                    mtvAddMessage("Selecciona el periodo", MessageType.warning);
                    return;
                }

                if (mainFechaDesde > mainFechaHasta)
                {
                    mtvAddMessage("frm_manlet.aspx", "manlet03", MessageType.warning);
                    return;
                }

                if (int.Parse(ddlEstado.SelectedValue) == 176)
                {
                    if (mainFechaDesde < DateTime.Parse(DateTime.Now.ToShortDateString()))
                    {
                        mtvAddMessage("frm_manlet.aspx", "manlet04", MessageType.warning);
                        return;
                    }

                    var dt = LegislaturaInicio.mtGetLegislaturaActiva();
                    string leg = "";
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow d in dt.Rows)
                        {
                            leg = d["AdmLetDescripcion"].ToString();
                        }

                        if (txtLegislatura.Text.ToUpper().Trim() != leg.ToUpper().Trim())
                        {
                            txtDesde.Text = mainFechaDesde.ToShortDateString();
                            txtHasta.Text = mainFechaHasta.ToShortDateString();
                            mtvAddMessage("La legislatura " + leg + " sigue activa, solo puede estar una legislatura con ese estado", MessageType.warning);
                            return;
                        }
                    }
                }

               
                LegislaturaInicio.mtPutLegislatura
                    (Int32.Parse(ViewState["hfId"].ToString())//Id del Registro
                    , txtAño.Text.Trim()
                    , txtLegislatura.Text.Trim()
                    , mainFechaDesde
                    , mainFechaHasta
                    , int.Parse(ddlPco.SelectedValue)
                    , int.Parse(ddlTipo.SelectedValue)
                    , int.Parse(ddlEstado.SelectedValue)
                    , Session["AdmusrCodigo"].ToString());

                ViewState["hfId"] = "0";
                mtvAddMessage("frm_ManMsj.aspx", "MensajeSatisfactorio", MessageType.success);

                PanelMantenimientos.Visible = false;
                btnGuardar.Visible = false;
                btnNuevo.Visible = true;
                btnCancel.Visible = false;
                panelLista.Visible = true;
                FBuscar.Visible = true;
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            PanelMantenimientos.Visible = false;
            btnGuardar.Visible = false;
            panelLista.Visible = false;
            btnCancel.Visible = false;
            btnNuevo.Visible = true;
            FBuscar.Visible = true;
            txtBLegislatura.Text = "";
            txtLegislatura.ReadOnly = true;
        }
    }
}