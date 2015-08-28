using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_VisorPDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Ruta = Session["PdfURL"].ToString().Trim();
                Response.Clear();
                Response.ClearHeaders();
                if (Session["PdfURL"].ToString().Contains(".pdf"))
                {
                    Response.ContentType = "application/pdf";
                }
                Response.WriteFile(Ruta);
                Response.Flush();
                Response.Close();
            }
        }
    }
}