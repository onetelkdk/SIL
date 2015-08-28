using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class Modulos : PageBase
    {
        MaxBll.clsAdmRolDetalles objRolDetalles;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                mtvClearMessage();

                if (!Page.IsPostBack)
                {
                    //Creando dinamicamente el Menu Principal
                    objRolDetalles = new MaxBll.clsAdmRolDetalles();
                    DataTable dtDataOpciones = objRolDetalles.mtGetRolDetalles(0, Int32.Parse(Session["AdmRolCodigo"].ToString()));
                    objRolDetalles.mtDispose();
                    Session["dtDataOpciones"] = dtDataOpciones;

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    if (dtDataOpciones.Rows.Count > 0)
                    {
                        var QueryModulo = from modulos in dtDataOpciones.AsEnumerable()
                                          group new
                                          {
                                              Codigo = modulos.Field<String>("AdmmodCodigo"),
                                              Nombre = modulos.Field<String>("AdmmodNombre"),
                                              Tamano = modulos.Field<String>("AdmModTamano"),
                                              ColorFondo = modulos.Field<String>("AdmModColorFondo"),
                                              Icono = modulos.Field<String>("AdmmodIcono"),
                                              Orden = modulos.Field<Int16>("AdmmodOrden")
                                          } by modulos.Field<String>("AdmmodCodigo") into newModulos
                                          select newModulos;




                        //sb.Append("<div class=\"modulo_contenido full\" id=\"modulo_contenido\">");
                        foreach (var Query in QueryModulo)
                        {

                            var item = from itemObject in Query
                                       orderby itemObject.Orden
                                       select itemObject;

                            sb.Append(String.Format("<div class=\"modulo_item {0} {1} \">", item.FirstOrDefault().Tamano, item.FirstOrDefault().ColorFondo));
                            sb.Append(String.Format("<a href= \"Menu.aspx?Mod={0} \"> <div class= \"item_contenido \">", item.FirstOrDefault().Codigo));
                            sb.Append(String.Format("<div class= \"modulo_nombre \" style= \"background: url('images/{0}'); \">", item.FirstOrDefault().Icono));
                            sb.Append(String.Format("<span>{0}</span></div></div> </a> </div>", item.FirstOrDefault().Nombre));

                        }
                        //sb.Append("</div>");

                    }

                    Menu_Principal_Content.InnerHtml = sb.ToString();

                }
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }

        }
    }
}