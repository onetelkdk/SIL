using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class Menu : PageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                mtvClearMessage();

                if (!Page.IsPostBack)
                {
                    String fModCode = String.Empty;

                    if (Request.QueryString["Mod"] == null && Session["AdmmodCodigo"] != null)
                        fModCode = Session["AdmmodCodigo"].ToString();
                    else
                        fModCode = Request.QueryString["Mod"];

                    Session["AdmmodCodigo"] = fModCode;

                    //Creando dinamicamente el Menu Principal
                    DataTable dtDataOpciones = (DataTable)Session["dtDataOpciones"];

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    if (dtDataOpciones.Rows.Count > 0)
                    {

                        //Set Titulo del Modulo
                        var TituloQuery = from p in dtDataOpciones.AsEnumerable()
                                          where p.Field<String>("AdmmodCodigo") == fModCode
                                          select new
                                          {

                                              NombreModulo = p.Field<String>("AdmmodNombre")
                                          };

                        lblModulo.Text = TituloQuery.FirstOrDefault().NombreModulo;
                        Session["AdmmodNombre"] = lblModulo.Text;


                        //Cargando los tipos
                        var QueryTipo = from tipos in dtDataOpciones.AsEnumerable()
                                          group new
                                          {
                                              Codigo = tipos.Field<String>("PrgtipCodigo"),
                                              Nombre = tipos.Field<String>("PrgtipNombre"),
                                              Orden = tipos.Field<String>("PrgtipCodigo")
                                          } by tipos.Field<String>("PrgtipCodigo") into newTipos
                                          select newTipos;

                        foreach (var Query in QueryTipo)
                        {

                            var item = from itemObject in Query
                                       orderby itemObject.Orden
                                       select itemObject;

                            

                            //Buscar los programas del tipo
                            var QueryProgramas = from programas in dtDataOpciones.AsEnumerable()
                                                 where programas.Field<String>("PrgtipCodigo") == item.FirstOrDefault().Codigo
                                                 && programas.Field<String>("AdmmodCodigo") == fModCode
                                                 orderby programas.Field<Int16>("PrgprgOrden")
                                                 select new
                                                     {
                                                         Nombre = programas.Field<String>("PrgPrgNombre"),
                                                         Descripcion = programas.Field<String>("PrgprgDescripcion"),
                                                         ColorFondo = programas.Field<String>("PrgPrgColorFondo"),
                                                         Icono = programas.Field<String>("PrgprgIcono")
                                                     };

                            if (QueryProgramas != null && QueryProgramas.Count() > 0)
                            {
                                sb.Append("<div class=\"bloque_body\">");
                                sb.Append(String.Format("<h3>{0}</h3>", item.FirstOrDefault().Nombre));

                                foreach (var QueryP in QueryProgramas)
                                {

                                    sb.Append("<div class=\"menu\">");

                                    sb.Append(String.Format("<a href=\"{0}\">", QueryP.Nombre));
                                    sb.Append(String.Format("<div class=\"item_menu {0}\">", QueryP.ColorFondo));
                                    sb.Append(String.Format("<div class=\"icomenu\" style=\"background:url('images/{0}')\">", QueryP.Icono));
                                    sb.Append("</div></div>");
                                    sb.Append(String.Format("<span>{0}</span>", QueryP.Descripcion));
                                    sb.Append("</a></div>");
                                }

                                sb.Append("</div>");
                            }

                        }
                    }

                    Bloques_Content.InnerHtml = sb.ToString();

                }
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }
    }
}