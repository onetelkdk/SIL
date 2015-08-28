using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace MaxApp
{
    public partial class frm_ManMsj : PageBase
    {
        MaxBll.clsProgramas objProgramas;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                mtvClearMessage();

                if (!Page.IsPostBack)
                {
                    if (mtValidPage("frm_ManMsj.aspx", lblModulo))
                    {
                        MenuFlotante.InnerHtml = mtGetMenu();

                        objProgramas = new MaxBll.clsProgramas();
                        mtBindDropDownList(cboProgramas, objProgramas.mtGetProgramas(String.Empty, String.Empty), "PrgprgDescripcion", "PrgprgNombre", "[Seleccione una Opción]");
                        objProgramas.mtDispose();
                        mtLoadData();
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
            txtCodigo.ReadOnly = false;
            mtHabilitar(false);
            DataSet ds = new DataSet();

            ds.ReadXml(System.Configuration.ConfigurationManager.AppSettings["xmlMensajes"].ToString());
            if (ds != null && ds.HasChanges())
            {
                mtBindGridView(gv_Mensajes, ds);
            }

        }

        protected void EditarRegistro(object sender, EventArgs e)
        {
            try
            {
                Button imgButton = (Button)sender;
                String fId = imgButton.CommandArgument;
                hfId.Value = fId.ToString();

                txtCodigo.ReadOnly = true;

                mtHabilitar(true);

                foreach (GridViewRow row in gv_Mensajes.Rows)
                {
                    if (fId == gv_Mensajes.DataKeys[row.RowIndex].Values["Id"].ToString())
                    {
                        txtCodigo.Text = gv_Mensajes.DataKeys[row.RowIndex].Values["Codigo"].ToString();
                        cboProgramas.SelectedValue = gv_Mensajes.DataKeys[row.RowIndex].Values["Programa"].ToString();
                        txtDescripcion.Text = gv_Mensajes.DataKeys[row.RowIndex].Values["Descripcion"].ToString();
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }

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
                txtCodigo.Focus();
            }
            else
            {
                PanelMantenimientos.Visible = false;
                panelLista.Visible = true;
                btnNuevo.Visible = true;
                btnGuardar.Visible = false;
                btnCancel.Visible = false;

                txtCodigo.Text = String.Empty;
                cboProgramas.SelectedValue = "0";
                txtDescripcion.Text = String.Empty;
                hfId.Value = "0";

            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                mtHabilitar(true);
                hfId.Value = "0";
                txtCodigo.Focus();
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        private Boolean mtValidar()
        {
            String fMensaje = String.Empty;

            Boolean fIsValid = true;

            if (String.IsNullOrEmpty(txtCodigo.Text))
            {
                //fMensaje = "El campo Código se encuentra en blanco, favor insertar";
                //mtvAddMessage(fMensaje, MessageType.warning);
                fIsValid = false;
                mtvAddMessage("frm_ManMsj.aspx", "01", MessageType.warning);
                txtCodigo.Focus();
            }
            else if (cboProgramas.SelectedValue == "0")
            {
                //fMensaje = "Seleccione un Nombre de Programa";
                //mtvAddMessage(fMensaje, MessageType.warning);
                fIsValid = false;
                mtvAddMessage("frm_ManMsj.aspx", "02", MessageType.warning);
                cboProgramas.Focus();
            }
            else if (String.IsNullOrEmpty(txtDescripcion.Text))
            {
                //fMensaje = "El campo Mensaje se encuentra en blanco, favor insertar";
                //mtvAddMessage(fMensaje, MessageType.warning);
                fIsValid = false;
                mtvAddMessage("frm_ManMsj.aspx", "03", MessageType.warning);
                txtDescripcion.Focus();
            }

            return fIsValid;//String.IsNullOrEmpty(fMensaje);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!mtValidar())
                    return;

                Int32 fId = Int32.Parse(hfId.Value);

                if (fId == 0)
                {
                   
                    DataSet ds = new DataSet();

                    ds.ReadXml(System.Configuration.ConfigurationManager.AppSettings["xmlMensajes"].ToString());

                    var Query = from f in ds.Tables[0].AsEnumerable()
                                where f.Field<String>("Programa") == cboProgramas.SelectedValue && f.Field<String>("Codigo") == txtCodigo.Text
                                select f;

                    if (Query == null || Query.Count()==0) //No Existe
                    {
                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.Load(System.Configuration.ConfigurationManager.AppSettings["xmlMensajes"].ToString());

                        XmlElement parentelement = xmldoc.CreateElement("Mensaje");

                        XmlElement Id = xmldoc.CreateElement("Id");
                        XmlElement Programa = xmldoc.CreateElement("Programa");
                        XmlElement DescPrograma = xmldoc.CreateElement("DescPrograma");
                        XmlElement Codigo = xmldoc.CreateElement("Codigo");
                        XmlElement Descripcion = xmldoc.CreateElement("Descripcion");

                        Int32 fNextId = gv_Mensajes.Rows.Count + 1;

                        Id.InnerText = fNextId.ToString();
                        Programa.InnerText = cboProgramas.SelectedValue;
                        DescPrograma.InnerText = cboProgramas.SelectedItem.Text;
                        Codigo.InnerText = txtCodigo.Text;
                        Descripcion.InnerText = txtDescripcion.Text;

                        parentelement.AppendChild(Id);
                        parentelement.AppendChild(Programa);
                        parentelement.AppendChild(DescPrograma);
                        parentelement.AppendChild(Codigo);
                        parentelement.AppendChild(Descripcion);

                        xmldoc.DocumentElement.AppendChild(parentelement);
                        xmldoc.Save(System.Configuration.ConfigurationManager.AppSettings["xmlMensajes"].ToString());
                    }
                    else
                    {
                        mtvAddMessage("frm_ManMsj.aspx", "05", MessageType.warning);
                        //mtvAddMessage("El código a Registrar para el programa seleccionado ya existe", MessageType.warning);
                        txtCodigo.Focus();
                        return;
                    }
                    
                }
                else//Modificando un Registro
                {
                    Int32 fRowIndex = fId - 1;
                    DataSet ds = new DataSet();
                    ds.ReadXml(System.Configuration.ConfigurationManager.AppSettings["xmlMensajes"].ToString());
                    ds.Tables[0].Rows[fRowIndex]["Programa"] = cboProgramas.SelectedValue;
                    ds.Tables[0].Rows[fRowIndex]["DescPrograma"] = cboProgramas.SelectedItem.Text;
                    ds.Tables[0].Rows[fRowIndex]["Codigo"] = txtCodigo.Text;
                    ds.Tables[0].Rows[fRowIndex]["Descripcion"] = txtDescripcion.Text;
                    ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["xmlMensajes"].ToString());
                }
                mtLoadData();
                 mtvAddMessage("frm_ManMsj.aspx", "04", MessageType.success);
                //mtvAddMessage("Mensaje guardado satisfactoriamente", MessageType.success);
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
                mtLoadData();
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }
    }
}