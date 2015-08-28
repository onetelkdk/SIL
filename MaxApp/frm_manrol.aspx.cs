using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_manrol : PageBase
    {
        MaxBll.clsRoles objRoles;
        protected void Page_Load(object sender, EventArgs e)
        {
            mtvClearMessage();

            if (!Page.IsPostBack)
            {
                try
                {
                    if (!Page.IsPostBack)
                    {
                        if (mtValidPage("frm_manrol.aspx", lblModulo))
                        {
                            MenuFlotante.InnerHtml = mtGetMenu();

                            mtLoadData();
                            MenuFlotante.InnerHtml = mtGetMenu();
                        }
                    }

                }
                catch (Exception ex)
                {
                    mtvAddMessage(ex.Message, MessageType.error);
                }
            }

        }

        protected void EditarRegistro(object sender, EventArgs e)
        {
            try
            {
                Button imgButton = (Button)sender;
                String fAdmrolCodigo = imgButton.CommandArgument;
                hfId.Value = fAdmrolCodigo.ToString();

                mtHabilitar(true);


                foreach (GridViewRow row in gv_Roles.Rows)
                {
                    if (hfId.Value == gv_Roles.DataKeys[row.RowIndex].Values["AdmrolCodigo"].ToString())
                    {
                        txtRol.Text = gv_Roles.DataKeys[row.RowIndex].Values["AdmRolDescripcion"].ToString();
                        if (Boolean.Parse(gv_Roles.DataKeys[row.RowIndex].Values["AdmEstCodigo"].ToString()))
                            chkEstado.Checked = true;
                        else
                            chkEstado.Checked = false;
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }

        }

        private void mtLoadData()
        {

            mtHabilitar(false);
            hfId.Value = "0";
            objRoles = new MaxBll.clsRoles();
            mtBindGridView(gv_Roles, objRoles.mtGetRoles());
            gv_Roles.UseAccessibleHeader = true;
            gv_Roles.HeaderRow.TableSection = TableRowSection.TableHeader;
            objRoles.mtDispose();

        }

        private void mtHabilitar(Boolean vEsNewEdit)
        {
            if (vEsNewEdit)
            {
                PanelMantenimientos.Visible = true;
                panelLista.Visible = false;
                btnNuevo.Visible = false;
                btnGuardar.Visible = true;
                btnCancel.Visible = true;
                txtRol.Focus();

            }
            else
            {
                PanelMantenimientos.Visible = false;
                panelLista.Visible = true;
                btnNuevo.Visible = true;
                btnGuardar.Visible = false;
                btnCancel.Visible = false;
                txtRol.Text = String.Empty;
                chkEstado.Checked = false;
                hfId.Value = "0";
            }

        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            mtHabilitar(true);
            hfId.Value = "0";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mtLoadData();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                objRoles = new MaxBll.clsRoles();
                objRoles.mtPutRoles(
                    Int32.Parse(hfId.Value)
                    , txtRol.Text.Trim()
                     , txtRol.Text.Trim()
                     , chkEstado.Checked
                    );

                objRoles.mtDispose();
                mtLoadData();
                mtvAddMessage("Usuario guardado satisfactoriamente", MessageType.success);

            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }
    }
}
