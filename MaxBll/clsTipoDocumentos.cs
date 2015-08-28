using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBll
{
    public class clsTipoDocumentos
    {
    MaxData.clsDatos objDatos;

    public clsTipoDocumentos()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }




        public DataTable mtGetTipoDocumentos()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                dtReturn = objDatos.getDataTable("Spc_mAdmDtp_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public void mtPutTipoDocumentos(Int32 vAdmDtpCodigo, String vAdmDtpDescripcion, Int32 vAdmDclCodigo, Boolean vAdmstsCodigo)
        {
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmDtpCodigo", vAdmDtpCodigo);
                objDatos.AddParameter("@AdmDtpDescripcion", vAdmDtpDescripcion);
                objDatos.AddParameter("@AdmDclCodigo", vAdmDclCodigo);
                objDatos.AddParameter("@AdmStsCodigo", vAdmstsCodigo);
                objDatos.ExecuteNonQuery("Spc_mAdmDtp_Put", CommandType.StoredProcedure);
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
