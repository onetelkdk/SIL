﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_orddia : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            mtvClearMessage();
            try {

                if (!Page.IsPostBack)
                {
                    if (mtValidPage("frm_orddia.aspx", lblModulo))
                    {
                        MenuFlotante.InnerHtml = mtGetMenu();
                    }
                }
            }

            catch(Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }

        }
    }
}