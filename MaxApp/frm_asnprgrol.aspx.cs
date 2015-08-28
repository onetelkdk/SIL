using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_asnprgrol : PageBase
    {
        MaxBll.clsRoles objRoles;
        MaxBll.clsModulos objModulos;
        MaxBll.clsTiposPrg objTipos;
        MaxBll.clsProgramas objProgramas;
        MaxBll.clsAdmRolDetalles objRolDetalles;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                mtvClearMessage();

                if (!Page.IsPostBack)
                {
                    if (mtValidPage("frm_asnprgrol.aspx", lblModulo))
                    {
                        MenuFlotante.InnerHtml = mtGetMenu();

                        objRoles = new MaxBll.clsRoles();
                        objModulos = new MaxBll.clsModulos();
                        objTipos = new MaxBll.clsTiposPrg();

                        mtBindDropDownList(cboRoles, objRoles.mtGetRoles(), "AdmrolDescripcion", "AdmrolCodigo", "Seleccione el Rol");
                        mtBindDropDownList(cboModulos, objModulos.mtGetModulos(String.Empty), "AdmmodNombre", "AdmmodCodigo", "Seleccione el Módulo");
                        mtBindDropDownList(cboOpciones, objTipos.mtGetTipos(), "PrgtipNombre", "PrgtipCodigo", "Seleccione la Opción/Tipo");
                        objRoles.mtDispose();
                        objModulos.mtDispose();
                        objTipos.mtDispose();
                        cboModulos.Enabled = false;
                        cboOpciones.Enabled = false;

                        imgAgregar.Enabled = false;
                        imgBorrar.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            { 
            mtvAddMessage(ex.Message ,MessageType.error);
               
            }
        }

       

        protected void imgAgregar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                //Asignando las opciones
                foreach (ListItem item in ListDisponibles.Items)
                {
                    if (item.Selected)
                    ListAsignadas.Items.Add(new ListItem(item.Text , item.Value));
                }

                //Eliminar de la lista las que se asignaron.

                foreach (ListItem item in ListAsignadas.Items)
                {
                    ListDisponibles.Items.Remove(new ListItem(item.Text, item.Value));
                }

               
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }

        }

        protected void imgBorrar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //Asignando las opciones
                foreach (ListItem item in ListAsignadas.Items)
                {
                    if (item.Selected)
                        ListDisponibles.Items.Add(new ListItem(item.Text, item.Value));
                }

                //Eliminar de la lista las que se asignaron.

                foreach (ListItem item in ListDisponibles.Items)
                {
                    ListAsignadas.Items.Remove(new ListItem(item.Text, item.Value));
                }
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        private void mtLoadData()
        {
            objProgramas = new MaxBll.clsProgramas();
            ListDisponibles.DataTextField = "PrgprgDescripcion";
            ListDisponibles.DataValueField = "PrgprgNombre";
            ListDisponibles.DataSource = objProgramas.mtGetProgramas(cboModulos.SelectedValue, cboOpciones.SelectedValue);
            ListDisponibles.DataBind();
            objProgramas.mtDispose();
            objRolDetalles = new MaxBll.clsAdmRolDetalles();

            ListAsignadas.DataTextField = "PrgprgDescripcion";
            ListAsignadas.DataValueField = "PrgprgNombre";
            ListAsignadas.DataSource = objRolDetalles.mtGetRolDetalles(cboRoles.SelectedValue,cboModulos.SelectedValue,cboOpciones.SelectedValue);
            ListAsignadas.DataBind();
            objRolDetalles.mtDispose();
            //Eliminar de Disponibles las que ya estan asignadas

            if (ListAsignadas.Items.Count > 0)
            {

                //Eliminar de la lista las que se asignaron.

                foreach (ListItem item in ListAsignadas.Items)
                {
                    ListDisponibles.Items.Remove(new ListItem(item.Text, item.Value));
                }
            }

        }

        protected void cboRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (cboRoles.SelectedValue != "0")
                    cboModulos.Enabled = true;
                else
                    cboModulos.Enabled = false;

                cboModulos.SelectedValue = "0";
                cboOpciones.SelectedValue = "0";
                cboOpciones.Enabled = false;
                mtClearData();
               


            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        protected void cboModulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                

                if (cboModulos.SelectedValue != "0")
                    cboOpciones.Enabled = true;
                else
                    cboOpciones.Enabled = false;

                mtClearData();
                cboOpciones.SelectedValue = "0";


            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }


        protected void cboOpciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboOpciones.SelectedValue != "0")
                {
                    mtLoadData();

                    imgAgregar.Enabled = true;
                    imgBorrar.Enabled = true;
                }
                else
                {
                    mtClearData();
                }


            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        private void mtClearData()
        {
            ListDisponibles.DataSource = new System.Data.DataTable();
            ListDisponibles.DataBind();

            ListAsignadas.DataSource = new System.Data.DataTable();
            ListAsignadas.DataBind();

            imgAgregar.Enabled = false;
            imgBorrar.Enabled = false;
        }

        private void mtIni()
        {
            cboModulos.Enabled = false;
            cboOpciones.Enabled = false;
            cboModulos.SelectedValue = "0";
            cboOpciones.SelectedValue = "0";
            mtClearData();
            cboRoles.SelectedValue = "0";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                mtIni();
              
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
                objRolDetalles = new MaxBll.clsAdmRolDetalles();
                objRolDetalles.mtBeginTran();

                //Procedo a eliminar las no asignadas antes de hacer la insercion de las seleccionadas
                    objRolDetalles.mtPutRolDetalle(1, Int32.Parse(cboRoles.SelectedValue), String.Empty, cboRoles.Text
                        , false, cboModulos.SelectedValue, cboOpciones.SelectedValue, true);
                 

                //Agregando las opciones asignadas
                foreach (ListItem item in ListAsignadas.Items)
                {
                    objRolDetalles.mtPutRolDetalle(0, Int32.Parse(cboRoles.SelectedValue), item.Value, cboRoles.Text
                        , true, cboModulos.SelectedValue, cboOpciones.SelectedValue, false);
                }

                objRolDetalles.mtCommitTran();
                mtIni();
                mtvAddMessage("Asignaciones agregadas satisfactoriamente", MessageType.success);
            
            }
            catch (Exception ex)
            {
                objRolDetalles.mtRollBackTran();
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

    }
}