using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBll
{
    public class clsExcusa
    {
        MaxData.clsDatos objDatos;

        public clsExcusa()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }

        public DataTable mtGetExcusas()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                dtReturn = objDatos.getDataTable("Spc_mAdmExc_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public void mtPutExcusa(Int32 vAdmExcCodigo, String vAdmExcDescripcion, Boolean vAdmstsCodigo)
        {
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmExcCodigo", vAdmExcCodigo);
                objDatos.AddParameter("@AdmExcDescripcion", vAdmExcDescripcion);
                objDatos.AddParameter("@AdmstsCodigo", vAdmstsCodigo);
                objDatos.ExecuteNonQuery("Spc_mAdmExc_Put", CommandType.StoredProcedure);
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
