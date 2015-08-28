using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MaxBll;
using System.Data;

namespace MaxApp
{
    public partial class frm_comcom : PageBase
    {
        clsComision Comision;
        clsTipoComision TipoComision;
        clsPeriodoConstitucional PeriodoConstitucional;
        clsEstado Estado;
        clsrFunUser rFunUser;
        clsConfirmacionComision ConfirmacionComision;
        clsLegisladores Legisladores;
        clsFuncion Funcion;

        protected void Page_Load(object sender, EventArgs e)
        {
            mtvClearMessage();
            if (!Page.IsPostBack)
            {
                try
                {
                    if (mtValidPage("frm_comcom.aspx", lblModulo))
                    {

                        MenuFlotante.InnerHtml = mtGetMenu();

                        PanelMantenimientos.Visible = false;
                        btnGuardar.Visible = false;
                        panelLista.Visible = true;

                        mtLoadCombos();
                        mtLoadData();
                        CargarViewState();
                        ViewState["visualizar"] = 0;
                    }

                }
                catch (Exception ex)
                {
                    mtvAddMessage(ex.Message, MessageType.error);
                }
            }
        }

        private void CargarViewState()
        {
            try
            {
                ViewState["dt_Miembros"] = null;
                DataTable dt_Miembros = new DataTable();
                dt_Miembros.Columns.Add("ComCfmId");
                dt_Miembros.Columns.Add("ComMbrSecuencia");
                dt_Miembros.Columns.Add("ComMbrNombre");
                dt_Miembros.Columns.Add("AdmFunCodigo");
                dt_Miembros.Columns.Add("AdmFunDescripcion");
                dt_Miembros.Columns.Add("ComMbrInterno");
                dt_Miembros.Columns.Add("Nuevo");
                dt_Miembros.Columns.Add("Eliminado");
                ViewState["dt_Miembros"] = dt_Miembros;
            }
            catch { }
        }

        private void mtLoadData()
        {
            ConfirmacionComision = new clsConfirmacionComision();

            mtBindGridView(GridViewConfirmacionComision, ConfirmacionComision.mtGetConfirmaComision());
            if (GridViewConfirmacionComision.Rows.Count > 0)
            {
                GridViewConfirmacionComision.UseAccessibleHeader = true;
                GridViewConfirmacionComision.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            ConfirmacionComision.mtDispose();
        }

        private void mtLoadData2(int ComCfmId)
        {
            ConfirmacionComision = new clsConfirmacionComision();

            DataTable dt_Miembros = (DataTable)ViewState["dt_Miembros"];
            dt_Miembros.Clear();
            var dt = ConfirmacionComision.mtGetConfirmaComisionMiembros(ComCfmId);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows) // Loop over the rows.
                {
                    DataRow row1 = dt_Miembros.NewRow();
                    row1["ComCfmId"] = row["ComCfmId"].ToString();
                    row1["ComMbrSecuencia"] = row["ComMbrSecuencia"].ToString();
                    row1["ComMbrNombre"] = row["ComMbrNombre"].ToString();
                    row1["AdmFunCodigo"] = row["AdmFunCodigo"].ToString();
                    row1["AdmFunDescripcion"] = row["AdmFunDescripcion"].ToString();
                    row1["ComMbrInterno"] = row["ComMbrInterno"].ToString();
                    row1["Nuevo"] = false;
                    row1["Eliminado"] = false;
                    dt_Miembros.Rows.Add(row1);

                    ViewState["dt_Miembros"] = dt_Miembros;
                }
            }
            
            EnumerableRowCollection<DataRow> query = from order in dt_Miembros.AsEnumerable()
                                                     where order.Field<string>("ComMbrInterno") == "True"
                                                     select order;

            DataView viewInterno = query.AsDataView();

            if (txtMCamra.Visible == true)
            {
                EnumerableRowCollection<DataRow> query2 = from order in dt_Miembros.AsEnumerable()
                                                          where order.Field<string>("ComMbrInterno") == "False"
                                                          select order;

                DataView viewExterno = query2.AsDataView();
                var cadena = "";
                foreach (DataRowView drv in viewExterno)
                {
                    cadena = cadena + drv["ComMbrNombre"].ToString() + "\t";
                }

                //txtMCamra.Value = cadena;
                txtMCamra.Text = cadena.Trim().Replace('\t',';');
            }

