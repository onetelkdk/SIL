using MaxBll;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_manppol : PageBase
    {
        clsPartido Partido;
        private const string imageLogoDefault = @"images/logotipo-inst.png";

        protected void Page_Load(object sender, EventArgs e)
        {
            mtvClearMessage();
            if (!Page.IsPostBack)
            {
                try
                {
                    if (mtValidPage("frm_manppol.aspx", lblModulo))
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
            Partido = new clsPartido();

            mtBindGridView(GridViewPartidos, Partido.mtGetPartido());
            if (GridViewPartidos.Rows.Count > 0)
            {
                GridViewPartidos.UseAccessibleHeader = true;
                GridViewPartidos.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            Partido.mtDispose();
        }

        protected void VisualizarRegistro(object sender, EventArgs e)
        {
            EditarRegistro(sender, e);
            btnGuardar.Visible = false;
            txtSiglas.Enabled = false;
            txtDescripcion.Enabled = false;
            ddlEstado.Enabled = false;
            FileUploadASP.Enabled = false;
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


                foreach (GridViewRow row in GridViewPartidos.Rows)
                {
                    if (fId == GridViewPartidos.DataKeys[row.RowIndex].Values["AdmPdoCodigo"].ToString())
                    {
                        var imgUrl = String.Concat(@System.Configuration.ConfigurationManager.AppSettings["ImagnesLogos"], GridViewPartidos.DataKeys[row.RowIndex].Values["AdmPdoLogo"].ToString()).Replace(Request.ServerVariables["APPL_PHYSICAL_PATH"], String.Empty);
                        //var imgUrl = System.Configuration.ConfigurationManager.AppSettings["ImagnesLogos"].ToString() + GridViewPartidos.DataKeys[row.RowIndex].Values["AdmPdoLogo"].ToString();
                        txtSiglas.Text = GridViewPartidos.DataKeys[row.RowIndex].Values["AdmPdoSiglas"].ToString();
                        txtDescripcion.Text = GridViewPartidos.DataKeys[row.RowIndex].Values["AdmPdoDescripcion"].ToString();
                        ViewState["logo"] = GridViewPartidos.DataKeys[row.RowIndex].Values["AdmPdoLogo"].ToString();
                        ImagenLogo.ImageUrl = String.IsNullOrEmpty(Path.GetExtension(imgUrl)) ? imageLogoDefault : imgUrl;
                        string codigo = String.IsNullOrEmpty(GridViewPartidos.DataKeys[row.RowIndex].Values["AdmStsCodigo"].ToString()) ? "1" : GridViewPartidos.DataKeys[row.RowIndex].Values["AdmStsCodigo"].ToString();
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
                btnNuevo.Visible = false;
                btnGuardar.Visible = true;
                btnCancel.Visible = true;
                txtSiglas.Text = "";
                txtDescripcion.Text = "";
                ViewState["logo"] = "";
                ImagenLogo.ImageUrl = imageLogoDefault;
                ddlEstado.SelectedIndex = 0;
                txtSiglas.Focus();
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
                txtSiglas.Enabled = true;
                txtDescripcion.Enabled = true;
                ddlEstado.Enabled = true;
                //btnSubir.Enabled = true;
                FileUploadASP.Enabled = true;
                mtLoadData();
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSiglas.Text.Trim()))
            {
                mtvAddMessage("Captura las siglas del partido", MessageType.warning);
                return;
            }

            if (string.IsNullOrEmpty(txtDescripcion.Text.Trim()))
            {
                mtvAddMessage("Captura la descripción del partido", MessageType.warning);
                return;
            }
            if (FileUploadASP.HasFile)
            {
                string nombreFile_ = Path.GetFileNameWithoutExtension(FileUploadASP.FileName) + Path.GetExtension(FileUploadASP.FileName);
                ViewState["logo"] = nombreFile_;
            }

            if (string.IsNullOrEmpty(ViewState["logo"].ToString().Trim()))
            {
                mtvAddMessage("Seleccione una imagen para su logo", MessageType.warning);
                return;
            }

            HttpPostedFile mifichero = FileUploadASP.PostedFile;
            string nombreFile = FileUploadASP.FileName;
            string nombreFile2 = Path.GetFileNameWithoutExtension(FileUploadASP.FileName) + Path.GetExtension(FileUploadASP.FileName);
            string extension = Path.GetExtension(nombreFile);
            string nomb = Path.GetFileNameWithoutExtension(FileUploadASP.FileName);


            double Kb = mifichero.ContentLength / 1024;
            if (Kb > 4096)
            {
                mtvAddMessage("No se puede subir la imagen, excede el tamaño permitido de 4 mb.", MessageType.warning);
                return;
            }

            if (!extension.ToUpper().Equals(".JPG") && !extension.ToUpper().Equals(".PNG"))
            {
                mtvAddMessage("La imagen de logo tiene un formato incorrecto. Favor, volver intentar.", MessageType.error);
                return;
            }


            // Condiciona ruta no mayor a 200 -tamaño de campo en DB-
            var fullPath = String.Concat(@System.Configuration.ConfigurationManager.AppSettings["ImagnesLogos"], nombreFile2);
            if (nombreFile2.Trim().Length > 50)
            {
                mtvAddMessage("El nombre de la imagen supera lo permitido por el sistema.", MessageType.error);
                return;
            }

            ViewState["logo"] = nombreFile2;

            // Guarda el archivo subido en la carpeta del AppSettings
            FileUploadASP.SaveAs(fullPath);

            Partido = new clsPartido();
            Partido.mtPutPartido
                (Int32.Parse(ViewState["hfId"].ToString())//Id del Registro
                , txtSiglas.Text.Trim()
                , txtDescripcion.Text.Trim()
                , ViewState["logo"].ToString()
                , bool.Parse(ddlEstado.SelectedValue)
                );

            ViewState["hfId"] = "0";
            ViewState["logo"] = "";
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