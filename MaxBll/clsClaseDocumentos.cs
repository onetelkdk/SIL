using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBll
{
    public class clsClaseDocumentos
    {
    MaxData.clsDatos objDatos;

    public clsClaseDocumentos()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }




        public DataTable mtGetClaseDocumentos()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                dtReturn = objDatos.getDataTable("Spc_mAdmDcl_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public void mtPutClaseDocumentos(Int32 vAdmDclCodigo, String vAdmDclDescripcion, Boolean vAdmStsCodigo)
        {
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmDclCodigo", vAdmDclCodigo);
                objDatos.AddParameter("@AdmDclDescripcion", vAdmDclDescripcion);
                objDatos.AddParameter("@AdmStsCodigo", vAdmStsCodigo);
                objDatos.ExecuteNonQuery("Spc_mAdmDcl_Put", CommandType.StoredProcedure);
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
