using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_regasi : PageBase
    {
        MaxBll.clsComision objComision;
        MaxBll.clsConfirmacionComision objConfirmacionComision;
        MaxBll.clsConfirmacionInvitados objConfirmacionInvitados;
        MaxBll.clsAsistenciaComision objAsistenciaComision;
        MaxBll.clsExcusa objExcusa;


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                mtvClearMessage();

                if (!Page.IsPostBack)
                {
                    if (mtValidPage("frm_regasi.aspx", lblModulo, lblNombrePagina))
                    {

                        MenuFlotante.InnerHtml = mtGetMenu();

                        objComision = new MaxBll.clsComision();
                        objConfirmacionComision = new MaxBll.clsConfirmacionComision();
                        objExcusa = new MaxBll.clsExcusa();

                        mtBindDropDownList(ddlActividades, objComision.mtGetmComActAsistencia(0), "ComActNumero", "ComActCodigoSis", "[Seleccione una Opción]");
                        mtBindDropDownList(ddlComision, objConfirmacionComision.mtGetConfirmaComision(), "ComComNombre", "ComCfmId", "[Seleccione una Opción]");
                        mtBindDropDownList(ddlExcusa, objExcusa.mtGetExcusas(), "AdmExcDescripcion", "AdmExcCodigo", "[Seleccione una Motivo]");
                        objComision.mtDispose();
                        objConfirmacionComision.mtDispose();

                        txtFechaReunion.Enabled = false;
                        txtFechaReunion.CssClass = "date fecha-240";
                        ddlComision.Enabled = false;
                        ddlComision.CssClass = "select-240";
                        txtAsunto.Disabled = true;
                        txtHoraInicio.Enabled = false;
                        txtHoraInicio.CssClass = "time time-240";
                        txtHoraFinal.Enabled = false;
                        txtHoraFinal.CssClass = "time time-240";

                        mtClearGridView(gv_Legisladores);
                        mtClearGridView(gv_Invitados);
                        mtClearGridView(gv_LegisladoresAsistencia);
                        mtClearGridView(gv_InvitadosAsistencia);

                        //DateTime fFecha = DateTime.Now;
                        //fFecha = new DateTime(2015, 08, 24, 1, 12, 23);

                    }

                }

            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        private void mtLoadAsistencia(Int32 vIdComision, Int32 vIdActividad)
        {
            //Las dos primeras Consultas son los que estan invitados para asistir, se le debe restar los agregados
            objConfirmacionComision = new MaxBll.clsConfirmacionComision();
            DataTable dtMiembros = objConfirmacionComision.mtGetConfirmaComisionMiembros(vIdComision);

            objConfirmacionInvitados = new MaxBll.clsConfirmacionInvitados();
            DataTable dtInvitados = objConfirmacionInvitados.mtGetConfirmaInvitados(vIdActividad);

            //Excluir de los DataTables Anteriores los registros que esten en los anteriores
            objAsistenciaComision = new MaxBll.clsAsistenciaComision();

            DataTable dtAsistenciaMiembros = objAsistenciaComision.mtGetAsistenciaMiembros(Int32.Parse(hfIdAsistencia.Value));
            DataTable dtAsistenciaInvitados = objAsistenciaComision.mtGetAsistenciaInvitados(Int32.Parse(hfIdAsistencia.Value));

            if (dtMiembros.Rows.Count > 0)
            {
                String fCodigosMiembros = String.Empty;
                if (dtAsistenciaMiembros.Rows.Count > 0)
                {
                    foreach (DataRow oRow in dtAsistenciaMiembros.Rows)
                    {
                        fCodigosMiembros += oRow["AdmLegCodigo"].ToString() + "\t";
                    }

                    mtBindGridView(gv_LegisladoresAsistencia, dtAsistenciaMiembros);

                    fCodigosMiembros = fCodigosMiembros.Trim().Replace('\t', ',');

                    if (!String.IsNullOrEmpty(fCodigosMiembros))
                    {
                        DataRow[] ListRows = dtMiembros.Select(String.Format("AdmLegCodigo NOT IN({0})", fCodigosMiembros));

                        mtBindGridView(gv_Legisladores, ListRows.CopyToDataTable());
                    }
                }
                else
                {
                    mtBindGridView(gv_Legisladores, dtMiembros);
                }

            }
            else
            {
                mtClearGridView(gv_Legisladores);
                mtClearGridView(gv_LegisladoresAsistencia);
            }


            if (dtInvitados.Rows.Count > 0)
            {
                String fCodigosInvitados = String.Empty;
                if (dtAsistenciaInvitados.Rows.Count > 0)
                {
                    foreach (DataRow oRow in dtAsistenciaInvitados.Rows)
                    {
                        fCodigosInvitados += oRow["ComAinLinea"].ToString() + "\t";
                    }

                    mtBindGridView(gv_InvitadosAsistencia, dtAsistenciaInvitados);

                    fCodigosInvitados = fCodigosInvitados.Trim().Replace('\t', ',');

                    if (!String.IsNullOrEmpty(fCodigosInvitados))
                    {
                        DataRow[] ListRows = dtInvitados.Select(String.Format("ComInvSecuencia NOT IN({0})", fCodigosInvitados));
                        mtBindGridView(gv_Invitados, ListRows.CopyToDataTable());
                    }
                }
                else
                {
                    mtBindGridView(gv_Invitados, dtInvitados);
                }

            }
            else
            {
                mtClearGridView(gv_Legisladores);
                mtClearGridView(gv_LegisladoresAsistencia);
            }

        }

        protected void ddlActividades_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlActividades.SelectedValue != "0")
                {
                    objComision = new MaxBll.clsComision();
                    DataTable dt = objComision.mtGetmComActAsistencia(Int32.Parse(ddlActividades.SelectedValue));
                    objComision.mtDispose();
                    if (dt.Rows.Count > 0)
                    {

                        ddlComision.SelectedValue = dt.Rows[0]["ComCfmId"].ToString();

                        txtFechaReunion.Text = Convert.ToDateTime(dt.Rows[0]["ComActFecha"].ToString()).ToShortDateString();
                        txtAsunto.Value = dt.Rows[0]["ComActDescripcion"].ToString();
                        txtHoraInicio.Text = Convert.ToDateTime(dt.Rows[0]["ComActHoraInicio"].ToString()).ToShortTimeString();
                        txtHoraFinal.Text = Convert.ToDateTime(dt.Rows[0]["ComActHoraCierre"].ToString()).ToShortTimeString();

                        objConfirmacionComision = new MaxBll.clsConfirmacionComision();
                        objConfirmacionInvitados = new MaxBll.clsConfirmacionInvitados();

                        objAsistenciaComision = new MaxBll.clsAsistenciaComision();

                        //Buscar Si se le ha pasado Asistencia
                        DataTable dtAsistencia = objAsistenciaComision.mtGetAsistenciaComision(Int32.Parse(dt.Rows[0]["ComActCodigoSis"].ToString()));
                        if (dtAsistencia.Rows.Count > 0)
                        {
                            hfIdAsistencia.Value = dtAsistencia.Rows[0]["ComAsiCodigo"].ToString();
                            mtLoadAsistencia(Int32.Parse(dt.Rows[0]["ComCfmId"].ToString()), Int32.Parse(dt.Rows[0]["ComActCodigoSis"].ToString()));

                        }
                        else
                        {
                            hfIdAsistencia.Value = "0";
                            mtBindGridView(gv_Legisladores, objConfirmacionComision.mtGetConfirmaComisionMiembros(Int32.Parse(dt.Rows[0]["ComCfmId"].ToString())));
                            mtBindGridView(gv_Invitados, objConfirmacionInvitados.mtGetConfirmaInvitados(Int32.Parse(dt.Rows[0]["ComActCodigoSis"].ToString())));
                            mtClearGridView(gv_LegisladoresAsistencia);
                            mtClearGridView(gv_InvitadosAsistencia);
                        }

                    }
                }
                else                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           //Limpiar los Campos
                {
                    ddlComision.SelectedValue = "0";
                    txtFechaReunion.Text = String.Empty;
                    txtAsunto.Value = String.Empty;
                    txtHoraInicio.Text = String.Empty;
                    txtHoraFinal.Text = String.Empty;
                    mtClearGridView(gv_Legisladores);
                }

            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        public Boolean mtSelectedRecord(GridView vGrid,String vField)
        {
            Boolean _Return = false;

            foreach (GridViewRow row in vGrid.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl(vField) as CheckBox);

                    if (chkRow.Checked)
                    {
                        _Return = true;

                        break;
                    }
                }
            }

            return _Return;
        }

        protected void btnClickMbr(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            objAsistenciaComision = new MaxBll.clsAsistenciaComision();

            //Buscar Si se le ha pasado Asistencia, De los Contrario paso Asistencia y busco dicho Codigo
            if (hfIdAsistencia.Value == "0")
            {
                objComision = new MaxBll.clsComision();
                DataTable dt = objComision.mtGetmComActAsistencia(Int32.Parse(ddlActividades.SelectedValue));
                objComision.mtDispose();
                if (dt.Rows.Count > 0)
                {
                    //DataTable dtAsistenciaComision = objAsistenciaComision.mtSetAsistenciaComision(
                    //    Int32.Parse(ddlActividades.SelectedValue)
                    //    , Int32.Parse(dt.Rows[0]["AdmLetCodigo"].ToString())
                    //    , Int32.Parse(dt.Rows[0]["AdmPcoCodigo"].ToString())
                    //    , Session["AdmusrCodigo"].ToString()
                    //    , 1
                    //    );

                    //hfIdAsistencia.Value = dt.Rows[0]["ComAsiCodigo"].ToString();
                }
            }

            Int32 fComAmbrCodigo = Int32.Parse(hfComAmbrCodigo.Value);

            switch (btn.ID)
            {
                case "btnAgregarMiembro":

                    if (fComAmbrCodigo == 0)//Es Insertando
                    {
                        if (mtSelectedRecord(gv_Legisladores, "chkRow"))
                        {


                        }
                        else
                        {
                            txtHoraLlegadaAsistenciaMiembro.Focus();
                            miembrosComision.Attributes.Add("class","active");
                            mtvAddMessage("Seleccione el Registro a Guardar", MessageType.warning);
                           
                        }

                        //foreach (GridViewRow row in gv_Legisladores.Rows)
                        //{
                        //    if (row.RowType == DataControlRowType.DataRow)
                        //    {
                        //        CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);

                        //        if (chkRow.Checked)
                        //        { 
                                
                        //        }
                        //    }
                        //}

                        //Buscar del Grid de Arriba el que está seleccionado

                    }
                    else//Es Editando
                    {
                        if (mtSelectedRecord(gv_LegisladoresAsistencia, "chkRowMbr"))
                        {


                        }
                        else
                        {
                            mtvAddMessage("Seleccione el Registro a Guardar", MessageType.warning);
                        }
                    }


                    /*
                    objAsistenciaComision.mtSetAsistenciaMiembros(
                        fComAmbrCodigo
                        , Int32.Parse("AdmLegCodigo")
                        , Int32.Parse("AdmComAmbrLinea")
                        , "ComAmbrNombre"
                        , DateTime.Parse("HoraLlegada")
                        , DateTime.Parse("HoraSalida")
                        , !chkExcusaMiembro.Checked
                        , chkExcusaMiembro.Checked
                        , Int16.Parse(ddlExcusa.SelectedValue)
                        , txtObservaciones.Value
                        , false
                        , Session["AdmusrCodigo"].ToString()
                        , Int32.Parse(hfIdAsistencia.Value)
                        );
                    */

                    break;
                case "btnEditarMiembro":
                    break;
                case "btnBorrarMiembro":
                    objAsistenciaComision.mtSetAsistenciaMiembros(0, 0, 0, String.Empty, DateTime.Now, DateTime.Now, false, false, 0, String.Empty, false, String.Empty, 0, true);
                    break;
            }
        }

        protected void btnClickInv(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
   
            switch (btn.ID)
            {
                case "btnAgreagarInvitados":
                    break;
                case "btnEditarInvitados":
                    break;
                case "btnBorrarInvitados":
                    break;
            }
        }
    }
}