using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MaxBll
{
    public class clsSubTipoIniciativa
    {
         MaxData.clsDatos objDatos;

         public clsSubTipoIniciativa()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }


         /// <summary>
         /// Método para obtener los tipos de iniciativas de la aplicación.
         /// </summary>
         /// <returns>DataTable</returns>
         public DataTable mtGetSubTiposIniciativas()
         {
             DataTable dtReturn = new DataTable();
             try
             {
                 dtReturn = objDatos.getDataTable("Spc_mIniStp_Get", CommandType.StoredProcedure);
             }
             catch
             {
                 throw;
             }
             return dtReturn;
         }

         public void mtPutSubTiposIniciativas(Int32 vIniStpCodigo, String vIniStpDescripcion, Int32 vIniTipCodigo, Boolean vAdmstsCodigo)
         {
             try
             {
                 objDatos.ClearParameter();
                 objDatos.AddParameter("@IniStpCodigo", vIniStpCodigo);
                 objDatos.AddParameter("@IniStpDescripcion", vIniStpDescripcion);
                 objDatos.AddParameter("@IniTipCodigo", vIniTipCodigo);
                 objDatos.AddParameter("@AdmstsCodigo", vAdmstsCodigo);
                 objDatos.ExecuteNonQuery("Spc_mIniStp_Put", CommandType.StoredProcedure);
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
