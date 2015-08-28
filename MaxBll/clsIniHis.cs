using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxBll
{
    public class clsIniHis
    {
        MaxData.clsDatos objDatos;

        public clsIniHis()
        {
            objDatos = new MaxData.clsDatos();
            objDatos.ConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            objDatos.OpenConection();
        }

        public DataTable mtGetIniHis(Int32 vIniIniCodigoSis)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@IniIniCodigoSis", vIniIniCodigoSis);
                dtReturn = objDatos.getDataTable("Spc_dIniHis_Get", CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
            return dtReturn;
        }

        public void mtPutIniHis(long vIniHisCodigo, Int32 vIniIniCodigoSis, Int32 vAdmEstCodigo, DateTime vIniHisFechainicio, DateTime vIniHisFechafin, Boolean vIniHisPreaviso, String vIniHisNotas)
        {
            try
            {
                objDatos.ClearParameter();
                objDatos.AddParameter("@IniHisCodigo", vIniHisCodigo);
                objDatos.AddParameter("@IniIniCodigoSis", vIniIniCodigoSis);
                objDatos.AddParameter("@AdmEstCodigo", vAdmEstCodigo);
                objDatos.AddParameter("@IniHisFechainicio", vIniHisFechainicio);
                objDatos.AddParameter("@IniHisFechafin", vIniHisFechafin);
                objDatos.AddParameter("@IniHisPreaviso", vIniHisPreaviso);
                objDatos.AddParameter("@IniHisNotas", vIniHisNotas);
                objDatos.ExecuteNonQuery("Spc_dIniHis_Put", CommandType.StoredProcedure);
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
