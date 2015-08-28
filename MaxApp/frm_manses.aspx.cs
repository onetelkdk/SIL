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
    public partial class frm_manses : PageBase
    {
        clsSesiones Sesiones;
        clsTipoIniciativa TipoIniciativa;
        clsLegislaturaInicio LegislaturaInicio;
        clsSalones Salones;

        protected void Page_Load(object sender, EventArgs e)
        {
            mtvClearMessage();
            if (!Page.IsPostBack)
            {
                try
                {
                    if (mtValidPage("frm_manses.aspx", lblModulo))
                    {

                        MenuFlotante.InnerHtml = mtGetMenu();

                        PanelMantenimientos.Visible = false;
                        btnGuardar.Visible = false;
                        panelLista.Visible = true;

                        mtLoadData();
                        mtLoadCombos();
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
            Sesiones = new clsSesiones();

            mtBindGridView(GridViewSesiones, Sesiones.mtGetSesiones());
            if (GridViewSesiones.Rows.Count > 0)
            {
                GridViewSesiones.UseAccessibleHeader = true;
                GridViewSesiones.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            Sesiones.mtDispose();
        }

        private void mtLoadCombos()
        {
            TipoIniciativa = new clsTipoIniciativa();
            LegislaturaInicio = new clsLegislaturaInicio();
            Salones = new clsSalones();

            String opcion = "[Seleccione una Opción]";
            mtBindDropDownList(ddlTipo, TipoIniciativa.mtGetTiposIniciativas(), "IniTipDescripcion", "IniTipCodigo", opcion);
            mtBindDropDownList(ddlLegislatura, LegislaturaInicio.mtGetLegislaturaActivaCombo(), "AdmLetDescripcion", "AdmLetCodigo", opcion);
            mtBindDropDownList(ddlLSesion, Salones.mtGetSalonesCodigo(2), "AdmSalDescripcion", "AdmSalCodigo", opcion);
            

            TipoIniciativa.mtDispose();
            LegislaturaInicio.mtDispose();
            Salones.mtDispose();
        }

        public int ObtenerPco()
        {
            int Pco = 0;
            LegislaturaInicio = new clsLegislaturaInicio();
            var dt = LegislaturaInicio.mtGetLegislaturaActivaCombo();
            LegislaturaInicio.mtDispose();
            var la = int.Parse(ddlLegislatura.SelectedValue);

            EnumerableRowCollection<DataRow> query1 = from order in dt.AsEnumerable()
                                                      where order.Field<Int16>("AdmLetCodigo") == la
                                                      select order;

            DataView viewPco = query1.AsDataView();

            foreach (DataRowView drv in viewPco)
            {
                Pco = int.Parse(drv["AdmPcoCodigo"].ToString());
            }

            return Pco;
        }

        public int ObtenerLegislaturaActiva()
        {
            int La = 0;
            LegislaturaInicio = new clsLegislaturaInicio();
            var dt = LegislaturaInicio.mtGetLegislaturaActivaCombo();
            LegislaturaInicio.mtDispose();
            EnumerableRowCollection<DataRow> query = from order in dt.AsEnumerable()
                                                     where order.Field<int>("AdmEstCodigo") == 176
                                                     select order;

            DataView viewLeg = query.AsDataView();

            foreach (DataRowView drv in viewLeg)
            {
                La = int.Parse(drv["AdmLetCodigo"].ToString());
            }

            return La;
        }

        protected void VisualizarRegistro(object sender, EventArgs e)
        {
            EditarRegistro(sender, e);
            btnGuardar.Visible = false;
            ddlLegislatura.Enabled = false;
            ddlTipo.Enabled = false;
            ddlLSesion.Enabled = false;
            ddlLegislatura.Enabled = false;
            ddlEstado.Enabled = false;
            txtFecha.Enabled = false;
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
                ddlLegislatura.Enabled = false;

                foreach (GridViewRow row in GridViewSesiones.Rows)
                {
                    if (fId == GridViewSesiones.DataKeys[row.RowIndex].Values["IniSesCodigoSis"].ToString())
                    {
                        txtNSesion.Text = GridViewSesiones.DataKeys[row.RowIndex].Values["IniSesNumero"].ToString();
                        txtFecha.Text = GridViewSesiones.DataKeys[row.RowIndex].Values["IniSesFecha"].ToString();
                        ddlLegislatura.SelectedValue = String.IsNullOrEmpty(GridViewSesiones.DataKeys[row.RowIndex].Values["AdmLetCodigo"].ToString()) ? "0" : GridViewSesiones.DataKeys[row.RowIndex].Values["AdmLetCodigo"].ToString();
                        ddlLSesion.SelectedValue = String.IsNullOrEmpty(GridViewSesiones.DataKeys[row.RowIndex].Values["AdmSalCodigo"].ToString()) ? "0" : GridViewSesiones.DataKeys[row.RowIndex].Values["AdmSalCodigo"].ToString();
                        ddlTipo.SelectedValue = String.IsNullOrEmpty(GridViewSesiones.DataKeys[row.RowIndex].Values["IniSesTipo"].ToString()) ? "0" : GridViewSesiones.DataKeys[row.RowIndex].Values["IniSesTipo"].ToString();
                        ddlEstado.SelectedValue = String.IsNullOrEmpty(GridViewSesiones.DataKeys[row.RowIndex].Values["AdmEstCodigo"].ToString()) ? "0" : GridViewSesiones.DataKeys[row.RowIndex].Values["AdmEstCodigo"].ToString(); 
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
                txtNSesion.Text = "";
                txtFecha.Text = "";
                ddlLegislatura.SelectedValue = ObtenerLegislaturaActiva().ToString();
                ddlEstado.SelectedIndex = 0;
                ddlLSesion.SelectedIndex = 0;
                ddlTipo.SelectedIndex = 0;
                ddlLegislatura.Enabled = false;
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

                txtFecha.Enabled = true;
                ddlLegislatura.Enabled = false;
                ddlEstado.Enabled = true;
                ddlLSesion.Enabled = true;
                ddlTipo.Enabled = true;
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
            try
            {
                DateTime mainFecha = Convert.ToDateTime(Request.Form[txtFecha.UniqueID]);
                if (string.IsNullOrEmpty(mainFecha.ToShortDateString()))
                {
                    mtvAddMessage("Selecione la fecha", MessageType.warning);
                    return;
                }

                if (ddlTipo.SelectedIndex == 0)
                {
                    mtvAddMessage("Selecione el tipo de sesion", MessageType.warning);
                    return;
                }

                if (ddlLegislatura.SelectedIndex == 0)
                {
                    mtvAddMessage("Selecione la ligislatura", MessageType.warning);
                    return;
                }

                if (ddlLSesion.SelectedIndex == 0)
                {
                    mtvAddMessage("Selecione el lugar de sesion", MessageType.warning);
                    return;
                }

                int pco = ObtenerPco();

                //DateTime mainFecha = Convert.ToDateTime(Request.Form[txtFecha.UniqueID]);
                string legis = ddlLegislatura.SelectedItem.ToString();
                Sesiones = new clsSesiones();
                Sesiones.mtPutSesiones
                    (Int32.Parse(ViewState["hfId"].ToString())//Id del Registro
                    ,0
                    , legis.Trim()
                    ,int.Parse(ddlTipo.SelectedValue)
                    , mainFecha
                    ,int.Parse(ddlLegislatura.SelectedValue)
                    ,int.Parse(ddlLSesion.SelectedValue)
                    , pco
                    ,int.Parse(ddlEstado.SelectedValue)
                    ,Session["AdmusrCodigo"].ToString());

                ViewState["hfId"] = "0";
                mtvAddMessage("frm_ManMsj.aspx", "MensajeSatisfactorio", MessageType.success);

                PanelMantenimientos.Visible = false;
                btnGuardar.Visible = false;
                btnNuevo.Visible = true;
                btnCancel.Visible = false;
                panelLista.Visible = true;

                mtLoadData();
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }
    }
}