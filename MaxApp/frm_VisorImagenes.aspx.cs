﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_VisorImagenes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Image1.ImageUrl = Session["imagenURL"].ToString().Replace(Request.ServerVariables["APPL_PHYSICAL_PATH"], String.Empty);
            }
        }
    }
}