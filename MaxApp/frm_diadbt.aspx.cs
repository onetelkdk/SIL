using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_diadbt : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblModulo.Text = Session["AdmmodNombre"].ToString();
                MenuFlotante.InnerHtml = mtGetMenu();
            }
        }
    }
}