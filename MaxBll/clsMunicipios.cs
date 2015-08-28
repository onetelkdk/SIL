using MaxData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBll
{
    public class clsMunicipios
    {
        MaxData.clsDatos objDatos;

        public clsMunicipios()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }

        public DataTable mtGetMunicipios()
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                dtReturn = objDatos.getDataTable("Spc_mAdmMun_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public void mtPutMunicipios(Int32 vAdmMunCodigo, String vAdmMunDescripcion, Int32 vAdmPrvCodigo, Boolean vAdmStsCodigo)
        {
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@AdmMunCodigo", vAdmMunCodigo);
                objDatos.AddParameter("@AdmMunDescripcion", vAdmMunDescripcion);
                objDatos.AddParameter("@AdmPrvCodigo", vAdmPrvCodigo);
                objDatos.AddParameter("@AdmStsCodigo", vAdmStsCodigo);
                objDatos.ExecuteNonQuery("Spc_mAdmMun_Put", CommandType.StoredProcedure);
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
