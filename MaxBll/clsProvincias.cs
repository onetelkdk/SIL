using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBll
{
    public class clsProvincias
    {
         MaxData.clsDatos objDatos;

         public clsProvincias()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }


         public DataTable mtGetProvincias()
         {
             DataTable dtReturn = new DataTable();
             try
             {
                 objDatos.ClearParameter();
                 dtReturn = objDatos.getDataTable("Spc_mAdmPrv_Get", CommandType.StoredProcedure);
             }
             catch
             {
                 throw;
             }
             return dtReturn;
         }

         public void mtPutProvincias(Int32 vAdmPrvCodigo, String vAdmPrvDescripcion, Boolean vAdmStsCodigo)
         {
             try
             {
                 objDatos.ClearParameter();
                 objDatos.AddParameter("@AdmPrvCodigo", vAdmPrvCodigo);
                 objDatos.AddParameter("@AdmPrvDescripcion", vAdmPrvDescripcion);
                 objDatos.AddParameter("@AdmStsCodigo", vAdmStsCodigo);
                 objDatos.ExecuteNonQuery("Spc_mAdmPrv_Put", CommandType.StoredProcedure);
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
