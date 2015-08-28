using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBll
{
    public class clsSectores
    {
     MaxData.clsDatos objDatos;

     public clsSectores()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }

        public DataTable mtGetSectores()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                dtReturn = objDatos.getDataTable("Spc_mAdmSec_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public void mtPutSectores(Int32 vAdmSecCodigo, String vAdmSecDescripcion, Int32 vAdmMunCodigo, Boolean vAdmStscodigo)
        {
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmSecCodigo", vAdmSecCodigo);
                objDatos.AddParameter("@AdmSecDescripcion", vAdmSecDescripcion);
                objDatos.AddParameter("@AdmMunCodigo", vAdmMunCodigo);
                objDatos.AddParameter("@AdmStscodigo", vAdmStscodigo);
                objDatos.ExecuteNonQuery("Spc_mAdmSec_Put", CommandType.StoredProcedure);
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