            mtBindGridView(GridViewLegisladores, viewInterno);
            if (GridViewLegisladores.Rows.Count > 0)
            {
                GridViewLegisladores.UseAccessibleHeader = true;
                GridViewLegisladores.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            ConfirmacionComision.mtDispose();
        }


        private void mtLoadDataViewState()
        {
            DataTable dt_Miembros = (DataTable)ViewState["dt_Miembros"];

            EnumerableRowCollection<DataRow> query = from order in dt_Miembros.AsEnumerable()
                                                     where order.Field<string>("Eliminado") == "False" && order.Field<string>("ComMbrInterno") == "True"
                                                     select order;

            DataView view = query.AsDataView();

            mtBindGridView(GridViewLegisladores, view);
            if (GridViewLegisladores.Rows.Count > 0)
            {
                GridViewLegisladores.UseAccessibleHeader = true;
                GridViewLegisladores.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            //ConfirmacionComision.mtDispose();
        }

        private void mtLoadCombos()
        {
            Comision = new clsComision();
            TipoComision = new clsTipoComision();
           
            Estado = new clsEstado();
            rFunUser = new clsrFunUser();
            Legisladores = new clsLegisladores();
            Funcion = new clsFuncion();

            String opcion = "[Seleccione una Opción]";
            mtBindDropDownList(ddlComision, Comision.mtGetComision2(), "ComComNombre", "ComComCodigo", opcion);
            mtBindDropDownList(ddlTipo, TipoComision.mtGetTipos(), "ComTipDescripcion", "ComTipCodigo", opcion);
            mtBindDropDownList(ddlEstado, Estado.mtGetEstado(), "AdmEstDescripcion", "AdmEstCodigo", opcion);
            mtBindDropDownList(ddlCTecnico, rFunUser.mtGetCoordinadorTecnico(), "AdmUsrNombre", "FunUsrCodigo", opcion);
            mtBindDropDownList(ddlSecretaria, rFunUser.mtGetSecretaria(), "AdmUsrNombre", "FunUsrCodigo", opcion);
            mtBindDropDownList(ddlMiembros, Legisladores.mtGetLegisladores(), "Nombre", "AdmLegCodigo", opcion);
            mtBindDropDownList(ddlFuncion, Funcion.mtGetFuncion(), "AdmFunDescripcion", "AdmFunCodigo", opcion);
           
            Comision.mtDispose();
            TipoComision.mtDispose();
            Estado.mtDispose();
            rFunUser.mtDispose();
            Legisladores.mtDispose();
            Funcion.mtDispose();
        }


        private void ObtenerPeriodoConstitucional()
        {
            try
            {
                PeriodoConstitucional = new clsPeriodoConstitucional();

                var dt = PeriodoConstitucional.mtGetPeriodoConstitucional2();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows) // Loop over the rows.
                    {
                        ViewState["AdmpcoCodigo"] = int.Parse(row["AdmpcoCodigo"].ToString());
                        lblPConstitucional.Text = row["AdmpcoDescripcion"].ToString();
                    }
                }
                else
                {
                    ViewState["AdmpcoCodigo"] = 0;
                    lblPConstitucional.Text = "Sin Periodo";
                }
            }
            catch
            { }
        }

