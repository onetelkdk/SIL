using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MaxBll
{
    public class clsTipoIniciativa
    {
         MaxData.clsDatos objDatos;

         public clsTipoIniciativa()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }



         /// <summary>
         /// Método para obtener los tipos de iniciativas de la aplicación.
         /// </summary>
         /// <returns>DataTable</returns>
         public DataTable mtGetTiposIniciativas()
         {
             DataTable dtReturn = new DataTable();
             try
             {
                 objDatos.ClearParameter();
                 dtReturn = objDatos.getDataTable("Spc_mIniTip_Get", CommandType.StoredProcedure);
             }
             catch
             {
                 throw;
             }
             return dtReturn;
         }

         public void mtPutTiposIniciativas(Int32 vIniTipCodigo, String vIniTipDescripcion, Boolean vAdmstsCodigo)
         {
             try
             {
                 objDatos.ClearParameter();
                 objDatos.AddParameter("@IniTipCodigo", vIniTipCodigo);
                 objDatos.AddParameter("@IniTipDescripcion", vIniTipDescripcion);
                 objDatos.AddParameter("@AdmstsCodigo", vAdmstsCodigo);
                 objDatos.ExecuteNonQuery("Spc_mIniTip_Put", CommandType.StoredProcedure);
             }
             catch
             {
                 throw;
             }
         }

         public void mtDispose()
         {
             objDatos.CloseConection();
             objDatos = null;
         }

    }
}