        /// <summary>
        /// Evento para edicion de registro de la lista datos de control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditarRegistro(object sender, EventArgs e)
        {
            try
            {
                Button imgButton = (Button)sender;
                String fId = imgButton.CommandArgument;
                ViewState["hfId"] = fId.ToString();
                idComision.Value = ViewState["hfId"].ToString();
                PanelMantenimientos.Visible = true;
                panelLista.Visible = false;
                btnNuevo.Visible = false;
                btnGuardar.Visible = true;
                btnCancel.Visible = true;
                HabilitarMiembro(1);
                int edit = 1;

                foreach (GridViewRow row in GridViewConfirmacionComision.Rows)
                {
                    if (fId == GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["ComCfmId"].ToString())
                    {
                        ddlComision.SelectedValue = String.IsNullOrEmpty(GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["ComComCodigo"].ToString()) ? "0" : GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["ComComCodigo"].ToString();
                        ddlCTecnico.SelectedValue = String.IsNullOrEmpty(GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["ComCfmCoordinador"].ToString()) ? "0" : GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["ComCfmCoordinador"].ToString();
                        ddlTipo.SelectedValue = String.IsNullOrEmpty(GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["ComTipCodigo"].ToString()) ? "0" : GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["ComTipCodigo"].ToString();
                        try
                        {
                            ddlSecretaria.SelectedValue = String.IsNullOrEmpty(GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["ComCfmSecretaria"].ToString()) ? "0" : GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["ComCfmSecretaria"].ToString();
                        }
                        catch
                        {
                            ddlSecretaria.SelectedValue = "0";
                        }
                        
                        ddlEstado.SelectedValue = String.IsNullOrEmpty(GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["AdmEstCodigo"].ToString()) ? "0" : GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["AdmEstCodigo"].ToString();
                        ViewState["Estado"] = GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["AdmEstCodigo"].ToString();
                        txtAtribuciones.Text = GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["ComCfmAtribucion"].ToString();
                        dtFecha.Text = GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["ComCfmFecha"].ToString();
                        ViewState["AdmpcoCodigo"] = GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["AdmPcoCodigo"].ToString();
                        lblPConstitucional.Text = GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["AdmpcoDescripcion"].ToString();
                        
                        break;
                    }
                }

                string bicam = ddlComision.SelectedItem.ToString();
                if (bicam.Trim().Contains("BICAMERAL"))
                {
                    txtMCamra.Visible = true;
                    lblMiembros.Visible = true;
                }
                else
                {
                    txtMCamra.Visible = false;
                    lblMiembros.Visible = false;
                }

                mtLoadData2(int.Parse(fId));

                if (int.Parse(ddlEstado.SelectedValue) != 174 && int.Parse(ddlEstado.SelectedValue) > 0)
                {
                    VisualizarRegistro(sender, e);
                    ddlEstado.Enabled = true;
                }
                if (Int32.Parse(ddlEstado.SelectedValue) == 14 && Int32.Parse(ddlEstado.SelectedValue) == 15)
                {
                    ddlEstado.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        protected void VisualizarRegistro(object sender, EventArgs e)
        {
            try
            {
                ViewState["visualizar"] = 1;
                Button imgButton = (Button)sender;
                String fId = imgButton.CommandArgument;
                ViewState["hfId"] = fId.ToString();
                idComision.Value = ViewState["hfId"].ToString();
                PanelMantenimientos.Visible = true;
                panelLista.Visible = false;
                btnNuevo.Visible = false;
                btnGuardar.Visible = false;
                btnCancel.Visible = true;
                HabilitarMiembro(1);


                foreach (GridViewRow row in GridViewConfirmacionComision.Rows)
                {
                    if (fId == GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["ComCfmId"].ToString())
                    {
                        ddlComision.SelectedValue = String.IsNullOrEmpty(GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["ComComCodigo"].ToString()) ? "0" : GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["ComComCodigo"].ToString();
                        ddlCTecnico.SelectedValue = String.IsNullOrEmpty(GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["ComCfmCoordinador"].ToString()) ? "0" : GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["ComCfmCoordinador"].ToString();
                        ddlTipo.SelectedValue = String.IsNullOrEmpty(GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["ComTipCodigo"].ToString()) ? "0" : GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["ComTipCodigo"].ToString();
                        try
                        {
                            ddlSecretaria.SelectedValue = String.IsNullOrEmpty(GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["ComCfmSecretaria"].ToString()) ? "0" : GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["ComCfmSecretaria"].ToString();
                        }
                        catch
                        {
                            ddlSecretaria.SelectedValue = "0";
                        }

                        ddlEstado.SelectedValue = String.IsNullOrEmpty(GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["AdmEstCodigo"].ToString()) ? "0" : GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["AdmEstCodigo"].ToString();
                        txtAtribuciones.Text = GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["ComCfmAtribucion"].ToString();
                        dtFecha.Text = GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["ComCfmFecha"].ToString();
                        ViewState["AdmpcoCodigo"] = GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["AdmPcoCodigo"].ToString();
                        lblPConstitucional.Text = GridViewConfirmacionComision.DataKeys[row.RowIndex].Values["AdmpcoDescripcion"].ToString();

                        ddlComision.Enabled = false;
                        ddlCTecnico.Enabled = false;
                        ddlTipo.Enabled = false;
                        ddlSecretaria.Enabled = false;
                        ddlEstado.Enabled = false;
                        txtAtribuciones.Enabled = false;
                        dtFecha.Enabled = false;

                        break;
                    }
                }

                string bicam = ddlComision.SelectedItem.ToString();
                if (bicam.Trim().Contains("BICAMERAL"))
                {
                    txtMCamra.Visible = true;
                    lblMiembros.Visible = true;
                    //txtMCamra.Disabled = true;
                    txtMCamra.Enabled = false;
                }
                else
                {
                    txtMCamra.Visible = false;
                    lblMiembros.Visible = false;
                }

                mtLoadData2(int.Parse(fId));

                ddlMiembros.Enabled = false;
                ddlFuncion.Enabled = false;
                btnAgregar.Enabled = false;
                
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }
        /// <summary>
        /// Metodo para borrar un registro del grid de legisladores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BorrarRegistro()
        {
            try
            {
                DataTable dt_Miembros = (DataTable)ViewState["dt_Miembros"];
                foreach (DataRow item in dt_Miembros.Rows)
                {
                    if (item["Eliminado"].ToString() == "True" && item["ComMbrSecuencia"].ToString() != "0")
                    {
                        ConfirmacionComision = new clsConfirmacionComision();
                        ConfirmacionComision.mtPutConfirmaComisionMiembros
                            (int.Parse(ViewState["hfId"].ToString()),
                             int.Parse(item["ComMbrSecuencia"].ToString()),
                             item["ComMbrNombre"].ToString(),
                             int.Parse(item["AdmFunCodigo"].ToString()),
                             true,
                             1
                             );
                        ConfirmacionComision.mtDispose();
                    }
                }
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        protected void BorrarRegistroExterno()
        {
            try
            {
                ConfirmacionComision = new clsConfirmacionComision();
                ConfirmacionComision.mtPutConfirmaComisionMiembros
                    (int.Parse(ViewState["hfId"].ToString()),
                     0,
                     "",
                     0,
                     false,
                     1
                     );
                ConfirmacionComision.mtDispose();
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        protected void BorrarRegistroViewState(object sender, EventArgs e)
        {
            try
            {
                if(int.Parse(ViewState["visualizar"].ToString()) == 1)
                {
                    return;
                }

                Button imgButton = (Button)sender;
                String NombreMiembro = imgButton.CommandArgument;



                foreach (GridViewRow row in GridViewLegisladores.Rows)
                {
                    if (NombreMiembro == GridViewLegisladores.DataKeys[row.RowIndex].Values["ComMbrNombre"].ToString())
                    {
                        DataTable dt_Miembros = (DataTable)ViewState["dt_Miembros"];
                        foreach (DataRow item in dt_Miembros.Rows)
                        {
                            if (item["ComMbrNombre"].ToString() == NombreMiembro.Trim())
                            {
                                item["Eliminado"] = true;
                            }
                        }
                        ViewState["dt_Miembros"] = dt_Miembros;
                        mtvAddMessage("Registro borrado satisfactoriamente", MessageType.success);
                        break;
                    }
                }
                mtLoadDataViewState();
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }
        /// <summary>
        /// Metodo para agregar un registro del grid de legisladores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AgregarRegistro()
        {
            DataTable dt_Miembros = (DataTable)ViewState["dt_Miembros"];
            foreach (DataRow item in dt_Miembros.Rows)
            {
                if (item["Nuevo"].ToString() == "False" && item["Eliminado"].ToString() == "False")
                {
                    ConfirmacionComision = new clsConfirmacionComision();
                    ConfirmacionComision.mtPutConfirmaComisionMiembros
                        (int.Parse(ViewState["hfId"].ToString()),
                         int.Parse(item["ComMbrSecuencia"].ToString()),
                         item["ComMbrNombre"].ToString(),
                         int.Parse(item["AdmFunCodigo"].ToString()),
                         bool.Parse(item["ComMbrInterno"].ToString()),
                         0
                         );
                    ConfirmacionComision.mtDispose();
                }
                if (item["Nuevo"].ToString() == "True" && item["ComMbrSecuencia"].ToString() == "0")
                {
                    ConfirmacionComision = new clsConfirmacionComision();
                    ConfirmacionComision.mtPutConfirmaComisionMiembros
                        (int.Parse(ViewState["hfId"].ToString()),
                         int.Parse(item["ComMbrSecuencia"].ToString()),
                         item["ComMbrNombre"].ToString(),
                         int.Parse(item["AdmFunCodigo"].ToString()),
                         bool.Parse(item["ComMbrInterno"].ToString()),
                         0
                         );
                    ConfirmacionComision.mtDispose();
                }
            }
        }

        protected void AgregarRegistroExterno()
        {
            try
            {
                //string MExternos = txtMCamra.Value.Trim();
                string MExternos = txtMCamra.Text.Trim();
                string[] split = MExternos.Split(new Char[] {';'});
                ConfirmacionComision = new clsConfirmacionComision();
                foreach (string s in split)
                {
                    if (s.Trim() != "")
                    {
                        ConfirmacionComision.mtPutConfirmaComisionMiembros
                        (int.Parse(ViewState["hfId"].ToString()),
                         0,
                         s.Trim(),
                         27,
                         false,
                         0
                         );
                        ConfirmacionComision.mtDispose();
                    }
                }
            }
            catch { }
        }

        protected void AgregarRegistroViewState(object sender, EventArgs e)
        {
            if (ddlMiembros.SelectedIndex == 0)
            {
                mtvAddMessage("Selecciona un miembro", MessageType.warning);
                return;
            }
            if (ddlFuncion.SelectedIndex == 0)
            {
                mtvAddMessage("Selecciona una funcion", MessageType.warning);
                return;
            }

            string nom = ddlMiembros.SelectedItem.ToString();
            string fun = ddlFuncion.SelectedItem.ToString();

            DataTable dt_Miembros = (DataTable)ViewState["dt_Miembros"];
            int existe = 0;
            foreach (DataRow item in dt_Miembros.Rows)
            {
                if (item["ComMbrNombre"].ToString() == nom.Trim() && item["Eliminado"].ToString() == "False")
                {
                    existe = 1;
                    break;
                }
            }

            if(existe == 1)
            {
                mtvAddMessage("Registro agregado anteriormente", MessageType.warning);
                return;
            }

            int existePresidente = 0;
            int existeVicePresidente = 0;
            int existeSecretario = 0;
            foreach (DataRow item in dt_Miembros.Rows)
            {
                if (int.Parse(item["AdmFunCodigo"].ToString()) == Int32.Parse(ddlFuncion.SelectedValue) && Int32.Parse(ddlFuncion.SelectedValue) == 24 && item["Eliminado"].ToString() == "False")
                {
                    existePresidente = 1;
                    break;
                }
            }

            if (existePresidente == 1)
            {
                mtvAddMessage("Ya existe un presidente registrado", MessageType.warning);
                return;
            }

            foreach (DataRow item in dt_Miembros.Rows)
            {
                if (int.Parse(item["AdmFunCodigo"].ToString()) == Int32.Parse(ddlFuncion.SelectedValue) && Int32.Parse(ddlFuncion.SelectedValue) == 25 && item["Eliminado"].ToString() == "False")
                {
                    existeVicePresidente = 1;
                    break;
                }
            }

            if (existeVicePresidente == 1)
            {
                mtvAddMessage("Ya existe un vicepresidente registrado", MessageType.warning);
                return;
            }

            foreach (DataRow item in dt_Miembros.Rows)
            {
                if (int.Parse(item["AdmFunCodigo"].ToString()) == Int32.Parse(ddlFuncion.SelectedValue) && Int32.Parse(ddlFuncion.SelectedValue) == 26 && item["Eliminado"].ToString() == "False")
                {
                    existeSecretario = 1;
                    break;
                }
            }

            if (existeSecretario == 1)
            {
                mtvAddMessage("Ya existe un secretario registrado", MessageType.warning);
                return;
            }

            DataRow row1 = dt_Miembros.NewRow();
            row1["ComCfmId"] = ViewState["hfId"].ToString();
            row1["ComMbrSecuencia"] = 0;
            row1["ComMbrNombre"] = nom.Trim();
            row1["AdmFunCodigo"] = Int32.Parse(ddlFuncion.SelectedValue);
            row1["AdmFunDescripcion"] = fun.Trim();
            row1["ComMbrInterno"] = true;
            row1["Nuevo"] = true;
            row1["Eliminado"] = false;
            dt_Miembros.Rows.Add(row1);

            ViewState["dt_Miembros"] = dt_Miembros;


            mtvAddMessage("Registro agregado satisfactoriamente", MessageType.success);

            mtLoadDataViewState();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["hfId"] = "0";
                idComision.Value = ViewState["hfId"].ToString();
                PanelMantenimientos.Visible = true;
                panelLista.Visible = false;
                btnEditar2.Visible = false;
                btnNuevo.Visible = false;
                btnGuardar.Visible = true;
                btnCancel.Visible = true;
                ddlComision.SelectedIndex = 0;
                ddlCTecnico.SelectedIndex = 0;
                ddlTipo.SelectedIndex = 0;
                ddlSecretaria.SelectedIndex = 0;
                ddlEstado.SelectedIndex = 0;
                ddlMiembros.SelectedIndex = 0;
                ddlFuncion.SelectedIndex = 0;
                //txtMCamra.Value = "";
                txtMCamra.Text = "";
                txtAtribuciones.Text = "";
                dtFecha.Text = "";
                ddlEstado.SelectedValue = "174";
                ddlEstado.Enabled = false;
                HabilitarMiembro(0);
                ObtenerPeriodoConstitucional();
                
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
                ViewState["visualizar"] = 0;
                ViewState["hfId"] = "0";
                idComision.Value = ViewState["hfId"].ToString();
                PanelMantenimientos.Visible = false;
                panelLista.Visible = true;
                btnNuevo.Visible = true;
                btnCancel.Visible = false;
                btnGuardar.Visible = false;
                ddlEstado.Enabled = true;
                mtLoadData();
                //habilitar controles
                ddlComision.Enabled = true;
                ddlCTecnico.Enabled = true;
                ddlTipo.Enabled = true;
                ddlSecretaria.Enabled = true;
                ddlEstado.Enabled = true;
                txtAtribuciones.Enabled = true;
                dtFecha.Enabled = true;
                //txtMCamra.Disabled = false;
                txtMCamra.Enabled = true;
                ddlMiembros.Enabled = true;
                ddlFuncion.Enabled = true;
                btnAgregar.Enabled = true;
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ddlComision.SelectedIndex == 0)
            {
                mtvAddMessage("Selecciona una comision", MessageType.warning);
                return;
            }
            if (ddlCTecnico.SelectedIndex == 0)
            {
                mtvAddMessage("Selecciona un coordinador", MessageType.warning);
                return;
            }
            if (ddlTipo.SelectedIndex == 0)
            {
                mtvAddMessage("Selecciona un tipo", MessageType.warning);
                return;
            }
            if (ddlEstado.SelectedIndex == 0)
            {
                mtvAddMessage("Selecciona un estado", MessageType.warning);
                return;
            }

            if (string.IsNullOrEmpty(dtFecha.Text.Trim()))
            {
                mtvAddMessage("Selecciona la fecha", MessageType.warning);
                return;
            }
           
            bool disuelta = false;
            if (Int32.Parse(ViewState["hfId"].ToString()) > 0)
            {
                if (Int32.Parse(ddlEstado.SelectedValue) == 14)
                {
                    disuelta = true;
                }

                if (string.IsNullOrEmpty(ViewState["Estado"].ToString()))
                {
                    ViewState["Estado"] = ddlEstado.SelectedValue;
                }

                if (int.Parse(ViewState["Estado"].ToString()) == 174)
                {
                    if (int.Parse(ViewState["Estado"].ToString()) != Int32.Parse(ddlEstado.SelectedValue))
                    {
                        if (Int32.Parse(ddlEstado.SelectedValue) != 13 && Int32.Parse(ddlEstado.SelectedValue) != 175)
                        {
                            mtvAddMessage("Solo puedes seleccionar el estado Activa o Inactiva.", MessageType.warning);
                            return;
                        }
                    }
                }
            
                if (int.Parse(ViewState["Estado"].ToString()) == 13)
                {
                    if (int.Parse(ViewState["Estado"].ToString()) != Int32.Parse(ddlEstado.SelectedValue))
                    {
                        if (Int32.Parse(ddlEstado.SelectedValue) != 14 && Int32.Parse(ddlEstado.SelectedValue) != 15)
                        {
                            mtvAddMessage("Solo puedes seleccionar el estado Disuelta o Segregada.", MessageType.warning);
                            return;
                        }
                    }
                }
            }

            if(Int32.Parse(ViewState["hfId"].ToString()) == 0)
            {
                //ClientScript.RegisterStartupScript(GetType(), "alert", "ActivarComision();", true);
                if(Activada.Value == "1")
                {
                    ddlEstado.SelectedValue = "13";
                }
            }
            
             ConfirmacionComision = new clsConfirmacionComision();
             ConfirmacionComision.mtPutConfirmaComision
                (Int32.Parse(ViewState["hfId"].ToString())//Id del Registro
                , Int32.Parse(ddlComision.SelectedValue)
                , Int32.Parse(ddlTipo.SelectedValue)
                , Int32.Parse(ViewState["AdmpcoCodigo"].ToString())
                ,DateTime.Parse(dtFecha.Text.Trim())
                , Int32.Parse(ddlCTecnico.SelectedValue)
                , Int32.Parse(ddlSecretaria.SelectedValue)
                , disuelta
                ,txtAtribuciones.Text.Trim()
                , Int32.Parse(ddlEstado.SelectedValue)
                );
             ConfirmacionComision.mtDispose();

            DataTable dt_Miembros = (DataTable)ViewState["dt_Miembros"];
            if (dt_Miembros.Rows.Count > 0)
            {
                //BorrarRegistro();
                BorrarRegistroExterno();
                AgregarRegistro();
            }

            if (txtMCamra.Visible == true && Int32.Parse(ViewState["hfId"].ToString()) > 0)
            {
                //BorrarRegistroExterno();
                AgregarRegistroExterno();
            }

            ViewState["hfId"] = "0";
            idComision.Value = ViewState["hfId"].ToString();
            mtvAddMessage("Registro guardado satisfactoriamente", MessageType.success);

            PanelMantenimientos.Visible = false;
            btnGuardar.Visible = false;
            btnNuevo.Visible = true;
            btnCancel.Visible = false;
            panelLista.Visible = true;

            mtLoadData();
        } 

        public void HabilitarMiembro(int habilitar)
        {
            if(habilitar == 0)
            {
                //txtMCamra.Disabled = true;
                txtMCamra.Enabled = false;
                ddlMiembros.Enabled = false;
                ddlFuncion.Enabled = false;
                btnAgregar.Enabled = false;
            }
            if (habilitar == 1)
            {
                //txtMCamra.Disabled = false;
                txtMCamra.Enabled = true;
                ddlMiembros.Enabled = true;
                ddlFuncion.Enabled = true;
                btnAgregar.Enabled = true;
                ddlMiembros.SelectedIndex = 0;
                ddlFuncion.SelectedIndex = 0;
            }
        }


          
    }
}